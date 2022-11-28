using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public float startingHealth;
    public float currentHealth { get; private set; }

    [Header("iFrames")]
    [SerializeField] private float IframesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;
    private AudioSource audioSourceHurt;
    //none yet
    private AudioSource audioSourceDead;

    private void Awake()
    {
        currentHealth = startingHealth;
      
    }
    private void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        audioSourceHurt = GetComponent<AudioSource>();
        audioSourceDead = GetComponent<AudioSource>();
    }
    public void Heal() 
    {
        currentHealth = currentHealth + 1;
    }
    public void TakeDamage(float _damage) 
    {
       
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0 && gameObject.tag.Equals("Player"))
        {
            //Player Hurt
            audioSourceHurt.Play();
            StartCoroutine(Invincibility());

        }
        else if (currentHealth>0&&gameObject.tag.Equals("Enemy")) 
        {
           // audioSourceHurt.Play();
        }
        else{
            //Player dead
            if (gameObject.tag.Equals("Player") && currentHealth <= 0)
            {
                GetComponent<Movement>().enabled = false;
         
                GetComponent<weapon>().enabled = false;
                //Later implement the awasome reboot and animation for death.
            }
            else if (gameObject.tag.Equals("Enemy"))
            {
                //Ranged Enemy Died
                if (GetComponentInParent<RangedEnemy>() != null)
                {
                    GetComponent<PatrolEnemy>().enabled = false;
                    GetComponent<RangedEnemy>().enabled = false;
                    GetComponent<Health>().enabled = false;

                    Destroy(gameObject);
                }
                //Boss Enemy Died
                if (GetComponent<Boss>() != null)
                {
                    GetComponent<Boss>().enabled = false;
                    Destroy(gameObject);
                }
            }
        }
    }
    private IEnumerator Invincibility() 
    {
        Physics2D.IgnoreLayerCollision(10,11,true);
        GetComponent<weapon>().enabled = false;
        //Stop the Player from shooting
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(IframesDuration/(numberOfFlashes*2));
            spriteRend.color = Color.red;
            yield return new WaitForSeconds(IframesDuration / (numberOfFlashes*2));

        }
        GetComponent<weapon>().enabled = true;
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }
}
