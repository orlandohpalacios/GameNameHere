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

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }


    void Update()
    {

        if (isDashing) return;
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

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

        body.AddForce(new Vector2(0f, 10f), ForceMode2D.Impulse);
        Grounded = false;
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Ground") 
        {
            Grounded = true;
        }if (collision.gameObject.tag == "Platform") 
        {
            Grounded = true;
            body.transform.parent = collision.gameObject.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Platform")) 
        {
            body.transform.parent = null;
        }

    }
    private IEnumerator Dash() 
    {
        Debug.Log("Hey I dashed");
        CanDash = false;
        isDashing = true;
        float orginalGravity = body.gravityScale;
        body.gravityScale = 0;
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        body.gravityScale = orginalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        CanDash = true;
        Debug.Log("end of cycle");
    }
}
