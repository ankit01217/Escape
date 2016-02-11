using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
	public float moveSpeed;
	public float rotationSpeed; //speed of turning

	private Transform target; //the enemy's target

	private Transform myTransform; //current transform data of this enemy


    void Awake ()
    {
		myTransform = transform;
    }

	void Start(){
		target = GameObject.FindWithTag("Player").transform; //target the player
		

	}

	void Update(){



		//rotate to look at the player
		myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed*Time.deltaTime);

		myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;

		/*
		Vector3 pos = transform.position;
		pos.z -= speed * Time.deltaTime;
		transform.position = pos;
	*/
	}
}
