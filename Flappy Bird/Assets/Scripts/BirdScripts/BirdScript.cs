using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BirdScript : MonoBehaviour {
	public static BirdScript instance;
	private Rigidbody2D rigidbody;
	private Animator animator;

	private float forwardSpeed=3f;
	private float bounceSpeed=4f;

	private bool didFlap;
	public bool isAlive;

	private Button flapBtn;
	void Awake(){
		if (instance == null) {
		
			instance = this;
			rigidbody=GetComponent<Rigidbody2D> ();
			animator = GetComponent<Animator> ();
		}
		isAlive = true;
		flapBtn = GameObject.FindGameObjectWithTag ("FlapBtn").GetComponent<Button>();

		//È la stessa cosa di aggiungere come onclick il metodo in questione
		flapBtn.onClick.AddListener (() => FlapTheBird ());


		setCameraX ();
	
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isAlive) {
		
			Vector3 temp = transform.position;
			temp.x += forwardSpeed * Time.deltaTime;
			transform.position = temp;

			if (didFlap) {
				didFlap = false;
				rigidbody.velocity = new Vector2 (0, bounceSpeed);
				animator.SetTrigger ("Flap");
			}

			if (rigidbody.velocity.y >= 0) {
				transform.rotation = Quaternion.Euler (0, 0, 0);
			} else {
				float angle = 0;
				angle = Mathf.Lerp (0,-90,-rigidbody.velocity.y/7);
				transform.rotation = Quaternion.Euler (0, 0, angle);
			}
		}

	}

	void setCameraX(){
		CameraScript.offsetX = (Camera.main.transform.position.x - transform.position.x - 1f);
	
	}

	public void FlapTheBird(){
		didFlap = true;
		Debug.Log ("Cliccato");
	
	}

	public float getPositionX(){
		return transform.position.x;
	}
}
