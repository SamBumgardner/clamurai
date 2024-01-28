using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    public string attackAnimationName = "attack";

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Animator anim;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        sr.enabled = false;
        rb.simulated = false;
    }

    public void StartAttack()
    {
        rb.simulated = true;
        sr.enabled = true;
        anim.Play(attackAnimationName, 0, 0);
    }

    public void AttackFinished()
    {
        rb.simulated = false;
        sr.enabled = false;
    }
}