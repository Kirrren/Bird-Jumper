using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileSpawner : MonoBehaviour
{

	public GameObject tile1;
	public GameObject tile2;
	public GameObject tile3;
	public float spawnRateVar = 6;
	public float spawnHeightVar = 2;
	public float tileSpawnRate;
	public StatManaging stats;

	private float varSpawnRate;
	private float timer = 20;

    // Start is called before the first frame update
    void Start()
    {
		stats = GameObject.FindGameObjectWithTag("StatManager").GetComponent<StatManaging>();
    }

    // Update is called once per frame
    void Update()
    {
		float scoreMultiplier = stats.score * 0.02f;
		if (stats.gameStart)
		{
			if (timer >= varSpawnRate)
			{
				varSpawnRate = (tileSpawnRate + Random.Range(0, spawnRateVar)) / (1f + scoreMultiplier);
				timer = 0;
				spawnTile(transform.position.x);
			}
			else
			{
				timer += Time.deltaTime;
			}
		}
    }

    public void spawnTile (float x_position) {
	int tilePicker = Random.Range(0,5);
	float y_position = -5 + Random.Range(0,spawnHeightVar);
	if (tilePicker == 0) {
	   Instantiate(tile1, new Vector3(x_position, y_position, 0), transform.rotation);
	   return;
	}
	if (tilePicker == 1) {
	     Instantiate(tile2, new Vector3(x_position, y_position, 0), transform.rotation);
			return;
	}
	Instantiate(tile3, new Vector3(x_position, y_position, 0), transform.rotation);
   }
}
