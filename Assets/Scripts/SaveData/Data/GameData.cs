using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData 
{
    public int deathCount;
    
    public Vector3 playerPosition;
    public SerialisableDictionary<string, bool> switchesPressed;
    public bool[] progression;
    public string sceneName;
    public bool mainLight1a;




    public GameData()
    {
        this.deathCount = 0;
        playerPosition= Vector3.zero;
        switchesPressed = new SerialisableDictionary<string, bool>();

        progression = new bool[20];
        sceneName= string.Empty;
        mainLight1a = false;
    }
}
