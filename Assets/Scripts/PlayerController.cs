using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour {

	private Rigidbody  rb ;
	public float speed;
	private int  count;
	public Text countText;
	public Text winText;

	void Start (){
		rb = GetComponent <Rigidbody> ();
		count = 0;
		setCountText();
		winText.text = "";
	}

	void Update (){
		/*Jump button */
		Vector3 jump = new Vector3 (0.0f, 10.0f, 0.0f); 
		Vector3 down = new Vector3 (0.0f, -10.0f, 0.0f);

		/*Jump touch android */
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
			rb.AddForce (jump * speed);	
		}
		else {
			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended){
				rb.AddForce (down * speed);	
			}
		}


	}


	void FixedUpdate(){

		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical	= Input.GetAxis ("Vertical");
		
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.AddForce (movement * speed);	
	

		/*Jump button */
		Vector3 jump = new Vector3 (0.0f, 10.0f, 0.0f); 
		Vector3 down = new Vector3 (0.0f, -10.0f, 0.0f);

		if (Input.GetKeyDown (KeyCode.Space)) {
			rb.AddForce (jump * speed);	
		}
		else {
			if (Input.GetKeyUp (KeyCode.Space)) {
				rb.AddForce (down * speed);	
			}
		}
	}


	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive(false);		
			count++;
			setCountText();
			Handheld.Vibrate();
		}			
	}


	void setCountText (){
		countText.text = "Count: " + count.ToString ();
		if (count >= 12) {
			winText.text = "You Win !";
		}
	}
}



/*
	void Update (){
		Vector3 dir = Vector3.zero;
		// we assume that the device is held parallel to the ground
		// and the Home button is in the right hand

		// remap the device acceleration axis to game coordinates:
		// 1) XY plane of the device is mapped onto XZ plane
		// 2) rotated 90 degrees around Y axis
		dir.x = -Input.acceleration.y;
		dir.z = Input.acceleration.x;

		// clamp acceleration vector to the unit sphere
		if (dir.sqrMagnitude > 1)
			dir.Normalize();

		// Make it move 10 meters per second instead of 10 meters per frame...
		dir *= Time.deltaTime;

		// Move object
		transform.Translate (dir * speed);
	}
	*/
