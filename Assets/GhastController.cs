using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhastController : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator= GetComponent<Animator>();
    }
}
