using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public Animator animator;
    
    public void LoadScene(String scene)
    {
        StartCoroutine(LoadSceneWithTransition(scene));
    }

    IEnumerator LoadSceneWithTransition(String scene)
    {
        animator.SetTrigger("start");
        
        yield return new WaitForSeconds(1.4f);
        
        SceneManager.LoadScene(scene);
    }
}
