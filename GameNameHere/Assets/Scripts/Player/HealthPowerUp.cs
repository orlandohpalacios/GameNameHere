using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameObject player = collision.gameObject;
            Health playersHealth = player.GetComponent<Health>();
            if (playersHealth.currentHealth != playersHealth.startingHealth && playersHealth.currentHealth < playersHealth.startingHealth)
            {
                playersHealth.Heal();
                Destroy(gameObject);
            }
        }
    }
}
