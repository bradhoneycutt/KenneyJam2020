using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amulet : MonoBehaviour
{
    //for game intro make it appear our theif picked up an amulet 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("triggered!");
        Destroy(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("triggered!");
        Destroy(collision.gameObject);
    }
}
