using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Abilities : MonoBehaviour {
    [Header("Ability 1")]
    [SerializeField] private Image AbilityImage1;
    [SerializeField] private TMP_Text Ability1Text;
    [SerializeField] private KeyCode Ability1Key;
    [SerializeField] private float Ability1Cooldown;
    [SerializeField] private float Ability1Cost = 25;

    [SerializeField] private Canvas Ability1Canvas;
    [SerializeField] private Image Ability1Targeter;

    [SerializeField] private GameObject Projectile;
    [SerializeField] private float Force;

    [Header("Ability 2")]
    [SerializeField] private Image AbilityImage2;
    [SerializeField] private TMP_Text Ability2Text;
    [SerializeField] private int Ability2Button;
    [SerializeField] private float Ability2Cooldown;
    [SerializeField] private float Ability2Cost = 15;

    [SerializeField] private Canvas Ability2Canvas;
    [SerializeField] private Image Ability2Targeter;

    [SerializeField] private AudioClip EmpowerSound;
    public bool EmpoweredAuto = false;
    [SerializeField] private float EmpoweredDuration = 2;

    [Header("Ability 3")]
    [SerializeField] private Image AbilityImage3;
    [SerializeField] private TMP_Text Ability3Text;
    [SerializeField] private KeyCode Ability3Key;
    [SerializeField] private float Ability3Cooldown;
    [SerializeField] private float Ability3Cost = 30;

    [SerializeField] private Canvas Ability3Canvas;
    [SerializeField] private Image Ability3Targeter;
    [SerializeField] private float MaxAbility3Range;

    [SerializeField] private GameObject Eye;

    [Header("Ability 4")]
    [SerializeField] private Image AbilityImage4;
    [SerializeField] private TMP_Text Ability4Text;
    [SerializeField] private KeyCode Ability4Key;
    [SerializeField] private float Ability4Cooldown;
    [SerializeField] private float Ability4Cost = 40;

    [SerializeField] private Canvas Ability4Canvas;
    [SerializeField] private Image Ability4Targeter;

    [SerializeField] private AudioClip UltimateSound;
    [SerializeField] private GameObject UltField;

    private bool IsAbility1Cooldown = false;
    private bool IsAbility2Cooldown = false;
    private bool IsAbility3Cooldown = false;
    private bool IsAbility4Cooldown = false;

    private float CurrentAbility1Cooldown;
    private float CurrentAbility2Cooldown;
    private float CurrentAbility3Cooldown;
    private float CurrentAbility4Cooldown;

    private Vector3 MousePosition;
    private Mana ManaSystem;
    private Animator animator;

    void Start() {
        ManaSystem = GetComponent<Mana>();
        animator = GetComponent<Animator>();

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

        AbilityCooldown(Ability1Cooldown, Ability1Cost, ref CurrentAbility1Cooldown, ref IsAbility1Cooldown, AbilityImage1, Ability1Text);
        AbilityCooldown(Ability2Cooldown, Ability2Cost, ref CurrentAbility2Cooldown, ref IsAbility2Cooldown, AbilityImage2, Ability2Text);
        AbilityCooldown(Ability3Cooldown, Ability3Cost, ref CurrentAbility3Cooldown, ref IsAbility3Cooldown, AbilityImage3, Ability3Text);
        AbilityCooldown(Ability4Cooldown, Ability4Cost, ref CurrentAbility4Cooldown, ref IsAbility4Cooldown, AbilityImage4, Ability4Text);

        ShowAbility1Canvas();
        ShowAbility3Canvas();
        ShowAbility4Canvas();
    }

    private void ShowAbility1Canvas() { 
        if (Ability1Targeter.enabled) {
            Vector2 Direction = new(MousePosition.x - Ability4Canvas.transform.position.x, MousePosition.y - Ability4Canvas.transform.position.y);
            Direction.Normalize();

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
            Direction.Normalize();

            Ability4Canvas.transform.up = Direction;
        }
    }

    private void Ability1Input() {
        if (Input.GetKeyDown(Ability1Key) && !IsAbility1Cooldown && ManaSystem.CanAffordAbility(Ability1Cost)) { 
            Ability1Canvas.enabled = true;
            Ability1Targeter.enabled = true;

            Ability3Canvas.enabled = false;
            Ability3Targeter.enabled = false;
            Ability4Canvas.enabled = false;
            Ability4Targeter.enabled = false;

            Cursor.visible = true;
        }

        if (Ability1Targeter.enabled && Input.GetMouseButtonDown(0)) {
            if (ManaSystem.CanAffordAbility(Ability1Cost)) {
                ManaSystem.UseAbility(Ability1Cost);
                IsAbility1Cooldown = true;
                CurrentAbility1Cooldown = Ability1Cooldown;

                Ability1Canvas.enabled = false;
                Ability1Targeter.enabled = false;

                // Actual ability
                Vector2 Direction = (MousePosition - transform.position).normalized;

                GameObject Fireball = Instantiate(Projectile, transform.position, Quaternion.identity);
                Fireball.GetComponent<Rigidbody2D>().AddForce(Direction * Force, ForceMode2D.Impulse);

                animator.SetTrigger("Fireball");
                animator.SetFloat("MouseXPos", Direction.x);
                animator.SetFloat("MouseYPos", Direction.y);
            }   
        }
    }

    private void Ability2Input() {
        if (Input.GetMouseButtonDown(Ability2Button) && !IsAbility2Cooldown && ManaSystem.CanAffordAbility(Ability2Cost)) {
            if (ManaSystem.CanAffordAbility(Ability2Cost)) {
                ManaSystem.UseAbility(Ability2Cost);
                IsAbility2Cooldown = true;
                CurrentAbility2Cooldown = Ability2Cooldown;

                // Actual Ability
                SoundManager.Instance.PlaySound(EmpowerSound);
                StartCoroutine(Empower(EmpoweredDuration));
            }
        }
    }

    private void Ability3Input() {
        if (Input.GetKeyDown(Ability3Key) && !IsAbility3Cooldown && ManaSystem.CanAffordAbility(Ability3Cost)) {
            Ability3Canvas.enabled = true;
            Ability3Targeter.enabled = true;

            Ability1Canvas.enabled = false;
            Ability1Targeter.enabled = false;
            Ability4Canvas.enabled = false;
            Ability4Targeter.enabled = false;

            Cursor.visible = false;
        }

        if (Ability3Canvas.enabled && Input.GetMouseButtonDown(0)) {
            if (ManaSystem.CanAffordAbility(Ability3Cost)) {
                ManaSystem.UseAbility(Ability3Cost);
                IsAbility3Cooldown = true;
                CurrentAbility3Cooldown = Ability3Cooldown;

                Ability3Canvas.enabled = false;
                Ability3Targeter.enabled = false;

                Cursor.visible = true;

                // Actual ability
                Instantiate(Eye, Ability3Canvas.transform.position, Quaternion.identity);
            }
        }
    }

    private void Ability4Input() {
        if (Input.GetKeyDown(Ability4Key) && !IsAbility4Cooldown && ManaSystem.CanAffordAbility(Ability4Cost)) {
            Ability4Canvas.enabled = true;
            Ability4Targeter.enabled = true;

            Ability1Canvas.enabled = false;
            Ability1Targeter.enabled = false;
            Ability3Canvas.enabled = false;
            Ability3Targeter.enabled = false;

            Cursor.visible = true;
        }

        if (Ability4Targeter.enabled && Input.GetMouseButtonDown(0)) {
            if (ManaSystem.CanAffordAbility(Ability4Cost)) {
                ManaSystem.UseAbility(Ability4Cost);
                IsAbility4Cooldown = true;
                CurrentAbility4Cooldown = Ability4Cooldown;

                Ability4Canvas.enabled = false;
                Ability4Targeter.enabled = false;

                // Actual ultimate
                SoundManager.Instance.PlaySound(UltimateSound);
                Instantiate(UltField, Ability4Canvas.transform.position, Quaternion.identity);
            }
        }
    }

    private void AbilityCooldown(float AbilityCooldown, float AbilityCost, ref float CurrentCooldown, ref bool IsCooldown, Image AbilityImage, TMP_Text AbilityText) {
        if (IsCooldown) {
            CurrentCooldown -= Time.deltaTime;

            if (CurrentCooldown <= 0) {
                IsCooldown = false;
                CurrentCooldown = 0;
            }

            if (AbilityImage != null) {
                AbilityImage.color = Color.grey;
                AbilityImage.fillAmount = 1;
            }

            if (AbilityText != null) AbilityText.text = Mathf.Ceil(CurrentCooldown).ToString();
        } else {
            if (ManaSystem.CanAffordAbility(AbilityCost)) {
                if (AbilityImage != null) {
                    AbilityImage.color = Color.grey;
                    AbilityImage.fillAmount = 0;
                }

                if (AbilityText != null) AbilityText.text = " ";
            } else {
                if (AbilityImage != null) {
                    AbilityImage.color = Color.red;
                    AbilityImage.fillAmount = 1;
                }

                if (AbilityText != null) AbilityText.text = "X";
            }
        }
    }

    IEnumerator Empower(float Duration) {
        for (float Timer = Duration; Timer >= 0; Timer -= Time.deltaTime) {
            EmpoweredAuto = true;
            yield return new WaitForSeconds(.01f);
        }
        EmpoweredAuto = false;
        Debug.Log(EmpoweredAuto);
    }

    float AngleBetweenPoints(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}