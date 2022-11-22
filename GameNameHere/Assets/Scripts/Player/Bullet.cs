using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 20;
    public int damage;
    public Rigidbody2D rb;
    public float dieTime = 1.7f;
    private int powerUpCoolDown;
    void Start()
    {
        rb.velocity = transform.right * Speed;
        StartCoroutine(CountDownHelper());
        damage = 10;

    }
private void OnTriggerEnter2D(Collider2D hitInfo)
    {

        if (hitInfo != null)
        {
            if (hitInfo.gameObject.tag == "Enemy")
            {
                Destroy(gameObject);
                hitInfo.GetComponent<Health>().TakeDamage(damage);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    IEnumerator CountDownHelper()
    {
        yield return new WaitForSeconds(dieTime);
        Destroy(gameObject);
    }

}
