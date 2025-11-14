using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class monsterAI : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float chaseRange = 5f;
    public float attackRange = 1.2f; 
    public float attackInterval = 1.0f;

    [Header("Recoil Settings")]
    public float recoilForce = 4f;

    private Rigidbody2D rb;
    private Animator animator;
    private Transform player;

    private bool isAttacking = false;
    private bool canMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (player.position.x < transform.position.x)
            transform.localScale = new Vector3(-1.4f, 1.4f, 1.4f);
        else
            transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);

        if (canMove && !isAttacking)
        {
            if (distance <= chaseRange && distance > attackRange)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                rb.velocity = direction * moveSpeed;
                animator.SetBool("isMoving", true);
            }
            else
            {
                rb.velocity = Vector2.zero;
                animator.SetBool("isMoving", false);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("isMoving", false);
        }
        if (distance <= attackRange && !isAttacking)
        {
            StartCoroutine(AttackPlayerLoop());
        }
    }

    private IEnumerator AttackPlayerLoop()
    {
        isAttacking = true;
        canMove = false;

        rb.velocity = Vector2.zero;
        animator.SetBool("isMoving", false);

        while (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            animator.ResetTrigger("Attack");
            animator.SetTrigger("Attack");

            yield return new WaitForSeconds(0.2f);
            ApplyDamageToPlayer(); 

            yield return new WaitForSeconds(attackInterval);
        }

        isAttacking = false;
        canMove = true;
        animator.ResetTrigger("Attack");
    }

    private void ApplyDamageToPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                Debug.Log("Player is hurt");
                DebugMessageUI.Instance.ShowMessage("Player is hurt");

                Rigidbody2D playerRb = hit.GetComponent<Rigidbody2D>();
                if (playerRb)
                {
                    Vector2 recoilDir = (hit.transform.position - transform.position).normalized;
                    playerRb.AddForce(recoilDir * recoilForce, ForceMode2D.Impulse);
                }

                Animator playerAnim = hit.GetComponent<Animator>();
                if (playerAnim)
                    playerAnim.SetTrigger("Hurt");
            }
        }
    }

}


