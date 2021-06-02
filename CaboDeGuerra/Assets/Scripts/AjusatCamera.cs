using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AjusatCamera : MonoBehaviour
{
    public SpriteRenderer FundoQueEuQuero;
    private void Start()
    {
        float orthoSize = FundoQueEuQuero.bounds.size.x * Screen.height / Screen.width * 0.5f;

        Camera.main.orthographicSize = orthoSize;

    }
}
