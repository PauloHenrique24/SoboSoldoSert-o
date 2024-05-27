using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropItem : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData){
        GameObject dropped = eventData.pointerDrag;
        ItemInv item = dropped.GetComponent<dragItem>().item;

        dropped.GetComponent<dragItem>().parentAfterDrag.GetComponent<InventorySlot>().isItem = false;

        if(dropped.GetComponent<dragItem>().inHand){
            dropped.GetComponent<dragItem>().parentAfterDrag.gameObject.GetComponent<HandController>().isItem = false;
            FindFirstObjectByType<GunController>().GunDesable();
            InvMunition_Controller.current.DesableTxtMunition();
            dropped.GetComponent<dragItem>().inHand = false;
        }

        for(int i = 0;i < int.Parse(dropped.GetComponent<dragItem>().qtd_txt.text);i++){
            var it = Instantiate(item.prefab,FindFirstObjectByType<PlayerController>().transform.position,Quaternion.identity);
            it.GetComponent<ItemPref>().Create();
        }

        InventoryController.current.CheckSlots();

        Destroy(dropped.gameObject);
    }
}
