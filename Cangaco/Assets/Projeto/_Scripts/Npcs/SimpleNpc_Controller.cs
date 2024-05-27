using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleNpc_Controller : MonoBehaviour, IDamage
{
    public Transform[] waypoints;
    [Header("Dialogo")]
    public float distance;

    [Header("Velocidade")]
    public float speedMax = 1f;
    public float acceleration = 1f;

    [Space]
    public float force;

    float speed;

    private int currentWaypointIndex = 0;

    PlayerController player;
    Animator anim;

    bool isDistance;

    bool isFurious;

    [Header("Life")]
    [HideInInspector] public float life = 25;
    float lifeMax;

    Rigidbody2D rb;

    bool isHit;

    [Header("Gun")]
    public ItemInv itemGun;

    [Header("In Game")]
    public GameObject obj_gun;
    public GameObject gunRoot;
    public Animator anim_gunHand;

    Vector3 posAncor;

    bool gunhand = false;
    bool randAncor;

    Vector3 rand;


    [Header("Life")]
    public Image life_img;
    public GameObject obj_life;
    public GameObject lifeCanva;


    void Start(){
       player = FindFirstObjectByType<PlayerController>();
       anim = GetComponent<Animator>();

       rb = GetComponent<Rigidbody2D>();

       lifeMax = life;
       DesableLife();
    }

    void Update()
    {
        if(!isHit){
            if(!isFurious){
                if(Vector2.Distance(transform.position,player.transform.position) < distance && !isDistance){
                    
                    //Parado
                    anim.SetInteger("transition",0);
                    
                    speed = Mathf.Lerp(speedMax,0,acceleration);

                    if(player.transform.position.x < transform.position.x){
                        transform.localScale = new Vector3(-1,1,1);
                    }
                    else if(player.transform.position.x > transform.position.x){
                        transform.localScale = new Vector3(1,1,1);   
                    }

                    isDistance = true;

                }else if(Vector2.Distance(transform.position,player.transform.position) > distance){
                    //Andando

                    anim.SetInteger("transition",1);
                    
                    isDistance = false;
                    Andar();
                }

                rb.bodyType = RigidbodyType2D.Kinematic;
            }else{
                speed = 2f;

                float distanceToplayer = Vector3.Distance(transform.position, player.transform.position);
                if(distanceToplayer < 16f){
                    
                    if(distanceToplayer > 5f){
                        transform.position = Vector3.MoveTowards(transform.position,player.transform.position,speed * Time.deltaTime);

                        if(player.transform.position.x < transform.position.x){
                            transform.localScale = new Vector3(-1,1,1);
                            if(lifeCanva != null)
                                lifeCanva.transform.localScale = new Vector3(-1,1,1);
                        }
                        else if(player.transform.position.x > transform.position.x){
                            transform.localScale = new Vector3(1,1,1);
                            if(lifeCanva != null)
                                lifeCanva.transform.localScale = new Vector3(1,1,1);
                        }

                        gunhand = false;
                        obj_gun.SetActive(false);

                        DesableLife();

                        anim.SetInteger("transition",1);

                        rb.bodyType = RigidbodyType2D.Dynamic;

                        randAncor = false;

                    }else{
                        //Distancia para atirar
                        anim.SetInteger("transition",0);

                        if(player.transform.position.x < transform.position.x){
                            transform.localScale = new Vector3(-1,1,1);
                            
                            if(lifeCanva != null)
                                lifeCanva.transform.localScale = new Vector3(-1,1,1);
                        }
                        else if(player.transform.position.x > transform.position.x){
                            transform.localScale = new Vector3(1,1,1);
                            
                            if(lifeCanva != null)
                                lifeCanva.transform.localScale = new Vector3(1,1,1);
                        }

                        if(!gunhand){
                            anim_gunHand.SetTrigger("Start");
                            posAncor = transform.position;

                            gunhand = true;
                        }

                        EnableLife();
                        
                        obj_gun.SetActive(true);
                        obj_gun.GetComponent<SpriteRenderer>().sprite = itemGun.image_;

                        rb.bodyType = RigidbodyType2D.Kinematic;

                        if(!randAncor){
                            rand = new Vector3(Random.Range(posAncor.x - 1f, posAncor.x + 1f), Random.Range(posAncor.y - 1f,posAncor.y + 1f));
                            randAncor = true;
                        }

                        // Rotate Arma
                       
                        // Obter a direção entre a posição da gunRoot e a posição do player
                        Vector3 directionToPlayer = player.transform.position - gunRoot.transform.position;

                        // Calcular o ângulo em radianos usando a função Mathf.Atan2
                        float angleRadians = 0f;

                        if(transform.position.x > player.transform.position.x){
                            angleRadians = Mathf.Atan2(-directionToPlayer.y, -directionToPlayer.x);
                        }else if(transform.position.x < player.transform.position.x){
                            angleRadians = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x);
                        }

                        // Converter o ângulo para graus
                        float angleDegrees = angleRadians * Mathf.Rad2Deg;

                        // Criar uma rotação a partir do ângulo e do eixo Z
                        Quaternion rotationToPlayer = Quaternion.AngleAxis(angleDegrees, Vector3.forward);

                        // Aplicar a rotação à gunRoot
                        gunRoot.transform.rotation = rotationToPlayer;

                        MoveCircle(rand);
                    }


                }else{
                    anim.SetInteger("transition",0);
                    //Adicionar Timer();
                    isFurious = false;
                }

                
            }
        }

        if(life <= 0) Die();
    }

    void Die(){
        anim.SetTrigger("dead");
        obj_gun.SetActive(false);
        
        Destroy(gameObject,.4f);
    }

    void Andar(){
        // Verifica se ha algum ponto de patrulha definido
        speed = Mathf.Lerp(0,speedMax,acceleration);

        if (waypoints.Length <= 0)
        {
            Debug.LogWarning("Nenhum ponto de patrulha definido para o NPC.");
            return;
        }

        // Move o NPC em direção ao ponto de patrulha atual
        Vector3 targetPosition = waypoints[currentWaypointIndex].position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if(targetPosition.x < transform.position.x){
            transform.localScale = new Vector3(-1,1,1);
        }
        else if(targetPosition.x > transform.position.x){
            transform.localScale = new Vector3(1,1,1);
        }

        // Verifica se o NPC chegou ao ponto de patrulha atual
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Muda para o próximo ponto de patrulha
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    void MoveCircle(Vector3 targetPosition){
        // Verifica se ha algum ponto de patrulha definido
        speed = .9f;

        anim.SetInteger("transition",1);

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Verifica se o NPC chegou ao ponto de patrulha atual
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Muda para o próximo ponto de patrulha
            randAncor = false;
        }
    }

    public void Damage(float damage){
        isFurious = true;
        StartCoroutine(DamageCoroutine(damage));
    }

    IEnumerator DamageCoroutine(float damage){
        float hit = 0f;

        while(hit < damage){
            life-=Time.deltaTime * 12;
            hit+=Time.deltaTime * 12;

            LifeManager();

            yield return null;
        }
    }

    public void Hit(GameObject bullet){
        isHit = true;

        Vector2 direction = (transform.position - player.transform.position).normalized;
        rb.AddForce(direction * force, ForceMode2D.Force);

        anim.SetTrigger("hit");

        bullet.GetComponent<BulletController>().HitText(gameObject);

        StartCoroutine(HitStop());
    }

    IEnumerator HitStop(){
        yield return new WaitForSeconds(.2f);
        isHit = false;
    }

    // Life

    public void EnableLife(){ 
        obj_life.SetActive(true);
    }

    public void DesableLife() { 
        obj_life.SetActive(false);
    }

    public void LifeManager(){
        if(obj_life != null)
            life_img.fillAmount = life / lifeMax;
    }
}
