using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent (typeof(Collider2D))]
[RequireComponent (typeof(Rigidbody2D))]

public class Projectile : MonoBehaviour, IColorable
{
    [SerializeField]
    private Animator animator;

    private Action breakingAnimationComplete;

    [InspectorName("Color")]
    public Color.ColorType setColor;
    public Color.ColorType color { get; set; }

    [SerializeField]
    private float speed;

    [SerializeField]
    private float despawnTime;

    private Rigidbody2D rb;
    private Collider2D colllider;

    private void Awake()
    {
        color = setColor;
    }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        colllider = GetComponent<Collider2D>();
    }


    private void Start()
    { 
        StartCoroutine(DespawnTimer(despawnTime));
        
    }

    public void Push(Vector3 direction)
    {
        GetComponent<Rigidbody2D>().velocity = direction * speed;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IKillable killable))
        {

            if (killable is IColorable colorable)
            {
                //Debug.Log($"In: {color} To: {colorable.color}");
                if (colorable.color == color)
                {
                    if (killable is ColorPowerUp colorPowerUp)
                        colorPowerUp.Execute();

                    killable.Kill();

                    Destroy(gameObject);
                    return; // <-- lmao funny line
                }

                AudioManager.main.PlaySound("BadHit");
                ConfigureBreakingAnimation();
                PreDestruction();
            }

         
        }
    }

    private void ConfigureBreakingAnimation()
    {
        animator.SetTrigger("breakApart");
        breakingAnimationComplete += () => Destroy(gameObject);
    }

    private void PreDestruction()
    {
        colllider.enabled = false;
        rb.velocity = Vector3.zero;
        
    }

    public void OnBreakingAnimationComplete()
    {
        breakingAnimationComplete?.Invoke();
    }

    private IEnumerator DespawnTimer(float despawnTime)
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        // don't need to unsubscribe since this is the publisher
    }

}
