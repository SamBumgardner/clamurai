using UnityEngine;

public class AirState : State<Player>
{
	protected AirState(Player player, StateMachine<Player> stateMachine) : base(player, stateMachine) { }

    public override int HandleInput()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        owner.rb.velocity = new Vector2(Player.RUN_SPEED * horizontalAxis, owner.rb.velocity.y);

        if (horizontalAxis != 0.0f)
        {
            var newXScale = horizontalAxis > 0 ? 1 : -1;
            var scale = owner.transform.localScale;
            scale.x = newXScale;
            owner.transform.localScale = scale;
        }

        // Velocity check makes sure this doesn't glue player back to ground when starting jump
        // Value doesn't matter too much, just check its positive and somewhat larger than 0.
        if (owner.IsOnGround() && owner.rb.velocity.y <= 1) 
        {
            return (int)PlayerStates.STAND;
        }

        return base.HandleInput();
    }
}