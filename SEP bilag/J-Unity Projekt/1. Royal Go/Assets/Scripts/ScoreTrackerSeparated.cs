//Scriptet ScoreTracker bruges til at holde styr på spillerens samlede score.



using UnityEngine;


public class ScoreTrackerSeparated : IAddScore, IGetScore
{
    private static ScoreTrackerSeparated Instance;

    private ScoreTrackerPresenter presenter;

    private int totalScore = 0;
    private const string SCORE_KEY = "GemScore";

    private ScoreTrackerSeparated()
    {
        LoadScore();
        UpdateScoreUI();
    }

    public void SetPresenter(ScoreTrackerPresenter presenter)
    {
        this.presenter = presenter;
    }

    public void AddScore(int amount)
    {
        totalScore += amount;
        SaveScore();
        UpdateScoreUI();
    }

    public static ScoreTrackerSeparated GetInstance()
    {
        if (Instance == null)
        {
            Instance = new ScoreTrackerSeparated();
        }
        return Instance;
    }

    private void UpdateScoreUI()
    {
        if (presenter != null)
        {
            presenter.UpdatePresenter(totalScore);
        }
    }

    private void SaveScore()
    {
        PlayerPrefs.SetInt(SCORE_KEY, totalScore);
        PlayerPrefs.Save();
    }

    private void LoadScore()
    {
        totalScore = PlayerPrefs.GetInt(SCORE_KEY, 0);
    }

    public int GetScore()
    {
        return totalScore;
    }
}
