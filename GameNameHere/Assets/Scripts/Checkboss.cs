using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkboss : MonoBehaviour
{
    [SerializeField]BoxCollider2D wall;
    [SerializeField]BoxCollider2D wall2;
    [SerializeField]Rigidbody2D player;
    [SerializeField] AudioSource bossAudio;
    [SerializeField] AudioSource prevAudio;
    [SerializeField] GameObject bossCurrent;
    int updatestop=  0;
    Boss enemyCheck;
    private void Start()
    {
        enemyCheck = bossCurrent.GetComponent<Boss>();
    }
    private void FixedUpdate()
    {
        if (!enemyCheck.bossAlive && updatestop == 0) 
        {
            //to stop from repeated use, using a int to not let repeat
            updatestop = 1;
            StartCoroutine(BossDefeated());
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemyCheck.bossAlive) 
        {
            GameObject collisionGameObject = collision.gameObject;
            if (collisionGameObject.tag == "Player")
            {
                StartCoroutine(BossEntrance());
            }
        }

    }


    IEnumerator BossEntrance() 
    {
        //switch Audio
        prevAudio.Stop();
        prevAudio.loop = false;
        bossAudio.Play();
        bossAudio.loop = true;
        //Wall creation and Movement disabled
        player.GetComponent<Movement>().enabled = false;
        player.velocity = new Vector2(0f,0f);
        player.position = new Vector2(player.position.x + 3, player.position.y);
        yield return new WaitForSeconds(1);
        wall.isTrigger = false;
        wall2.isTrigger = false;
        player.GetComponent<Movement>().enabled = true;
    }
    IEnumerator BossDefeated()
    {
       
        bossAudio.Stop();
        prevAudio.Play();
        prevAudio.loop = true;
        //wait until wall closed, than allow movement
        player.GetComponent<Movement>().enabled = false;
        yield return new WaitForSeconds(1);
        wall.isTrigger = true;
        wall2.isTrigger = true;
        player.GetComponent<Movement>().enabled = true;
    }
}
