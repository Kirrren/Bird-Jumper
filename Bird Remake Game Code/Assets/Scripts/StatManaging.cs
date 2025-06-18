using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatManaging : MonoBehaviour
{
    [Header("Score")]
    public int score = 0;
    public int maxScore;
    public float scoreIncRate;
    public Text scoreText;

    [Header("Screens")]
    public GameObject titleScreen;
    public GameObject scoreScreen;
    public GameObject endScreen;
    public GameObject pauseButton;
    public GameObject pauseScreen;
    public Text finalScoreText;

    [Header("Audio")]
    public AudioClip startButtonAudio;
    public AudioClip birdDeathAudio;
    public AudioClip endScreenAudio;
    public float volume = 1f;

    [Header("Game Assets")]
    public GameObject character;
    public Rigidbody2D playerRB;
    public GameObject startBox;
    public tileSpawner platformSpawner;

    [Header("Game Logic")]
    public playerScript playerLogic;
    public bool gameStart = false;
    public bool gamePaused = false;

    private float timer = 0;
    private Vector3 startPosition;

    void Start()
    {
        gameStart = false;
        endScreen.SetActive(false);
        scoreScreen.SetActive(false);
        pauseButton.SetActive(false);
        pauseScreen.SetActive(false);
        titleScreen.SetActive(true);
        startPosition = character.transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            PauseGame();
        }
        if (gameStart && !gamePaused)
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
        pauseScreen.SetActive(false);
        scoreScreen.SetActive(true);
        pauseButton.SetActive(true);
        gameStart = true;
        score = 0;
        platformSpawner.spawnTile(-1.5f);
        platformSpawner.spawnTile(6f);
        AudioSource.PlayClipAtPoint(startButtonAudio, new Vector3(0f, 0f, 0f), volume);
    }

    public void EndGame()
    {
        gameStart = false;
        startBox.SetActive(true);
        character.transform.position = startPosition;
        AudioSource.PlayClipAtPoint(birdDeathAudio, new Vector3(0f, 0f, 0f), volume);
        scoreScreen.SetActive(false);
        pauseButton.SetActive(false);
        finalScoreText.text = "Score: " + score.ToString();
        endScreen.SetActive(true);
    }

    public void ResetGame()
    {
        AudioSource.PlayClipAtPoint(endScreenAudio, new Vector3(0f, 0f, 0f), volume);
        titleScreen.SetActive(true);
        endScreen.SetActive(false);
    }

    public void PauseGame()
    {
        if (!gamePaused)
        {
            gamePaused = true;
            pauseButton.SetActive(false);
            pauseScreen.SetActive(true);
            playerRB.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            gamePaused = false;
            pauseScreen.SetActive(false);
            pauseButton.SetActive(true);
            playerRB.constraints = RigidbodyConstraints2D.None;
            playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
            if (!playerLogic.onGround) {
                character.transform.position += Vector3.down;
            }
        }
    }
    
}
