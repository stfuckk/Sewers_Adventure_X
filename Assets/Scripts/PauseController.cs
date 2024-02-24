using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public GameObject pausePanel;
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
        
        if (isPaused && Input.GetKeyDown(KeyCode.Q))
            SceneManager.LoadScene("Main_Menu");
    }

    private void TogglePause() {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1; // сокращаем if до ?
    }
}
