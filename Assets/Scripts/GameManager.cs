using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _player1;

    [SerializeField]
    private GameObject _player2;

    [SerializeField]
    private GameObject _winTextBackground;

    public UnityEvent OnGameWin;

    private TimerSystem _player1Timer;
    private TimerSystem _player2Timer;

    private TagSystem _player1TagSystem;
    private TagSystem _player2TagSystem;

    private PlayerControl _player1Control;
    private PlayerControl _player2Control;

    private Rigidbody _player1Rigidbody;
    private Rigidbody _player2Rigidbody;

    private bool _gameWon = false;

    private void Start()
    {
        if (_player1)
        {
            if (!_player1.TryGetComponent(out _player1Timer))
            {
                Debug.LogError("GameManager: Could not get the Player 1 Timer.");
            }
            if (!_player1.TryGetComponent(out _player1TagSystem))
            {
                Debug.LogError("GameManager: Could not get the Player 1 Tag System");
            }
            if (!_player1.TryGetComponent(out _player1Control))
            {
                Debug.LogError("GameManager: Could not get the Player 1 Controller");
            }
            if (!_player1.TryGetComponent(out _player1Rigidbody))
            {
                Debug.LogError("GameManager: Could not get the Player 1 Rigidbody");
            }

        }
        else
        {
            Debug.LogError("GameManager: Player 1 is not assigned.");
        }

        if (_player2)
        {
            if (!_player2.TryGetComponent(out _player2Timer))
            {
                Debug.LogError("GameManager: Could not get the Player 2 Timer.");
            }
            if (!_player2.TryGetComponent(out _player2TagSystem))
            {
                Debug.LogError("GameManager: Could not get the Player 2 Tag System");
            }
            if (!_player2.TryGetComponent(out _player2Control))
            {
                Debug.LogError("GameManager: Could not get the Player 2 Controller");
            }
            if (!_player2.TryGetComponent(out _player2Rigidbody))
            {
                Debug.LogError("GameManager: Could not get the Player 2 Rigidbody");
            }
        }
        else
        {
            Debug.LogError("GameManager: Player 2 is not assigned.");
        }

        if (!_winTextBackground)
        {
            Debug.LogWarning("GameManager: Win Text Background is not assigned.");
        }
    }

    private void Update()
    {
        //if either timer is unassigned, do nothing
        if (!(_player1Timer && _player2Timer))
        {
            return;
        }

        //if the game is already won, do nothing
        if(_gameWon)
        {
            return;
        }

        if (_player1Timer.TimeRemaining <= 0)
        {
            Win("Player 1 Wins!");
        }
        else if (_player2Timer.TimeRemaining <=0)
        {
            Win("Player 2 Wins!");
        }
    }

    private void Win(string winText)
    {
        //enables the win screen UI and sets the text to winText
        if (_winTextBackground)
        {
            _winTextBackground.SetActive(true);
            TextMeshProUGUI text = _winTextBackground.GetComponentInChildren<TextMeshProUGUI>(true);
            if (text)
            {
                text.text = winText;
            }
        }

        //turns off player controllers
        if(_player1Control)
        {
            _player1Control.enabled = false;
        }
        if (_player2Control)
        {
            _player2Control.enabled = false;
        }

        //turns off the tag system
        if (_player1TagSystem)
        {
            _player1TagSystem.enabled = false;
        }
        if (_player2TagSystem)
        {
            _player2TagSystem.enabled = false;
        }

        //turns off the timer system
        if (_player1Timer)
        {
            _player1Timer.enabled = false;
        }
        if (_player2Timer)
        {
            _player2Timer.enabled = false;
        }

        //turns off the rigidbody system
        if (_player1Rigidbody)
        {
            _player1Rigidbody.isKinematic = true;
        }
        if (_player2Rigidbody)
        {
            _player2Rigidbody.isKinematic = true;
        }

        _gameWon = true;
        OnGameWin.Invoke();
    }

    public void ResetGame()
    {
        //reloads the active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
