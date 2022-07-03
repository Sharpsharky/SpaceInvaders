[System.Serializable]
public class Score
{
    public int Day { get; private set; }
    public int Month { get; private set; }
    public int Year { get; private set; }
    public int _Score { get; private set; }
    public int Waves { get; private set; }

    public Score (int score, int waves)
    {
        _Score = score;
        Waves = waves;
        Day = System.DateTime.Now.Day;
        Month = System.DateTime.Now.Month;
        Year = System.DateTime.Now.Year;
    }

}
