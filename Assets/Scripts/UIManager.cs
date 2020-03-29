using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private Text _scoreText = null;

    [SerializeField]
    private Text _gameOverText = null;

    [SerializeField]
    private Text _restartText = null;

    [SerializeField]
    private Image _livesImage = null;

    [SerializeField]
    private Sprite[] _liveSprites = null;

    private GameManager _gameManager = null;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (!_gameManager)
            Debug.LogError("Can't find Game Manager!");
    }

    public void UpdateScore (int score) {
        _scoreText.text = "Score: " + score;
    }

    public void UpdateLives (int lives) {
        _livesImage.sprite = _liveSprites[lives];

        if (lives == 0)
            GameOver();
    }

    public void UpdateDiscordPowerUpActivity(string powerUpId) {
        _gameManager.UpdateDiscordPowerUpActivity(powerUpId);
    }

    public void GameOver () {
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        _gameManager.GameOver();
        StartCoroutine(BlinkGameOver());
    }

    IEnumerator BlinkGameOver () {
        while (true) {
            yield return new WaitForSeconds(1);
            _gameOverText.gameObject.SetActive(!_gameOverText.IsActive());
        }
    }
}
