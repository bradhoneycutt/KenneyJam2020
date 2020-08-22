using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DamageHandler : MonoBehaviour
{

    public int Health = 1;
    public float inulnPeriod = 0;
    public GameObject damageTilemapGameObject;
    Tilemap tilemap;

    float invulnTimer = 0f;
    int correctLayer;

    SpriteRenderer spriteRender;

    private void Start()
    {
        correctLayer = gameObject.layer;
        spriteRender = GetComponent<SpriteRenderer>();
        if (spriteRender == null)
        {
            spriteRender = transform.GetComponentInChildren<SpriteRenderer>();
            if (spriteRender == null)
            {
                Debug.LogError("Object '" + gameObject.name + "' has no sprite renderer.");
            }
        }

        if (damageTilemapGameObject != null)
        {
            tilemap = damageTilemapGameObject.GetComponent<Tilemap>();
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("You Fucking died!");
        Debug.Log("Object Name: " +collision.gameObject.name); 
        if (tilemap != null && damageTilemapGameObject == collision.gameObject)
        {
            Health--;
            invulnTimer = inulnPeriod;
        }else if(collision.gameObject.name == "FireProjectile(Clone)")
        {
            Health--;
            invulnTimer = inulnPeriod;
        }


    }
    
    void Update()
    {
        if (invulnTimer > 0)
        {
            invulnTimer -= Time.deltaTime;

            if (invulnTimer <= 0)
            {
                gameObject.layer = correctLayer;
                if (spriteRender != null)
                {
                    spriteRender.enabled = true;
                }
            }
            else
            {
                if (spriteRender != null)
                {
                    spriteRender.enabled = !spriteRender.enabled;
                }
            }
        }
        if (Health <= 0)
        {
            Debug.Log("Health: " + Health);
            Die();
            if(gameObject.name == "Player")
            {
                gameObject.GetComponent<PlayerController>().KillPlayer(); 
            }
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
