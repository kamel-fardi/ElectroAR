using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class load : MonoBehaviour
{
    public GameObject ui;
    public string a;
    public void navto()
    {
        Debug.Log("aaaaaaaaa");
        // Unload the current scene asynchronously (if you're using additive scenes)
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.UnloadSceneAsync(currentScene);
        SceneManager.LoadScene(a);
        
    }
}
