using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : BaseEnemy
{
    private Rigidbody2D rb;
    private int direction = 1;
    // Start is called before the first frame update
    public override void StartExtra()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public override void UpdateExtra()
    {
        rb.velocity = new Vector2(direction * 3, rb.velocity.y);
    }


    void OnCollisionStay2D(Collision2D other)
    {
        Debug.Log(other.gameObject.name);

        if (rb.velocity.x == 0)
        {
            direction *= -1;
        }

    }
}
