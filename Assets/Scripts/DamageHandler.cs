using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DamageHandler : MonoBehaviour
{

    public int PlayerHealth = 6;
    public float inulnPeriod = 0;
    public GameObject damageTilemapGameObject;
    Tilemap tilemap;

    float invulnTimer = 0f;
    int correctLayer;

    public float DamageDelay = 2.5f; //play with this value in the editor to find the right balance for the player to die from curse. 
    private float _coolDown = 0f;


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

        //traps, spikes and water instantly kill the player. Curse damage happens over time.
        Debug.Log("You Fucking died!");
        Debug.Log("Object Name: " +collision.gameObject.name); 
        if (tilemap != null && damageTilemapGameObject == collision.gameObject && !gameObject.GetComponent<PlayerController>().IsLevitate)
        {
            PlayerHealth = 0; 
            invulnTimer = inulnPeriod;
        }else if(collision.gameObject.name == "FireProjectile(Clone)")
        {
            PlayerHealth = 0;
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
        if (PlayerHealth <= 0)
        {
           
            Die();
            if(gameObject.name == "Player")
            {
                gameObject.GetComponent<PlayerController>().KillPlayer(); 
            }
        }

        _coolDown -= Time.deltaTime;
        if (_coolDown <= 0)
        {
            
            GameObject.Find("Player").GetComponent<Health>().PlayerHealth -= 1;
            PlayerHealth = PlayerHealth - 1; 
            _coolDown = DamageDelay;
        }

    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
