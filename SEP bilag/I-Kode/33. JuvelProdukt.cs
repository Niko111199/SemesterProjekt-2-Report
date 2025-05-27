// Den definerende del af juvelerne. Herfra inisialiseres alle instancer og generel brug. Juvel factorien er afh�ngig af denne for at kunne spawne noget

using UnityEngine;

public class JuvelProdukt : MonoBehaviour, ICanSpawnInARWorld, IPressHandler
{

    ARPressDetector pressDetector;

    public int goldAmount = 1;

    private IGoldManager goldManager;

    public void Initialize()
    {
        this.goldManager = GoldManager.GetInstance();
        pressDetector = this.gameObject.AddComponent<ARPressDetector>(); // tilf�j press detection
        pressDetector.SetUpDetector(this, "Interactable");
    }

    public void OnPressed()
    {

        goldManager.AddGold(goldAmount);
        Destroy(this.gameObject);
    }


    // skrevet af: Peter
    // valideret af: Victor

}
