using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityStatic : MonoBehaviour
{
  [SerializeField]  int elecDmg;
    [SerializeField] int knockback = 25;

   [SerializeField] bool knockToRight;

    [SerializeField] float crouchLaunch; // to be added into player movement as a corrupted ability




    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerMovement>().isCrouching) {
                other.gameObject.GetComponent<Health>().TakeDamage(elecDmg, 0, knockback * crouchLaunch);
            }
            else
            {
                other.gameObject.GetComponent<Health>().TakeDamage(elecDmg, RightOrLeft(), knockback);

            }



        }
    }
    int RightOrLeft()
    {
        if (knockToRight) { return elecDmg; }
        else { return -elecDmg; }
    }
}
