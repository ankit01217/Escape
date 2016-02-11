using UnityEngine;
using System.Collections;

public class ScrollManager : MonoBehaviour {

	public float scrollSpeed = 5f;
	public float maxZ = 120;
	private EnemyManager enemyManager;
	private float speedtTimer = 0f;

	void Start () {
		enemyManager = GameObject.FindObjectOfType<EnemyManager> ();

	}
	
	// Update is called once per frame
	void Update () {

		speedtTimer++;
		if (speedtTimer == 500) {
			scrollSpeed += 1;
			speedtTimer = 0;
		}


		GameObject[] floorObjects = GameObject.FindGameObjectsWithTag ("Floor");
		for (int i=0; i<floorObjects.Length; i++) {
			Vector3 pos = floorObjects[i].transform.position;
			pos.z -= scrollSpeed*Time.deltaTime;
			floorObjects[i].transform.position = new Vector3 (pos.x, 0.0f, pos.z);


		}
	}
	
	void OnTriggerEnter(Collider other) {
		Debug.Log ("OnTriggerEnter" + other.name);

		if (other.gameObject.tag == "Obstacle") {
			Destroy (other.gameObject);
			enemyManager.createRandomEnemy ();
			
		} else {
			var pos = other.gameObject.transform.position;
			pos.z = maxZ;
			other.gameObject.transform.position = pos;

		}


	}


}
