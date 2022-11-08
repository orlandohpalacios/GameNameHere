using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 20;
    public int damage = 10;
    public Rigidbody2D rb;
    public float dieTime = 1.7f;

    void Start()
    {
        rb.velocity = transform.right * Speed;
        StartCoroutine(CountDownHelper());
    }
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        
            if (hitInfo.tag == "enemy")
            {
            hitInfo.GetComponent<Health>().TakeDamage(damage);
            }

            Debug.Log(hitInfo.name);
            Destroy(gameObject);
        
    }
    IEnumerator CountDownHelper()
    {
        yield return new WaitForSeconds(dieTime);
        Destroy(gameObject);
    }

}
