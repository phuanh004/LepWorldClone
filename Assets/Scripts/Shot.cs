using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour {
	private Rigidbody2D rb2d;
	public Transform player;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	IEnumerator OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.CompareTag("Enemy")){
			Destroy (other.gameObject);
			Destroy (this.gameObject);
		}
		yield return new WaitForSeconds (1f);
		Destroy (this.gameObject);
	}

//	IEnumerator DestroyBullet(){
////		yield return new WaitForSeconds (.1f);
////		Destroy (gameObject);
//	}
}
