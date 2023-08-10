using System;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IKillable
{
    private Action deathAnimationComplete;

    [SerializeField]
    protected Animator animator;

    public virtual void Kill()
    {
        animator.SetTrigger("deathTrigger");
        deathAnimationComplete += Destroy;
    }

    public void OnDeathAnimationComplete()
    {
        deathAnimationComplete?.Invoke();
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        deathAnimationComplete -= Destroy;
    }

}
