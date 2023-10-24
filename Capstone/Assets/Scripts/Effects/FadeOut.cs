using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FadeOut : MonoBehaviour {
    [SerializeField] private bool Light = false;

    private SpriteRenderer Sprite;
    private Light2D CurrentLight;

    void Start() {
        Sprite = GetComponent<SpriteRenderer>();
        CurrentLight = GetComponent<Light2D>();

        if (Light) StartCoroutine(FadeOutLight());
        else StartCoroutine(FadeOutObject());
    }

    IEnumerator FadeOutObject() {
        for (float Transparency = 1; Transparency >= 0; Transparency -= 0.025f) {
            Color color = Sprite.material.color;
            color.a = Transparency;
            Sprite.material.color = color;
            yield return new WaitForSeconds(.05f);
        }
    }

    IEnumerator FadeOutLight() {
        for (float Transparency = 1; Transparency >= 0; Transparency -= 0.025f) {
            Color color = CurrentLight.color;
            color.a = Transparency;
            CurrentLight.color = color;
            yield return new WaitForSeconds(.05f);
        }
    }
}