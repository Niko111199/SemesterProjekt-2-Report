// Dette script håndterer UI-panelet, hvor spilleren kan indsende deres initialer sammen med deres score til leaderboardet.
// Scoren hentes automatisk fra en ScoreTracker-instans og vises i et skrivebeskyttet inputfelt, mens spilleren indtaster sine initialer.
// Når brugeren trykker "Submit", kaldes AddScore-scriptet for at sende dataen til highscore-serveren.
// Scriptet sikrer, at initialer ikke er tomme, før der sendes.

using UnityEngine;
using TMPro;

public class SubmitScorePanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField initials;
    [SerializeField] private TMP_InputField score;

    private IGetScore scoreTracker;

    public AddScore addscorescript;

    //void Awake()
    //{
    //    initials.contentType = TMP_InputField.ContentType.Name;

    //    // Gør score-feltet til visning og hent scoren
    //    score.readOnly = true;

    //    int currentScore = ScoreTracker.GetInstance().GetScore();
    //    score.text = currentScore.ToString();
    //}

    private void Start()
    {
        scoreTracker = ScoreTrackerSeparated.GetInstance();

        initials.contentType = TMP_InputField.ContentType.Name;
        score.readOnly = true;

        //if (ScoreTracker.GetInstance() == null)
        //{
        //    Debug.LogError("ScoreTracker er ikke klar!");
        //    return;
        //}

        int currentScore = scoreTracker.GetScore();
        score.text = currentScore.ToString();
    }

    public void Submitscore()
    {
        string playerInitials = initials.text.Trim();
        int result = scoreTracker.GetScore();

        if (!string.IsNullOrEmpty(playerInitials))
        {
            addscorescript.SubmitScore(playerInitials, result);
            Debug.Log("Sending initials " + playerInitials + " with score " + result);
        }
        else
        {
            Debug.LogWarning("Initials field is empty!");
        }
    }
}



// skrevet af: Pelle:
//public class SubmitScorePanel : MonoBehaviour
//{
//    [SerializeField] private TMPro.TMP_InputField initials;
//    [SerializeField] private TMPro.TMP_InputField score;

//    public AddScore addscorescript;

//    void Awake()
//    {
//        score.contentType = TMPro.TMP_InputField.ContentType.IntegerNumber;
//        initials.contentType = TMPro.TMP_InputField.ContentType.Name;

//        score.onValueChanged.AddListener(ValidateInput);
//    }

//    private void ValidateInput(string value)
//    {
//        string digitsOnly = System.Text.RegularExpressions.Regex.Replace(value, @"[^0-9-]", "");
//        if (digitsOnly != value)
//        {
//            score.text = digitsOnly;
//        }
//    }

//    public void Submitscore()
//    {
//        int result;
//        if (int.TryParse(score.text, out result))
//        {
//            addscorescript.SubmitScore(initials.text, result);
//            Debug.Log("Sending initials " + initials.text);
//        }
//        {
//            Debug.LogWarning("Invalid integer");
//        }
//    }
//}

// skrevet af: Patrick
// valideret af: TODO valider
