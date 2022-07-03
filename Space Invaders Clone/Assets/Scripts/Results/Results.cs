using System;

public static class Results
{
    private static int currentScore;
    private static int currentWave;

    public static event Action<int> OnScoreChange = delegate { };
    public static event Action<int> OnWavesChange = delegate { };

    public static int CurrentScore { get => currentScore; set => currentScore = value; }
    public static int CurrentWave { get => currentWave; set => currentWave = value; }

    public static void ResetResults()
    {
        currentScore = 0;
        currentWave = 0;
        
        OnScoreChange(currentScore);
    }

    public static void AddScore(int score)
    {
        currentScore += score * (currentWave);
        OnScoreChange(currentScore);
    }
    public static void AddWave()
    {
        currentWave++;
        OnWavesChange(currentWave);
    }
}
