using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _isGameOver = false;

    private DiscordManager _discordManager = null;

    public void Start() {
        _discordManager = GameObject.Find("DiscordManager").GetComponent<DiscordManager>();
    }

    public void Update () {
        if(Input.GetKeyDown(KeyCode.R) && _isGameOver) {
            SceneManager.LoadScene(1);
        }
    }

    public void GameOver () {
        _isGameOver = true;
    }

    public void UpdateDiscordPowerUpActivity (string powerUpId) {
		_discordManager.UpdateActivity(_discordManager.GetPowerUpActivity(powerUpId));
	}
}
