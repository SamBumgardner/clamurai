using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy<T> : MonoBehaviour, ITriggerOwner
{
    public const float DESPAWN_DIST = 15;
    public const float DIST_GROUND = .55f;
    public const float DIST_SIDE = .5f;

    public bool tookDamage = false; // Should trigger transition to hurt state
    public Vector2 standardKnockback = new Vector2(1, 2);
    public Vector2 knockbackToApply;

    public float health = 1;
    public float contactDamage;
    public float invulnTimeMax;

    public LayerMask terrainMask;
    protected LayerMask playerHurtboxLayerMask;
    protected StateMachine<T> stateMachine = new StateMachine<T>();
    protected List<State<T>> states = new List<State<T>>();

    protected float invulnTimeCurrent = 0f;
    protected bool invuln = false;

    public Rigidbody2D rb;
    public BoxCollider2D terrainCollider;
    public SpriteRenderer spriteRenderer;
    protected OverlapDetector[] overlapDetectors;

    private GameObject mainCameraRef;

    // Start is called before the first frame update
    void Start()
    {
        mainCameraRef = GameObject.FindWithTag("MainCamera");

        rb = GetComponent<Rigidbody2D>();
        terrainCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        overlapDetectors = GetComponentsInChildren<OverlapDetector>();

        terrainMask = LayerMask.GetMask("Terrain");
        playerHurtboxLayerMask = LayerMask.GetMask("PlayerHurtbox");

        stateMachine.Initialize(states[(int)States.DEFAULT]);
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

    public virtual void StartExtra()
    {

    }

    // Update is called once per frame
    void Update()
    {
        applyInputAndTransitionStates();
        stateMachine.CurrentState.LogicUpdate();

        if (!invuln)
        {
            // Attempt to hurt player?
        }
        else
        {
            invulnTimeCurrent += Time.deltaTime;
            if (invulnTimeCurrent >= invulnTimeMax)
            {
                invuln = false;
                foreach (var overlapDetector in overlapDetectors)
                {
                    overlapDetector.EnableCollision();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }

    private void LateUpdate()
    {
        if (health <= 0)
        {
            // enemy defeated, blow em up
            Defeat();
        }

        if ((transform.position - mainCameraRef.transform.position).magnitude > DESPAWN_DIST)
        {
            // Despawn due to distance. Probably don't want to handle the same as "dying" above,
            Destroy(gameObject);
        }

        if (invuln)
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

    public void DamagingCollision(float damage, float knockbackDirection)
    {
        if (!invuln) // if not invuln
        {
            //  if using state machine, hand it off to state
            // NOTE: in state, want to be consistent with "hurt" handling happening before or after normal update. Consistent rule can be to set variable that'll be used when picking next frame's transitional state.
            if (false)
            {

            }
            else
            {
                // default behavior - or just have everything have a state to hand off to, that's probably better
                Hurt(damage, knockbackDirection);
            }
        }
    }

    public virtual void Hurt(float damage, float knockbackDirection)
    {
        health -= damage;
        tookDamage = true;
        invuln = true;
        foreach (var overlapDetector in overlapDetectors)
        {
            overlapDetector.DisableCollision();
        }
        invulnTimeCurrent = 0;
        knockbackToApply = new Vector2(standardKnockback.x * knockbackDirection, standardKnockback.y);
    }

    public virtual void Defeat()
    {
        // stop state machine processing, activate defeated animation. Make sure have callback to perform cleanup when done.
        Destroy(gameObject);
    }

    public bool IsOnGround()
    {
        Debug.DrawRay(transform.position + new Vector3(DIST_SIDE * terrainCollider.size.x, 0, 0), Vector2.down * DIST_GROUND * terrainCollider.size.y, Color.green);
        Debug.DrawRay(transform.position + new Vector3(-DIST_SIDE * terrainCollider.size.x, 0, 0), Vector2.down * DIST_GROUND * terrainCollider.size.y, Color.green);
        var hitLeft = Physics2D.Raycast(transform.position + new Vector3(DIST_SIDE * terrainCollider.size.x, 0, 0), Vector2.down, DIST_GROUND * terrainCollider.size.y, terrainMask);
        var hitRight = Physics2D.Raycast(transform.position - new Vector3(DIST_SIDE * terrainCollider.size.x, 0, 0), Vector2.down, DIST_GROUND * terrainCollider.size.y, terrainMask);
        return hitLeft.collider != null || hitRight.collider != null;
    }

    public void TriggerOverlapOccurred(TriggerBoxType myTriggerType, Collider2D other)
    {
        if (myTriggerType == TriggerBoxType.HURTBOX)
        {
            var otherOverlapDetector = other.GetComponent<OverlapDetector>();
            var damage = otherOverlapDetector.owner.GetCurrentDamageInflicted();
            var knockbackDirection = other.transform.position.x > transform.position.x ? -1 : 1;

            DamagingCollision(damage, knockbackDirection);
        }
        else
        {
            Debug.Log($"{gameObject.name}: Ha ha! I hit them for {GetCurrentDamageInflicted()} damage!");
        }
    }

    public float GetCurrentDamageInflicted()
    {
        return contactDamage;
    }
}
