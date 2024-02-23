using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [Header("Получаем ссылки на все нужные объекты для состояния GameOver")]
    [SerializeField] private Player player;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private Animator fadeScreenAnimator;
    [SerializeField] private Animator gameOverTextAnimator;
    [SerializeField] private Animator restartTextAnimator;

    void Update()
    {
        OnPlayerDied();
    }

    private void OnPlayerDied() {
        if (player.isDead) {
            gameManager.enabled = false;
            scoreManager.enabled = false;
            
            // запускаем анимации показа сообщения "Игра окончена"
            fadeScreenAnimator.SetTrigger("FadeIn");
            gameOverTextAnimator.SetTrigger("ShowText");
            restartTextAnimator.SetTrigger("ShowText");

            if (Input.GetKeyDown(KeyCode.R))
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
