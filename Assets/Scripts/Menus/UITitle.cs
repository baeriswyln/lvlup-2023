using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UITitle : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(Globals.Scenes.Menu);
    }

    public void About()
    {
        
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
