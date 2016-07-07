using UnityEngine;
using System.Collections;

public class EnemyLookFoward : MonoBehaviour {

	public Transform _startSight, _endSight;
	private bool collision = false;
	public bool needsCollision = true;
	
	// Update is called once per frame
	void Update () {
		collision = Physics2D.Linecast (_startSight.position, _endSight.position, 1 << LayerMask.NameToLayer ("Solid"));

		if (collision == needsCollision) {
			transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
		}
	}
}
