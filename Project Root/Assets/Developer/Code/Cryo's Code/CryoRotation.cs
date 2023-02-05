using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CryoStorage;
public class CryoRotation : MonoBehaviour
{
    public GameObject center;
    public float radius;
    public float angle;

    // Update is called once per frame
    void Update()
    {
        Vector3 centerPos = center.transform.position;
        // Vector3 dir = transform.position - centerPos; 
        
        transform.position = CryoStorageMath.PointOnRadius(centerPos, radius, angle);
        // Debug.DrawRay(centerPos, dir,Color.cyan);
        // transform.rotation = Quaternion.LookRotation(dir);  
        transform.rotation = CryoStorageMath.AimAtDirection(centerPos, transform.position);
    }
}
