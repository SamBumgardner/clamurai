using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    public float health;
    public float contactDamage;
    public float invulnTimeMax;

    private LayerMask playerHurtboxLayerMask;
    private float invulnTimeCurrent = 0f;
    private bool invuln = false;


    // Start is called before the first frame update
    void Start()
    {
        StartExtra();
        playerHurtboxLayerMask = LayerMask.GetMask("Default");
    }

    public virtual void StartExtra()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateExtra();
        if (!invuln)
        {
            //attempt to hurt player (check for overlap and collision
            var hitLeft = Physics2D.Raycast(transform.position + new Vector3(-0.5f, 0, 0), Vector2.down, -0.5f, playerHurtboxLayerMask);
            var hitRight = Physics2D.Raycast(transform.position - new Vector3(0.5f, 0, 0), Vector2.down, -0.5f, playerHurtboxLayerMask);
            if (hitLeft.collider != null)
            {
                Destroy(hitLeft.collider.gameObject);
            }

            if (hitRight.collider != null)
            {
                Destroy(hitRight.collider.gameObject);
            }
        }
        else
        {
            invulnTimeCurrent += Time.deltaTime;
            if (invulnTimeCurrent >= invulnTimeMax)
            {
                invuln = false;
            }
        }
    }

    public virtual void UpdateExtra()
    {

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
}
