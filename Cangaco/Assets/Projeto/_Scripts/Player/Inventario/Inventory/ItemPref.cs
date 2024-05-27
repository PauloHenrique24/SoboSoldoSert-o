using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPref : MonoBehaviour
{
    public ItemInv item;
    
    [Space]
    public float distance;

    private PlayerController player;

    bool isDistance;

    [HideInInspector] public Rigidbody2D rb;

    void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void LateUpdate()
    {
        if(Vector2.Distance(transform.position,player.transform.position) < distance && !isDistance && !player.isItemCollecting && player.itemCollecting == null){
            
            isDistance = true;
            player.isItemCollecting = true;
            player.itemCollecting = this;

        }else if(Vector2.Distance(transform.position,player.transform.position) > distance){
            isDistance = false;

            if(player.itemCollecting == this){
                player.isItemCollecting = false;
                player.itemCollecting = null;
                UIManager.current.CollectOff();
            }
        }

        if(player.itemCollecting == this){
            UIManager.current.CollectOn();
            if(Input.GetKeyDown(KeyCode.E)){
                if(InventoryController.current.AddItem(item)){
                    UIManager.current.ItemColect(item.image_,item.name_);
                    UIManager.current.CollectOff();

                    isDistance = false;
                    player.isItemCollecting = false;
                    
                    Destroy(gameObject);
                }
            }
        }
    }

    public void Create(){
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 2f;
        rb.AddForce(Vector2.up * Random.Range(6f,8f), ForceMode2D.Impulse);

        var rand = Random.Range(0,2);

        if(rand == 0)
            rb.AddForce(Vector2.right * Random.Range(1f,3f),ForceMode2D.Impulse);
        else
            rb.AddForce(Vector2.left * Random.Range(1f,3f),ForceMode2D.Impulse);

        StartCoroutine(StopRig());
    }

    IEnumerator StopRig(){
        yield return new WaitForSeconds(.6f);

        Destroy(rb);
        rb = null;
    }
}
