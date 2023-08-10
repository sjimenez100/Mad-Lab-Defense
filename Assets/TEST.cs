using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    public SpawnEntity entity;
    public float speed;
    private float lastSpeed;

    private void Start()
    {
        entity.Shove(speed, Vector3.right);
    }

    private void Update()
    {
        if(speed != lastSpeed)
        {
            entity.Shove(speed, Vector3.right);
        }

        if(Input.GetMouseButtonDown(0))
        {
            entity.transform.position = Vector3.zero;
        }
    }
}
