using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour
{


    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.collider.isTrigger = false;
        Debug.Log(collision.collider.isTrigger);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.collider.isTrigger = true;
        Debug.Log(collision.collider.isTrigger);
    }
}
