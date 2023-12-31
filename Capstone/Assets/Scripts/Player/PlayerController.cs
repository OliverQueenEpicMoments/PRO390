using System;
using System.Collections;
using UnityEngine;
using static Item;

public class PlayerController : MonoBehaviour, IShopCustomer {
    [SerializeField] private SpriteRenderer spriterenderer;
    [SerializeField] private UIInventory UIinventory;
    [SerializeField] private Animator animator;
    [SerializeField] private float Speed;

    [Header("Rolling")]
    private bool CanRoll = true;
    private bool IsRolling;
    private bool IsAttacking;
    private bool IsRooted;
    [SerializeField] private float RollingPower = 1.5f;
    [SerializeField] private float RollingTime = 0.8f;
    [SerializeField] private float RollingCooldown = 1.25f;
    [SerializeField] private AudioClip RollSound;
    [SerializeField] private AudioClip Footsteps;

    public static PlayerController Instance { get; private set; }

    public event EventHandler OnGoldAmountChanged;
    public event EventHandler OnEstusAmountChanged;

    private Rigidbody2D RB;
    private Health PlayerHealth;
    private Inventory PlayerInventory;
    private Vector2 Velocity = Vector2.zero;
    private Vector3 MousePosition;
    private int EstusFlasks = 3;
    private int GoldAmount = 9990;
    private bool DanceSwap = false;

    public enum Attacks {
        Primary,
        Secondary,
        Tertiary,
        Punch,
        Bash
    }

    private void Awake() {
        Instance = this;
    }

    void Start() {
        RB = GetComponent<Rigidbody2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
        PlayerHealth = GetComponent<Health>();

        PlayerInventory = new Inventory();
        UIinventory.SetInventory(PlayerInventory);
    }

    void Update() {
        if (IsRolling || IsAttacking) return;
        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector3 MouseDirection = (MousePosition - transform.position).normalized;

        Vector2 Direction = Vector2.zero;
        Direction.x = Input.GetAxis("Horizontal");
        Direction.y = Input.GetAxis("Vertical");
        Direction.Normalize();

        Velocity.x = Direction.x * Speed;
        Velocity.y = Direction.y * Speed;

        // Estus flask chug
        if (Input.GetKeyDown(KeyCode.Q)) {
            TryDrinkEstus();
        }

        if (IsRooted) return;

        // Rolling
        if (Input.GetKeyDown(KeyCode.LeftShift) && CanRoll) {
            animator.SetTrigger("Roll");
            SoundManager.Instance.PlaySound(RollSound);
            StartCoroutine(Roll());
        }

        // Dance
        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            if (!DanceSwap) {
                animator.SetTrigger("Dance");
                DanceSwap = true;
            } else {
                animator.SetTrigger("Dance2");
                DanceSwap = false;
            }
            //SoundManager.Instance.PlaySound(DanceMusic);
        }

        // Move player
        RB.velocity = Velocity;

        animator.SetFloat("Horizontal", Direction.x);
        animator.SetFloat("Vertical", Direction.y);
        animator.SetFloat("Speed", Direction.sqrMagnitude);

        //animator.SetFloat("MouseXPos", MouseDirection.x);
        //animator.SetFloat("MouseYPos", MouseDirection.y);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.TryGetComponent<ItemWorld>(out var worlditem)) {
            PlayerInventory.AddItem(worlditem.GetItem());
            Destroy(collision.gameObject);
        }
    }

    IEnumerator Roll() {
        IsRolling = true;
        PlayerHealth.Invulnerable = true;
        CanRoll = false;
        Physics2D.IgnoreLayerCollision(3, 6, true);
        Velocity = RB.velocity * RollingPower;
        yield return new WaitForSeconds(RollingTime);
        IsRolling = false;
        Physics2D.IgnoreLayerCollision(3, 6, false);
        yield return new WaitForSeconds(RollingCooldown);
        PlayerHealth.Invulnerable = false;
        CanRoll = true;
    }

    IEnumerator Root(float duration) {
        IsRooted = true;
        RB.velocity = Vector2.zero;
        animator.SetFloat("Speed", 0);
        yield return new WaitForSeconds(duration);
        IsRooted = false;
    }
    public void TryDrinkEstus() {
        if (EstusFlasks > 0) {
            EstusFlasks--;
            animator.SetTrigger("IsHealing");
            StartCoroutine(Root(2));
            PlayerHealth.MaxHealthHeal(35);
            OnEstusAmountChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public int GetEstusAmount() {
        return EstusFlasks;
    }

    private void AddEstusFlask() { 
        EstusFlasks++;
        OnEstusAmountChanged?.Invoke(this, EventArgs.Empty);
    }

    public void AddGoldAmount(int addgoldamount) {
        GoldAmount += addgoldamount;
        OnGoldAmountChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetGoldAmount() {
        return GoldAmount;
    }

    private void BuySpikedShield() {
        Instantiate(GameAssets.Instance.SpikedShieldStats.ItemPowerup, transform.position, Quaternion.identity);
    }

    private void BuyBruiserItem() {
        Instantiate(GameAssets.Instance.BruiserStats.ItemPowerup, transform.position, Quaternion.identity);
    }

    private void BuyBetrayersSword() {
        Instantiate(GameAssets.Instance.BetrayersSwordStats.ItemPowerup, transform.position, Quaternion.identity);
    }

    public void BoughtItem(ItemType itemtype) {
        SoundManager.Instance.PlaySound(GameAssets.Instance.ItemBought);

        switch (itemtype) {
            default:
            case ItemType.SpikedShield: BuySpikedShield(); break;
            case ItemType.Bruiser: BuyBruiserItem(); break;
            case ItemType.BetrayersSword: BuyBetrayersSword(); break;

            case ItemType.EstusFlask: AddEstusFlask(); break;
        }
    }

    public bool TrySpendGoldAmmount(int spentgoldammount) {
        if (GetGoldAmount() >= spentgoldammount) {
            GoldAmount -= spentgoldammount;
            OnGoldAmountChanged?.Invoke(this, EventArgs.Empty);
            return true;
        }
        else return false;
    }

    public void PlayAttackSound(Attacks attack) {
        switch (attack) {
            case Attacks.Primary: SoundManager.Instance.PlaySound(GameAssets.Instance.SwordStun); break;
            case Attacks.Secondary: SoundManager.Instance.PlaySound(GameAssets.Instance.SwordSlash); break;
            case Attacks.Tertiary: SoundManager.Instance.PlaySound(GameAssets.Instance.SwordSlash2); break;

            case Attacks.Punch: SoundManager.Instance.PlaySound(GameAssets.Instance.Punch); break;
            case Attacks.Bash: SoundManager.Instance.PlaySound(GameAssets.Instance.Bash); break;
        }
    }
}