// Interface til at tilgå goldManageren

using System;
using UnityEngine;

public interface IGoldManager 
{
    public void AddGold(int added);
    public bool SpendGold(int spend);
    public int GetGoldAmount();

    event Action<int> OnGoldAmountChanged;

}
// skrevet af: Peter
// valideret af: Victor