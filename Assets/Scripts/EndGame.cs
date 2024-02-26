using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [Header("Получаем ссылки на все нужные объекты для состояния GameOver")]
    [SerializeField] private Player player;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private new CinemachineVirtualCamera camera;
    [SerializeField] private Animator fadeScreenAnimator;
    [SerializeField] private Animator gameOverTextAnimator;
    [SerializeField] private Animator restartTextAnimator;

    void Update() {
        OnPlayerDied();
    }

    private void OnPlayerDied() {
        if (player.isDead) {
            gameManager.enabled = false;
            scoreManager.enabled = false;
            camera.enabled = false;
            player.enabled = false;
            GameObject []bgs = GameObject.FindGameObjectsWithTag("background");
            foreach (var bg in bgs) {
                bg.GetComponent<Parallax>().enabled = false;
            }

            // выключаем перемещение платформ
            Chunk[] chunks = FindObjectsOfType<Chunk>();
            foreach (Chunk chunk in chunks)
                chunk.enabled = false;
            
            // запускаем анимации показа сообщения "Игра окончена"
            fadeScreenAnimator.SetTrigger("FadeIn");
            gameOverTextAnimator.SetTrigger("ShowText");
            restartTextAnimator.SetTrigger("ShowText");

            if (Input.GetKeyDown(KeyCode.R))
                SceneManager.LoadScene("Game");
            if (Input.GetKeyDown(KeyCode.Escape))
                SceneManager.LoadScene("Main_Menu");
        }
    }
}
