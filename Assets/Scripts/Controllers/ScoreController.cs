using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

	public Text scoreLabel,highcoreLabel;
	public Text scoreText,highscoreText,replayButton;
	public AudioClip clickSound, scorecardSound;
	private AudioSource audioSource;

	
	// Use this for initialization
	void Start () {

		audioSource = GetComponent<AudioSource> ();
		audioSource.clip = scorecardSound;
		audioSource.Play ();
		

		int curscore = (int)PlayerController.score;
		int highScore = PlayerPrefs.GetInt("highscore");
		Debug.Log ("curscore " + curscore +  " highscore : " + highScore);
		if(highScore == 0 || (highScore != 0 && curscore > highScore))
		{
			highScore = curscore;
			PlayerPrefs.SetInt("highscore",(int)curscore);
			PlayerPrefs.Save();
			Debug.Log ("new highscore : " + PlayerPrefs.GetInt("highscore"));

		}

		scoreText.text = curscore.ToString();
		highscoreText.text = highScore.ToString();

		animateButtons();
	}
	
	void animateButtons(){
		LeanTween.scale( scoreLabel.gameObject, new Vector3 (1f, 1f, 1f), 0.45f).setEase(LeanTweenType.easeSpring).setOnComplete(highlightRePlayButton);
		LeanTween.scale( highcoreLabel.gameObject, new Vector3 (1f, 1f, 1f), 0.45f).setEase(LeanTweenType.easeSpring).setOnComplete(highlightRePlayButton);

		LeanTween.scale( scoreText.gameObject, new Vector3 (1f, 1f, 1f), 0.45f).setEase(LeanTweenType.easeSpring).setOnComplete(highlightRePlayButton);
		LeanTween.scale( highscoreText.gameObject, new Vector3 (1f, 1f, 1f), 0.45f).setEase(LeanTweenType.easeSpring).setOnComplete(highlightRePlayButton);

		LeanTween.scale( replayButton.gameObject, new Vector3 (1, 1, 1), 0.45f).setEase(LeanTweenType.easeSpring).setOnComplete(highlightRePlayButton);

		
	}
	
	void highlightRePlayButton(){
		LeanTween.scale( replayButton.gameObject, new Vector3 (0.98f, 0.98f, 0.98f), 0.3f).setEase(LeanTweenType.easeOutCirc).setLoopPingPong(-1);

	}
	
	// Update is called once per frame
	void Update () {
		if ((Input.GetButtonDown ("Fire1") || Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.KeypadEnter))) 
		{
			onReplay();
		}
	}
	
	public void onReplay(){
		Debug.Log("Replay Selected");

		audioSource.clip = clickSound;
		audioSource.Play ();

		LeanTween.pause (replayButton.gameObject);
		Vector3 curScale = replayButton.gameObject.transform.localScale;
		LeanTween.scale( replayButton.gameObject, new Vector3 (curScale.x - 0.02f , curScale.y - 0.02f, curScale.z - 0.2f), 0.25f).setEase(LeanTweenType.easeInOutCirc).setOnComplete(startGameScene);

	}
	
	void startGameScene(){
		PlayerController.isInfoActive = false;
		Application.LoadLevel (1);
	}

	void OnApplicationQuit() {
		PlayerPrefs.Save();
	}
}
