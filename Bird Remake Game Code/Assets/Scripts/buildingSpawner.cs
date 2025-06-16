using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingSpawner : MonoBehaviour
{

	public GameObject building;
	public float spawnRate = 10.2f;
	private float timer = 11f;

    // Start is called before the first frame update
    void Start()
    {
         setBackground(transform.position.x - 2 * spawnRate);
	 setBackground(transform.position.x - spawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= spawnRate) {
	   setBackground(transform.position.x);
	   timer = 0f;
	} else {
	   timer += 0.3f * Time.deltaTime; 
	}



     }

   void setBackground(float x_position) {
	Instantiate(building, new Vector3(x_position, transform.position.y, 0.0f), transform.rotation);
   }

}
