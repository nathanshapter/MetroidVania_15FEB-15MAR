using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyValues", menuName = "EnemyValues")]
public class EnemyValues : ScriptableObject
{
    public string gameObjectName, description;
   
    public int health;
    public int attackDamage;

   public bool doesSimplePatrol; // ie walks back and forth avoiding ledges and wall
    public bool doesChasePlayer;

}
