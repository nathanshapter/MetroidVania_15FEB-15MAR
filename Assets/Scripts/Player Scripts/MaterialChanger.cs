using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MaterialChanger : MonoBehaviour
{
    Material material;
    [SerializeField] SpriteRenderer spriteRenderer;
    int fadePropertyID, poisonPropertyID;
  [SerializeField]  float fadeValue;
    [SerializeField] float poisonValue;

    PlayerStatus playerStatus;

  [SerializeField]  float periodPoison = 2f;
    private void Start()
    {
        playerStatus = GetComponent<PlayerStatus>();
        material = spriteRenderer.material;

        fadePropertyID = Shader.PropertyToID("_FullGlowDissolveFade");
        poisonPropertyID = Shader.PropertyToID("_PoisonFade");


        fadeValue = 0;
        poisonValue= 0;
        material.SetFloat(poisonPropertyID, poisonValue);
    }

    void Update()
    {
        //Update while fade value is less than 1.
        if (fadeValue < 1)
        {
            //Increase fade value over time.
            fadeValue += Time.deltaTime;
            if (fadeValue > 1) fadeValue = 1;

            //Update value in material.
            material.SetFloat(fadePropertyID, fadeValue);
        }

        if (playerStatus.isPoisoned)
        {
            float cycles = Time.time / (periodPoison);

            const float tau = Mathf.PI * 2;
            float rawSinWave = Mathf.Sin(cycles * tau );

            if(rawSinWave > 0)
            {
                poisonValue = rawSinWave;
            }
            else
            {
                poisonValue = -rawSinWave;
            }
            

            material.SetFloat(poisonPropertyID, poisonValue);
            
        }


    }
  
}
