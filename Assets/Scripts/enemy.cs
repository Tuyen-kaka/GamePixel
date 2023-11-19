using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public Transform player; // Tham chiếu đến transform của người chơi
    public float attackRange = 1f; // Khoảng cách để quái vật tấn công
    public float moveRage = 3f;
    public float moveSpeed = 2f;
    private Animator animator;
    private Rigidbody2D rb;
    private bool isAttacking = false;
    private Vector3 initialPosition; // Biến để lưu vị trí ban đầu của quái vật

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        // Lưu vị trí ban đầu khi bắt đầu trò chơi
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // Tính toán hướng chạy của quái vật
            Vector2 direction = (player.position - transform.position).normalized;
            // Kiểm tra khoảng cách giữa quái vật và người chơi
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= moveRage)
            {
                // Di chuyển quái vật theo hướng của nhân vật
                rb.velocity = direction * moveSpeed;
                animator.SetTrigger("Walking");
            }
            else
            {
                transform.position = initialPosition;
                animator.SetTrigger("Idle");
            }
            // Nếu khoảng cách nhỏ hơn hoặc bằng khoảng cách tấn công, quái vật tấn công
            if (distanceToPlayer <= attackRange)
            {
                //AttackPlayer();

                isAttacking = true;
                // Chuyển sang animation tấn công.
                animator.SetTrigger("Attack");
            }
            else
            {
                // Nếu không, quái vật có thể thực hiện các hành động khác, ví dụ: di chuyển
                //MoveEnemy(); // Hàm di chuyển của quái vật
                isAttacking = false;
                animator.SetTrigger("Idle");
            }
        }
    }
    void AttackPlayer()
    {
        // Viết logic xử lý khi quái vật tấn công người chơi ở đây
        // Ví dụ: trừ điểm máu của người chơi, hiển thị animation tấn công, vv.
        StartCoroutine(AttackAnimation());
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
