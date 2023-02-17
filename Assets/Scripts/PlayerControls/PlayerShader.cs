using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpriteShadersUltimate;

public class PlayerShader : MonoBehaviour
{
  [SerializeField]  Material damageMat, DeathMat;
    int fadePropertyID;
    float fadeValue;

   [SerializeField] SpriteRenderer sr;

    private void Start()
    {
        
        fadeValue = 0f;

         fadePropertyID = Shader.PropertyToID("_AddColorFade");
    }
    private void Update()
    {
        
    }
   

    public void PlayShaderDamage()
    {
        sr.material = damageMat;
        Debug.Log("playershaderdamage should work");
        fadeValue = 1f; // takes to full
        damageMat.SetFloat(fadePropertyID, fadeValue);
      //  fadeValue -= Time.deltaTime;

    }
    
}
