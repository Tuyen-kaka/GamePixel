using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D rb;
    public Vector3 moveInput;
    private Animator animator;
    private bool isAttacking = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        transform.position += moveInput * moveSpeed * Time.deltaTime;

        if (moveInput.x != 0)
        {
            if (moveInput.x > 0)
            {
                transform.localScale = new Vector3((float)1.5, (float)1.5, 0);
            }
            else
            {
                transform.localScale = new Vector3((float)-1.5, (float)1.5, 0);
            }
        }

        animator.SetFloat("speed", moveInput.sqrMagnitude);

        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
        {
            StartCoroutine(AttackAnimation());
        }
    }
    IEnumerator AttackAnimation()
    {
        isAttacking = true;

        // Chuyển sang animation tấn công.
        animator.SetTrigger("Attack");

        // Đợi cho đến khi animation attack kết thúc.
        float attackAnimationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(attackAnimationLength);

        // Sau khi hoàn thành animation tấn công, chuyển về trạng thái idle.
        isAttacking = false;
        animator.SetTrigger("Idle");
    }
}
