using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform FirePoint;
    public GameObject bulletPrefab;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F)) 
        {
            Shoot();
        }  
    }
    
    public void PowerUp()
    {
        StartCoroutine(CountDownHelperPowerUp());
    }
    IEnumerator CountDownHelperPowerUp()
    {
        Bullet bll = bulletPrefab.GetComponent<Bullet>();
        int prevDam = bll.damage;

        bll.damage = bll.damage * 2;

        yield return new WaitForSeconds(5);
        bll.damage = prevDam;
    }
    void Shoot() 
    {
        //Shooting Logic;
        Instantiate(bulletPrefab, FirePoint.position, FirePoint.rotation);
    }
}
