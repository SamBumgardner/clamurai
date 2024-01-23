using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    public float health;
    public float contactDamage;
    public float invulnTimeMax;

    private LayerMask playerLayerMask; // gotta make this layer, then make sure the player's on it, then make sure this grabs it during startup
    private float invulnTimeCurrent = 0f;


    // Start is called before the first frame update
    void Start()
    {
        // set playerLayerMask value
    }

    // Update is called once per frame
    void Update()
    {
        // if not invuln:
        //  Check for overlap between hitbox & player hurtbox - if so, hurt player
        //  Expect that other folks will tell enemy if it got hurt.

        // if invuln:
        // increment invuln timer by delta
        // if invuln timer >= invulnTime, return to normal
    }

    private void LateUpdate()
    {
        if (health <= 0)
        {
            // enemy defeated, blow em up
        }

        // if transitioned between invuln / non-invuln, update visuals accordingly
    }
    
    public void DamagingCollision(float damage)
    {
        if (true) // if not invuln
        {
            //  if using state machine, hand it off to state
            // NOTE: in state, want to be consistent with "hurt" handling happening before or after normal update. Consistent rule can be to set variable that'll be used when picking next frame's transitional state.
            if (false)
            {

            } 
            else
            {
                // default behavior - or just have everything have a state to hand off to, that's probably better
                health -= damage;
                // become invuln, reset invuln timer
            }
        }

        // 
    }
    // Need to have a method for handling getting hit - should defer to state for behaviorif using state machine
}
