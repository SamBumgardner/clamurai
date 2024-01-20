using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public const float RUN_SPEED = 10f;

    private StateMachine stateMachine = new StateMachine();
    private List<State> states = new List<State>();
    public Rigidbody2D rb;

    public bool on_ground = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        states.Add(new StandState(this, stateMachine));
        states.Add(new RunState(this, stateMachine));
        stateMachine.Initialize(states[(int)PlayerStates.STAND]);
    }

    private void applyInputAndTransitionStates()
    {
        PlayerStates nextState;
        do
        {
            nextState = stateMachine.CurrentState.HandleInput();
            if (nextState != PlayerStates.NO_CHANGE)
            {
                stateMachine.ChangeState(states[(int)nextState]);
            }
        } while (nextState != PlayerStates.NO_CHANGE);
    }

    // Update is called once per frame
    void Update()
    {
        

        applyInputAndTransitionStates();
        stateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {

        var new_isOnGround = IsOnGround();
        if (new_isOnGround != on_ground)
        {
            on_ground = new_isOnGround;
            print("woah, on_ground is now " + on_ground.ToString());
        }

        stateMachine.CurrentState.PhysicsUpdate();
    }

    public bool IsOnGround()
    {
        Debug.DrawRay(transform.position + new Vector3(.5f, 0, 0), Vector2.down * .55f, Color.green);
        Debug.DrawRay(transform.position - new Vector3(.5f, 0, 0), Vector2.down * .55f, Color.green);
        var hitLeft = Physics2D.Raycast(transform.position + new Vector3(.5f, 0, 0), Vector2.down, .55f, LayerMask.GetMask("Terrain"));
        var hitRight = Physics2D.Raycast(transform.position - new Vector3(.5f, 0, 0), Vector2.down, .55f, LayerMask.GetMask("Terrain"));
        return hitLeft.collider != null || hitRight.collider != null;
    }
}
