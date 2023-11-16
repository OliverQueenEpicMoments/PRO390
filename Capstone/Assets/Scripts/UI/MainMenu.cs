using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    [SerializeField] private Animator animator;
    [SerializeField] private float TransitionTime;

    public void PlayGame() {
        StartCoroutine(LoadLevel());
    }

    public void QuitGame() { 
        Application.Quit();
    }

    IEnumerator LoadLevel() {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(TransitionTime);
        SceneManager.LoadSceneAsync(1);
    }
}