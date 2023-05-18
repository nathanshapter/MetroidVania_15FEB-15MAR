using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    MaterialChanger mc;
    public bool isPoisoned, isBurnt, isStrong, isWeak, isSmall, isLarge, isFrozen;

    private void Start()
    {
        mc = GetComponent<MaterialChanger>();   
    }
    private void Update()
    {
        if(isPoisoned)
        {
            print("you are poisoned");
        }
    }
}
