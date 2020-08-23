using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAIMovement : MonoBehaviour
{
    // percentage of chance the enemy will move. If zero, then staying stationary
    [Range(0, 100)]
    public int moveChance = 80;

    // Speed of normal traveling
    public float walkingSpeed;

    // Option to allow enemy to pursuit player if seen
    public bool pursuitPlayerIfSeen = false;

    // Speed traveled when pursuing player
    public float pursuitSpeed;

    // Number of seconds before changing movement
    [Range(0, 5)]
    public float movementChangeInterval = 0.5f;

    private Rigidbody2D rb2d;
    private Animator animator;

    private float currentSpeed;

    private Transform playerTransform = null;

    private Coroutine currentMovementCoroutine;

    private Vector3 endPosition;

    private bool canMove = true;

    private PlayerController playerController;

    private float timePassed = 0.0f;

    private void Start()
    {
        canMove = true;
        currentSpeed = walkingSpeed;
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        endPosition = transform.position;
    }

    private void DetermineMovement()
    {
        if (moveChance == 0 || Random.Range(0, 100) > (moveChance-1))
        {
            // do nothing
        } else {
			int direction = Random.Range((int)MovementDirectionEnum.North, (int)MovementDirectionEnum.MAX);
			endPosition += Degree2Vector3(45.0f * (float)direction);
		}
    }

    private Vector3 Degree2Vector3(float degree)
    {
        float radiansAngle = degree * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(radiansAngle), Mathf.Sin(radiansAngle), 0);
    }

    private void SetAnimatorDirection(int direction)
    {
        if (animator != null) {
            animator.SetInteger("direction", direction);
        }
    }

    private void Update()
    {
        if (endPosition != null)
        {
            // for debugging
            Debug.DrawLine(rb2d.position, endPosition, Color.red);
        }

        timePassed += Time.deltaTime;

        if (timePassed >= movementChangeInterval)
        {
            timePassed = 0f;

            if (PlayerIsAlive())
            {
                DetermineMovement();
            } else
            {
                endPosition = transform.position;
            }
        }
    }

    private void FixedUpdate()
    {
        if (canMove && endPosition != null)
        {
            float remaining = (transform.position - endPosition).sqrMagnitude;

            if (remaining > float.Epsilon)
            {
                if (playerTransform != null)
                {
                    // update position because player moves
                    endPosition = playerTransform.position;
                }

                int direction = ((int)(Mathf.Repeat((float)transform.eulerAngles.y, 360))) / 45;
                SetAnimatorDirection(direction);

                Vector3 newPosition = Vector3.MoveTowards(transform.position, endPosition, currentSpeed * Time.deltaTime);
                rb2d.MovePosition(newPosition);
            }
            else
            {
                SetAnimatorDirection((int)MovementDirectionEnum.Idle);
            }
        }
    }


    private bool PlayerIsAlive()
    {
        // todo - playerController.PlayerAlive() actually returns "isDead" instead of a "is alive" boolean. Fix later :D
        if (playerController != null && !playerController.PlayerAlive())
        {
            return true;
        }

        canMove = false;
        return false;
    }

    private bool KillColliderFound(Collider2D collision)
    {
        List<Collider2D> contacts = new List<Collider2D>();
        int contactCount = collision.GetContacts(contacts);

        int enemyColliderCount = 0;
        if (contacts != null && contacts.Count > 0)
        {
            foreach (Collider2D c in contacts)
            {
                // if collider contact's name matches this game object name, then increase count. If both colliders are found, then kill collider is touched as well
                enemyColliderCount += (c.gameObject.name.Equals(gameObject.name) ? 1 : 0);
            }
        }

        return enemyColliderCount == 2;
    }


    private void OnTriggerEnter2D(Collider2D collision) { 
        bool isPlayer = collision.gameObject.name == "Player";
        bool enemyKillCollider = KillColliderFound(collision);

        if (isPlayer && enemyKillCollider)
        {
            Debug.Log("Player killed by " + gameObject.name);
            playerController.KillPlayer();
            endPosition = transform.position;
        }
        else if (enemyKillCollider)
        {
            // if inner collided hit with something else, stop moving
            endPosition = transform.position;
        }
        else if (isPlayer && pursuitPlayerIfSeen)
        {
            Debug.Log(gameObject.name + " saw player and starting pursuit!");
            currentSpeed = pursuitSpeed;
            playerTransform = collision.gameObject.transform;
            endPosition = playerTransform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        bool isPlayer = collision.gameObject.name == "Player";
        bool enemyKillCollider = KillColliderFound(collision);

        // If player left pursuit vision collider, then start walking normally again
        if (isPlayer && !enemyKillCollider)
        {
            Debug.Log(gameObject.name + " lost player. Stopping puisuit");
            SetAnimatorDirection((int)MovementDirectionEnum.Idle);
            endPosition = transform.position;
            currentSpeed = walkingSpeed;
            playerTransform = null;
        }
    }

    // for debugging
    private void OnDrawGizmos()
    {
        CircleCollider2D[] colliders = gameObject.GetComponents<CircleCollider2D>();

        if (colliders != null && colliders.Length > 0)
        {
            foreach(CircleCollider2D c in colliders) {
                Gizmos.DrawWireSphere(transform.position, c.radius);
            }
        }
    }

}
