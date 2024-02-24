using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private float currentScore;
    private float highScore;
    public TextMeshProUGUI scoreText; // UI отображение очков
    public TextMeshProUGUI recordScoreText; // UI Отображение рекордных очков
    

    void Start()
    {   
        ResetScore();
        // Подгружаем рекорд из PlayerPrefs
        highScore = PlayerPrefs.GetFloat("HighScore", 0);    
        recordScoreText.text = "Рекорд: " + Math.Round(highScore).ToString();
    }

    void Update()
    {
        if (!player.GetComponent<Player>().isDead) {
            currentScore += 10 * Time.deltaTime; // увеличиваем очки на 10/сек
            if (currentScore > highScore) {
                highScore = currentScore;
                PlayerPrefs.SetFloat("HighScore", highScore);
            }
            UpdateScoreDisplay();
            Canvas.ForceUpdateCanvases();
        } else
            recordScoreText.text = "Рекорд: " + Math.Round(highScore).ToString();
    }

    private void ResetScore() {
        currentScore = 0;
    }

    private void UpdateScoreDisplay() {
        scoreText.text = Math.Round(currentScore).ToString();
    }

}
