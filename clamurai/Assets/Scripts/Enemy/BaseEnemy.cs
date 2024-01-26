using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TerrainUtils;

public abstract class BaseEnemy<T> : MonoBehaviour
{
    public const float DIST_GROUND = .55f;
    public const float DIST_SIDE = .5f;

    public float health;
    public float contactDamage;
    public float invulnTimeMax;

    protected LayerMask terrainMask;
    protected LayerMask playerHurtboxLayerMask;
    protected StateMachine<T> stateMachine = new StateMachine<T>();
    protected List<State<T>> states = new List<State<T>>();

    protected float invulnTimeCurrent = 0f;
    protected bool invuln = false;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

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
        } while (false);//nextState != (int)States.NO_CHANGE);
    }

    // Update is called once per frame
    void Update()
    {
        applyInputAndTransitionStates();
        stateMachine.CurrentState.LogicUpdate();

        if (invuln)
        {
            invulnTimeCurrent += Time.deltaTime;
            if (invulnTimeCurrent >= invulnTimeMax)
            {
                invuln = false;
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
        }

        if (invuln)
        {
            // dim color to grey if not already
        }
        else
        {
            // return color to normal
        }
    }
    
    public void DamagingCollision(float damage)
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
                Hurt(damage);
            }
        }
    }

    public virtual void Hurt(float damage)
    {
        health -= damage;
        invuln = true;
        invulnTimeCurrent = 0;
    }

    public virtual void Defeat()
    {
        // stop state machine processing, activate defeated animation. Make sure have callback to perform cleanup when done.
    }

    public bool IsOnGround()
    {
        Debug.DrawRay(transform.position + new Vector3(DIST_SIDE, 0, 0), Vector2.down * DIST_GROUND, Color.green);
        Debug.DrawRay(transform.position - new Vector3(DIST_SIDE, 0, 0), Vector2.down * DIST_GROUND, Color.green);
        var hitLeft = Physics2D.Raycast(transform.position + new Vector3(DIST_SIDE, 0, 0), Vector2.down, DIST_GROUND, terrainMask);
        var hitRight = Physics2D.Raycast(transform.position - new Vector3(DIST_SIDE, 0, 0), Vector2.down, DIST_GROUND, terrainMask);
        return hitLeft.collider != null || hitRight.collider != null;
    }
}
