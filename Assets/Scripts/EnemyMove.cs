using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {

	private Rigidbody2D _rigidbody2D;
	public float speed = 2.5f;
	public int direction = 1;
	// Use this for initialization
	void Start () {
		_rigidbody2D = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
		_rigidbody2D.velocity = new Vector2 (transform.localScale.x, 0) * speed * direction;
	}
}
