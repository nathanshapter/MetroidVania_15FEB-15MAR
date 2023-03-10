using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrades", menuName = "Upgrades")]
public class Upgrades : ScriptableObject
{

    [Header("==========Variables To Set==========")]
    [Space(20)]
    public string gameObjectName, description;
    public int upgradeNumber;
}
