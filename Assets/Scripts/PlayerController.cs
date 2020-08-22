using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
   
    Rigidbody2D body;

    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;

    public float runSpeed = 20.0f;
    private bool _isDead = false; 

    public float ThrowDelay = 0.25f;
    //public GameObject ProjectilePrefab;
    private float _coolDownTimer = 0;

    public Text GameOverText; 

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Gives a value between -1 and 1
        //Using named inputs for unity should support controller as well as keyboard 
        //Edit>Prefrences>Input Manager to see supported values. 

        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down

        //_coolDownTimer -= Time.deltaTime;
        //if (Input.GetMouseButtonDown(0) && _coolDownTimer <= 0)
        //{
        // // We clicked, but on what?
        //Vector3 mouseWorldPos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector2 mousePos2D = new Vector2(mouseWorldPos3D.x, mouseWorldPos3D.y);

        //        _coolDownTimer = ThrowDelay;

        //   Vector3 direction = mouseWorldPos3D - transform.position;
        //     direction.Normalize();
        //    float zAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        //    Quaternion rot = Quaternion.Euler(0, 0, zAngle);
        //    Instantiate(ProjectilePrefab, body.transform.position, rot);
        //}

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
        _isDead = true;

        var go = GameObject.Find("GameManager");
        go.GetComponent<GameManager>().SetPlayerStatus(false);
        
        GameOverText.gameObject.SetActive(true);


    }

    public bool PlayerAlive()
    {
        return _isDead;
    }
}
