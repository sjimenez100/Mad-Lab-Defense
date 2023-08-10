using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]

// all and only things that come out of the spawners
public abstract class SpawnEntity : Entity
{
    [SerializeField]
    protected SpriteRenderer sprite;

    protected Rigidbody2D rb;
    protected Collider2D collider;

    [SerializeField]
    private float speedMultiple;
    private IEnumerator movementRoutine;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    public override void Kill()
    {
        
        EventManager.main.OnSpawnEntityKill();

        // stop movement and disable collision
        StopCoroutine(movementRoutine);
        collider.enabled = false;
        base.Kill();

    }

    public virtual void Shove(float speed, Vector3 direction)
    {
        FlipSprite(direction);
        if(movementRoutine != null) 
            StopCoroutine(movementRoutine);
        movementRoutine = Move(speed, direction);
        StartCoroutine(movementRoutine);

        // ensures animation is in sync with movement speed
        animator.SetFloat("speedMultiplier", speedMultiple*speed);
    }

 

    private void FlipSprite(Vector3 direction)
    {
        direction.Normalize();

        if(sprite.flipX = direction == Vector3.up)
        {
            sprite.flipX = true;
            return;
        }
        

        float righwardsContrib = Vector3.Dot(direction, Vector3.right);
        float leftwardsContrib = Vector3.Dot(direction, Vector3.left);

        sprite.flipX = righwardsContrib < leftwardsContrib;

    }


    private IEnumerator Move(float speed, Vector3 direction)
    {
        while (true)
        {
            Vector3 newPosition = transform.position + speed *
                Time.fixedDeltaTime * direction;

                rb.MovePosition(newPosition);

            yield return new WaitForFixedUpdate();
        }

    }


    public virtual void OnDestroy()
    {
        EventManager.main.OnSpawnEntityDestroy();
    }
}
