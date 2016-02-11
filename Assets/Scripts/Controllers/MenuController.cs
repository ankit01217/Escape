using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	public Text titleText;
	public Text playButton;
	private AudioSource audioSource;
	private GameObject player;


	// Use this for initialization
	void Start () {

		audioSource = GetComponent<AudioSource> ();
		player = (GameObject)GameObject.FindGameObjectWithTag("Player");

		LeanTween.scale( titleText.gameObject, new Vector3 (1, 1, 1), 0.45f).setEase(LeanTweenType.easeSpring).setOnComplete(animatePlayButton);

	}

	void animatePlayButton(){
		//playButton.transform.scaleTo (0.45f, new Vector3 (1, 1, 1), true).setOnCompleteHandler(c => highlightPlayButton());

		LeanTween.scale( playButton.gameObject, new Vector3 (1, 1, 1), 0.45f).setEase(LeanTweenType.easeSpring).setOnComplete(highlightPlayButton);

	}

	void highlightPlayButton(){
		LeanTween.scale( playButton.gameObject, new Vector3 (0.98f, 0.98f, 0.98f), 0.3f).setEase(LeanTweenType.easeOutCirc).setLoopPingPong(-1);

	}

	
	// Update is called once per frame
	void Update () {
	
	}

	public void onClickPlay(){
		Debug.Log("Selected");

		audioSource.Play ();
		LeanTween.pause (playButton.gameObject);
		Vector3 curScale = playButton.gameObject.transform.localScale;
		LeanTween.scale( playButton.gameObject, new Vector3 (curScale.x - 0.02f , curScale.y - 0.02f, curScale.z - 0.2f), 0.25f).setEase(LeanTweenType.easeInOutCirc).setOnComplete(startGameScene);

	}

	void startGameScene(){

		Application.LoadLevel (1);
	}
}
