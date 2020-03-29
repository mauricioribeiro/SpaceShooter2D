using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	[SerializeField]
	private float _speed = 4f;

	private Player _player = null;

	private Animator _animator = null;

	void Start () {
		_player = GameObject.Find("Player").GetComponent<Player>();
		_animator = GetComponent<Animator>(); // don't have to find because animator is part of Enemy

		if(!_animator)
			Debug.LogError("Can't load enemy animator!");
	}
	
	void Update () {
		transform.Translate(Vector3.down * _speed * Time.deltaTime, Space.World);

		if (transform.position.y <= -5f)
			transform.position = new Vector3(Random.Range(-10f, 10f), 7, 0);
	}

	private void OnTriggerEnter2D(Collider2D other) {

		switch(other.tag) {
			case "Player":
				if (_player.gameObject)
					_player.Damage();

				_animator.SetTrigger("OnEnemyDeath");
				break;
			case "Laser":
				if (_player.gameObject)
					_player.AddScore(10);

				_animator.SetTrigger("OnEnemyDeath");
				Destroy(other.gameObject);
				break;
		}

		_speed = 0.0f;
		Destroy(this.gameObject, 2.8f);
	}
}
