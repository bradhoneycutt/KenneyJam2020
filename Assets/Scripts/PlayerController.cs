﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PlayerController : MonoBehaviour
{
   
    Rigidbody2D body;

    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;

    public float runSpeed = 20.0f;
    public Sprite GhostSprite; 

    public GameObject blockingTileMapObject;


    Tilemap tilemap;


    /// <summary>
    /// Available potion effects
    /// 
    /// Only 2 may be in effect player dies if all three effects are active. 
    /// </summary>
    public bool IsInvincible = false;
    public bool IsLevitate = false;
    public bool IsSpectral = false;
    
    private int PotCount = 0; 
    private bool _isDead = false;

    public Text GameOverText; 

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        if (blockingTileMapObject != null)
        {
            tilemap = blockingTileMapObject.GetComponent<Tilemap>();
        }


    }

    void Update()
    {
        // Gives a value between -1 and 1
        //Using named inputs for unity should support controller as well as keyboard 
        //Edit>Prefrences>Input Manager to see supported values. 

        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down

        if(IsInvincible && IsLevitate && IsSpectral)
        {
            gameObject.GetComponent<DamageHandler>().PlayerHealth = 0;
            gameObject.GetComponent<Health>().PlayerHealth = 0; 
        }
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "PotionOfLevitation")
        {
            IsLevitate = true;
            collision.gameObject.SetActive(false);
            var audioManager = GameObject.Find("AudioManager");
            audioManager.GetComponent<AudioManager>().PlayPotionPickup();

            var gameManger = GameObject.Find("GameManager").GetComponent<GameManager>();

            gameManger.PlayerDialog("This strange brew makes me feel lighter than air.", 1f);
            PotCount += 1;
            PotionWarning();
        }
        else if (collision.gameObject.name == "WhiskeyOfStrength")
        {
            Debug.Log("Whiskey");
            IsInvincible = true;
            collision.gameObject.SetActive(false);
            var audioManager = GameObject.Find("AudioManager");
            audioManager.GetComponent<AudioManager>().PlayPotionPickup();

            var gameManger = GameObject.Find("GameManager").GetComponent<GameManager>();

            gameManger.PlayerDialog("Imbibing this familar spirit steels resolve and body.", 1f);
            PotCount += 1;
            PotionWarning();
        }
        else if (collision.gameObject.name == "ReapersBreath")
        {
            IsSpectral = true;
            collision.gameObject.SetActive(false);
            var audioManager = GameObject.Find("AudioManager");
            audioManager.GetComponent<AudioManager>().PlayPotionPickup();

            var gameManger = GameObject.Find("GameManager").GetComponent<GameManager>();
            gameObject.GetComponent<SpriteRenderer>().sprite = GhostSprite;

            gameManger.PlayerDialog("The vapors burn my nostrils. The world seems to fade around me.", 1f);
            blockingTileMapObject.GetComponent<TilemapCollider2D>().enabled = false;
            PotCount += 1;
            PotionWarning();

        }
        else if (collision.gameObject.name == "Meat")
        {
            collision.gameObject.SetActive(false);
            var gameManger = GameObject.Find("GameManager").GetComponent<GameManager>();
            gameManger.PlayerDialog("A momentary reprieve from the curse that seeks to destroy me.", 1f);
            gameObject.GetComponent<DamageHandler>().PlayerHealth = 6;
            gameObject.GetComponent<Health>().PlayerHealth = 6;
        }
        else if (collision.gameObject.name == "Candle")
        {
            collision.gameObject.SetActive(false);
            var light  = gameObject.GetComponentInChildren<Light>();
            light.range = 15; 



        }
    }

    void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }

    public void KillPlayer()
    {
        gameObject.GetComponent<DamageHandler>().PlayerHealth = 0;
        gameObject.GetComponent<Health>().PlayerHealth = 0;

        GameObject.Find("AudioManager").GetComponent<AudioManager>().PlayDeathSound();
        _isDead = true;

        var go = GameObject.Find("GameManager");
        go.GetComponent<GameManager>().SetPlayerStatus(false);
        
        GameOverText.gameObject.SetActive(true);
    }

    public bool PlayerAlive()
    {
        return !_isDead;
    }

    public void PotionWarning()
    {
        gameObject.GetComponent<PotionSickness>().NumberOfPotions += 1; 
    }
}
