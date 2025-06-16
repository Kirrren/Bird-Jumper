using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundScroller : MonoBehaviour
{
	public float scrollSpd = 0.3f;
	public float deadZone = -20f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

	transform.position += (Vector3.left * scrollSpd) * Time.deltaTime;
	if (transform.position.x < deadZone) {
	  Destroy(gameObject);
	}
        
    }
}
