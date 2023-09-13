
using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour
{
   public float damage = 10f;
   public float range = 50f;
   public Camera fpsCam;
   public ParticleSystem muzzleFlash;
   public GameObject Impact;
   public float fireRate = 15f;
   private float nextFire = 0f;
   public int maxAmmo = 15;
   private int currentAmmo;
   public float impactForce = 25f;
   public float reloadTime = 3f;
   private bool isReloading = false;
   public Animator animator;
    // Update is called once per frame

    void Start()
    {
        currentAmmo = maxAmmo;
    }
    void Update()
    {

        if(isReloading)
        {
            return;
        }

        if(currentAmmo <= 0f)
        {
            StartCoroutine(Reload());
            return;
        }

        if(Input.GetButton("Fire1") && Time.time >= nextFire)
        {
            nextFire = Time.time + 1f/fireRate;
            Shoot();
        }

        IEnumerator Reload()
        {
            isReloading = true;
            Debug.Log("Reloading...");
            animator.SetBool("Reloading", true);
            
            yield return new WaitForSeconds(reloadTime - .25f);
            
            animator.SetBool("Reloading", false);
            yield return new WaitForSeconds(.25f);
            currentAmmo = maxAmmo;
            isReloading = false;
        }
        
        void Shoot()
        {
            currentAmmo--;

            muzzleFlash.Play();

            RaycastHit hit;
            if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);

                Target target = hit.transform.GetComponent<Target>();

                if(target != null)
                {
                    target.TakeDamage(damage);
                }

                Enemy enemy = hit.transform.GetComponent<Enemy>();

                if(enemy != null)
                {
                    enemy.TakeDamage(damage);
                }

                if(hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }

                GameObject bulletHole = Instantiate(Impact, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(bulletHole, 3f);

            }
        }
     }
}
