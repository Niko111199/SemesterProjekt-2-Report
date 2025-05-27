//dette script er en del af minigame strategy pattern som er klassen som k�re spillet flappy king   

using UnityEngine;

public class FlappyKing : MiniGameBase
{
    [SerializeField] private GameObject gameUI;
    public override string name => "Flappy King";

    public override void Play()
    {
        gameUI.SetActive(true);
    }
}

//skrevet af Nikolaj Br�mer
//Valideret af: Victor