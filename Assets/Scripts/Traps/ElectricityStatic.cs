using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityStatic : MonoBehaviour
{
  [SerializeField]  int elecDmg;
    [SerializeField] int knockback = 25;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Health>().TakeDamage(elecDmg, knockback, knockback);
        }
    }
}
