using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapTestPlayer : MonoBehaviour
{
    const float MAX_RUN_SPEED = 10f;
    const float MAX_FALL_SPEED = 10f;
    
    private static float GRAVITY = .98f;
    private static float JUMP_FORCE = 15f;

    private Vector2 internalVelocity = new Vector2(0, 0);

    private bool jump_input = false;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jump_input = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        internalVelocity = new Vector2(MAX_RUN_SPEED * h, internalVelocity.y);


        if (jump_input)
        {
            print("attempted jump");
            if (internalVelocity.y < JUMP_FORCE)
            {
                internalVelocity.y = JUMP_FORCE;
            }
            jump_input = false;
        }

        internalVelocity.y -= GRAVITY;

        rb.velocity = internalVelocity;

    }
}
