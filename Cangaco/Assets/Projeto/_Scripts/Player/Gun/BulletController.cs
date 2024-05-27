using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject collFx;

    public bool isRot;

    [HideInInspector]
    public float speed;
    
    [HideInInspector]
    public bool isDirect;

    Vector3 direct;
    
    [HideInInspector]
    public float offSet;

    [HideInInspector] 
    public float timerLife;

    bool isIdle;

    private IDamage damageable;

    [HideInInspector]
    public float damage;

    [HideInInspector]
    public GameObject pref;

    void Start()
    {
        if(!isRot){
            Destroy(gameObject,timerLife);
        }else{
            StartCoroutine(CreateItem());
        }

        transform.rotation = FindFirstObjectByType<PlayerController>().targetBullet.rotation;
    
        // Obtém a rotação atual do objeto em graus
        float angle = transform.eulerAngles.z;

        // Converte o ângulo de rotação em radianos
        float angleInRadians = angle * Mathf.Deg2Rad;

        // Calcula a direção para frente com base na rotação do objeto
        if(!isDirect)
            direct = new Vector2(-Mathf.Cos(angleInRadians) + offSet, -Mathf.Sin(angleInRadians) + offSet);
        else 
            direct = new Vector2(Mathf.Cos(angleInRadians) + offSet, Mathf.Sin(angleInRadians) + offSet);
    }


    void Update()
    {
        if(!isIdle){
            transform.position += direct * speed * Time.deltaTime;

            if(isRot){
                if(!isDirect)
                    transform.Rotate(0,0,4);
                else
                    transform.Rotate(0,0,-4);
            }
        }
    }

    IEnumerator CreateItem(){
        yield return new WaitForSeconds(timerLife);
        Instantiate(pref,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider && !other.collider.CompareTag("Mun") && !other.collider.CompareTag("Player")){
            if(!isRot){
                Destroy(Instantiate(collFx,transform.position,Quaternion.identity),1f);

                DamageWithObject(other.gameObject,damage);
                HitWithObject(other.gameObject);
                
                Destroy(gameObject);
            }else{
                isIdle = true;
                transform.SetParent(other.collider.transform);

                StopAllCoroutines();

                DamageWithObject(other.gameObject,damage);
                HitWithObject(other.gameObject);

                Destroy(GetComponent<BoxCollider2D>());
                Destroy(GetComponent<Rigidbody2D>());

                Destroy(gameObject,.5f);
            }
        }
    }

    public void HitText(GameObject other){

        var rand = Random.Range(-.6f,.6f);

        var posHit = new Vector3(other.transform.position.x + rand, other.transform.position.y + 1f);
        var hitT = Instantiate(FindFirstObjectByType<GunController>().hit_Txt,posHit,Quaternion.identity);
        hitT.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "-" + damage;

        Destroy(hitT,.3f);
    }

    void DamageWithObject(GameObject objectToDamage,float damage)
    {
        if (objectToDamage.TryGetComponent(out IDamage damageObject))
        {
            damageObject.Damage(damage);
        }
    }

    void HitWithObject(GameObject objectToHit)
    {
        if (objectToHit.TryGetComponent(out IDamage hitObject))
        {
            hitObject.Hit(gameObject);
        }
    }
}
