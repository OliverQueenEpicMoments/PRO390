using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager gameManager;

    [SerializeField] SubMenu TitleUI;
    [SerializeField] SubMenu GameWinUI;
    [SerializeField] SubMenu PauseUI;
    [SerializeField] SubMenu ControlsUI;

    public enum State {
        TITLE,
        START_GAME,
        PLAY_GAME,
        GAME_WIN,
        PAUSE_GAME,
    }

    public State state = State.START_GAME;
    float StateTimer = 0;
    public float Timer = 0;

	private void Awake() {
        if (gameManager != null && gameManager != this) {
            Destroy(this);
            return;
        } else {
            gameManager = this;
        }

        DontDestroyOnLoad(gameObject);
	}

	private void Start() {
        Timer = 0;
    }

    private void Update() {
        switch (state) { 
            case State.TITLE:
                TitleUI.MakeActive();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                //GameMusic.Stop();
                break;
            case State.START_GAME:
                Cursor.lockState = CursorLockMode.Locked;
                TitleUI.MakeInactive();
                ControlsUI.MakeInactive();
                //GameMusic.Play();
                state = State.PLAY_GAME;
                break;
            case State.PLAY_GAME:
                if (Input.GetKeyDown(KeyCode.Tab)) {
                    SetPause();
                }
                break;
            case State.GAME_WIN:
                Timer = 0;
                StateTimer -= Time.deltaTime;
                if (StateTimer <= 0) {
                    GameWinUI.MakeActive();
                    state = State.TITLE;
                }
                break;
            case State.PAUSE_GAME:
                //EventSystem.current.isFocused;
                break;
            default:
                break;
        }
    }

    public void StartGame() {
        state = State.START_GAME;
    }

    public void SetPause()  {
        state = State.PAUSE_GAME;

		PauseUI.MakeActive();
        ControlsUI.MakeActive();

		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
        Time.timeScale = Mathf.Epsilon;
	}

    public void ResumeGame(){
        state = State.PLAY_GAME;
        Time.timeScale = 1;
		Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
		PauseUI.MakeInactive();
        ControlsUI.MakeInactive();
	}

    public void QuitGame() {
        Application.Quit();
    }

    [Serializable]
    public struct SubMenu {
        public GameObject ParentObject;
        public GameObject DefaultSelect;
        public bool SetDefault;

        public void MakeActive() {
            if (ParentObject == null) return;

            ParentObject.SetActive(true);
            if (DefaultSelect != null && SetDefault)
                EventSystem.current.SetSelectedGameObject(DefaultSelect);
        }

        public void MakeInactive() {
			if (ParentObject == null) return;

			ParentObject.SetActive(false);
		}
    }
}