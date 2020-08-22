using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject myTarget;

    // Update is called once per frame
    void Update()
    {
        if (myTarget != null)
        {
            Vector3 targetPos = myTarget.transform.position;
            targetPos.z = transform.position.z; //get current z of camera
            transform.position = targetPos; //Could use Vector3.Lerp to 
        }
    }
}
