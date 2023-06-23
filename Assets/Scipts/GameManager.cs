using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI playerHPText;
    public TextMeshProUGUI enemyCounterText;
    public TextMeshProUGUI endGameInfoText;
    public GameObject endGameScreen;
    public bool IsGameEnd {get; private set;}
    int enemyAliveCounter;
    int score;
    // Start is called before the first frame update
    void Start()
    {
        enemyAliveCounter = GetComponent<InstatiateAll>().enemyCount;
        enemyCounterText.text = $"Enemys: {enemyAliveCounter}";
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsGameEnd) return;

        if (Input.anyKeyDown)
        {
            Restart();
        }
    }

    void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void EndGame(bool isWin)
    {
        Time.timeScale = 0;
        IsGameEnd = true;
        endGameScreen.SetActive(true);
        if (isWin)
            endGameInfoText.text = $"You win!\n\nScore: {score}";
        else
            endGameInfoText.text = $"You lose!\n\nScore: {score}";
    }

    public void EnemyDie(int addScore)
    {
        enemyAliveCounter--;
        enemyCounterText.text = $"Enemys: {enemyAliveCounter}";
        score += addScore;
        if (enemyAliveCounter == 0)
            EndGame(true);
    }

    public void SetPlayerHP(int value)
    {
        playerHPText.text = $"Lives: {value}";
    }
}
