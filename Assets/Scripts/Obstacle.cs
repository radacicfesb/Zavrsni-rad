using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour//trenutacno ova skripta nema svrhe
{
    
    int hitCounter = 0;

    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player")
        {
            hitCounter++;
            //if(hitCounter == 1)
            
           // if (hitCounter == 2)
               // playerMovement.Die();
        }
    }

    void Update()
    {
        
            //Debug.Log(hitCounter);
    }
}
