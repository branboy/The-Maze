using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP { get; private set; }
    public Stats damage;
    bool isDead = false;
    bool takeDamage = false;

    void Start()
    {
        currentHP = maxHP;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T) && takeDamage == false);
        {
            takeDamage = true;
            TakeHit(10);
        }
    }

    public void TakeHit(int damage)
    {
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        
        currentHP -= damage;
        Debug.Log(transform.name + "takes" + damage + "damage");
        if(currentHP <= 0 && isDead == false)
        {
            isDead = true;
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + "died");
    }
}
