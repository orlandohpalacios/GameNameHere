using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int health = 100;
    public GameObject deathEffect;

    public void takeDamage(int damage) 
    {
        health -= damage;
        if (health <=0) 
        {
            Die();
        }
    }

    // Update is called once per frame
    public void Die() 
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
