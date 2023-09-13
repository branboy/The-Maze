using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public static event Action OnCollected;
    public bool inRange = false;
    public CapsuleCollider trigger;
    public static int count;
    public TextMeshProUGUI collect;

    private void Awake()
    {
        collect.enabled = false;
        count =8;
    }
    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetKey(KeyCode.E))
        {
            OnCollected?.Invoke();
            Destroy(gameObject);
            collect.enabled = false;
        }
    }

    public void OnTriggerEnter(Collider trigger)
    {
        if (trigger.CompareTag("Player") && !trigger.isTrigger)
        {
            inRange = true;
            collect.enabled = true;
            
        }
    }


}
