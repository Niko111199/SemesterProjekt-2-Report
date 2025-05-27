// Dette script er et leaderboard-system, der henter en highscore-liste fra en ekstern server via HTTP GET.
// Det bruger data fra et HighscoreData ScriptableObject til at opbygge API-kaldet og sender foresp�rgslen via UnityWebRequest.
// Resultatet bliver parset fra JSON til en liste af ScoreEntry-objekter, som s� bliver sendt videre til alle List via en UnityAction.
// JsonHelper bruges som en hj�lpemetode til at omg� Unitys begr�nsninger med arrays i JSON.
// ScoreEntry definerer strukturen for hvert highscore-element (initialer, score og tidspunkt).

using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class Leaderboard : MonoBehaviour
{

    [SerializeField]HighscoreData highscoredata;

    public UnityAction<List<ScoreEntry>> OnHighscoreUpdated; 

    public void GetLeaderboard(int limit = 10)
    {
        Debug.Log("Get Leaderboard"); 
        string url = $"{highscoredata.leaderboardUrl}?api_key={highscoredata.apiKey}&game_id={highscoredata.gameId}&limit={limit}";
        StartCoroutine(FetchLeaderboard(url));
    }

    IEnumerator FetchLeaderboard(string url)
    {
        using UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("User-Agent", "Mozilla/5.0 (UnityGame)");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;
            List<ScoreEntry> entries = JsonHelper.FromJson<ScoreEntry>(json);
            foreach (var entry in entries)
            {
                Debug.Log($"{entry.initials} - {entry.score} ({entry.submitted_at})");
            }
            //send a message to all listeners regarding new scores
            OnHighscoreUpdated?.Invoke(entries);
        }
        else
        {
            Debug.LogError("Leaderboard fetch failed: " + request.error);
        }
    }
}

// Utility helper for JSON arrays
public static class JsonHelper
{
    public static List<T> FromJson<T>(string json)
    {
        string newJson = "{\"array\":" + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return new List<T>(wrapper.array);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }
}

    [System.Serializable]
    public class ScoreEntry
    {
        public string initials;
        public int score;
        public string submitted_at;
    }
// skrevet af: Pelle
// valideret af: TODO valider
