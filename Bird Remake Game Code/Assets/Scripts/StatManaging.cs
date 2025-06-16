using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatManaging : MonoBehaviour
{
    public bool gameStart = false;
    public int score = 0;
    public int maxScore;
    public float scoreIncRate;
    public GameObject character;
    public GameObject titleScreen;
    public GameObject startBox;
    public GameObject scoreScreen;
    public GameObject endScreen;
    public Text scoreText;
    public Text finalScoreText;
    public tileSpawner platformSpawner;

    private float timer = 0;
    private Vector3 startPosition;

    void Start()
    {
        gameStart = false;
        endScreen.SetActive(false);
        scoreScreen.SetActive(false);
        startPosition = character.transform.position;
    }

    void Update()
    {
        if (gameStart)
        {
            if (timer > scoreIncRate)
            {
                timer = 0;
                score += 1;
            }
            else
            {
                timer += Time.deltaTime;
            }
            scoreText.text = score.ToString();

            if (character.transform.position.y < -7.5)
            {
                EndGame();
            }
        }
    }

    public void StartGame()
    {
        titleScreen.SetActive(false);
        startBox.SetActive(false);
        endScreen.SetActive(false);
        scoreScreen.SetActive(true);
        gameStart = true;
        score = 0;
        platformSpawner.spawnTile(-1.5f);
        platformSpawner.spawnTile(6f);
    }

    public void EndGame() 
    {
        gameStart = false;
        startBox.SetActive(true);
        character.transform.position = startPosition;
        scoreScreen.SetActive(false);
        finalScoreText.text = "Score: " + score.ToString();
        endScreen.SetActive(true);
    }

    public void ResetGame() 
    {
        titleScreen.SetActive(true);
        endScreen.SetActive(false);
    }
    
}
