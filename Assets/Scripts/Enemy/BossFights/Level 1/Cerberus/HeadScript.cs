using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class HeadScript : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float rotationOffset;
    


    private void Update()
    {
     

    }

    public void LookAtPlayer()
    {
        var dir = target.position - this.transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + rotationOffset;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
