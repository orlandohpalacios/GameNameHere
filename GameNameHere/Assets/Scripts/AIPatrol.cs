using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    public float walkSpeed;

    [HideInInspector]
    public bool mustPatrol;
    private bool mustFlip, canShoot;

    public Rigidbody2D rb;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public Collider2D Bodycollider;

    public Transform Player, shootPos;
    public float Range,TimeBTWShots, shootSpeed;
    public GameObject bullet;


    private float distToPlayer;
    void Start()
    {
        mustPatrol = true;
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (mustPatrol) 
        {
            Patrol();
        }
        //calculate distance of player
        distToPlayer = Vector2.Distance(transform.position, Player.transform.position);

        if ( distToPlayer <= Range)
        {
            if (Player.position.x > transform.position.x && transform.localScale.x < 0 ||
               Player.position.x < transform.position.x && transform.localScale.x > 0)
            {
                Flip();
            }
        

            mustPatrol = false;
            rb.velocity = Vector2.zero; 
            if(canShoot) StartCoroutine(Shoot());
        }
        else
        {
            mustPatrol = true;
        }
    }

    private void FixedUpdate()
    {
        if (mustPatrol) { 
            //if touching ground flip, ! is do not flip
            mustFlip = !Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer); 
        }
    }
    void Patrol() 
    {
        if (mustFlip||Bodycollider.IsTouchingLayers(groundLayer)) 
        {
            Flip(); 
        }
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    
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
    IEnumerator Shoot()
    {
        //Shoot
        canShoot = false;
        yield return new WaitForSeconds(TimeBTWShots);
        GameObject newBullet = Instantiate(bullet,shootPos.position,Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * walkSpeed*Time.fixedDeltaTime,0);
        canShoot = true;
    }
}
