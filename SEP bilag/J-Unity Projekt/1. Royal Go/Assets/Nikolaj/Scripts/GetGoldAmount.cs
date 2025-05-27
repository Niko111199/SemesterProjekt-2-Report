using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetGoldAmount : MonoBehaviour
{
    private IGoldManager goldManager;
    [SerializeField] private TMP_Text display;

    private void Awake()
    {
        
        goldManager = GoldManager.GetInstance();

        
        goldManager.OnGoldAmountChanged += UpdateGoldDisplay;

        
        UpdateGoldDisplay(goldManager.GetGoldAmount());
    }

    private void OnDestroy()
    {
        
        goldManager.OnGoldAmountChanged -= UpdateGoldDisplay;
    }

    private void UpdateGoldDisplay(int currentGold)
    {
        if (display != null)
        {
            display.text = "Gold: " + currentGold.ToString();
        }
    }
}

