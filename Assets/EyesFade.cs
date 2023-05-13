using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesFade : MonoBehaviour
{
    [SerializeField] SpriteRenderer leftEye, rightEye;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(CloseEyes());
    }
    IEnumerator CloseEyes()
    {
        yield return new WaitForSeconds(1.5f);
        leftEye.enabled = false;
        rightEye.enabled = false;
    }
}
