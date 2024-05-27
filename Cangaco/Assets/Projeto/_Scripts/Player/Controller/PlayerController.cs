using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimentação")]
    public float speed = 3f;

    Vector2 mov;

    [HideInInspector]
    public bool isFacing;

    Rigidbody2D rb;
    Animator anim;

    [HideInInspector] public bool isItemCollecting;
    [HideInInspector] public ItemPref itemCollecting;

    [Header("Gun")]
    public Transform targetBullet;

    public static bool inRecharge;
    bool isShoot;
    bool isRecharge;
    
    public float range;

    [Header("In Door House")]
    GameObject house_;
    bool inDoor;
    

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void LateUpdate(){
        Movimentacao();

        if((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && GunController.itemHand.InHand && !inRecharge){
            if(!isShoot){
                Shooting();
                StartCoroutine(ShootRate(GunController.itemHand.itemInHand.item.fireRate));
            }
        }

        if(Input.GetKeyDown(KeyCode.R) && !isRecharge) RechargeGun();
        if(isRecharge && GunController.itemHand.InHand && GunController.itemHand.itemInHand.qtdMunition >= GunController.itemHand.itemInHand.item.maxMunition) isRecharge = false;
    
        if(inDoor){
            if(Input.GetKeyDown(KeyCode.E)){
                if(house_.GetComponentInParent<Home_Controller>())
                    house_.GetComponentInParent<Home_Controller>().EnterHouse();
                else if(house_.GetComponentInParent<Home>())
                    house_.GetComponentInParent<Home>().Exit();
            }       
        }
    }


    void Movimentacao(){
        mov.x = Input.GetAxisRaw("Horizontal");
        mov.y = Input.GetAxisRaw("Vertical");

        mov = mov.normalized;

        rb.velocity = mov * speed;
    
        // Flip
        if(mov.x < 0 && isFacing) Flip();
        else if(mov.x > 0 && !isFacing) Flip();

        // Animações
        if(mov.x != 0 || mov.y != 0) anim.SetInteger("transition",1);
        else anim.SetInteger("transition",0);
    }

    void Flip(){
        isFacing = !isFacing;
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // Shoot (Atirar)
    void Shooting(){
        if(GunController.itemHand.itemInHand.item.type != typeItem.edible){
            if(GunController.itemHand.itemInHand.item.type == typeItem.gun){
                
                if(GunController.itemHand.itemInHand.qtdMunition > 0){
                    //Arma (Atirar)
                    for(int i =0;i < GunController.itemHand.itemInHand.item.qtd_Bullet;i++){
                        var bullet = Instantiate(GunController.itemHand.itemInHand.item.bulletPref,targetBullet.position,Quaternion.identity);
                        
                        bullet.GetComponent<BulletController>().speed = GunController.itemHand.itemInHand.item.speedBullet;
                        bullet.GetComponent<BulletController>().damage = GunController.itemHand.itemInHand.item.damage;
                        bullet.GetComponent<BulletController>().timerLife = GunController.itemHand.itemInHand.item.timer_Life;


                        if(GunController.itemHand.itemInHand.item.qtd_Bullet > 1){
                            var rand = Random.Range(-.3f,.3f);

                            bullet.GetComponent<BulletController>().offSet = rand;
                        }

                        var scale = bullet.transform.localScale;

                        if(targetBullet.position.x < transform.position.x){
                            scale.x *= 1;
                            bullet.GetComponent<BulletController>().isDirect = false;
                        }else if(targetBullet.position.x > transform.position.x){
                            scale.x *= -1;
                            bullet.GetComponent<BulletController>().isDirect = true;
                        }

                        bullet.transform.localScale = scale;
                    }

                    GunController.itemHand.itemInHand.qtdMunition--;

                    FindFirstObjectByType<GunController>().ShootAnim();

                    var txt = GunController.itemHand.itemInHand.qtdMunition + "/" + GunController.itemHand.itemInHand.item.maxMunition;
                    InvMunition_Controller.current.MunitionText(txt);
                }
                
            }else if(GunController.itemHand.itemInHand.item.type == typeItem.throwable){
                
                if(GunController.itemHand.itemInHand.qtd_ > 0){
                    var bullet = Instantiate(GunController.itemHand.itemInHand.item.bulletPref,targetBullet.position,Quaternion.identity);
                    
                    bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Random.Range(2f,3f),ForceMode2D.Impulse);

                    bullet.GetComponent<BulletController>().speed = GunController.itemHand.itemInHand.item.speedBullet;
                    bullet.GetComponent<BulletController>().damage = GunController.itemHand.itemInHand.item.damage;
                    
                    bullet.GetComponent<BulletController>().pref = GunController.itemHand.itemInHand.item.prefab;

                    bullet.GetComponent<BulletController>().timerLife = GunController.itemHand.itemInHand.item.timer_Life;

                    var scale = bullet.transform.localScale;

                    if(targetBullet.position.x < transform.position.x){
                        scale.x *= 1;
                        bullet.GetComponent<BulletController>().isDirect = false;
                    }else if(targetBullet.position.x > transform.position.x){
                        scale.x *= -1;
                        bullet.GetComponent<BulletController>().isDirect = true;
                    }

                    bullet.transform.localScale = scale;

                //Diminuir Qtd
                    GunController.itemHand.itemInHand.qtd_--;
                    GunController.itemHand.itemInHand.qtd_txt.text = GunController.itemHand.itemInHand.qtd_.ToString(); 

                    if(FindFirstObjectByType<HandController>().CheckHand()){
                        FindFirstObjectByType<GunController>().GunDesable();
                    }
                }

            }else{
                //Arma Branca
                FindFirstObjectByType<GunController>().Atk();
                var hitColliders = Physics2D.OverlapCircle(targetBullet.position,GunController.itemHand.itemInHand.item.range);
            
                if(hitColliders.tag != "Player"){
                    print(hitColliders.tag);
                }
            }
        }
    }

    IEnumerator ShootRate(float timer){
        isShoot = true;
        
        yield return new WaitForSeconds(timer);
        isShoot = false;

        if(GunController.itemHand.itemInHand.item.type == typeItem.gun)
            FindFirstObjectByType<GunController>().ShootAnimStop();
    }

    public void RechargeGun(){
        if(GunController.itemHand.InHand){
            if(GunController.itemHand.itemInHand.item.type == typeItem.gun){
                if(GunController.itemHand.itemInHand.qtdMunition < GunController.itemHand.itemInHand.item.maxMunition){
                    //Pode Recarregar
                    isRecharge = true;
                    foreach(var i in InvMunition_Controller.current.slots){
                        if(i.GetComponent<SlotMunition>().type == GunController.itemHand.itemInHand.item.TypeMunition){
                            int mun = 0;
                            for(int j =0;j < (GunController.itemHand.itemInHand.item.maxMunition - GunController.itemHand.itemInHand.qtdMunition);j++)
                            {
                                i.GetComponent<SlotMunition>().qtdMunition--;

                                mun++;

                                i.GetComponent<SlotMunition>().txt_qtd.text = i.GetComponent<SlotMunition>().qtdMunition + "/" + i.GetComponent<SlotMunition>().item.qtdMax;

                                if(i.GetComponent<SlotMunition>().qtdMunition <= 0){
                                    break;
                                }
                            }
                            
                            FindFirstObjectByType<GunController>().Recharge();

                            StartCoroutine(Recharged(mun));

                            isRecharge = false;
                            
                            InvMunition_Controller.current.CheckSlots();
                            break;
                        }
                    }
                }
            }
        }

        isRecharge = false;
    }

    IEnumerator Recharged(int qtd){
        inRecharge = true;
        yield return new WaitForSeconds(GunController.itemHand.itemInHand.item.timerRecharge);

        GunController.itemHand.itemInHand.qtdMunition += qtd;
        inRecharge = false;

        var txt = GunController.itemHand.itemInHand.qtdMunition + "/" + GunController.itemHand.itemInHand.item.maxMunition;
        InvMunition_Controller.current.MunitionText(txt);
        isRecharge = false;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(targetBullet.position,range);
    }


    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("EnterHouse")){
            house_ = other.gameObject;
            inDoor = true;

            UIManager.current.CollectOn();
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("EnterHouse")){
            UIManager.current.CollectOff();
            inDoor = false;
        }
    }
}
