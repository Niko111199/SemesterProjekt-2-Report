//dette script er en del af mingame strategi pattern, som gør at vi kan skifte mellem forskellige minigames uden at ændre på koden i GameManager.cs.

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

//skrevet af Nikolaj Bræmer
//Valideret af: Victor