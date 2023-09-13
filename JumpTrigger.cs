using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JumpTrigger : MonoBehaviour
{
    float timeValue;
    public string gameOver;
    public string winner;
    public bool inRange = false;
    public CapsuleCollider trigger;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI keysText;
    int keyCount;

    private void Start()
    {
        UpdateCount();
        timeValue = Random.Range(200, 700);
    }
    void Update()
    {
        if(timeValue > 0)
        {
            timeValue -= Time.deltaTime;
            countText.text = timeValue.ToString(format:"0.00");
            if(keyCount == 8)
            {
                StartCoroutine(Win());
            }
            if(inRange)
            {
                StartCoroutine(Death());
            }
        }
        else
        {
            StartCoroutine(Death());
        }
    }

    private void OnEnable()
    {
        Collectable.OnCollected += Collected;
    }

    private void OnDisable()
    {
        Collectable.OnCollected -= Collected;
    }
    void Collected()
    {
        keyCount++;
        UpdateCount();
    }

    private void UpdateCount()
    {
        keysText.text = "Keys: " + keyCount.ToString() + "/" + Collectable.count;
    }

    IEnumerator Win()
    {
        yield return new WaitForSeconds(0.5f);
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(winner);
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(0.5f);
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(gameOver);
    }

    public void OnTriggerEnter(Collider trigger)
    {
        if(trigger.CompareTag("Player") && !trigger.isTrigger)
        {
            inRange = true;
        }
    }
}
