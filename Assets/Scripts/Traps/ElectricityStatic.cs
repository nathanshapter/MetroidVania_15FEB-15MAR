using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityStatic : MonoBehaviour
{
  [SerializeField]  int elecDmg;
    [SerializeField] int knockback = 25;

   [SerializeField] bool knockToRight;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           
                other.gameObject.GetComponent<Health>().TakeDamage(elecDmg, RightOrLeft(), knockback);
           
           
            
        }
    }
    int RightOrLeft()
    {
        if (knockToRight) { return elecDmg; }
        else { return -elecDmg; }
    }
}
