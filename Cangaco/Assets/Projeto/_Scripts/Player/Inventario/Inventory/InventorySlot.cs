using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    [HideInInspector]
    public bool isItem;

    public bool isHand;
    
    public void OnDrop(PointerEventData eventData){
        if(!isItem){
            GameObject dropped = eventData.pointerDrag;
            dragItem dragitem = dropped.GetComponent<dragItem>();

            dragitem.parentAfterDrag.gameObject.GetComponent<InventorySlot>().isItem = false;
            
            if(dragitem.inHand){
                dragitem.parentAfterDrag.gameObject.GetComponent<HandController>().isItem = false;
                FindFirstObjectByType<GunController>().GunDesable();
                InvMunition_Controller.current.DesableTxtMunition();

                FindFirstObjectByType<Pointer>().DesablePointer();
                dragitem.inHand = false;
            }

            if(isHand){
                dragitem.inHand = true;

                GetComponent<HandController>().isItem = true;
                GetComponent<HandController>().GunInHand(dropped.GetComponent<dragItem>());
            }
            
            dragitem.parentAfterDrag = transform;

            isItem = true;
        }
    }
}
