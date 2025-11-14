using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Goblin : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 1.2f;
    public float moveTime = 2f;
    public float idleTime = 2f;
    public float areaLimit = 6f; 

    private Vector2 startPos;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveDir;
    private bool isMoving;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        startPos = transform.position;

        StartCoroutine(RandomMovement());
    }

    void Update()
    {
        if (isMoving)
        {
            rb.MovePosition(rb.position + moveDir * moveSpeed * Time.deltaTime);
        }
    }

    private IEnumerator RandomMovement()
    {
        while (true)
        {
            isMoving = false;
            animator?.SetBool("isMoving", false);
            yield return new WaitForSeconds(Random.Range(idleTime * 0.5f, idleTime * 1.5f));

            isMoving = true;
            moveDir = Random.insideUnitCircle.normalized;
            animator?.SetBool("isMoving", true);

            Vector2 targetPos = startPos + moveDir * areaLimit;
            float timer = Random.Range(moveTime * 0.7f, moveTime * 1.2f);

            while (timer > 0)
            {
                timer -= Time.deltaTime;

                if (Vector2.Distance(transform.position, startPos) > areaLimit)
                {
                    moveDir = -moveDir;
                }

                rb.MovePosition(rb.position + moveDir * moveSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
