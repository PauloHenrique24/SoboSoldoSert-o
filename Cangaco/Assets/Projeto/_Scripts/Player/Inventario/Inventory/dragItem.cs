using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class dragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public Transform parentAfterDrag;

    [Header("Info")]
    public Image image;
    public TextMeshProUGUI qtd_txt;

    //[HideInInspector]
    public int qtd_;

    [HideInInspector] public ItemInv item;

    [HideInInspector] public bool inHand;

    [Header("Gun")]
    public int qtdMunition;
    
    public void OnBeginDrag(PointerEventData eventData){
        
        if(!PlayerController.inRecharge){
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            image.raycastTarget = false;
        }
    }

    public void OnDrag(PointerEventData eventData){
        if(!PlayerController.inRecharge)
            transform.position = Input.mousePosition;
    } 

    public void OnEndDrag(PointerEventData eventData){
        if(!PlayerController.inRecharge){
            transform.SetParent(parentAfterDrag);
            image.raycastTarget = true;
        }
    }
}
