using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    [SerializeField] Light2D iceLight;
    [SerializeField] float period = 2f;
    [SerializeField] float addedIntensity;
    [SerializeField] bool turnOnProximity = false;
    PlayerMovement player;
    [SerializeField] int turnOnDistance = 5;
    [SerializeField] bool varyLights;
    [SerializeField] bool turnOffRandomly;
    [SerializeField]
    [Range(0f, 100f)] float chanceToTurnOffPercentage;
    float periodIncrease;
    [SerializeField] float timeToStayOff = 5;
    [SerializeField] int timeBetweenTicks = 1;
    int timeBetweenTicksVariance = 0;
    private void Start()
    {
        timeBetweenTicksVariance = Random.Range(-5, 5);
        player= FindObjectOfType<PlayerMovement>();
        if(varyLights)
        {
            periodIncrease = Random.Range(0, 5);
        }
        else { periodIncrease = 0; }
        
    }
 [SerializeField]   bool lightOn = true;
    private void Update()
    {
        if (!lightOn) { return; }
        float numberVariance = Random.Range(0, 0.2f);



        if (turnOnProximity)
        {
            if(Vector2.Distance(player.transform.position, iceLight.transform.position)< turnOnDistance)
            {
                turnOnProximity= false;
            }

            iceLight.intensity = 0;
            return;
        }

        float cycles = Time.time / (period  + periodIncrease);

        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau +1);

        iceLight.intensity = (rawSinWave + 1) +addedIntensity * (numberVariance) +1;


       if(turnOffRandomly && !isRunningCoroutine)
        {
            StartCoroutine(TurnOffChance());
        }
      
    }
    bool isRunningCoroutine;
   float percentageToTurnOff;
    IEnumerator TurnOffChance()
    {
        percentageToTurnOff = Random.Range(0, 100);
       
        isRunningCoroutine= true;
        yield return new WaitForSeconds(timeBetweenTicks + timeBetweenTicksVariance);
        if (percentageToTurnOff > chanceToTurnOffPercentage)
        {
            lightOn= false;
            iceLight.intensity = 0;
            yield return new WaitForSeconds(timeToStayOff);
            lightOn= true;
        }
        isRunningCoroutine= false;
    }

    // have a global boolvariable list that acts as an instance, and classes use that information to base themselves on what to do
}
