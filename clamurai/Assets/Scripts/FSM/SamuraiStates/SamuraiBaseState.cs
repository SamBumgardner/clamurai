using UnityEngine;

public class SamuraiBaseState : State<Samurai>
{
    public SamuraiBaseState(Samurai samurai, StateMachine<Samurai> stateMachine) : base(samurai, stateMachine) {}

    public override int HandleInput()
    {
        if (owner.tookDamage)
        {
            return (int)SamuraiStates.HURT;
        }

        return base.HandleInput();
    }

    public override void LogicUpdate()
    {
        UpdateFacing();

        base.LogicUpdate();
    }

    private void UpdateFacing()
    {
        var scale = owner.transform.localScale;
        if ((scale.x < 0 && owner.directionX > 0) || scale.x > 0 && owner.directionX < 0)
        {
            scale.x = -scale.x;
        }
        owner.transform.localScale = scale;
    }
}