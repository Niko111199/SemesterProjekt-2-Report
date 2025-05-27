//dette script tilføjer guld til spilleren når de vinder et minigame

using UnityEngine;

public class AddGold : MonoBehaviour //TODO scriptet skal laves om til et interface
{
    GoldManager gold = GoldManager.GetInstance();

    public void WinGold(int goldToAdd)
    {
        gold.AddGold(goldToAdd);
    }
}

//Skrevet af Nikolaj Bræmer
//Valideret af: Victor