//dette script er en Persistante Singlton af Guld, der sørger for at man kan tjene og få guld, og dette
//guld forbliver på tværs af alle scenerene

using UnityEngine;
using TMPro;

public class Gold : MonoBehaviour
{
    private static Gold Instance;

    [SerializeField] private TMP_Text displaygoldamunt;

    private int guld = 0;

    private const string GULD_KEY = "GemGuld";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        LoadGold();
        UpdateGoldUI();
    }


    // added auto start if empty for singleton
    public static Gold GetInstance ()
    {
        if (Instance == null)
        {
            //GameObject gm = new GameObject(); // lave nyt game obj
            //gm.name = "GOLD";
            //gm.AddComponent<Gold>();
            //gm = Instantiate(gm);

            //Instance = gm.GetComponent<Gold>();

        }
        return Instance;
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
    }

    public bool SpendGold(int amount)
    {
        if (guld >= amount)
        {
            guld -= amount;
            SaveGold();
            UpdateGoldUI();
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

