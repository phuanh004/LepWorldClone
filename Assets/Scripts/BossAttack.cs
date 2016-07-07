using UnityEngine;
using System.Collections;

public class BossAttack : MonoBehaviour {

	public GameObject bullet;
	private Animator anim;
	private Rigidbody2D rb2d;

	private bool isJump = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();

		StartCoroutine (Shot ());
	}
	
	// Update is called once per frame
	void Update () {
		if (!isJump) {
			this.GetComponent<EnemyMove> ().enabled = false;
			this.GetComponent<EnemyLookFoward> ().enabled = false;
			this.GetComponent<EnemyLookFoward1> ().enabled = false;

			rb2d.AddForce (new Vector2 (rb2d.velocity.x, 50));
			StartCoroutine (BossFall());
			StartCoroutine (setJumpState (true));
		}else{
			this.GetComponent<EnemyMove> ().enabled = true;
			this.GetComponent<EnemyLookFoward> ().enabled = true;
			this.GetComponent<EnemyLookFoward1> ().enabled = true;

			StartCoroutine (setJumpState (false));
		}
//		Debug.Log (isJump);
	}

	IEnumerator Shot(){
		while (true) {
			yield return new WaitForSeconds (3);
			GameObject b = Instantiate (bullet, transform.position, Quaternion.identity) as GameObject;
			if (transform.localScale.x == 1) {
				b.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-7f, rb2d.velocity.y);
			} else {
				b.GetComponent<Rigidbody2D> ().velocity = new Vector2 (7f, rb2d.velocity.y);
			}
			Destroy (b, 0.7f);
		}
	}

	IEnumerator setJumpState(bool j){
		yield return new WaitForSeconds(2.5f);
		isJump = j;
	}
	IEnumerator BossFall(){
		yield return new WaitForSeconds(1.5f);
		rb2d.AddForce (new Vector2 (rb2d.velocity.x, -50));
	}
}
