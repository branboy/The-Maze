using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    public float delay = 4f;
    float countdown;
    bool hasExploded = false;
    public GameObject explosionEffect;
    public float radius = 5f;
    public float explosionForce = 200f;

    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;

        if(countdown <= 0 && hasExploded == false)
        {
            Explode();
            hasExploded = true;
        }
    }

    void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Collider[] collidersToDestroy = Physics.OverlapSphere(transform.position, radius);

        foreach(Collider nearbyObject in collidersToDestroy)
        {
           Target tar = nearbyObject.GetComponent<Target>();
           if(tar != null)
           {
               tar.Death();
           }
        }

         Collider[] collidersToMove = Physics.OverlapSphere(transform.position, radius);
         
         foreach(Collider nearbyObject in collidersToMove)
         {
             Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
           if(rb != null)
           {
               rb.AddExplosionForce(explosionForce, transform.position, radius);
           }
         }

        Destroy(gameObject);
    }
}

