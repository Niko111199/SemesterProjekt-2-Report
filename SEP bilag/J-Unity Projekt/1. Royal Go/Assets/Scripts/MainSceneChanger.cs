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

    public void �bnSkattekammer()
    {
        SceneManager.LoadScene("SkattekammerScene");
    }

    public void �bnButik()
    {
        SceneManager.LoadScene("ButikScene");
    }

    public void �benSubmitScoreScene()
    {
        SceneManager.LoadScene("Highscore");
    }

    public void �benleaderboardScene()
    {
        SceneManager.LoadScene("leaderboard");
    }

    public void �benDebug()
    {
        SceneManager.LoadScene("Debug");
    }
}

//valideret af: Nikolaj Br�mer
//skrevet af Patirck

// �ndret af Peter:
//valideret af: Nikolaj Br�mer