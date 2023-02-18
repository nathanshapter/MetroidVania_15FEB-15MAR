using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrades", menuName = "Upgrades")]
public class Upgrades : ScriptableObject
{
    public string gameObjectName, description;
    public int upgradeNumber;
}
