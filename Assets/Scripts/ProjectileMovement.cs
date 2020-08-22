using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float MaxSpeed = 5f;

    //Update is called once per frame

    private int _directionX = 0, _directionY = 0; 

    void Update()
    {

        Vector3 posDelta = transform.position;
        Vector3 velocity = new Vector3(_directionX *(MaxSpeed * Time.deltaTime), _directionY * (MaxSpeed * Time.deltaTime), 0);
        //Quaternon has to come  first
        posDelta += transform.rotation * velocity;

        transform.position = posDelta;

    }

    public void SetDirection(int directionX, int directionY)
    {
        _directionX = directionX;
        _directionY = directionY;
    }


}
