using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Threading;
using System.Text.RegularExpressions;
using System;
using System.Linq;
using System.Collections.Generic;


public class PlayerController : MonoBehaviour {
	private bool time;
	public float speed;
	public int jumpHeight;

	private float scale = 1f;
	private bool isGrounded;

	private Rigidbody2D rb2d;
	private Animator anim;

	public Text score, bulletText;
	private AudioSource audioS;
	private int count, bulletCount, sceneName;
	private bool checkpoint = false;

	public GameObject bullet, coinEffect;

	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		audioS = GetComponent<AudioSource> ();

		sceneName = Int32.Parse(Regex.Replace(SceneManager.GetActiveScene ().name, "[^0-9]", ""));
		if (sceneName.Equals (1)) {
			bulletCount = 0;
			bulletText.text = bulletCount.ToString ();
			PlayerPrefs.SetInt ("bullet", 0);
		}
		try{
			bulletCount = PlayerPrefs.GetInt("bullet");
			bulletText.text = bulletCount.ToString ();
		}catch(Exception e){}

	}

	void Update(){
		Vector2 moveDir = new Vector2 (Input.GetAxisRaw ("Horizontal") * speed, rb2d.velocity.y);
		rb2d.velocity = moveDir;

		if (Input.GetAxisRaw ("Horizontal") == 1) {
			anim.SetInteger ("State", 1);
			transform.localScale = new Vector2 (scale, scale);
		}else if (Input.GetAxisRaw ("Horizontal") == -1) {
			anim.SetInteger ("State", 1);
			transform.localScale = new Vector2 (-scale, scale);
		}else if (Input.GetAxisRaw ("Horizontal") == 0) {
			anim.SetInteger ("State", 0);
		}
		if (Input.GetAxisRaw ("Vertical").Equals(1) && isGrounded) {
			Jump ();
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (bulletCount > 0) {
				if (transform.localScale.x == 1) {
					GameObject b = Instantiate (bullet, transform.position, Quaternion.Euler (new Vector3 (0, 0, -90))) as GameObject;
					b.GetComponent<Rigidbody2D> ().velocity = new Vector2 (10f, rb2d.velocity.y);
				} else {
					GameObject b = Instantiate (bullet, transform.position, Quaternion.Euler (new Vector3 (0, 0, -90))) as GameObject;
					b.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-10f, rb2d.velocity.y);
				}
				bulletCount -= 1;
				bulletText.text = bulletCount.ToString ();
			}
		}
	}
		
	public void Jump(){
		isGrounded = false;
		anim.Play( Animator.StringToHash( "Jump" ) );
		rb2d.AddForce (new Vector2 (rb2d.velocity.x, jumpHeight));
	}

	void OnCollisionEnter2D(Collision2D coll) {
        if(coll.gameObject.CompareTag("Tile")){
			isGrounded = true;
		}
		if(coll.gameObject.CompareTag("Enemy") || coll.gameObject.CompareTag("Boss")){
			anim.SetInteger ("State", 3);
			this.enabled = false;
			this.GetComponent<BoxCollider2D> ().isTrigger = true;
			this.GetComponent<CircleCollider2D> ().isTrigger = true;
		}
    }

	void OnCollisionStay2D(Collision2D coll) {
		if(coll.gameObject.CompareTag("Tile")){
			isGrounded = true;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Coin")) {
//			Debug.Log (other.gameObject.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName("Destroy"));
			other.gameObject.GetComponent<Animator> ().SetInteger ("State", 1);
			audioS.Play ();
			Instantiate (coinEffect, transform.position, transform.rotation);
			Destroy (other.gameObject);

//			Destroy (other.gameObject, .462f);
			if (other.gameObject.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("Flip")) {
				count += 1;
				score.text = count.ToString ();
			}
		}
		if (other.gameObject.CompareTag ("Enemy")) {
//			other.gameObject.SetActive (false);
			try{
				other.gameObject.GetComponent<Animator>().SetBool("Dead", true);
			}catch(Exception e){
			}
			Destroy(other.gameObject);
		}
		if (other.gameObject.CompareTag ("Bullet")) {
			bulletCount += 1;
			bulletText.text = bulletCount.ToString ();
			Destroy(other.gameObject);
		}
		if (other.gameObject.CompareTag ("Boss")) {
			anim.SetInteger ("State", 3);
			this.enabled = false;
			this.GetComponent<BoxCollider2D> ().isTrigger = true;
			this.GetComponent<CircleCollider2D> ().isTrigger = true;
		}
		if (other.gameObject.CompareTag ("Finish") && checkpoint) {
//			Debug.Log ( Regex.Replace(SceneManager.GetActiveScene ().name, "[^0-9]", ""));
			PlayerPrefs.SetInt ("bullet", bulletCount);
			SceneManager.LoadScene ("S" + (Int32.Parse(Regex.Replace(SceneManager.GetActiveScene ().name, "[^0-9]", ""))+1));
		}
		if (other.gameObject.CompareTag ("Flag")) {
			checkpoint = true;
			other.gameObject.GetComponent<Animator> ().SetInteger ("State", 1);
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.CompareTag ("Wall")) {
			PlayerPrefs.SetInt ("score", count);
			SceneManager.LoadScene ("Done");
		}
	}
}