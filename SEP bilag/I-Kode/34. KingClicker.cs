//dette script er en del af minigame strategy pattern som er klassen som køre spillet kingclicker


using System;
using UnityEngine;

public class KingClicker : MiniGameBase
{
    [SerializeField] private GameObject gameUI;
    [SerializeField] private SliderTimer timer;
    public override string name => "King Clicker";

    public override void Play()
    {
        gameUI.SetActive(true);
        timer.RunTime();
    }
}

//skrevet af Nikolaj Bræmer
//Valideret af: Victor