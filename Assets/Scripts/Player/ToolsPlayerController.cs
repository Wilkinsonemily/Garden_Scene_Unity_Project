using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ToolsPlayerController : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float interactRadius = 1.2f;
    [SerializeField] LayerMask interactableLayer;

    [Header("Input Settings")]
    [SerializeField] KeyCode interactKey = KeyCode.Mouse0; 

    private Rigidbody2D rb;
    private playerController player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<playerController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            UseTool();
        }
    }

    private void UseTool()
    {
        Vector2 toolPosition = rb.position + player.lastMotionVector * offsetDistance;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(toolPosition, interactRadius, interactableLayer);

        foreach (Collider2D c in colliders)
        {
            ToolHit toolHit = c.GetComponent<ToolHit>();
            if (toolHit != null)
            {
                toolHit.Hit(); 
                break;
            }
        }
    }

}
