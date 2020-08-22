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
            case "MushroomOfSmall":
                rigidbody2D.transform.localScale = new Vector3(.5f, .5f, 1f);
                break;
                 
            default:
                break;
        }
        
        
        Destroy(gameObject);
    }
}
