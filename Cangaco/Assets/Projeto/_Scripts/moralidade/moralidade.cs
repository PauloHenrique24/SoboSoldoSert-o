using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moralidade : MonoBehaviour
{
    // moralidade do personagem
    public int moralidade = 0;

    // o personagem é bom ou ruim
    public int limiteBom = 50;
    public int limiteRuim = -50;

    // Método para aumentar a moralidade
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
            Debug.Log("O personagem é considerado Bom.");
            // Aqui você pode adicionar lógicas adicionais para quando o personagem for bom
        }
        else if (moralidade <= limiteRuim)
        {
            Debug.Log("O personagem é considerado Ruim.");
            // Aqui você pode adicionar lógicas adicionais para quando o personagem for ruim
        }
        else
        {
            Debug.Log("O personagem é Neutro.");
            // Aqui você pode adicionar lógicas adicionais para quando o personagem for neutro
        }
    }
}