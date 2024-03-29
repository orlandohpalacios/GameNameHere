using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Boss Stats")]
    [SerializeField] private float Health;
    [SerializeField] private float Range;
    [SerializeField] private float damage;
    [SerializeField] private float upgradePoint;

    [Header("Collider parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] LayerMask PlayerLayer;

    [SerializeField] SpriteRenderer eyeTwo, eyeOne;
    [SerializeField]private float attackCooldown;
    private float cooldownTimer = Mathf.Infinity;
    public bool bossAlive;
    public bool PowerUpUsed;
    public bool coolDownHit;

    //Reference 
    private Health playerHealth;
    private Health EnemyHealth;
    private PatrolEnemy enemyIsPatroling;
    private Animator animate;


    private void Awake()
    {
        enemyIsPatroling = GetComponentInParent<PatrolEnemy>();

        bossAlive = true;
        PowerUpUsed = false;
        EnemyHealth = GetComponent<Health>();
        if (upgradePoint == 0.0f)upgradePoint = Health / 2;
        animate = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (EnemyHealth.currentHealth <= upgradePoint && PowerUpUsed == false)
        {
            PowerUpUsed = true;
            StartCoroutine(Powerup());
        }
        if (EnemyHealth.currentHealth == 10f||EnemyHealth.currentHealth ==0) 
        {
            bossAlive = false;
        }

        cooldownTimer += Time.deltaTime;
        
        if (PlayerInSight()) 
        {
          
            if (cooldownTimer >= attackCooldown) 
            {
                cooldownTimer = 0;
                DamagePlayer();
            }
        }
        if (enemyIsPatroling != null) 
        {
            enemyIsPatroling.enabled = !PlayerInSight();
        }
    }
    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center +transform.right*Range* -transform.localScale.x*colliderDistance,
            new Vector3(boxCollider.bounds.size.x*Range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left,0,PlayerLayer);
        if (hit.collider != null) 
        {
            playerHealth = hit.transform.GetComponent<Health>();

        }
        return hit.collider != null ; 
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        //creating a hitbox to see where the boss would hit
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * Range * -transform.localScale.x*colliderDistance, 
            new Vector3(boxCollider.bounds.size.x * Range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
    private void DamagePlayer() 
    {
        if (PlayerInSight() && !coolDownHit ) 
        {
            //damage health and let the player recover
            playerHealth.TakeDamage(damage);
            StartCoroutine(CoolDownHit());
        }
    }
    private IEnumerator Powerup() 
    {
        
        Physics2D.IgnoreLayerCollision(7,9, true);
        eyeOne.material.color = new Color(1, 0, 0);
        eyeTwo.material.color = new Color(1, 0, 0);
        //eye change and boost
        yield return new WaitForSeconds(1);
        //all stats up or doubled
        animate.speed = 2;
       enemyIsPatroling.walkSpeed = enemyIsPatroling.walkSpeed* 2;
        damage = damage* 2;
        Physics2D.IgnoreLayerCollision(7, 9, false);

    }
    IEnumerator CoolDownHit() 
    {
        coolDownHit =true;
        yield return new WaitForSeconds(3);
        coolDownHit = false;
    }
}
