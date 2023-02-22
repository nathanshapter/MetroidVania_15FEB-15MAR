using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
    public bool[] progression = new bool[6];

    // 0 == crouch
    // 1 = double jump
    // 2 = dash
    // 3 = platform bullet
    // new 4 = ledge climb
    // 5 = wall jump

    public static ProgressionManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }
}
