using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutSpawner : MonoBehaviour
{
    public static OutSpawner instance;
   public Transform up, down, left, right;
    bool _up, _down, _left, _right;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }

    private void Update()
    {
        
    }
}
