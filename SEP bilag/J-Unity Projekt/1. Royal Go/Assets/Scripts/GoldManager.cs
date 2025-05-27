//dette script er en Persistante Singlton af Guld, der sørger for at man kan tjene og få guld, og dette
//guld bliver gemt lokalt i player prefs

using UnityEngine;
using TMPro;
using System;

public class GoldManager : IGoldManager
{
    private static GoldManager Instance;

    [SerializeField] private TMP_Text displaygoldamunt; 

    private int guld = 0;

    private const string GULD_KEY = "GemGuld";

    public event Action<int> OnGoldAmountChanged;

    // don't need monobehaviour
    //private void Awake()
    //{
    //    if (Instance != null && Instance != this)
    //    {
    //        Destroy(gameObject);
    //        return;
    //    }

    //    Instance = this;

    //    DontDestroyOnLoad(gameObject);

    //    LoadGold();
    //    UpdateGoldUI();
    //}


    private GoldManager ()
    {
        //LoadGold();
        UpdateGoldUI();
    }


    
    public static GoldManager GetInstance ()
    {
        if (Instance == null)
        {
            Instance = new GoldManager();
        }

        return Instance;

        // dont need monobehaviout
        // added auto start if empty for singleton
        //if (Instance == null)
        //{
        //    GameObject gm = new GameObject(); // lave nyt game obj
        //    gm.name = "GOLD";
        //    gm.AddComponent<GoldManager>();
        //    gm = Instantiate(gm);

        //    Instance = gm.GetComponent<GoldManager>();

        //}
        //return Instance;
    }

    private void UpdateGoldUI()
    {
        if (displaygoldamunt != null)
        {
            displaygoldamunt.text = "Gold: " + guld.ToString();
        }
    }
        
    public void AddGold(int amount)
    {
        guld += amount;
        SaveGold(); 
        UpdateGoldUI();
        OnGoldAmountChanged?.Invoke(guld);

        ScoreTrackerSeparated.GetInstance().AddScore(amount);
    }

    public bool SpendGold(int spend)
    {
        if (guld >= spend)
        {
            guld -= spend;
            SaveGold();
            UpdateGoldUI();
            OnGoldAmountChanged?.Invoke(guld);
            return true;
        }
        return false;
    }

    private void SaveGold()
    {
        PlayerPrefs.SetInt(GULD_KEY, guld);
        PlayerPrefs.Save();
    }

    private void LoadGold()
    {
        guld = PlayerPrefs.GetInt(GULD_KEY, 0);
    }

    public int GetGoldAmount()
    {
        return guld;
    }
}

//valideret af: Victor
//skrevet af: Nikolaj Bræmer

