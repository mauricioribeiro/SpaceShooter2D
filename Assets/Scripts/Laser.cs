using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

	[SerializeField]
	private float _speed = 8f;
	
	void Update () {
		transform.Translate(Vector3.up * _speed * Time.deltaTime, Space.World);

		if (transform.position.y > 8f) {
			if (transform.parent)
				Destroy(transform.parent.gameObject);

			Destroy(this.gameObject);
		}
	}
	
}
