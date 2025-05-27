// Dette script h�ndterer visningen af highscore-listen i UI'et ved at instantiere ScoreView-elementer baseret p� data fra Leaderboard.
// N�r der modtages nye highscores, ryddes listen for gamle entries, og de nye scores vises i r�kkef�lge.
// ScoreView-instansene s�ttes som b�rn af en container (list).
// Scriptet abonnerer p� Leaderboard's OnHighscoreUpdated-event n�r det aktiveres og afmelder sig igen ved deaktivering for at undg� memory leaks.

using System.Collections.Generic;
using UnityEngine;

public class ScoreList : MonoBehaviour
{
    //the transform that is used for the score items
    [SerializeField] private Transform list; 
    [SerializeField] private Leaderboard leaderboard; 
    [SerializeField] private ScoreView prefab; 
    
    void Start()
    {
        ClearList();
    }

    void OnEnable()
    {
        leaderboard.OnHighscoreUpdated += OnNewLeaderBoard; 
    }

    void OnDisable()
    {
        leaderboard.OnHighscoreUpdated -= OnNewLeaderBoard; 
    }

    private void OnNewLeaderBoard(List<ScoreEntry> newlist)
    {
        Debug.Log("New leaderboard");
        ClearList(); 

        foreach(ScoreEntry item in newlist)
        {
            ScoreView sw = Instantiate(prefab); 
            sw.SetScore(item.initials,item.score.ToString());
            sw.transform.SetParent(list);
        }
    }
    //use this to clear old score items
    private void ClearList()
    {
        while (list.childCount > 0) 
        {
            DestroyImmediate(list.GetChild(0).gameObject);
        }
    }

    //add score item to the list transform. 
    public void AddScore(ScoreView item)
    {
        item.transform.SetParent(list);
    }
}
// skrevet af: Pelle
// valideret af: TODO valider
