using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformScroller : MonoBehaviour
{
    public float moveSpd = 0.8f;
    public StatManaging stats;
    public float spdMultiplier = 1f;

    private float deadZone = -15f;

    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.FindGameObjectWithTag("StatManager").GetComponent<StatManaging>();
    }

    // Update is called once per frame
    void Update()
    {
        float scoreMultiplier = stats.score * 0.05f;
        transform.position += Vector3.left * moveSpd * Time.deltaTime * (1 + scoreMultiplier);
	if (transform.position.x < deadZone || !stats.gameStart)
        {
            Destroy(gameObject);
        }

    }
}
