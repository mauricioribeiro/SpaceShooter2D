using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	[SerializeField]
	private float _speed = 3.5f;

	[SerializeField]
	private float _fireRate = 0.5f;

	[SerializeField]
	private GameObject _laser;

	private SpawnManager _spawnManager;

	[SerializeField]
	private int _lives = 3;

	private float _lastFire = -1f;

	void Start () {
		transform.position = new Vector3(0, 0, 0);
		_spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
	}
	
	void Update () {
		CalculateMovement();
		CheckFireLaser();
	}

	void CalculateMovement () {
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");

		transform.Translate(new Vector3(x, y, 0) * _speed * Time.deltaTime, Space.World);

		transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4, 6), 0);

		if (transform.position.x >= 11.5f || transform.position.x <= -11.5f)
			transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
	}

	void CheckFireLaser () {
		if (Input.GetKeyDown(KeyCode.Space) && Time.time > _lastFire) {
			_lastFire = Time.time + _fireRate;
			Instantiate(_laser, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
		}
	}

	public void Damage () {
		_lives--;
			
		if (_lives == 0) {
			if (_spawnManager != null)
				_spawnManager.StopSpawning();

			Destroy(this.gameObject);
		}
	}
}
