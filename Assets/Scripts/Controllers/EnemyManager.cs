using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	public GameObject[] enemies;
	public float spawnInterval = 3f;
	private float playerOffset;
	public int enemyCount = 10;
	private GameObject enemiesPT;
	private GameObject player;


	// Use this for initialization
	void Start () {
		enemiesPT = GameObject.FindGameObjectWithTag("EnemiesContainer");
		player = GameObject.FindGameObjectWithTag("Player");

		playerOffset = player.transform.position.z - enemiesPT.transform.position.z;
		//start coroutine
		StartCoroutine ("spawnEnemies");
	}

	IEnumerator spawnEnemies(){
		for (int i=0; i<enemyCount; i++) {
			createRandomEnemy();
			yield return new WaitForSeconds(spawnInterval);
		}
	}

	public void createRandomEnemy(){
		int randIndex = Random.Range(0,enemies.Length);
		var randEnemy = enemies[randIndex];

		Vector3 pos = randEnemy.transform.position;
		var newEnemyObj = (GameObject)Instantiate(randEnemy, new Vector3(Random.Range(-6,6),pos.y,enemiesPT.transform.position.z), randEnemy.transform.rotation);
		newEnemyObj.transform.parent = enemiesPT.transform;


	}
	
	// Update is called once per frame
	void Update () {

		Vector3 newPos = enemiesPT.transform.position;
		newPos.z = player.transform.position.z - playerOffset;
		enemiesPT.transform.position = newPos;
	}
}
