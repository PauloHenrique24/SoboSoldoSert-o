using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MunitionsPref : MonoBehaviour
{
    public int qtd;
    public ItemMunition item;

    bool isColl;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !isColl){
            isColl = true;
            if(InvMunition_Controller.current.AddItem(item,qtd)){
                UIManager.current.ItemColect(item.spr_,item.name_);
                Destroy(gameObject);
            }
        }

        if(!other) isColl = false;
    }
}
