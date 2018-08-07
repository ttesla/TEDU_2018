using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyObject : MonoBehaviour
{
    public float AutoDestroyTime;

    private void Awake()
    {
        GameObject.Destroy(gameObject, AutoDestroyTime);
    }
}
