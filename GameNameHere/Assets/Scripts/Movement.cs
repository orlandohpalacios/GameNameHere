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

        if (Input.GetKey(KeyCode.UpArrow)) 
        {
            body.AddForce(new Vector2(0f, 5f), ForceMode2D.Impulse);
        }

/*
        if (Input.GetKey(KeyCode.UpArrow)&& Time.deltaTime<1)
            body.AddForce(Vector2.up* jumpamount,ForceMode2D.Impulse);*/
           // body.velocity = new Vector2(body.velocity.x, speed);
    }
}
