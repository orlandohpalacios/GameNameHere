using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement Speed")]
    [SerializeField] public float speed;
    bool Grounded;
    private Rigidbody2D body;

    private bool CanDash =true;
    private bool isDashing = false;
    private float dashingPower = 30f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    private bool FacingRight = true;//determine whether to flip the character or not.

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }


    void Update()
    {

        if (isDashing) return;
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);
        //checks the position in which the player is facing
        if (body.velocity.x > 0&&FacingRight) 
        {
            Flip();
        }
        if (body.velocity.x <0&&!FacingRight)
        {
            Flip();
        }

        if (Input.GetKey(KeyCode.UpArrow)&&Grounded) 
        {
            Jump();
        }
        if (Input.GetKey(KeyCode.Space)&&CanDash) 
        {
           StartCoroutine( Dash());
        }
    }
    private void Jump() 
    {
        if (isDashing) return;
        body.AddForce(new Vector2(0f, 9f), ForceMode2D.Impulse);
        Grounded = false;
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Ground") 
        {
            Grounded = true;
        }
        else if (collision.gameObject.tag == "Platform") 
        {
            Grounded = true;
            body.transform.parent = collision.gameObject.transform;
        }
        if (collision.otherCollider.tag == "PowerUp") 
        {
            Destroy(collision.gameObject);

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Platform")) 
        {
            body.transform.parent = null;
        }

    }
    private void Flip() 
    {
        FacingRight = !FacingRight;
        transform.Rotate(0f, 180f, 0f);
    }
    private IEnumerator Dash() 
    {
        CanDash = false;
        
        isDashing = true;
        
        float orginalGravity = body.gravityScale;
        
        body.gravityScale = 0;

        Physics2D.IgnoreLayerCollision(8, 9, true);
        Physics2D.IgnoreLayerCollision(10, 11, true);

        
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * dashingPower, 0f);
        
        yield return new WaitForSeconds(dashingTime);

        Physics2D.IgnoreLayerCollision(8, 9, false);
        Physics2D.IgnoreLayerCollision(10, 11, false);
        body.gravityScale = orginalGravity;
        
        isDashing = false;
        
        yield return new WaitForSeconds(dashingCooldown);
        
        CanDash = true;
        
    }
}
