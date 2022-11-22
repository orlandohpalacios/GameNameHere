using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkboss : MonoBehaviour
{
    [SerializeField]BoxCollider2D wall;
    [SerializeField]Rigidbody2D player;
    [SerializeField] AudioSource bossAudio;
    [SerializeField] GameObject previousAudio;
    [SerializeField] GameObject bossCurrent;
    Boss enemyCheck;
    private void Start()
    {
        enemyCheck = bossCurrent.GetComponent<Boss>();
    }
    private void FixedUpdate()
    {
        if (!enemyCheck.bossAlive) 
        {

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
        previousAudio.GetComponent<AudioSource>().Stop();
        bossAudio.Play();
        bossAudio.loop = true;
        player.GetComponent<Movement>().enabled = false;
        player.position = new Vector2(player.position.x + 1, player.position.y);
        //wait until wall closed, than allow movement
        yield return new WaitForSeconds(1);
        wall.isTrigger = false;
        player.GetComponent<Movement>().enabled = true;
    }
    IEnumerator BossDefeated()
    {
        bossAudio.Stop();
        previousAudio.GetComponent<AudioSource>().Play();
        previousAudio.GetComponent<AudioSource>().loop = true;
        //wait until wall closed, than allow movement
        yield return new WaitForSeconds(1);
        wall.isTrigger = true;
        player.GetComponent<Movement>().enabled = true;
    }
}
