using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nav : MonoBehaviour
{
    //public DataContainer dataContainer;
    // Start is called before the first frame update
    public void LoadScene()
    {
        
        SceneManager.LoadScene("ARScene");
    }
    public void BackToScene()
    {
        SceneManager.LoadScene("MAIN");
        
    }
    // You can also add a function to quit the application:
    public void QuitApplication()
    {
        Application.Quit();
    }
}
