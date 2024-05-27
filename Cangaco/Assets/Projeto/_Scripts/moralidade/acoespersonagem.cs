using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class acoespersonagem : MonoBehaviour
{
    private Moralidade moralidade;

    void Start()
    {
        // Obt�m o componente Moralidade anexado ao GameObject
        moralidade = GetComponent<Moralidade>();
    }

    public void AjudarAlguem()
    {
        // Aumenta a moralidade por ajudar algu�m
        moralidade.AumentarMoralidade(10);
        Debug.Log("Voc� ajudou algu�m. Moralidade aumentada!");
    }

    public void Roubar()
    {
        // Diminui a moralidade por roubar
        moralidade.DiminuirMoralidade(15);
        Debug.Log("Voc� roubou. Moralidade diminu�da!");
    }
}

