// Skatte Event produktet bliver kaldt af SkatteEventFactory, og er det script der skal s�ttes p� et GameObject.
// Den er ansvarligt for at starte alle processer op der skal ske n�r et SkatteEvent prefab spawner i scenen.

using UnityEngine;

public class SkatteEventProdukt : MonoBehaviour, IPressHandler, ICanSpawnInARWorld
{
    ARPressDetector pressDetector;
    private MiniGameManger games;

    

    public void Initialize()
    {
        pressDetector = this.gameObject.AddComponent<ARPressDetector>(); // tilf�j press detection
        pressDetector.SetUpDetector(this, "KingCrown");
        SetMiniGames();
    }

    private void SetMiniGames ()
    {
        games = FindFirstObjectByType<MiniGameManger>();
    }

    public void OnPressed()
    {
        //Destroy(this.gameObject);
        games.startMingame();
        this.gameObject.SetActive(false);

    }




}

// skrevet af: Peter
// Valideret af: Victor