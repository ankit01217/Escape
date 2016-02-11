using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	public float speed = 60f;
	public float jumpForce = 10f;
	public AudioClip hurtSound;
	public AudioClip clickSound;
	public AudioClip dieSound;
	public AudioClip jumpSound;
	public Slider healthSlider;
	public Text scoreText;
	public Image tintImage;
	public Image infoPanel;
	public float scoreSpeed = 2f;
	public float damageValue;
	public Text tapToContinue;
	
	private AudioSource audioSource;
	private Rigidbody rg;
	private Animator animator;
	private Vector3 movement;
	private float oriYPos;
	public static float score; 
	private EnemyManager enemyManager;
	private ScrollManager scrollManager;

	private bool isHurt = false;
	private bool isDead = false;
	public static bool isInfoActive = true;
	
	
	void Awake(){
		
		rg = GetComponent<Rigidbody> ();
		animator = GetComponent<Animator> ();
		enemyManager = GameObject.FindObjectOfType<EnemyManager> ();
		scrollManager = GameObject.FindObjectOfType<ScrollManager> ();
		audioSource = GetComponent<AudioSource>();
		
		score = 0.0f;
		healthSlider.value = 1;
		
		this.oriYPos = this.transform.position.y;

		Debug.Log ("isInfoActive "+ isInfoActive);
		if (isInfoActive) {

			enemyManager.enabled = false;
			//scrollManager.enabled = false;
			LeanTween.scale( tapToContinue.gameObject, new Vector3 (0.98f, 0.98f, 0.98f), 0.3f).setEase(LeanTweenType.easeOutCirc).setLoopPingPong(-1);

		} else {
			DestroyInfoPanel();
			animator.SetBool("IsWalking", true);

		}
		
		
	}
	
	void Update(){

		if (isDead) {
			tintImage.color = Color.Lerp (Color.clear, Color.red, Mathf.PingPong (Time.time * 2000f, 1.0f));
			Invoke("resetColor",0.1f);

			return;

		}
			
		if (isInfoActive) {
			if ((Input.GetButtonDown ("Fire1") || Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.KeypadEnter))) {
				audioSource.clip = clickSound;
				audioSource.Play ();
				scrollManager.enabled = true;
				enemyManager.enabled = true;

				animator.SetBool ("IsWalking", true);

				Invoke ("DestroyInfoPanel", 0.35f);
				
			}
		} else {
			Debug.Log ("scoreSpeed " + scoreSpeed + " score" + score);
			score += (scoreSpeed * Time.deltaTime);
			
			scoreText.text = "Score : "+ (int)score;
			float h = Input.GetAxisRaw ("Horizontal");
			float v = Input.GetAxisRaw("Vertical");
			
			Vector3 pos = transform.position;
			transform.position = new Vector3 (Mathf.Clamp (pos.x + h*Time.deltaTime * speed, -8, 7), pos.y, pos.z);
			if (transform.position.y > 8) {
				this.rg.velocity -= Vector3.up * jumpForce;
			}
			
			if ((Input.GetButtonDown ("Fire1") || Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.UpArrow)) && this.transform.position.y <= 2.4) {
				Jump();
			}
			
			if (isHurt == true) {
				tintImage.color = Color.Lerp (Color.clear, Color.red, Mathf.PingPong (Time.time * 2000f, 1.0f));
			}
		
		}



	}


	
	void DestroyInfoPanel(){
		isInfoActive = false;
		Destroy(infoPanel.gameObject);
	}
	
	void Jump(){
		
		if (isInfoActive) {
			return;
		}
		
		Debug.Log("Jump");
		audioSource.clip = jumpSound;
		audioSource.Play();
		

		Vector3 vel = this.rg.velocity;
		this.rg.velocity += Vector3.up * jumpForce;
		Invoke ("ApplyDownwardVelocity", 0.3f);


	}
	
	void ApplyDownwardVelocity(){
		this.rg.velocity -= Vector3.up * jumpForce * 1.2f;
		
		
	}
	
	
	
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Obstacle") {
			Debug.Log ("enemy :"+other.gameObject.name);
			
			healthSlider.value = Mathf.Clamp(healthSlider.value - damageValue,0.0f,1.0f);
			
			Invoke("resetColor",0.1f);
			if(isDead == false)
			{
				isHurt = true;
				other.rigidbody.isKinematic = true;
				
				audioSource.clip = hurtSound;
				audioSource.Play();
				
			}
			
			if(healthSlider.value == 0 && isDead == false) 
			{
				isDead = true;
				
				GameObject[] obstacles = (GameObject[])GameObject.FindGameObjectsWithTag("Obstacle");
				for (int i=0; i<obstacles.Length; i++) {
					GameObject obj = obstacles[i];
					ObstacleMovement st= obj.GetComponent<ObstacleMovement>();
					if(st != null)
					{
						st.enabled = false;
						
					}
					
					
				}
				
				ScrollManager sm = GameObject.FindObjectOfType<ScrollManager>();
				sm.enabled = false;
				rg.isKinematic = true;
				
				audioSource.clip = dieSound;
				audioSource.Play();

				animator.SetTrigger("Die");
				Invoke ("startScoreScreen", 3f);
				
			}
			
			
			
		}
		
	}
	
	void resetColor(){
		this.tintImage.color = Color.clear;
		isHurt = false;

		if (isDead) {
			this.tintImage.enabled = false;
		}
	}
	
	void startScoreScreen(){
		Application.LoadLevel (2);
		
	}
	
}
