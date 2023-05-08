using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
    public bool[] progression = new bool[20];

    // 0 == crouch
    // 1 = double jump
    // 2 = dash
    // 3 = platform bullet
    // 4 = ledge climb      // not yet implemented
    // 5 = wall jump
    // 6 play the flute    // coding done, music and item to be added
    // 7 open hidden areas
    // 8 wall vision to see through certain tiles // not yet implement
    // 9 bow and arrow weapon // to be implemented for lvl 2
    //10 double jump off wall



    // corrupted abilities
    // 1 = ghost mode, invincibility for 3 seconds every 30 seconds
    // 2 = fire ball, shoot a weak fireball at the enemy
    

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
