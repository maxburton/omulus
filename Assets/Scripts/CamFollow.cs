using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public GameObject target;
    public Camera playerCam;
    
    void Start()
    {
        
    }

    void Update()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
        transform.rotation = playerCam.transform.rotation;

    }
}
