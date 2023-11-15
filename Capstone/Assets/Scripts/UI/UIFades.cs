using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFades : MonoBehaviour {
    [SerializeField] private CanvasGroup UIGroup;
    [SerializeField] private float FadeSpeed = 1;
    [SerializeField] private bool FadeIn;
    [SerializeField] private bool FadeOut;

    void Update() {
        if (FadeIn) {
            if (UIGroup.alpha < 1) {
                UIGroup.alpha += FadeSpeed;
                if (UIGroup.alpha >= 1) FadeIn = false;
            }
        } else if (FadeOut) {
            if (UIGroup.alpha >= 0) {
                UIGroup.alpha -= FadeSpeed;
                if (UIGroup.alpha == 0) FadeOut = false;
            }
        }
    }
}