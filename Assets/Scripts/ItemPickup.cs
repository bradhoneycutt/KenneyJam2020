using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var rigidbody2D = collision.attachedRigidbody;

        switch (gameObject.name)
        {
            case "Player":
                
                
                break;
                 
            default:
                break;
        }
        
        
        
    }
}
