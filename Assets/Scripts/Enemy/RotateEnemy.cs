using UnityEngine;
using System.Collections;

public class RotateEnemy : MonoBehaviour {

	public float rotationSpeed = 5f;
	private float rot;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.Rotate(Vector3.up * Time.deltaTime* rotationSpeed, Space.World);

	}
}
