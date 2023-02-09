using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyValues", menuName = "EnemyValues")]
public class EnemyValues : ScriptableObject
{
    public string gameObjectName, description;
   
    public int health;
    public int attackDamage;

}
