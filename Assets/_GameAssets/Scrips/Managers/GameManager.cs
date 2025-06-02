using System;
using System.Collections;
using UnityEngine;

public class GameManagers : MonoBehaviour
{
    public static GameManagers Instance { get; private set; }
    public event Action<GameState> OnGameStateChanged;
    [Header("Referances")]
    [SerializeField] private EggCounterUI _eggCounterUI;
    [SerializeField] private WinLoseUI _winLoseUI;

    [Header("Settings")]
    [SerializeField] private int _maxEggCount = 5;
    private GameState _currentGameState;
    [SerializeField] private float _delay;

    private int _currentEggCount;
    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        ChangeGameState(GameState.Play);
    }

    public void ChangeGameState(GameState gameState)
    {
        OnGameStateChanged?.Invoke(gameState);
        _currentGameState = gameState;

    }

    public void OnEggCollected()
    {
        _currentEggCount++;
        _eggCounterUI.SetEggCounterText(_currentEggCount, _maxEggCount);
        if (_currentEggCount == _maxEggCount)
        {
            _eggCounterUI.SetEggCompleted();
            ChangeGameState(GameState.GameOver);
            _winLoseUI.OnGameWin();

        }
    }
    private IEnumerator OnGameOver()
    {
        yield return new WaitForSeconds(_delay);
        ChangeGameState(GameState.GameOver);
        _winLoseUI.OnGameLose();
    }
    public void PlayGameOver()
    {
        StartCoroutine(OnGameOver());
    }

    public GameState GetCurrentGameState()
    {
        return _currentGameState;
    }

}
