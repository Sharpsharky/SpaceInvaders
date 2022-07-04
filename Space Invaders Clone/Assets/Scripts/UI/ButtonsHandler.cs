using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsHandler : MonoBehaviour
{
    //[SerializeField] string nameOfArcadeScene = "Arcade";
    [SerializeField] GameObject menuUI;
    [SerializeField] GameObject gameUI;
    [SerializeField] GameObject scoreUI;
    [SerializeField] GameObject highScore;
    private UISystem uiSystem;

    public event Action OnTurnOnResults = delegate { };
    public static event Action OnGoToMenu;
    public static event Action OnStartGame;

    private void Awake()
    {
        uiSystem = GetComponent<UISystem>();
        uiSystem.OnUpdateMenu += UpdateUI;
    }


    private void UpdateUI(UIState lastState, UIState newState)
    {
        switch (lastState)
        {
                case UIState.Menu:
                {
                    menuUI.SetActive(false);
                    if (newState == UIState.Game)
                    {
                        OnStartGame();
                        gameUI.SetActive(true);
                    }
                    else //highScore
                    {
                        highScore.SetActive(true);
                    }
                        break;
                    }
                case UIState.Game:
                    {
                    Debug.Log("new state" + newState);
                    if (newState == UIState.Results)
                    {
                        OnTurnOnResults();
                        scoreUI.SetActive(true);
                    }
                    else if(newState == UIState.Menu)
                    {
                        gameUI.SetActive(false);
                        menuUI.SetActive(true);
                        OnGoToMenu();

                    }
                    break;
                }
                case UIState.HighScore:
                    {
                        menuUI.SetActive(true);
                        highScore.SetActive(false);

                        break;
                    }
        default: //results
            {
                    scoreUI.SetActive(false);
                    if (newState == UIState.Game)
                    {
                        gameUI.SetActive(true);
                        OnStartGame();
                    }
                    else if (newState == UIState.Menu)
                    {
                        gameUI.SetActive(false);
                        menuUI.SetActive(true);

                    }
                    break;
            }

        }

    }


}
