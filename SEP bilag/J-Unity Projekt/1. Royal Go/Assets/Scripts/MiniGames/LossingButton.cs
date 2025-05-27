//dette scirpt sidder p� en knap i CatchKing spillet, hvor vis spilleren trykker p� den, s� taber han spillet

using UnityEngine;

public class LossingButton : MonoBehaviour
{
    //T�nker i en object pool situation er det optimalt at give objekter s� f� variable som muligt
    // gameUI bliver blot brugt til at deaktivere gameobjectet s� har tilf�jet en metode i CatchKingGame der g�r det samme.

    //private GameObject gameUI;
    private CatchKingGame game;

    private void Awake()
    {
        //gameUI = FindFirstObjectByType<CatchKingGame>().gameObject;
        game = FindFirstObjectByType<CatchKingGame>();
    }

    public void loseGame()
    {
        game.ReturnAllButtonsToPool();
        game.LoseGame();
        //gameUI.SetActive(false);
    }


}

//skrevet af: Nikolaj Br�mer
//Valideret af: Victor
