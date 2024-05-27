using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public static InventoryController current;

    [Header("Listas")]
    public List<InventorySlot> slots;

    [Header("Slots")]
    public GameObject slot_Pref;
    public Transform parent_Slot;
    public int qtd_Slots;

    [Header("Itens")]
    public GameObject item_Pref;

    [HideInInspector] public bool inventory_full;

    void Awake()
    {
        if(current == null)
            current = this;
    }

    void Start(){
        for(int i = 0;i < qtd_Slots;i++){
            var slot = Instantiate(slot_Pref,parent_Slot);
            slots.Add(slot.GetComponent<InventorySlot>());
        }
    }

    public bool AddItem(ItemInv item){
        var ret = false;
        var b1 = false; // se o item não tiver espaço, cria
        var b2 = false; // se não tiver um item igual no inventario então cria

        var add = false; // se o item for acrescentado ele não pode ser criado

        if(item.can_group){

            foreach(var i in slots){
                if(i.transform.childCount > 0 && i.transform.GetChild(0).GetComponent<dragItem>().item.type == item.type){
                    if(i.transform.GetChild(0).GetComponent<dragItem>().qtd_ < item.max_Itens){
                        //Add Item
                        i.transform.GetChild(0).GetComponent<dragItem>().qtd_++;
                        i.transform.GetChild(0).GetComponent<dragItem>().qtd_txt.text = i.transform.GetChild(0).GetComponent<dragItem>().qtd_.ToString("");

                        add = true;

                        b1 = false;
                        ret = true;
                        break;
                    }else{
                        b1 = true;
                    }

                    b2 = false;
                }else{
                    b2 = true;
                }
            }

            if(!add){

                if(b1){
                    if(!inventory_full){
                        //Criar item
                        CreateItem(item);
                        ret = true;
                    }else{
                        ret = false;
                    }
                }

                if(b2){

                    if(FindFirstObjectByType<HandController>().gameObject.transform.childCount > 0 &&
                    FindFirstObjectByType<HandController>().gameObject.transform.GetChild(0).GetComponent<dragItem>().item.type == item.type){
                        if(FindFirstObjectByType<HandController>().gameObject.transform.GetChild(0).GetComponent<dragItem>().qtd_ < FindFirstObjectByType<HandController>().gameObject.transform.GetChild(0).GetComponent<dragItem>().item.max_Itens){
                            FindFirstObjectByType<HandController>().gameObject.transform.GetChild(0).GetComponent<dragItem>().qtd_++;
                            FindFirstObjectByType<HandController>().gameObject.transform.GetChild(0).GetComponent<dragItem>().qtd_txt.text = FindFirstObjectByType<HandController>().gameObject.transform.GetChild(0).GetComponent<dragItem>().qtd_.ToString("");
                            add = true;
                            ret = true;
                        }
                    }

                    if(!add){
                        if(!inventory_full){
                            //Criar item
                            CreateItem(item);
                            ret = true;
                        }else{
                            ret = false;
                        }
                    }
                }
            }

        }else{
            if(!inventory_full){
                //Criar Item
                CreateItem(item);
                ret = true;
            }else{
                ret = false;
            }
        }

        foreach(var i in slots){
            if(i.transform.childCount > 0){
                inventory_full = true;
            }else{
                inventory_full = false;
                break;
            }
        }

        return ret;
    }

    public void CreateItem(ItemInv item){
        foreach(var i in slots){
            if(!i.isItem){
                var it = Instantiate(item_Pref,i.gameObject.transform);
                it.GetComponent<dragItem>().image.sprite = item.InvImage_;
                
                it.GetComponent<dragItem>().qtd_txt.text = "1";

                if(item.can_group){
                    it.GetComponent<dragItem>().qtd_ = 1;
                }else{
                    it.GetComponent<dragItem>().qtd_txt.gameObject.SetActive(false);
                }

                it.GetComponent<dragItem>().item = item;

                i.isItem = true;
                break;
            }
        }
    }
    

    public void CheckInventory(){
        foreach(var i in slots){
            if(i.transform.childCount > 0 && i.transform.GetChild(0).GetComponent<dragItem>().qtd_ <= 0){
                slots.Remove(i);
                Destroy(i.gameObject);
                break;
            }
        }
    }

    public void CheckSlots(){
        foreach(var i in slots){
            if(i.transform.childCount <= 0){
                inventory_full = false;
                break;
            }else{
                inventory_full = true;
            }
        }
    }

}

public enum typeItem{
    gun,
    gunWhite,
    throwable,
    edible
}
