using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KitchenGameManager : MonoBehaviour
{
    public static KitchenGameManager Instance { get; private set;}
   private enum State
    { WaitingToStart, CountdownToStart, GamePlaying, GameOver};

    private State state;
    private float countdownToStartTimer = 3f;
    private float gameplayingTimer = 120;
    private const float GAME_PLAYING_TIMER_MAX = 120;

    private bool isGamePaused = false;

    public event EventHandler OnStateChange;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnPaused;
    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnInteractionAction += GameInput_OnInteractionAction;
    }

    private void GameInput_OnInteractionAction(object sender, EventArgs e)
    {
        if (state == State.WaitingToStart) { state = State.CountdownToStart;OnStateChange?.Invoke(this, EventArgs.Empty); }
    }

    private void Update()
    {
        switch(state)
        {
            case State.WaitingToStart:
                break;

            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if(countdownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                }

                OnStateChange?.Invoke(this, EventArgs.Empty);
                break;

            case State.GamePlaying:
                gameplayingTimer -= Time.deltaTime;
                if(gameplayingTimer < 0f)
                {
                    state = State.GameOver;
                }

                OnStateChange?.Invoke(this, EventArgs.Empty);
                break;

            case State.GameOver:

                OnStateChange?.Invoke(this, EventArgs.Empty);
                break;
        }

       
    }

    public float GetCountDownToStartTimer()
    {
        return countdownToStartTimer;
    }
    public bool IsCountDownToStartActive()
    {
        return state == State.CountdownToStart;
    }
    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public float GetPlayTimeNormalized()
    {
        return 1 - (gameplayingTimer / GAME_PLAYING_TIMER_MAX);  //because we are counting down
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        ToggleGamePause();
    }

    public void ToggleGamePause()
    {
        isGamePaused = !isGamePaused;

        if (isGamePaused)
        { Time.timeScale = 0f; OnGamePaused?.Invoke(this, EventArgs.Empty); }
        else
        { Time.timeScale = 1f; OnGameUnPaused?.Invoke(this, EventArgs.Empty); }
    }

}
