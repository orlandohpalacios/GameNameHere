using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private bool dead;

    [Header("iFrames")]
    [SerializeField]private float IframesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    private void Awake()
    {
        currentHealth = startingHealth;
        spriteRend = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(float _damage) 
    {
       
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        if (currentHealth>0)
        {
            //Player Hurt
            StartCoroutine(Invincibility());
        }
        else 
        {
            //Player dead
            

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
