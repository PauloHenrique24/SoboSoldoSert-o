using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InvMunition_Controller : MonoBehaviour
{
    public static InvMunition_Controller current;

    public List<GameObject> slots;
    
    [Header("Local")]
    public GameObject slot;
    public Transform targetSlot;

    [Header("Munition")]
    public GameObject Obj_Mun;
    public TextMeshProUGUI txt_mun;

    void Awake(){
        if(current == null)
            current = this;
    }

    public bool AddItem(ItemMunition item, int qtd){
        bool retur = false;

        var type = false;
        int indice = 0;
        
        for(int i = 0;i < slots.Count;i++){
            if(item.type == slots[i].GetComponent<SlotMunition>().type){
                type = true;
                indice = i;
                break;
            }else{
                type = false;
            }
        }

        if(type){
            if(slots[indice].GetComponent<SlotMunition>().qtdMunition < item.qtdMax){
                for(int i = 0;i < qtd;i++){
                    slots[indice].GetComponent<SlotMunition>().qtdMunition++;
                    if(slots[indice].GetComponent<SlotMunition>().qtdMunition >= item.qtdMax) break;
                }
                slots[indice].GetComponent<SlotMunition>().txt_qtd.text = slots[indice].GetComponent<SlotMunition>().qtdMunition + "/" + item.qtdMax;

                retur = true;
            }else{
                retur = false;
            }
        }else{
            var it = Instantiate(slot,targetSlot);
            
            it.GetComponent<SlotMunition>().txt_name.text = item.name_;
            it.GetComponent<SlotMunition>().icone.sprite = item.spr_;

            for(int i = 0;i < qtd;i++){
                it.GetComponent<SlotMunition>().qtdMunition++;
                if(it.GetComponent<SlotMunition>().qtdMunition >= item.qtdMax) break;
            }

            it.GetComponent<SlotMunition>().type = item.type;
            it.GetComponent<SlotMunition>().item = item;

            it.GetComponent<SlotMunition>().txt_qtd.text = it.GetComponent<SlotMunition>().qtdMunition + "/" + item.qtdMax;

            slots.Add(it);

            retur = true;
        }

        return retur;
    }

    public void CheckSlots(){
        foreach(var i in slots){
            if(i.GetComponent<SlotMunition>().qtdMunition <= 0){
                slots.Remove(i);
                Destroy(i.gameObject);

                break;
            }
        }
    }

    public void MunitionText(string txt){
        Obj_Mun.SetActive(true);

        txt_mun.text = txt;
    }

    public void DesableTxtMunition() => Obj_Mun.SetActive(false);

}

public enum typeMunition{
    mp18,
    espingarda,
    pistol
}
