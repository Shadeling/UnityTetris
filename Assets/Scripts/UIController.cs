using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject gameOverPopUp;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] TextMeshProUGUI scoreText;

    private int score = 0;

    private void Awake()
    {
        Time.timeScale = 1.0f;
    }

    public void GameOverMenu()
    {
        Time.timeScale = 0f;
        gameOverPopUp.SetActive(true);
        gameOverText.text = "Your score: " + score.ToString();
    }

    public void GameOverReverse()
    {
        score = 0;
        scoreText.text = score.ToString();
        Time.timeScale = 1f;
        gameOverPopUp.SetActive(false);
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ScoreAdd(int score)
    {
        this.score += score;
        scoreText.text = score.ToString();
    }
}
