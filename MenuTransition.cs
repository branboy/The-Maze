using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTransition : MonoBehaviour
{

    public string sceneToLead;
    public void NextScene()
    {
        StartCoroutine(Loadscene());
    }
        
    IEnumerator Loadscene()
    {
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(sceneToLead);
    }

        
    
}
