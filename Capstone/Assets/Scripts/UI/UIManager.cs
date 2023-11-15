using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    [SerializeField] private GameObject GameOverMenu;

    public void OnEnable() {
        Health.OnPlayerDeath += EnableGameOverMenu;
    }

    private void OnDisable() {
        Health.OnPlayerDeath -= EnableGameOverMenu;
    }

    public void EnableGameOverMenu() { 
        GameOverMenu.SetActive(true);
    }

    public void RestartLevel() {
        SceneManager.LoadScene(1);
    }

    public void MainMenu() {
        SceneManager.LoadScene(0);
    }
}