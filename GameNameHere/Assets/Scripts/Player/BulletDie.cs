using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDie : MonoBehaviour
{
   public  float dieTime, Damage;

    void Start()
    {
        StartCoroutine(CountDown());   
    }

    // Update is called once per frame

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
    IEnumerator CountDown() 
    {
        yield return new WaitForSeconds(dieTime);
        Destroy(gameObject);
    }
}
