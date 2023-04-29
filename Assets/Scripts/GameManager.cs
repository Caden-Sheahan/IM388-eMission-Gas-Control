using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public enum GameStatus { Losing, Middle, Winning }

    public static GameManager main;

    [Header("Game Manager")]
    [SerializeField] private GameStatus _status;
    [SerializeField] private float _winTime;
    [SerializeField] private float _loseTime;
    private float _curTimer;
    public bool _isGameActive = false;

    [Header("Gas Management")]
    [SerializeField] [Tooltip("0 = Easiest, 1 = Hardest")] [Range(0, 1)] private float _difficulty;
    [SerializeField] private AnimationCurve _spawnDelay;
    [SerializeField] [Range(0, 0.5f)] private float _winRatio;
    [SerializeField] [Range(0, 0.5f)] private float _loseRatio;
    [SerializeField] [Range(0, 1)] private float _startingRatio;
    [SerializeField] private int _maxGas;

    [Header("Game UI")]
    [SerializeField] private GameObject HUD;
    [SerializeField] private Slider _difficultySlider;
    [SerializeField] private GameObject GameOverUI;
    [SerializeField] private TMP_Text _gameOverMessage;

    private float _gameTime;

    public string GameTime => Mathf.FloorToInt(_gameTime / 60) + ":" + string.Format("{0:00}", Mathf.FloorToInt(_gameTime % 60)); 
    /*Mathf.FloorToInt(_gameTime / 60) + ((_gameTime - Mathf.FloorToInt(_gameTime / 60) < 10) ? ":0" : ":") + Mathf.FloorToInt(_gameTime - Mathf.FloorToInt(_gameTime / 60));*/
    public float WinRatio => _winRatio;
    public float LoseRatio => _loseRatio;
    public int MaxGas => _maxGas;

    public float CurTime => _curTimer;
    public int Status => _status == GameStatus.Middle ? 0 : _status == GameStatus.Losing ? -1 : 1;

    private void Awake()
    {
        if (main == null)
        {
            main = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _difficultySlider.onValueChanged.AddListener((v) => { _difficulty = v/4; });
        print(_difficulty);
    }

    public void StartGame()
    {
        GasManager.main.InitialGas(Mathf.RoundToInt(_startingRatio * _maxGas));
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            _gameTime += Time.deltaTime;
        }

        if (GasManager.main.GasRatio < _winRatio)
        {
            if (_status != GameStatus.Winning)
            {
                _status = GameStatus.Winning;
                _curTimer = _winTime;
            }
        }
        else if (GasManager.main.GasRatio > 1 - _loseRatio)
        {
            if (_status != GameStatus.Losing)
            {
                _status = GameStatus.Losing;
                _curTimer = _loseTime;
            }
        }
        else
        {
            _status = GameStatus.Middle;
        }

        if (_status != GameStatus.Middle)
        {
            _curTimer -= Time.deltaTime;
            if (_curTimer <= 0)
            {
                _isGameActive = false;
                ActivateUI(GameOverUI);
                Time.timeScale = 0;
                // disable controls for the player here so only UI is interactive
                if (_status == GameStatus.Winning)
                {
                    print("You win!");
                    _gameOverMessage.text = "You Win!\nYour time is " + GameTime;
                }
                else
                {
                    print("You lose!");
                    _gameOverMessage.text = "You Lose...";
                }
            }
        }

        if (_isGameActive)
        {
            HUD.SetActive(true);
        }
        else
        {
            HUD.SetActive(false);
        }
    }

    public float GetCooldownTime()
    {
        return _spawnDelay.Evaluate(_difficulty);
    }

    private void ResetTimer()
    {
        _curTimer = float.MaxValue;
    }

    private void ActivateUI(GameObject UI)
    {
        Cursor.lockState = CursorLockMode.None;
        UI.SetActive(true);
        if (UI.name == "GameOverUI")
        {
            _isGameActive = false;
        }
    }
}
