using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotMunition : MonoBehaviour
{   
    [HideInInspector]
    public typeMunition type;
    
    [HideInInspector]
    public int qtdMunition;

    [HideInInspector]
    public ItemMunition item;

    [Header("Slot References")]
    public Image icone;
    public TextMeshProUGUI txt_name;
    public TextMeshProUGUI txt_qtd;
}
