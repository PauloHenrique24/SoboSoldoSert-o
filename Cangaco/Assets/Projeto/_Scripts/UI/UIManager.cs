using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager current;

    [Header("Collected")]
    public GameObject pressUI;
    public GameObject colectUI;

    [Header("Inventario")]
    public GameObject Inventory;
    bool invOpen;

    // [Header("Lifes")]
    // public GameObject lifes_obj;
    // public Image img_life;
    // public TextMeshProUGUI txt_life;

    [Header("House Enter")]
    public Transform targetHouse;


    void Awake()
    {
        if(current == null)
            current = this;

    }

    public void CollectOn() => pressUI.SetActive(true);
    public void CollectOff() => pressUI.SetActive(false);

    public void ItemColect(Sprite img,string nome){
        colectUI.SetActive(true);

        colectUI.transform.Find("icon_item").GetComponent<Image>().sprite = img;
        colectUI.transform.Find("nome_item").GetComponent<TextMeshProUGUI>().text = nome;

        StartCoroutine(EndAnimCollect());
    }

    IEnumerator EndAnimCollect(){
        yield return new WaitForSeconds(.6f);
        colectUI.SetActive(false);
    }

    void LateUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Tab)){
            invOpen = !invOpen;
            Inventory.SetActive(invOpen);
        }
    }

    // public void EnableLife() => lifes_obj.SetActive(true);
    // public void DesableLife() => lifes_obj.SetActive(false);

    // public void LifeName(string txt) => txt_life.text = txt;
    // public void LifeManager(float num, float numMax) => img_life.fillAmount = num / numMax;
}
