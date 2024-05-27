using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformController : MonoBehaviour
{
    [Header("Movimentação")]
    public float speed;

    bool isDown;
    bool _isFacing;
    Animator anim;

    [Header("Jump")]
    public float speedJump;
    public Transform targetDown;

    [Space]
    public float distanceToDown;

    bool isGround;
    Rigidbody2D rb;

    [Header("Shoot")]
    public Transform target_Shoot;

    Transform targetShoot;
    public GameObject Bullet_Pref;

    [Space]
    public float timer_Shoot;
    bool isShoot = true;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // Variaveis
        isShoot = true;
    }

    void Update()
    {
        if(Input.GetButtonDown("Jump") && isGround) Jump();
        if(Input.GetKeyDown(KeyCode.Space) && isShoot) Shoot();

        OnMoviment();
        OnCollisionGround();
    }

    void Shoot(){
        isShoot = false;

        //Direção do tiro
        if(isDown)targetShoot = targetDown;
        else targetShoot = target_Shoot;

        //Criar Bullet
        var projetil = Instantiate(Bullet_Pref,targetShoot.position,Quaternion.identity);

        //Direção da bala
        if(isDown)
            projetil.GetComponent<BulletPlatformController>().direct = Vector3.down;
        else{
            if(_isFacing) projetil.GetComponent<BulletPlatformController>().direct = Vector3.left;
            else projetil.GetComponent<BulletPlatformController>().direct = Vector3.right;
        }

        //Animação de Tiro
        anim.SetTrigger("shoot");

        //Tempo entre um tiro e outro
        StartCoroutine(TimerShoot());
    }

    IEnumerator TimerShoot(){
        yield return new WaitForSeconds(timer_Shoot);
        isShoot = true;
    }

    void OnMoviment(){
        if(!isDown){
            // Movimentação
            float x = Input.GetAxisRaw("Horizontal");
            transform.position += new Vector3(x * speed * Time.deltaTime, 0);

            // Animações
            if(Input.GetButton("Horizontal")){
                anim.SetInteger("transition",1);
            }else{
                anim.SetInteger("transition",0);
            }

            // Direction
            if(x < 0 && !_isFacing) Flip();
            else if(x > 0 && _isFacing) Flip();
        }


        if(Input.GetButton("DownPlatform")){
            isDown = true;
            anim.SetInteger("transition",3);
        }else{
            isDown = false;
        }
    }

    void Flip(){
        _isFacing = !_isFacing;
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void Jump(){
        rb.AddForce(Vector3.up * speedJump,ForceMode2D.Impulse);
        isGround = false;
    }

    void OnCollisionGround(){
        RaycastHit2D hit = Physics2D.Raycast(targetDown.position,Vector3.down,distanceToDown);

        if(hit.collider){
            isGround = true;
        }else{
            isGround = false;
            anim.SetInteger("transition",2);
        }
    }
}
