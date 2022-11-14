using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkboss : MonoBehaviour
{
    [SerializeField]BoxCollider2D wall;
    [SerializeField]Rigidbody2D player;


   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.rigidbody.tag == "Player")
        {
            Debug.Log("I am now entering the area");
            StartCoroutine(BossEntrance());
        }
    }

    IEnumerator BossEntrance() 
    {
        player.GetComponent<Movement>().enabled = false;
        wall.isTrigger = false;
        player.velocity = new Vector2(player.position.x+1, 0);
        yield return new WaitForSeconds(1);
        player.GetComponent<Movement>().enabled = true;
    }
}
