using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {

	[SerializeField]
	private GameObject _enemy;

	[SerializeField]
	private GameObject _container;

	private bool _stopSpawning = false;

	void Start () {
		StartCoroutine(SpawnRoutine());	
	}

	IEnumerator SpawnRoutine () {
		while (!_stopSpawning) {
			GameObject newEnemy = Instantiate(_enemy, new Vector3(Random.Range(-10f, 10f), 7, 0), Quaternion.identity) as GameObject;
			newEnemy.transform.parent = _container.transform;
			yield return new WaitForSeconds(5);
		}
	}

	public void StopSpawning () {
		_stopSpawning = true;
	}
}
