﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DamageHandler : MonoBehaviour
{

    public int PlayerHealth = 6;
    public float invulnPeriod = 0;
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

    private void SetHealthToZero(string colliderName)
    {
        Debug.Log("You Fucking died! By " + colliderName);
        PlayerHealth = 0;
        invulnTimer = invulnPeriod;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {

        //traps, spikes and water instantly kill the player. Curse damage happens over time.
        if (tilemap != null && damageTilemapGameObject == collision.gameObject && !gameObject.GetComponent<PlayerController>().IsLevitate)
        {
            SetHealthToZero(collision.gameObject.name);
        }else if(collision.gameObject.tag == "Projectile" && !gameObject.GetComponent<PlayerController>().IsInvincible)
        {
            SetHealthToZero(collision.gameObject.name);
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

            StartCoroutine(DamageFlash());

            GameObject.Find("AudioManager").GetComponent<AudioManager>().PlayDamageSound();
        }

    }

    IEnumerator DamageFlash()
    {
        var renderer = gameObject.GetComponent<SpriteRenderer>();
        var normal = renderer.material.color;
        renderer.material.color = Color.green;
        //renderer.material.color = collideColor;
        yield return new WaitForSeconds(.5f);
        renderer.material.color = normal;
        
    }

        private void Die()
    {
        Destroy(gameObject);
    }
}
