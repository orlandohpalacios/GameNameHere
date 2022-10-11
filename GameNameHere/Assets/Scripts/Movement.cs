using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D body;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        body.velocity = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            }
}
