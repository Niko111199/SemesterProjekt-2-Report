// Dette script er en ScoreTracker-Presenterklasse, der viser spillerens nuv�rende score p� UI'et ved hj�lp af TextMeshPro.
// Den henter scoren fra en ScoreTracker-instans og opdaterer tekstfeltet med den aktuelle score.
// Presenter bliver sat i ScoreTrackerSeparated-klassen, s� den kan opdateres udefra.

using TMPro;
using UnityEngine;

public class ScoreTrackerPresenter : MonoBehaviour
{
    [SerializeField] private TMP_Text displayScoreText;

    IGetScore scoreTracker;


    private void Awake()
    {
        scoreTracker = ScoreTrackerSeparated.GetInstance();
        ScoreTrackerSeparated.GetInstance().SetPresenter(this); // ikke smuk l�sning, men den er der
    }

    private void Start()
    {
        UpdatePresenter(scoreTracker.GetScore());
    }

    public void UpdatePresenter (int totalScore)
    {
        displayScoreText.text = "Score: " + totalScore.ToString();
    }


}
// skrevet af: Peter og Patrick
// valideret af: TODO valider
