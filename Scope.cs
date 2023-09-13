using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    public Animator animator;
    private bool isScoped = false;
    public GameObject scopeOverlay;
    public GameObject WeaponCamera;
    public Camera mainCam;
    public float scopedVision = 15f;
    private float normalVision;
   void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            isScoped = !isScoped;
            animator.SetBool("Scoping", isScoped);

            if(isScoped)
            {
                StartCoroutine(Scoped());
            }
            else
            {
                Unscoped();
            }
        }
    }

    void Unscoped()
    {
        scopeOverlay.SetActive(false);
        WeaponCamera.SetActive(true);

        mainCam.fieldOfView = normalVision;
    }

    IEnumerator Scoped()
    {
        yield return new WaitForSeconds(.15f);

        scopeOverlay.SetActive(true);
        WeaponCamera.SetActive(false);

        normalVision = mainCam.fieldOfView;
        mainCam.fieldOfView = scopedVision;
    }
}
