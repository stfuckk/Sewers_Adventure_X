using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Настройки персонажа")]
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private float rollingTime = .5f;

    [Space(20)]

    [Header("Настройки проверки земли")]
    [SerializeField] private Transform groundCheck1;
    [SerializeField] private Transform groundCheck2;
    [SerializeField] private float groundCheckLen = .5f;
    [SerializeField] private LayerMask groundLayer;
    // Флаги
    private bool isRolling = false;
    private bool isJumping = false;
    public bool isDead = false;

    // Компоненты
    private Rigidbody2D rb;
    private Animator animator;
    

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    void Update() {
        CheckJump();
        CheckRoll();
        if (IsGrounded()) {
            isJumping = false;
            if (rb.velocity.y == 0)
                animator.SetBool("isJumping", false);
        }
    }

    // --- Основные функции ---
    // Функция проверяет нажатие клавиши, нахождение персонажа не в прыжке, и выполняет прыжок
    private void CheckJump() {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!isJumping && IsGrounded())
            {
                isJumping = true;
                rb.velocity = Vector2.up * jumpForce;
                animator.SetBool("isJumping", true);
            }
        }
    }

    // Функция проверяет нажатие клавиши, нахождение персонажа не в кувырке,
    // запуская анимацию кувырка и корутину, по истечению которой, кувырок прекращается
    private void CheckRoll() {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!isRolling && !isJumping)
            {
                isRolling = true;
                animator.SetBool("isRolling", true);
                StartCoroutine(Roll());
            }
        }
    }
    
    private IEnumerator Roll() {
        yield return new WaitForSeconds(rollingTime);
        animator.SetBool("isRolling", false);
        isRolling = false;
    }

    // функция, проверяющая, соприкасается ли персонаж с землей, чтобы обновить возможность прыжка
    private bool IsGrounded() {
        RaycastHit2D hit1 = Physics2D.Raycast(groundCheck1.position, Vector2.down, groundCheckLen, groundLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(groundCheck2.position, Vector2.down, groundCheckLen, groundLayer);
        if (hit1.collider != null || hit2.collider != null)
            return true;
        else return false;
    }
}
