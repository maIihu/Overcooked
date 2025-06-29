using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    public static KitchenGameManager Instance { get; private set; }

    public enum GameState
    {
        WaitingToStart, CountdownToStart, GamePlaying, GameOver, Pause
    }
    
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject pauseUI;
    
    private GameState _currentState;

    private float _waitingToStartTimer = 1f;
    private float _countdownToStartTimer = 1f;
    private float _gamePlayingToStartTimer = 1f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        
    }

    private void Start()
    {
        gameOverUI.SetActive(false);
        pauseUI.SetActive(false);
        ChangeState(GameState.GamePlaying);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeState(GameState.Pause);
        }
    }

    public void ChangeState(GameState newState)
    {
        _currentState = newState;
        ApplyState();
    }

    private void ApplyState()
    {
        switch (_currentState)
        {
            case GameState.GamePlaying:
                Time.timeScale = 1f;
                break;
            case GameState.Pause:
                Time.timeScale = 0f;
                pauseUI.SetActive(true);
                break;
            case GameState.GameOver:
                Time.timeScale = 0f;
                gameOverUI.SetActive(true);
                break;
            
        }

        
    }
}
