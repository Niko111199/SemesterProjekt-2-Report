//Dette script har til hensigt at være en del af et mini-spil, hvor spilleren skal klikke på knapper for at optjene guld.


using UnityEngine;

public class PointButton : MonoBehaviour
{
    private GoldManager gold = GoldManager.GetInstance();
    private CatchKingGame catchKingGame;
    [SerializeField] RectTransform button;

    private void Start()
    {
        catchKingGame = FindFirstObjectByType<CatchKingGame>();
    }

    public void GiveGold()
    {
        catchKingGame.ReturnButtonToPool(button);
        gold.AddGold(2);
    }
}

//skrevet af Nikolaj Bræmer Christensen
//valideret af: TODO mangler validering