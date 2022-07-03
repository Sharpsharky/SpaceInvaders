using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public event Action OnSetScores = delegate { };
    private List<Score> scores = new List<Score>();

    public List<Score> Scores { get => scores; set => scores = value; }

    private void Awake()
    {
        Scores = SaveSystem.LoadScores();
    }

    public void TryAddScore(Score score)
    {
        if (Scores.Count >= SaveSystem.CountOfSavedScores && !CheckIfScoreIsBetterThanAnyHigh(score._Score)) return;

        Scores.Add(score);
        if (Scores.Count > 1) SortScore();
        SaveSystem.SaveScores(Scores);
        OnSetScores();
    }

    private bool CheckIfScoreIsBetterThanAnyHigh(int score)
    {
        for (int i = 0; i < Scores.Count; i++)
        {
            if (score > Scores[i]._Score) return true;
        }
        return false;
    }

    private void SortScore()
    {
        for (int i = 0; i < Scores.Count; i++)
        { 
            for (int j = 0; j < Scores.Count - 1; j++)
            {
                if (Scores[j + 1]._Score > Scores[j]._Score)
                {

                    Score bufor = Scores[j + 1];
                    Scores[j + 1] = Scores[j];
                    Scores[j] = bufor;

                }
            }
        }


        TrimScore();

    }

    private void TrimScore()
    {
        if(Scores.Count > SaveSystem.CountOfSavedScores)
        {
            for(int i = SaveSystem.CountOfSavedScores; i < Scores.Count; i++)
            {
                Scores.RemoveAt(i);
            }
        }
    }

}
