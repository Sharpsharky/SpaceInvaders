using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreUI : MonoBehaviour
{
    [SerializeField] private Transform scoreParent;
    [SerializeField] ScoreSystem scoreSystem;
    private List<ScoreObjectUI> scoresUI = new List<ScoreObjectUI>();
    private void Awake()
    {
        scoreSystem.OnSetScores += ResetScores;

        for (int i = 1; i < scoreParent.childCount; i++)
        {
            scoresUI.Add(scoreParent.GetChild(i).GetComponent<ScoreObjectUI>());
        }
    }
    private void Start()
    {

        ResetScores();
        
    }
    private void ResetScores()
    {
        AddScores(scoreSystem.Scores);
    }

    private void AddScores(List<Score> scores)
    {
        TurnOffEveryScore();
        Debug.Log("Add scores UI " + scores.Count);
        for(int i = 0; i < scores.Count; i++)
        {
            scoresUI[i].Score.text = scores[i]._Score.ToString();
            scoresUI[i].Date.text = scores[i].Day + "." + scores[i].Month + "." + scores[i].Year;
            scoresUI[i].gameObject.SetActive(true);
        }
    }

    private void TurnOffEveryScore()
    {
        foreach (ScoreObjectUI scoreUI in scoresUI)
        {
            scoreUI.gameObject.SetActive(false);
        }
    }
    

}
