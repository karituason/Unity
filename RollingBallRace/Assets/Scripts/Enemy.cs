using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
	public float speed;
	public Text winText;
	public Text enemyText;

	private Rigidbody rb;
	private GameObject[] pickups;
	private int count;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		pickups = GameObject.FindGameObjectsWithTag ("Pickup");
		count = 0;
		setCountText ();
		winText.text = "";
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		GameObject p = findClosest ();
		if (p != null) {
			float moveHorizontal = p.transform.position.x - this.gameObject.transform.position.x;
			float moveVertical = p.transform.position.z - this.gameObject.transform.position.z;
			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			rb.AddForce (movement * speed);
		} else if (count == 6) {
			winText.text = "It's a tie!";
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("Pickup")) {
			other.gameObject.SetActive (false);
		}
		count++;
		setCountText ();
	}

	GameObject findClosest(){
		GameObject min = null;
		float minDist = Mathf.Infinity;
		foreach (GameObject t in pickups) {
			if (t.activeSelf) {
				float dist = Vector3.Distance (t.transform.position, transform.position);
				if (dist < minDist) {
					min = t;
					minDist = dist;
				}
			}
		}
		return min;
	}

	void setCountText(){
		enemyText.text = "Enemy Count: " + count.ToString ();
		if (count >= 7) {
			winText.text = "You Lose!";
		}
	}
}
