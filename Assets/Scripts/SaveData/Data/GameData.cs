using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData 
{
    public int deathCount;
    
    public Vector3 playerPosition;
    public SerialisableDictionary<string, bool> switchesPressed;
    

    public GameData()
    {
        this.deathCount = 0;
        playerPosition= Vector3.zero;
        switchesPressed = new SerialisableDictionary<string, bool>();
    }
}
