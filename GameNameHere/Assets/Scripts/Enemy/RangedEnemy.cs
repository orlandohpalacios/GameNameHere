using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{

    [SerializeField] private float Range;

    [Header("Collider parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] BoxCollider2D boxCollider;

    [Header("Ranged Attack")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletImage;
    [SerializeField] private float bulletSpeed;


    [SerializeField] LayerMask PlayerLayer;

    [SerializeField] private float attackCooldown;
    private float cooldownTimer = Mathf.Infinity;

    private PatrolEnemy EnemyPatrol;
  

    private void Awake()
    {
        EnemyPatrol = GetComponentInParent<PatrolEnemy>();
    }
    void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                //Create Bullet
                GameObject newBullet = Instantiate(bulletImage, firePoint.position, Quaternion.identity);
                newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed * EnemyPatrol.walkSpeed * Time.fixedDeltaTime, 0);
            }
        }

    }
   
    private bool PlayerInSight()
    {  
        
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * Range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x* Range , boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, PlayerLayer);

        return hit.collider != null ;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        //creating a hitbox to check player
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * Range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * Range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

}
