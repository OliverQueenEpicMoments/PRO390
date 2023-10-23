using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour {
    [Header("Ability 1")]
    [SerializeField] Image AbilityImage1;
    [SerializeField] TMP_Text Ability1Text;
    [SerializeField] KeyCode Ability1Key;
    [SerializeField] float Ability1Cooldown;

    [SerializeField] Canvas Ability1Canvas;
    [SerializeField] Image Ability1Targeter;

    [Header("Ability 2")]
    [SerializeField] Image AbilityImage2;
    [SerializeField] TMP_Text Ability2Text;
    [SerializeField] int Ability2Button;
    [SerializeField] float Ability2Cooldown;

    [SerializeField] Canvas Ability2Canvas;
    [SerializeField] Image Ability2Targeter;

    [Header("Ability 3")]
    [SerializeField] Image AbilityImage3;
    [SerializeField] TMP_Text Ability3Text;
    [SerializeField] KeyCode Ability3Key;
    [SerializeField] float Ability3Cooldown;

    [SerializeField] Canvas Ability3Canvas;
    [SerializeField] Image Ability3Targeter;
    [SerializeField] float MaxAbility3Range;

    [Header("Ability 4")]
    [SerializeField] Image AbilityImage4;
    [SerializeField] TMP_Text Ability4Text;
    [SerializeField] KeyCode Ability4Key;
    [SerializeField] float Ability4Cooldown;

    [SerializeField] Canvas Ability4Canvas;
    [SerializeField] Image Ability4Targeter;

    private bool IsAbility1Cooldown = false;
    private bool IsAbility2Cooldown = false;
    private bool IsAbility3Cooldown = false;
    private bool IsAbility4Cooldown = false;

    private float CurrentAbility1Cooldown;
    private float CurrentAbility2Cooldown;
    private float CurrentAbility3Cooldown;
    private float CurrentAbility4Cooldown;

    private Vector3 MousePosition;

    void Start() {
        AbilityImage1.fillAmount = 0;
        AbilityImage2.fillAmount = 0;
        AbilityImage3.fillAmount = 0;
        AbilityImage4.fillAmount = 0;

        Ability1Text.text = "";
        Ability2Text.text = "";
        Ability3Text.text = "";
        Ability4Text.text = "";

        Ability1Targeter.enabled = false;
        //Ability2Targeter.enabled = false;
        Ability3Targeter.enabled = false;
        Ability4Targeter.enabled = false;

        Ability1Canvas.enabled = false;
        //Ability2Canvas.enabled = false;
        Ability3Canvas.enabled = false;
        Ability4Canvas.enabled = false;
    }

    void Update() {
        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MousePosition.z = 0;

        Ability1Input();
        Ability2Input();
        Ability3Input();
        Ability4Input();

        AbilityCooldown(ref CurrentAbility1Cooldown, Ability1Cooldown, ref IsAbility1Cooldown, AbilityImage1, Ability1Text);
        AbilityCooldown(ref CurrentAbility2Cooldown, Ability2Cooldown, ref IsAbility2Cooldown, AbilityImage2, Ability2Text);
        AbilityCooldown(ref CurrentAbility3Cooldown, Ability3Cooldown, ref IsAbility3Cooldown, AbilityImage3, Ability3Text);
        AbilityCooldown(ref CurrentAbility4Cooldown, Ability4Cooldown, ref IsAbility4Cooldown, AbilityImage4, Ability4Text);

        ShowAbility1Canvas();
        ShowAbility3Canvas();
        ShowAbility4Canvas();
    }

    private void ShowAbility1Canvas() { 
        if (Ability1Targeter.enabled) {
            Vector2 Direction = new(MousePosition.x - Ability4Canvas.transform.position.x, MousePosition.y - Ability4Canvas.transform.position.y);

            Ability1Canvas.transform.up = Direction;
        }
    }

    private void ShowAbility3Canvas() {
        var HitPosDir = (MousePosition - transform.position).normalized;
        float Distance = Vector3.Distance(MousePosition, transform.position);
        Distance = Mathf.Min(Distance, MaxAbility3Range);

        var NewHitPos = transform.position + HitPosDir * Distance;
        Ability3Canvas.transform.position = NewHitPos + (Vector3.down * 1.3f);
    }

    private void ShowAbility4Canvas() {
        if (Ability4Targeter.enabled) {
            Vector2 Direction = new(MousePosition.x - Ability4Canvas.transform.position.x, MousePosition.y - Ability4Canvas.transform.position.y);

            Ability4Canvas.transform.up = Direction;
        }
    }

    private void Ability1Input() {
        if (Input.GetKeyDown(Ability1Key) && !IsAbility1Cooldown) { 
            Ability1Canvas.enabled = true;
            Ability1Targeter.enabled = true;

            Ability3Canvas.enabled = false;
            Ability3Targeter.enabled = false;
            Ability4Canvas.enabled = false;
            Ability4Targeter.enabled = false;

            Cursor.visible = true;
        }

        if (Ability1Targeter.enabled && Input.GetMouseButtonDown(0)) {
            IsAbility1Cooldown = true;
            CurrentAbility1Cooldown = Ability1Cooldown;

            Ability1Canvas.enabled = false;
            Ability1Targeter.enabled = false;
        }
    }

    private void Ability2Input() {
        if (Input.GetMouseButtonDown(Ability2Button) && !IsAbility2Cooldown) {
            IsAbility2Cooldown = true;
            CurrentAbility2Cooldown = Ability2Cooldown;
        }
    }

    private void Ability3Input() {
        if (Input.GetKeyDown(Ability3Key) && !IsAbility3Cooldown) {
            Ability3Canvas.enabled = true;
            Ability3Targeter.enabled = true;

            Ability1Canvas.enabled = false;
            Ability1Targeter.enabled = false;
            Ability4Canvas.enabled = false;
            Ability4Targeter.enabled = false;

            Cursor.visible = false;
        }

        if (Ability3Canvas.enabled && Input.GetMouseButtonDown(0)) {
            IsAbility3Cooldown = true;
            CurrentAbility3Cooldown = Ability3Cooldown;

            Ability3Canvas.enabled = false;
            Ability3Targeter.enabled = false;

            Cursor.visible = true;
        }
    }

    private void Ability4Input() {
        if (Input.GetKeyDown(Ability4Key) && !IsAbility4Cooldown) {
            Ability4Canvas.enabled = true;
            Ability4Targeter.enabled = true;

            Ability1Canvas.enabled = false;
            Ability1Targeter.enabled = false;
            Ability3Canvas.enabled = false;
            Ability3Targeter.enabled = false;

            Cursor.visible = true;
        }

        if (Ability4Targeter.enabled && Input.GetMouseButtonDown(0)) {
            IsAbility4Cooldown = true;
            CurrentAbility4Cooldown = Ability4Cooldown;

            Ability4Canvas.enabled = false;
            Ability4Targeter.enabled = false;
        }
    }

    private void AbilityCooldown(ref float CurrentCooldown, float MaxCooldown, ref bool IsCooldown, Image AbilityImage, TMP_Text AbilityText) { 
        if (IsCooldown) {
            CurrentCooldown -= Time.deltaTime;

            if (CurrentCooldown <= 0) {
                IsCooldown = false;
                CurrentCooldown = 0;

                if (AbilityImage != null) AbilityImage.fillAmount = 0;
                if (AbilityText != null) AbilityText.text = "";
            } else {
                if (AbilityImage != null) AbilityImage.fillAmount = CurrentCooldown / MaxCooldown;
                if (AbilityText != null) AbilityText.text = Mathf.Ceil(CurrentCooldown).ToString();
            }
        }
    }
}