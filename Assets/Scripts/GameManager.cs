using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Variable Declaration 
    public int score = 0;
    public int lives = 3;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI LivesText;
    void NewGame()
    {
        lives = 3;
        score = 0;
    }


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        NewGame();
    }
    public void CompleteLevel()
    {
        score += 1000;
        Application.Quit();
    }
    public void FailLevel() 
    {
        lives--;
        if (lives <= 0)
        {
            NewGame();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;
        LivesText.text = "Lives: " + lives;
    }
}
