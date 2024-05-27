using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class acoespersonagem : MonoBehaviour
{
    private Moralidade moralidade;

    void Start()
    {
        // Obtém o componente Moralidade anexado ao GameObject
        moralidade = GetComponent<Moralidade>();
    }

    public void AjudarAlguem()
    {
        // Aumenta a moralidade por ajudar alguém
        moralidade.AumentarMoralidade(10);
        Debug.Log("Você ajudou alguém. Moralidade aumentada!");
    }

    public void Roubar()
    {
        // Diminui a moralidade por roubar
        moralidade.DiminuirMoralidade(15);
        Debug.Log("Você roubou. Moralidade diminuída!");
    }
}

