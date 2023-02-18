using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCounter : MonoBehaviour
{
    public int totalDeaths;

   
    public int returnDeathAmount() // will be needed for endgame
    {
        return totalDeaths;
    }
}
