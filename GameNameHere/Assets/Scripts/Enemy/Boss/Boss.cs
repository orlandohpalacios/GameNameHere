using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private float Health;
    [SerializeField] private float Range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private float damage;
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] LayerMask PlayerLayer;
 
    [SerializeField]private float attackCooldown;
    private float cooldownTimer = Mathf.Infinity;

    //animation
    //private Animator anim;

    //Reference 
    private Health playerHealth;

    void Start()
    {
     //   anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (PlayerInSight()) 
        {
            if (cooldownTimer >= attackCooldown) 
            {
                cooldownTimer = 0;
                Debug.Log(cooldownTimer + ": I have attacked and time reset");
                DamagePlayer();
                //Attack
                //later with the animations will this be implemented
                //anim.SetTrigger("Melee Attack");

            }
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
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * Range * -transform.localScale.x*colliderDistance, 
            new Vector3(boxCollider.bounds.size.x * Range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
    private void DamagePlayer() 
    {
        if (PlayerInSight()) 
        {
            //damage health
            playerHealth.TakeDamage(damage);
        }
    }
}
