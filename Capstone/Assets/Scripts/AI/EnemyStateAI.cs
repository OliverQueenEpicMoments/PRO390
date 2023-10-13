using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyStateAI : MonoBehaviour {
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriterenderer;
    [SerializeField] Health EnemyHealth;
    [SerializeField] float Speed;
    [Header("AI")]
    [SerializeField] Transform PlayerLocation;
    [SerializeField] Transform RaycastLocation;
    [SerializeField] Transform[] Waypoints;
    [SerializeField] float RayDistance = 1;
    [SerializeField] string EnemyTag;
    [SerializeField] LayerMask RaycastLayerMask;
    [SerializeField] float EnemyDamage = 1;
    [SerializeField] float EnemyKnockback = 0;

    private bool AttackSwap = true;

    Rigidbody2D RB;
    Vector2 Velocity = Vector2.zero;
    bool FaceRight = true;
    float GroundAngle = 0;
    Transform TargetWaypoint = null;
    GameObject Enemy = null;

    enum State {
        IDLE,
        PATROL,
        CHASE,
        ATTACK,
        DEATH
    }

    State state = State.IDLE;
    float StateTimer = 1;

    void Start() {
        RB = GetComponent<Rigidbody2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
        EnemyHealth = GetComponent<Health>();
    }

    void Update() {
        // Update AI
        CheckEnemySeen();
        if (EnemyHealth.CurrentHealth <= 0) state = State.DEATH;

        Vector2 Direction = Vector2.zero;
        switch (state)
        {
            case State.IDLE:
                if (Enemy != null) state = State.CHASE;

                StateTimer -= Time.deltaTime;
                if (StateTimer <= 0)
                {
                    SetNewWaypointTarget();
                    state = State.PATROL;
                }
                break;
            case State.PATROL:
                {
                    if (Enemy != null) state = State.CHASE;

                    Direction.x = Mathf.Sign(TargetWaypoint.position.x - transform.position.x);
                    float DX = Mathf.Abs(TargetWaypoint.position.x - transform.position.x);
                    if (DX <= 0.25f)
                    {
                        state = State.IDLE;
                        StateTimer = 1;
                    }
                }
                break;
            case State.CHASE:
                {
                    if (Enemy == null)
                    {
                        state = State.IDLE;
                        StateTimer = 1;
                        break;
                    }

                    float DX = Mathf.Abs(Enemy.transform.position.x - transform.position.x);
                    if (DX <= 0.9f)
                    {
                        state = State.ATTACK;
                        if (AttackSwap)
                        {
                            //StartCoroutine(HitStun(AttackSwap));
                            break;
                        }
                        else if (AttackSwap == false)
                        {
                            //StartCoroutine(HitStun(AttackSwap));
                            break;
                        }
                    }
                    else
                    {
                        Direction.x = Mathf.Sign(Enemy.transform.position.x - transform.position.x);
                    }
                }
                break;
            case State.ATTACK:
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))
                {
                    state = State.CHASE;
                }
                break;
            case State.DEATH:
                StartCoroutine(Death());
                break;
            default:
                break;
        }

        // Transform direction to slope space
        Direction = Quaternion.AngleAxis(GroundAngle, Vector3.forward) * Direction;
        Debug.DrawRay(transform.position, Direction, Color.green);

        Velocity.x = Direction.x * Speed;

        // set velocity
        //if (Velocity.y < 0) Velocity.y = 0;
        //Velocity.y += Physics.gravity.y * GravityMultiplier * Time.deltaTime;

        // Move character
        RB.velocity = Velocity;

        // Rotate character to face direction of movement
        if (Velocity.x > 0 && !FaceRight) Flip();
        if (Velocity.x < 0 && FaceRight) Flip();

        // Update the animator
        animator.SetFloat("Speed", Mathf.Abs(Velocity.x));
    }

    private void Flip() {
        Vector3 CurrentScale = gameObject.transform.localScale;
        CurrentScale.x *= -1;
        gameObject.transform.localScale = CurrentScale;

        FaceRight = !FaceRight;
    }

    private void SetNewWaypointTarget() {
        Transform Waypoint = null;
        do {
            Waypoint = Waypoints[Random.Range(0, Waypoints.Length)];
        } while (Waypoint == TargetWaypoint);
        TargetWaypoint = Waypoint;
    }

    private void CheckEnemySeen() {
        Enemy = null;
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, ((FaceRight) ? Vector2.right : Vector2.left), RayDistance, RaycastLayerMask);
        if (raycastHit.collider != null && raycastHit.collider.gameObject.CompareTag(EnemyTag)) {
            Enemy = raycastHit.collider.gameObject;
            Debug.DrawRay(RaycastLocation.position, ((FaceRight) ? Vector2.right : Vector2.left) * RayDistance, Color.red);
        }
    }

    IEnumerator Death() {
        yield return new WaitForSeconds(0.75f);
        Destroy(gameObject);
    }
}