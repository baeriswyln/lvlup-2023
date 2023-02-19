using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public Animator animator;
    public AudioSource audio;
    
    public void LoadScene(String scene)
    {
        StartCoroutine(LoadSceneWithTransition(scene));
    }

    IEnumerator LoadSceneWithTransition(String scene)
    {
        animator.SetTrigger("start");
        
        yield return new WaitForSeconds(0.5f);
        audio.Play(0);
        yield return new WaitForSeconds(1f);
        
        SceneManager.LoadScene(scene);
    }
}
