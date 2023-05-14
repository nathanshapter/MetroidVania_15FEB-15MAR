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
        sceneName= string.Empty; /// for when you panic in the future. this does not get used when playing from the scene, but only from continue button
        // should create a debuggin tool that on click you can change to the new scene
        mainLight1a = false;
    }
}
