//dette script er en del af minigame strategy pattern som er klassen som køre spillet

using UnityEngine;

public class CatchKing : MiniGameBase
{
    [SerializeField] private GameObject gameUI;
    public override string name => "Catch King";

    public override void Play()
    {
        gameUI.SetActive(true);
    }
}

//skrevet af Nikolaj Bræmer
//TO mangler Validering