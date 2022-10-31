using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    public float walkSpeed;

    [HideInInspector]
    public bool mustPatrol;
    private bool mustFlip;

    public Rigidbody2D rb;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public Collider2D Bodycollider;
    void Start()
    {
        mustPatrol = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (mustPatrol) 
        {
            Patrol();
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
   
}
