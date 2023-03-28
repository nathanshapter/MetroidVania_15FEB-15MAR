using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MaterialChanger : MonoBehaviour
{
    Material material;
    [SerializeField] SpriteRenderer spriteRenderer;
    int fadePropertyID;
    float fadeValue;

    private void Start()
    {
        material = spriteRenderer.material;

        fadePropertyID = Shader.PropertyToID("_FullGlowDissolveFade");

        fadeValue = 0;
    }

    private void Update()
    {
       
    }
}
