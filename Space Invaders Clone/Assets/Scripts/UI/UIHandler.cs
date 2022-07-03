using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [SerializeField] GameObject resultsPanel;
    [SerializeField] TMP_Text scoreInGame;
    [SerializeField] TMP_Text scoreInResultsPanel;
    [SerializeField] TMP_Text wavesInGame;
    [SerializeField] TMP_Text wavesInResultsPanel;

    [SerializeField] WaveManager waveManager;
    
    [SerializeField] ScoreSystem scoreSystem;

    private ButtonsHandler buttonsHandler;

    private void Awake()
    {
        buttonsHandler = GetComponent<ButtonsHandler>();

        Results.OnScoreChange += SetScoreInGame;
        Results.OnWavesChange += SetWavesInGame;

        buttonsHandler.OnTurnOnResults += TurnOnResultsPanel;
    }


    public void TurnOnResultsPanel()
    {
        if(!resultsPanel.activeSelf)
        {
            scoreInResultsPanel.text = "Score: " + Results.CurrentScore;
            wavesInResultsPanel.text = "Waves survived: " + (Results.CurrentWave - 1);

            Score score = new Score(Results.CurrentScore, Results.CurrentWave - 1);

            scoreSystem.TryAddScore(score);

        }
        
    }

    public void SetScoreInGame(int score)
    {
        scoreInGame.text = "Score: " + score;
    }

    public void SetWavesInGame(int waves)
    {
        wavesInGame.text = "Wave: " + waves;
    }

}
