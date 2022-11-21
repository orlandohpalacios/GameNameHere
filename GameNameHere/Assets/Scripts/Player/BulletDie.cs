using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDie : MonoBehaviour
{
   public  float dieTime, Damage;
    Health health;

    void Start()
    {
        StartCoroutine(CountDown());

    }

    // Update is called once per frame

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        if (collision.otherCollider.tag == "Player") 
        {
            health = collision.otherCollider.GetComponent<Health>();
            health.TakeDamage(Damage);
        }
    }
    IEnumerator CountDown() 
    {
        yield return new WaitForSeconds(dieTime);
        Destroy(gameObject);
    }
}
