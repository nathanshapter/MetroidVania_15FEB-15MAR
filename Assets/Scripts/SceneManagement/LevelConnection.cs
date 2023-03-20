using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Levels/Connection")]
public class LevelConnection : ScriptableObject
{
   
   public static LevelConnection ActiveConnection { get; set; }
}
