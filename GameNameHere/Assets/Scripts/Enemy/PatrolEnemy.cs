using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    [Header("walk Speed")]
    public float walkSpeed;


    [Header("Enemy")]
    [SerializeField] public Rigidbody2D enemy;

    [HideInInspector]
    public bool mustPatrol;
    private bool mustFlip;

    public Collider2D Bodycollider;
    public LayerMask groundLayer;
    public Transform groundCheck;
    private float distToPlayer;
    public float Range;
    public Transform Player;
    public bool coolDownHit;

    private void Start()
    {
        mustPatrol = true;
        coolDownHit = false; 
    }

    void Update()
    {
        if (mustPatrol)
        {
            Patrol();
        }
        distToPlayer = Vector2.Distance(transform.position, Player.transform.position);
       

        if (distToPlayer <= Range)
        {
            if (Player.position.x > transform.position.x && transform.localScale.x < 0 ||
               Player.position.x < transform.position.x && transform.localScale.x > 0)
            {
                Flip();
            }

            mustPatrol = false;
            Debug.Log(mustPatrol+":MustPatrol is this");
            enemy.velocity = Vector2.zero;

        }
        else 
        {
            mustPatrol = true;
            enemy.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, enemy.velocity.y); 
        }
    }
    private void FixedUpdate()
    {
        if (mustPatrol)
        {     
            mustFlip = !Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        }
    }
  
    void Patrol()
    {

        if (mustFlip || !Bodycollider.IsTouchingLayers(groundLayer))
        {
            Flip();
        }
        enemy.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, enemy.velocity.y);

    }
    void Flip()
    {
        //stops AI
        mustPatrol = false;
        //flips AI
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        //set Walk speed
        walkSpeed *= -1;
        //Start AI
        mustPatrol = true;
    }
}
