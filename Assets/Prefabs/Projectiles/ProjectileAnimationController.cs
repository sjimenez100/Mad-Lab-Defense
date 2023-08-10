using UnityEngine;

public class ProjectileAnimationController : MonoBehaviour
{
    [SerializeField]
    private Projectile projectile;

    public void BreakingComplete()
    {
        projectile.OnBreakingAnimationComplete();
    }

}
