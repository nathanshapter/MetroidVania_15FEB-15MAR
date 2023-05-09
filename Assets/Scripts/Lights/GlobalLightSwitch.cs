using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLightSwitch : MonoBehaviour, iSaveData
{
    public bool isOn;
    GlobalLightScript gls;
    [SerializeField] private string id;
    [ContextMenu(" Generate guid for ID")]


    public void LoadData(GameData data)
    {
        data.switchesPressed.TryGetValue(id, out isOn);
        if (isOn)
        {
            gameObject.SetActive(false);
        }
    }
    public void SaveData(GameData data)
    {
        if (data.switchesPressed.ContainsKey(id))
        {
            data.switchesPressed.Remove(id);
        }
        data.switchesPressed.Add(id, isOn);
    }
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }


    private void Start()
    {
        gls = GetComponentInParent<GlobalLightScript>();
        if (isOn)
        {
            gameObject.SetActive(false);
        }

    }
    void turnOn()
    {

        isOn= true;
        gls.CheckSwitches();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Array.Resize(ref gls.switches, gls.switches.Length -1);
        print(gls.switches.Length);
        gls.CheckSwitches();
        isOn = true;
        gameObject.SetActive(false);
    }
}
