//dette scirpt sidder på en knap i CatchKing spillet, hvor vis spilleren trykker på den, så taber han spillet

using UnityEngine;

public class LossingButton : MonoBehaviour
{
    //Tænker i en object pool situation er det optimalt at give objekter så få variable som muligt
    // gameUI bliver blot brugt til at deaktivere gameobjectet så har tilføjet en metode i CatchKingGame der gør det samme.

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

//skrevet af: Nikolaj Bræmer
//Valideret af: Victor
