//dette script tilf�jer guld til spilleren n�r de vinder et minigame

using UnityEngine;

public class AddGold : MonoBehaviour //TODO scriptet skal laves om til et interface
{
    GoldManager gold = GoldManager.GetInstance();

    public void WinGold(int goldToAdd)
    {
        gold.AddGold(goldToAdd);
    }
}

//Skrevet af Nikolaj Br�mer
//Valideret af: Victor