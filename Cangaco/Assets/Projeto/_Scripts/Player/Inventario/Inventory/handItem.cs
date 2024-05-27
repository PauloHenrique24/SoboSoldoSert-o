using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class handItem : MonoBehaviour, IDropHandler
{
    public GameObject handObj;

    public void OnDrop(PointerEventData eventData){
        if(!handObj.GetComponent<HandController>().isItem){
            GameObject dropped = eventData.pointerDrag;

            dragItem dragitem = dropped.GetComponent<dragItem>();

            dragitem.inHand = true;

            dragitem.parentAfterDrag.gameObject.GetComponent<InventorySlot>().isItem = false;
            dragitem.parentAfterDrag = handObj.transform;

            handObj.GetComponent<HandController>().isItem = true;

            if(dropped.GetComponent<dragItem>().item.type != typeItem.edible){
                handObj.GetComponent<HandController>().GunInHand(dropped.GetComponent<dragItem>());
            }
        }
    }

    

}
