using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private void Awake()
    {
        // 5 saniye sonra kendini yok et
        GameObject.Destroy(gameObject, 5.0f);    
    }
}
