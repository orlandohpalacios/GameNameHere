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
    void Shoot() 
    {
        //Shooting Logic;
        Instantiate(bulletPrefab, FirePoint.position, FirePoint.rotation);
    }
}
