using System;
using UnityEngine;

public class CollectObjective : MonoBehaviour, IObjective
{
    public event EventHandler<ObjectiveStatusChangeEventArgs> ObjectiveStatusChange;

    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2D;
    ParticleSystem particleSystemComponent;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        particleSystemComponent = GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var collectedStatusChange = new ObjectiveStatusChangeEventArgs {
            IsComplete = true,
            Progress = 1.0f,
        };
        //ObjectiveStatusChange(this, collectedStatusChange); // will fail if there are no subscribers
        spriteRenderer.enabled = false;
        boxCollider2D.enabled = false;
        particleSystemComponent.Stop();
        // play fun pickup noise
    }
}