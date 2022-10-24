using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement Speed")]
    [SerializeField] public float speed;
    bool Grounded;
    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

        if (Input.GetKey(KeyCode.Space)&&Grounded) 
        {
            Jump();
        }

/*
        if (Input.GetKey(KeyCode.UpArrow)&& Time.deltaTime<1)
            body.AddForce(Vector2.up* jumpamount,ForceMode2D.Impulse);*/
           // body.velocity = new Vector2(body.velocity.x, speed);
    }
    private void Jump() 
    {
        body.AddForce(new Vector2(0f, 10f), ForceMode2D.Impulse);
        Grounded = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Ground") 
        {
            Grounded = true;
        }
    }
}
