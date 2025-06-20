using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{

	[Header("Movement")]
	public Rigidbody2D playerRigidbody;
	public float jumpStr;
	public float moveSpd;
	public float downMultiplier = 2;

	[Header("Audio")]
	public AudioClip landingSound1;
	public AudioClip jumpSound;
	[SerializeField] float volume;

	[Header("Logic")]
	public bool onGround = true;
	public StatManaging stats;

	// for checking player inputs
	bool jumpKeyPressed;
	bool leftKeyPressed;
	bool rightKeyPressed;
	bool downKeyPressed;

	// Start is called before the first frame update
	void Start()
	{
		stats = GameObject.FindGameObjectWithTag("StatManager").GetComponent<StatManaging>();
		onGround = false;
	}

	// Update is called once per frame
	void Update()
	{
		getPlayerInput();
		if (stats.gameStart && !stats.gamePaused)
		{
			if (jumpKeyPressed && onGround)
			{
				AudioSource.PlayClipAtPoint(jumpSound, transform.position, volume);
				playerRigidbody.velocity = Vector2.up * jumpStr;
			}

			if (leftKeyPressed)
			{
				transform.position += Vector3.left * moveSpd * Time.deltaTime;
			}

			if (rightKeyPressed)
			{
				transform.position += Vector3.right * moveSpd * Time.deltaTime;
			}

			if (downKeyPressed && !onGround)
			{
				playerRigidbody.velocity = Vector2.down * downMultiplier;
			}
		}
	}

	public void getPlayerInput() 
	{
		jumpKeyPressed = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W);
		leftKeyPressed = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
		rightKeyPressed = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
		downKeyPressed = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);
	}

	private void OnTriggerEnter2D()
	{
		onGround = true;
		AudioSource.PlayClipAtPoint(landingSound1, transform.position, volume);
	}

	private void OnTriggerExit2D()
	{
		onGround = false;
	}

	private void OnTriggerStay2D()
	{
		if (stats.gameStart && !stats.gamePaused && !(rightKeyPressed))
		{
			float spdMultiplier = stats.score * 0.05f;
			transform.position += Vector3.left * 2f * Time.deltaTime * (1f + spdMultiplier);
		}
	}
}
