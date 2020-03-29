using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {

	[SerializeField]
	private GameObject _enemy = null;

	[SerializeField]
	private GameObject[] _powerUps = null;

	[SerializeField]
	private GameObject _container = null;

	private bool _stopSpawning = false;

	void Start () {
		StartCoroutine(SpawnEnemyRoutine());
		StartCoroutine(SpawnTripleLaserPowerUpRoutine());
	}

	GameObject CreateInRandomPosition (GameObject gameObject) {
		return Instantiate(gameObject, new Vector3(Random.Range(-10f, 10f), 7, 0), Quaternion.identity) as GameObject;
	}

	IEnumerator SpawnEnemyRoutine () {
		while (!_stopSpawning) {
			GameObject newEnemy = CreateInRandomPosition(_enemy);
			newEnemy.transform.parent = _container.transform;
			yield return new WaitForSeconds(5);
		}
	}

	IEnumerator SpawnTripleLaserPowerUpRoutine () {
		while(!_stopSpawning) {
			yield return new WaitForSeconds(Random.Range(5, 7));
			int random = Random.Range(0, _powerUps.Length);
			CreateInRandomPosition(_powerUps[random]);
		}
	}

	public void StopSpawning () {
		_stopSpawning = true;
	}
}
