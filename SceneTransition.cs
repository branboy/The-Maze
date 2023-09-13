using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public Animator transition;
    public float transitionTime = 1f;


    public void OnTriggerEnter2D(Collider2D trigger)
    {
        if(trigger.CompareTag("Player") && !trigger.isTrigger)
        {
            StartCoroutine(LoadLevel(sceneToLoad));
        }
    }

    IEnumerator LoadLevel(string NextScene)
    {
        SceneManager.LoadScene(NextScene);
        yield return null;
    }
}
