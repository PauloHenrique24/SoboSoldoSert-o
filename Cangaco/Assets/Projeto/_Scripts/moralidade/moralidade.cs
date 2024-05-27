using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moralidade : MonoBehaviour
{
    // moralidade do personagem
    public int moralidade = 0;

    // o personagem � bom ou ruim
    public int limiteBom = 50;
    public int limiteRuim = -50;

    // M�todo para aumentar a moralidade
    public void AumentarMoralidade(int valor)
    {
        moralidade += valor;
        VerificarEstado();
    }

    // diminuir a moralidade
    public void DiminuirMoralidade(int valor)
    {
        moralidade -= valor;
        VerificarEstado();
    }

    // Verifica o estado atual do personagem
    private void VerificarEstado()
    {
        if (moralidade >= limiteBom)
        {
            Debug.Log("O personagem � considerado Bom.");
            // Aqui voc� pode adicionar l�gicas adicionais para quando o personagem for bom
        }
        else if (moralidade <= limiteRuim)
        {
            Debug.Log("O personagem � considerado Ruim.");
            // Aqui voc� pode adicionar l�gicas adicionais para quando o personagem for ruim
        }
        else
        {
            Debug.Log("O personagem � Neutro.");
            // Aqui voc� pode adicionar l�gicas adicionais para quando o personagem for neutro
        }
    }
}