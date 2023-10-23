using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FadeIn : MonoBehaviour {
    [SerializeField] private bool Light = false;

    private SpriteRenderer Sprite;
    private Light2D CurrentLight;

    void Start() {
        Sprite = GetComponent<SpriteRenderer>();
        CurrentLight = GetComponent<Light2D>();

        if (Light) StartCoroutine(FadeInLight());
        else StartCoroutine(FadeInObject());
    }

    IEnumerator FadeInObject() {
        for (float Transparency = 0; Transparency <= 1; Transparency += 0.05f) {
            Color color = Sprite.material.color;
            color.a = Transparency;
            Sprite.material.color = color;
            yield return new WaitForSeconds(.05f);
        }
    }

    IEnumerator FadeInLight() {
        for (float Transparency = 0; Transparency <= 1; Transparency += 0.05f) {
            Color color = CurrentLight.color;
            color.a = Transparency;
            CurrentLight.color = color;
            yield return new WaitForSeconds(.05f);
        }
    }
}