using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
	private float _speed = 3.5f;

    [SerializeField]
    private string _powerUpId = null;

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime, Space.World);

        if (transform.position.y <= -5f)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
         if(other.tag == "Player") {
            Player player = other.transform.GetComponent<Player>();

            if(player != null) {
                switch(_powerUpId) {
                    case "trippleShot":
                        player.ActiveTripleShotPowerUp();
                        break;
                    case "speed":
                        player.ActiveSpeedPowerUp();
                        break;
                    case "shield":
                        player.ActiveShieldPowerUp();
                        break;
                }
            }

            Destroy(this.gameObject);
        }
    }
}
