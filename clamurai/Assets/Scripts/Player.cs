using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ITriggerOwner
{
    public const float RUN_SPEED = 10f;
    public const float JUMP_SPEED = 10f;
    public const float DIST_GROUND = .05f;
    public const float DIST_SIDE = .5f;
    public const float FALL_YSPEED_CUTOFF = 3f;

    private StateMachine<Player> stateMachine = new StateMachine<Player>();
    private List<State<Player>> states = new List<State<Player>>();
    private LayerMask terrainMask;

    private Animator animator;

    public Rigidbody2D rb;
    public bool on_ground = false;

    // Start is called before the first frame update
    void Start()
    {
        terrainMask = LayerMask.GetMask("Terrain");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        states.Add(new StandState(this, stateMachine));
        states.Add(new RunState(this, stateMachine));
        states.Add(new JumpState(this, stateMachine));
        states.Add(new FallState(this, stateMachine));
        stateMachine.Initialize(states[(int)PlayerStates.STAND]);
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
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
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
            Debug.Log($"{gameObject.name}: Ouch, I'm going to take {damage} damage");
        }
        else
        {
            Debug.Log($"{gameObject.name}: Ha ha! I hit them for {GetCurrentDamageInflicted()} damage!");
        }
    }

    public float GetCurrentDamageInflicted()
    {
        return 1;
    }
}
