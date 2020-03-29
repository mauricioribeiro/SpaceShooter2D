using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	[SerializeField]
	private int _lives = 3;

	[SerializeField]
	private float _speed = 3.5f;

	[SerializeField]
	private int _score = 0;

	[SerializeField]
	private float _speedMultiplier = 2;

	[SerializeField]
	private float _fireRate = 0.5f;

	[SerializeField]
	private GameObject _laser = null;

	[SerializeField]
	private GameObject _tripleLaser = null;

	[SerializeField]
	private GameObject _shield = null;

	[SerializeField]
	private bool _isTripleLaserActive = false;

	[SerializeField]
	private bool _isShieldActive = false;

	[SerializeField]
	private bool _isSpeedBoostActive = false;

	private SpawnManager _spawnManager = null;

	private UIManager _uiManager = null;

	private float _lastFire = -1f;

	void Start () {
		transform.position = new Vector3(0, 0, 0);
		_spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
		_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

		if (!_uiManager)
			Debug.LogError("Can't find UI Manager!");
	}
	
	void Update () {
		CalculateMovement();

		if (Input.GetKeyDown(KeyCode.Space) && Time.time > _lastFire)
			FireLaser();
	}

	void CalculateMovement () {
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");

		transform.Translate(new Vector3(x, y, 0) * _speed * Time.deltaTime, Space.World);

		transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4, 6), 0);

		if (transform.position.x >= 11.5f || transform.position.x <= -11.5f)
			transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
	}

	void FireLaser () {
		_lastFire = Time.time + _fireRate;
		if (_isTripleLaserActive) {
			Instantiate(_tripleLaser, transform.position + new Vector3(-1.11f, 0.2f, 0), Quaternion.identity);
		} else {
			Instantiate(_laser, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
		}
	}

	public void Damage () {
		if (_isShieldActive) {
			DeactivateShieldPowerUp();
			return;
		}
		
		_lives--;
		_uiManager.UpdateLives(_lives);
			
		if (_lives == 0) {
			if (_spawnManager != null)
				_spawnManager.StopSpawning();

			Destroy(this.gameObject);
		}
	}

	public void AddScore (int points) {
		_score += points;
		_uiManager.UpdateScore(_score);
	}

	public void DeactivateShieldPowerUp () {
		_isShieldActive = false;
		if(_shield)
			_shield.SetActive(false);
	}

	public void ActiveTripleShotPowerUp () {
		_isTripleLaserActive = true;
		StartCoroutine(TripleShotDownRoutine());
		_uiManager.UpdateDiscordPowerUpActivity("Triple Shot");
	}
	public void ActiveSpeedPowerUp () {
		_isSpeedBoostActive = true;
		_speed *= _speedMultiplier;
		StartCoroutine(SpeedDownRoutine());
		_uiManager.UpdateDiscordPowerUpActivity("Speed");
	}

	public void ActiveShieldPowerUp () {
		_isShieldActive = true;
		if (_shield)
			_shield.SetActive(true);
		_uiManager.UpdateDiscordPowerUpActivity("Shield");
	}

	IEnumerator TripleShotDownRoutine () {
		yield return new WaitForSeconds(5);
		_isTripleLaserActive = false;
	}

	IEnumerator SpeedDownRoutine () {
		yield return new WaitForSeconds(5);
		_isSpeedBoostActive = false;
		_speed /= _speedMultiplier;
	}
}
