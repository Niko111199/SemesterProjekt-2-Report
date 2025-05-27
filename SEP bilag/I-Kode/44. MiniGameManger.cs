//dette script er mangeren i mini game, som holder styr på hvilke mini games der er i scenen og som starter en af dem.

using System.Linq;
using UnityEngine;


public class MiniGameManger : MonoBehaviour
{
    public void startMingame()
    {
        IMiniGame[] games = FindObjectsOfType<MonoBehaviour>()
                         .OfType<IMiniGame>()
                         .ToArray();

        if (games.Length == 0)
        {
            Debug.LogWarning("No mini games found in the scene.");
            return;
        }

        IMiniGame selectedGame = games[UnityEngine.Random.Range(0, games.Length)];

        MiniGameContext context = new MiniGameContext(selectedGame);
        context.PlayGame();
    }
}

//skrevet af Nikolaj Bræmer
//Valideret af: Victor