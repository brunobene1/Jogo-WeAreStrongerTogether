using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletarEmTempo : MonoBehaviour
{
    public float _tempo;
    private void Start()
    {
        deletar();
    }
    void deletar()
    {
        Destroy(gameObject, _tempo);
    }
}
