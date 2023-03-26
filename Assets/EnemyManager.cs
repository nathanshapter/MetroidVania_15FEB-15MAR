using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
  [SerializeField]  EnemyHealth[] enemies;

    private void Start()
    {
        enemies = FindObjectsOfType<EnemyHealth>();
    }
}
