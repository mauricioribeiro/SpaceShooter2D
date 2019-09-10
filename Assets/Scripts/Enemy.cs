using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	[SerializeField]
	private float _speed = 4f;

	void Start () {
	
	}
	
	void Update () {
		transform.Translate(Vector3.down * _speed * Time.deltaTime, Space.World);

		if (transform.position.y <= -5f)
			transform.position = new Vector3(Random.Range(-10f, 10f), 7, 0);
	}

	private void OnTriggerEnter(Collider other) {

		switch(other.tag) {
			case "Player":
				Player player = other.transform.GetComponent<Player>();
				
				if (player != null)
					player.Damage();

				break;
			case "Laser":
				Destroy(other.gameObject);
				Debug.Log("exposion!");
				break;
		}

		Destroy(this.gameObject);
	}
}
