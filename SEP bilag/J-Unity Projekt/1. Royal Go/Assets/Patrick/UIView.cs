//dette skript er til at navigere i startmenunen, videre ud til de andre funktioner i appen

using UnityEngine;
using UnityEngine.SceneManagement;

public class UIView : MonoBehaviour
{
    // normalt ville jeg ikke havde hardkodet scenerene ind, men i dette tilf�lde til en start menu
    // fungere det fint
    public void StartScene()
    {
        SceneManager.LoadScene("UIView");
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

    public void Debug()
    {
        SceneManager.LoadScene("Debug");
    }
}

//valideret af: Nikolaj Br�mer
//skrevet af Patirck