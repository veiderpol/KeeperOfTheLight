using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public enum GameState
    { 
        PREGAME,
        RUNNING,
        PAUSED
    }
    public EventGameState onGameStateChanged;
    public static Action onRunning;
    GameState currentGameState = GameState.RUNNING;
    void Start()
    {
        DontDestroyOnLoad(this);
    }
    public GameState CurrentGameState
    {
        get { return currentGameState; }
        private set { currentGameState = value; }
    }
    void UpdateState(GameState state)
    {
        switch (CurrentGameState) 
        {
            case GameState.RUNNING:
                Time.timeScale = 1;
                break;
        }
    }
    void OnLoadOperationComplete(AsyncOperation ao) 
    {
        ao = SceneManager.UnloadSceneAsync("Menu");
        UpdateState(GameState.RUNNING);
    }
    public void StartGame(string levelName) 
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        if (ao == null)
            return;

        ao.completed += OnLoadOperationComplete;
    }
    [System.Serializable] public class EventGameState : UnityEvent<GameState, GameState> { }
}
