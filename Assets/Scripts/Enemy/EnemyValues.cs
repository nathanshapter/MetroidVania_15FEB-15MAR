using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyValues", menuName = "EnemyValues")]
public class EnemyValues : ScriptableObject
{
    public string gameObjectName, description;
   
    public int health;
    public int contactDamage;
   public bool doesSimplePatrol; // ie walks back and forth avoiding ledges and wall // i think both these bools can be removed
    public bool doesChasePlayer;
    public float knockbackY, knockbackX;

}
