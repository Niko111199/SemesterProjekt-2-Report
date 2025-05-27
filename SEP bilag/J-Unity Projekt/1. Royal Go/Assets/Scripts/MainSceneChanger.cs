//dette skript er til at navigere i startmenunen, videre ud til de andre funktioner i appen

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneChanger: MonoBehaviour
{

    public void StartScene()
    {
        print("Jeg er trykkeyt");
        SceneManager.LoadScene("LandingPage");
    }

    public void StartSkattejagt()
    {
        SceneManager.LoadScene("SkattejagtScene");
    }

    public void ÅbnSkattekammer()
    {
        SceneManager.LoadScene("SkattekammerScene");
    }

    public void ÅbnButik()
    {
        SceneManager.LoadScene("ButikScene");
    }

    public void ÅbenSubmitScoreScene()
    {
        SceneManager.LoadScene("Highscore");
    }

    public void ÅbenleaderboardScene()
    {
        SceneManager.LoadScene("leaderboard");
    }

    public void ÅbenDebug()
    {
        SceneManager.LoadScene("Debug");
    }
}

//valideret af: Nikolaj Bræmer
//skrevet af Patirck

// ændret af Peter:
//valideret af: Nikolaj Bræmer