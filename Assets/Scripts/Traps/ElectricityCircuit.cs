using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityCircuit : MonoBehaviour
{
    [SerializeField] bool circuitOn = true; 

  [SerializeField]  GameObject[] electricityStatics;
   

    [SerializeField] float onTime, offTime, timer;

    private void Start()
    {
       
    }


    private void Update()
    {
        timer += Time.deltaTime;

        if(timer > onTime && circuitOn)
        {
            TurnOffCircuit();
            timer= 0;
           
        }
        else if(timer > offTime && !circuitOn)
        {
            TurnOnCircuit();
            timer= 0;
        }





      
    }

  void TurnOnCircuit()
    {
        circuitOn = true;
        foreach (var circuit in electricityStatics)
        {
            circuit.gameObject.SetActive(circuitOn);
        }
       
    }

    void TurnOffCircuit()
    {
        circuitOn = false;
        foreach (var circuit in electricityStatics)
        {
            circuit.gameObject.SetActive(circuitOn);
        }
       
        
    }
   
}
