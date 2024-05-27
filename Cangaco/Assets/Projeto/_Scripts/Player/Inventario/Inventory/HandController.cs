using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public bool isItem;

    public void GunInHand(dragItem item){
        FindFirstObjectByType<GunController>().GunActive(item.item.image_,item);

        if(item.item.type == typeItem.gun){
            var txt = item.qtdMunition + "/" + item.item.maxMunition;
            InvMunition_Controller.current.MunitionText(txt);
        }

        if(item.item.type != typeItem.edible) FindFirstObjectByType<Pointer>().EnablePointer();
        else FindFirstObjectByType<Pointer>().DesablePointer();

        InventoryController.current.CheckSlots();
    }

    public bool CheckHand(){
        bool ret = false;
        if(transform.childCount > 0 && transform.GetChild(0).GetComponent<dragItem>().qtd_ <= 0){
            isItem = false;
            Destroy(transform.GetChild(0).gameObject);
            FindFirstObjectByType<Pointer>().DesablePointer();
            ret = true;
        }
        return ret;
    }
}
