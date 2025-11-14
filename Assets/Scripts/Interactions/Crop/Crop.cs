using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : ToolHit
{
    [SerializeField] GameObject harvestedDrop; 
    [SerializeField] int dropCount = 1;
    [SerializeField] float dropRadius = 0.3f;

    public override void Hit()
    {
        for (int i = 0; i < dropCount; i++)
        {
            Vector3 dropPos = transform.position;
            dropPos.x += Random.Range(-dropRadius, dropRadius);
            dropPos.y += Random.Range(-dropRadius, dropRadius);

            Instantiate(harvestedDrop, dropPos, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}