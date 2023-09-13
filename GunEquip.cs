
using UnityEngine;

public class GunEquip : MonoBehaviour
{
    
    public int currentWeapon = 0;

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int previousWeapon = currentWeapon;

        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if(currentWeapon >= transform.childCount - 1)
            {
                currentWeapon = 0;
            }

            else
            {
             currentWeapon++;
            }
            
        }
         if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if(currentWeapon <= 0)
            {
                currentWeapon = transform.childCount - 1;
            }

            else
            {
             currentWeapon--;
            }
            
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = 0;
        }

        if(Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            currentWeapon = 1;
        }

        if(Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            currentWeapon = 2;
        }

        if(Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
        {
            currentWeapon = 3;
        }

        if(Input.GetKeyDown(KeyCode.Alpha5) && transform.childCount >= 5)
        {
            currentWeapon = 4;
        }

         if(Input.GetKeyDown(KeyCode.Alpha6) && transform.childCount >= 6)
        {
            currentWeapon = 5;
        }
         
         if(Input.GetKeyDown(KeyCode.Alpha7) && transform.childCount >= 7)
        {
            currentWeapon = 6;
        }

        if(previousWeapon != currentWeapon)
        {
            SelectWeapon();
        }

    }

    void SelectWeapon()
        {
            int i = 0;
            foreach (Transform weapon in transform)
            {
                if(i == currentWeapon)
                {
                    weapon.gameObject.SetActive(true);
                }
                else
                {
                    weapon.gameObject.SetActive(false);
                }
                i++;

            }
        }
}
