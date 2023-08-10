using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable : IKillable
{
    public float Health { get; set; }

    public void TakeDamage(float damage);
}
