using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinhaTrigger : MonoBehaviour
{
    public GameObject MeuManagerParaVariavelGameOver;

  
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.CompareTag("Jogador"))
        {
            Debug.Log("Cpu Venceu");
            MeuManagerParaVariavelGameOver.GetComponent<Manager>().GameOverBoolP = true;
            
        }
        else if (collision.CompareTag("Cpu"))
        {
            Debug.Log("Jogador Venceu");
            MeuManagerParaVariavelGameOver.GetComponent<Manager>().GameOverBoolC = true;
        }
    }
    
}
