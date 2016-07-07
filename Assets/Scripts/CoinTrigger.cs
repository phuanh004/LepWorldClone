using UnityEngine;
using System.Collections;

public class CoinTrigger : MonoBehaviour {
	public AudioSource audioS;
	public GameObject effect;

	// Use this for initialization
	void Start () {
		audioS = GetComponent<AudioSource> ();
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Player")) {
			audioS.Play ();
			Instantiate (effect, transform.position, transform.rotation);
		}
	}
}
