using System;
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

    [Space(20)]

    [Header("Настройки проверки столкновения/смерти")]
    [SerializeField] private Transform deadCheck1;
    [SerializeField] private Transform deadCheck2;
    [SerializeField] private float deadCheckLen = .5f;
    [SerializeField] private LayerMask deadLayer;

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
            animator.SetBool("isJumping", false);
        }
        IsDead();
    }

    // --- Основные функции ---
    // Функция проверяет нажатие клавиши, нахождение персонажа не в прыжке, и выполняет прыжок
    private void CheckJump() {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            if (!isJumping && IsGrounded())
            {   
                StartCoroutine(isJumpedDelay());
                rb.velocity = Vector2.up * jumpForce;
            }
        }
    }

    // Функция проверяет нажатие клавиши, нахождение персонажа не в кувырке,
    // запуская анимацию кувырка и корутину, по истечению которой, кувырок прекращается
    private void CheckRoll() {
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && !isRolling && !isJumping)
        {
            isRolling = true;
            animator.SetBool("isRolling", true);
            StartCoroutine(Roll());
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

    private IEnumerator isJumpedDelay() { // задержка для фикса зависания анимации прыжка
        yield return new WaitForSeconds(0.1f);
        isJumping = true;
        animator.SetBool("isJumping", true);
    }

    // функция, проверяющая, соприкосается ли персонаж со стеной или ловушкой
    private void IsDead() {
        RaycastHit2D hit1 = Physics2D.Raycast(deadCheck1.position, Vector2.right, deadCheckLen, deadLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(deadCheck2.position, Vector2.right, deadCheckLen, deadLayer);
        if (hit1.collider != null || hit2.collider != null) {
            isDead = true;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation; // оставим замороженным только поворот тела, чтобы сымитировать отталкивание влево
            animator.SetBool("isDead", isDead);
            rb.velocity = 2f * jumpForce * Vector2.left;
        }
    }
}
