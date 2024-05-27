using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class bau : MonoBehaviour
{
    public GameObject baupanel;
    public GameObject contentspanel;
    public GameObject contenttexto;

    private bool isopem = false;

    // Start is called before the first frame update
    void Start()
    {
        contentspanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o jogador entrou na área de gatilho do baú
        if (other.CompareTag("player"))
        {
            // Ativa o painel do baú para abrir
            baupanel.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("player"))
        {
            baupanel.SetActive(false);

            if (isopem)
            {
                isopem = false;
                contentspanel.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && baupanel.activeSelf &&!isopem)
        {
            openbau();
        }
    }
    void openbau()
    {
        isopem = true;
        baupanel.SetActive(false);
        contentspanel.SetActive(true);

        contenttexto.GetComponent<TextMeshProUGUI>().text = "e isso ai ";
    }
}
