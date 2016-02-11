using UnityEngine;
using System.Collections;

public class PlayEnemySound : MonoBehaviour {

	private AudioSource audioSource;
	private Renderer renderer;
	private int timer = 0;
	private bool isSoundEnabled = true;
	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();	
		renderer = GetComponentInChildren<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		timer++;
		if (timer == 50 && isSoundEnabled == false) {
			Vector3 scale = this.transform.localScale;
			scale.y *= -1;
			this.transform.localScale = scale;
			timer = 0;
			isSoundEnabled = true;
		
		}
	}

	public void OnBeginMove(string objName){
		if (renderer.isVisible && isSoundEnabled) {
			Invoke("playSound",3f);
			isSoundEnabled = false;
		}

	}

	void playSound(){

		audioSource.Play();

	}
}
