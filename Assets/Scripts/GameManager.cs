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
    public GameObject uiWin;
    public EventGameState onGameStateChanged;
    public static Action onRunning;
    public static Func<bool> onCountDeath;
    public int deathCount = 0;
    GameState currentGameState = GameState.RUNNING;
    void Start()
    {
        CharacterStats.onDeath = RestartGame;
        onCountDeath = DeathCount;
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
    public void RestartGame() 
    {
        SceneManager.LoadScene("MainGame");
        Time.timeScale = 1;
    }
    public bool DeathCount() 
    {
        deathCount++;
        print(deathCount);
        return deathCount >= 15;
    }
    [System.Serializable] public class EventGameState : UnityEvent<GameState, GameState> { }
}
