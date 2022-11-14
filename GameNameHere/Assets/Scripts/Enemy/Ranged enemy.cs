using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rangedenemy : MonoBehaviour
{
    [SerializeField] private float Health;
    [SerializeField] private float Range;
    [Header("Collider parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] BoxCollider2D boxCollider;

    [Header("Ranged Attack")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletImage;
    [SerializeField] private GameObject bulletSpeed;
    [SerializeField] private float damage;

    [SerializeField] LayerMask PlayerLayer;

    [SerializeField] private float attackCooldown;
    private float cooldownTimer = Mathf.Infinity;

    private Health playerHealth;
    private PatrolEnemy enemyIsPatroling;

    // Update is called once per frame

    void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;

                DamagePlayer();

                //Attack
                //later with the animations will this be implemented
                //anim.SetTrigger("Melee Attack");

            }
        }
    }
    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            //damage health
            if (PlayerLayer.Equals(false)) return;
            playerHealth.TakeDamage(damage);
        }
    }
    private bool PlayerInSight()
    {
        //almost done with this rework, just need to fix up enemy shooters, which are the basic enemies;
        return false;
    }
}
