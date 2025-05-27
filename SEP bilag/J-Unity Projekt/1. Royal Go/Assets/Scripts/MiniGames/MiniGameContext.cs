//dette script er en del af mingame strategi pattern, som g�r at vi kan skifte mellem forskellige minigames uden at �ndre p� koden i GameManager.cs.

using UnityEngine;

public class MiniGameContext 
{
    private IMiniGame _currentMiniGame;

    public MiniGameContext(IMiniGame miniGame)
    {
        _currentMiniGame = miniGame;
    }

    public void SetStrategy(IMiniGame miniGame)
    {
        _currentMiniGame = miniGame;
    }

    public void PlayGame()
    {
        _currentMiniGame?.Play();
    }
}

//skrevet af Nikolaj Br�mer
//Valideret af: Victor