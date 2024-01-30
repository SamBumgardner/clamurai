using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ITriggerOwner
{
    public const float RUN_SPEED = 10f;
    public const float JUMP_SPEED = 10f;
    public const float DIST_GROUND = 1.05f;
    public const float DIST_SIDE = .4f;
    public const float FALL_YSPEED_CUTOFF = 3f;
    public static Vector2 HURT_KNOCKBACK = new Vector2(1, 2);

    public float healthMax = 10;
    public float health;

    private StateMachine<Player> stateMachine = new StateMachine<Player>();
    private List<State<Player>> states = new List<State<Player>>();
    private LayerMask terrainMask;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    protected OverlapDetector hurtboxOverlapDetector;

    private AttackHandler standSlash;
    private AttackHandler airSlash;

    public Rigidbody2D rb;
    public bool on_ground = false;
    public string animationToPlay = null;

    private LastAttackType lastAttackType = LastAttackType.NONE;
    private float attackCooldownMax = 1f;
    private float attackCooldown = 0;

    public float invulnTimeMax = 2;
    protected float invulnTimeCurrent = 0f;
    protected bool invuln = false;
    public Vector2 hurtKnockback = Vector2.zero;
    public bool tookDamage = false;

    // Start is called before the first frame update
    void Start()
    {
        terrainMask = LayerMask.GetMask("Terrain");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        hurtboxOverlapDetector = GetComponentInChildren<OverlapDetector>();

        states.Add(new StandState(this, stateMachine));
        states.Add(new RunState(this, stateMachine));
        states.Add(new JumpState(this, stateMachine));
        states.Add(new FallState(this, stateMachine));
        states.Add(new HurtState(this, stateMachine));
        states.Add(new DyingState(this, stateMachine));
        stateMachine.Initialize(states[(int)PlayerStates.STAND]);

        var attackHandlers = gameObject.GetComponentsInChildren<AttackHandler>();
        standSlash = attackHandlers[0];
        airSlash = attackHandlers[1];

        health = healthMax;
    }

    private void applyInputAndTransitionStates()
    {
        int nextState;
        do
        {
            nextState = stateMachine.CurrentState.HandleInput();
            if (nextState != (int)States.NO_CHANGE)
            {
                stateMachine.ChangeState(states[(int)nextState]);
            }
        } while (nextState != (int)States.NO_CHANGE);
    }

    // Update is called once per frame
    void Update()
    {
        applyInputAndTransitionStates();
        stateMachine.CurrentState.LogicUpdate();
        AttemptAttack();
        if (invuln)
        {
            invulnTimeCurrent += Time.deltaTime;
            if (invulnTimeCurrent >= invulnTimeMax)
            {
                invuln = false;
                hurtboxOverlapDetector.EnableCollision();
            }
        }
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }

    private void LateUpdate()
    {
        if (animationToPlay != null)
        {
            animator.Play(animationToPlay);
            animationToPlay = null;
        }
        
        if (invuln && health > 0)
        {
            // dim color to grey if not already
            spriteRenderer.color = Color.gray;

        }
        else
        {
            // return color to normal
            spriteRenderer.color = Color.white;
        }
    }

    public void AttemptAttack()
    {
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            bool attackOnCooldown = attackCooldown > 0;

            if (stateMachine.CurrentState is GroundState
                && (lastAttackType != LastAttackType.STAND || !attackOnCooldown))
            {
                standSlash.StartAttack();
                lastAttackType = LastAttackType.STAND;
                attackCooldown = attackCooldownMax;
            }
            else if (stateMachine.CurrentState is AirState
                && (lastAttackType != LastAttackType.AIR || !attackOnCooldown))
            {
                airSlash.StartAttack();
                lastAttackType = LastAttackType.AIR;
                attackCooldown = attackCooldownMax;
            }
        }
    }

    public bool IsOnGround()
    {
        Debug.DrawRay(transform.position + new Vector3(DIST_SIDE, 0, 0), Vector2.down * DIST_GROUND, Color.green);
        Debug.DrawRay(transform.position - new Vector3(DIST_SIDE, 0, 0), Vector2.down * DIST_GROUND, Color.green);
        var hitLeft = Physics2D.Raycast(transform.position + new Vector3(DIST_SIDE, 0, 0), Vector2.down, DIST_GROUND, terrainMask);
        var hitRight = Physics2D.Raycast(transform.position - new Vector3(DIST_SIDE, 0, 0), Vector2.down, DIST_GROUND, terrainMask);
        return hitLeft.collider != null || hitRight.collider != null;
    }

    public void TriggerOverlapOccurred(TriggerBoxType myTriggerType, Collider2D other)
    {
        if (myTriggerType == TriggerBoxType.HURTBOX)
        {
            var otherOverlapDetector = other.GetComponent<OverlapDetector>();
            var damage = otherOverlapDetector.owner.GetCurrentDamageInflicted();
            var knockbackDirection = other.transform.position.x > transform.position.x ? -1 : 1;

            Hurt(damage, knockbackDirection);
        }
        else
        {
            // Trigger on-hit effects here
        }
    }

    public virtual void Hurt(float damage, float knockbackDirection)
    {
        if (!invuln)
        {
            health -= damage;
            tookDamage = true;
            invuln = true;
            hurtboxOverlapDetector.DisableCollision();
            invulnTimeCurrent = 0;

            hurtKnockback = new Vector2(HURT_KNOCKBACK.x * knockbackDirection, HURT_KNOCKBACK.y);
        }
    }

    public float GetCurrentDamageInflicted()
    {
        return 1;
    }

    enum LastAttackType
    {
        NONE,
        STAND,
        AIR,
    }
}
