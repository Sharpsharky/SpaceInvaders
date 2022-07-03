using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum UIState { Menu, Game, Results, HighScore, Loading }


public class UISystem : MonoBehaviour
{
    private UIState state = UIState.Loading;

    public UIState State { get => state; set => state = value; }

    public event Action<UIState, UIState> OnUpdateMenu = delegate { };

    private void Awake()
    {
        WaveManager.OnFinishGame += OpenResults;

    }
    void Start()
    {
        OpenuMenu();
    }

    public void OpenuMenu()
    {
        UIState previousState = State;
        State = UIState.Menu;
        OnUpdateMenu(previousState, State);
    }
    public void OpenGame()
    {
        UIState previousState = State;
        State = UIState.Game;
        OnUpdateMenu(previousState, State);
    }

    public void OpenResults()
    {
        Debug.Log("open results");
        UIState previousState = State;
        State = UIState.Results;
        OnUpdateMenu(previousState, State);
    }

    public void OpenHighScores()
    {
        UIState previousState = State;
        State = UIState.HighScore;
        OnUpdateMenu(previousState, State);
    }


}
