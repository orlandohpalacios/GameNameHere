using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }

    [Header("iFrames")]
    [SerializeField]private float IframesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;
    public AudioSource audioSourceHurt;
    public AudioSource audioSourceDead;

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
            Debug.Log(currentHealth);
            audioSourceHurt.Play();
        }
        else{
            //Player dead
            if(GetComponent<Movement>() !=null)
            GetComponent<Movement>().enabled = false;
            if (gameObject.tag.Equals("Enemy"))
            {
                //Enemy Died
                if (GetComponentInParent<PatrolEnemy>() != null)
                    GetComponentInParent<PatrolEnemy>().enabled = false;

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
        //
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(IframesDuration/(numberOfFlashes*2));
            spriteRend.color = Color.red;
            yield return new WaitForSeconds(IframesDuration / (numberOfFlashes*2));

        }
        Physics2D.IgnoreLayerCollision(10, 11, false);

    }
}
