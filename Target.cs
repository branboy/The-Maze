
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 1000f;
    public GameObject destroyed;
    public void TakeDamage (float amount)
    {
        health -= amount;
        if(health <= 0f)
        {
            Death();
        }
    }

    public void Death ()
    {
        Instantiate(destroyed, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
