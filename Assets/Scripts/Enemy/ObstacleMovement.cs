using UnityEngine;
using System.Collections;

public class ObstacleMovement : MonoBehaviour {

	public float speed = 10f;
	private float speedtTimer = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		speedtTimer++;
		if (speedtTimer == 500) {
			speed += 1;
			speedtTimer = 0;
		}

		Vector3 pos = transform.position;
		pos.z -= speed * Time.deltaTime;
		transform.position = pos;
	}
}
