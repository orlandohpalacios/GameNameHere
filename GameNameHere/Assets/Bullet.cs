using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 20;
    public int damage = 10;
    public Rigidbody2D rb;

    void Start()
    {
        rb.velocity = transform.right * Speed; 
    }
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy= hitInfo.GetComponent<Enemy>();
        if (enemy != null) 
        {
            enemy.takeDamage(damage);
        }

        Debug.Log(hitInfo.name);
        Destroy(gameObject);
    }
}
