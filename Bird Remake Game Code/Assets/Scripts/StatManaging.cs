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
    public Text scoreText;
    public GameObject character;
    public GameObject titleScreen;
    public GameObject startBox;
    public tileSpawner platformSpawner;
    public GameObject endScreen;

    private float timer = 0;
    private Vector3 startPosition;

    void Start()
    {
        gameStart = false;
        endScreen.SetActive(false);
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

            if (character.transform.position.y < -15)
            {
                gameStart = false;
                character.transform.position = startPosition;
                endScreen.SetActive(true);
                startBox.SetActive(true);
                titleScreen.SetActive(true);
            }
        }
    }

    public void StartGame()
    {
        titleScreen.SetActive(false);
        startBox.SetActive(false);
        endScreen.SetActive(false);
        gameStart = true;
        score = 0;
        platformSpawner.spawnTile(-1.5f);
        platformSpawner.spawnTile(6f);
    }
    
}
