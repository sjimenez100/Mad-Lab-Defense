using UnityEngine;

public class EntityAnimatonController : MonoBehaviour
{
    [SerializeField]
    private SpawnEntity entity;

    public void DeathAnimationComplete()
    {
        entity.OnDeathAnimationComplete();
    }
}
