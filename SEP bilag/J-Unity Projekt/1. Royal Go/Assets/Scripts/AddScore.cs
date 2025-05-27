// Dette script håndterer indsendelsen af en spillers score til en ekstern highscore-server via HTTP POST.
// Det samler spillerens initialer og score samt API-nøgle og spil-ID i et JSON-objekt og sender det til serveren.
// Ved succes logges serverens svar, og ved fejl logges fejlkoden og beskeden fra serveren.
// ScoreData og Response er serialiserbare klasser, der bruges til henholdsvis at sende og fortolke JSON-data.

using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;

public class AddScore : MonoBehaviour
{
    [System.Serializable]
    public class Response
    {
        public string status;
        public string error;
    }

    [System.Serializable]
    public class ScoreData
    {
        public string api_key;
        public string initials;
        public int score;
        public string game_id;
    }

    [SerializeField]HighscoreData highscoredata;

    public void SubmitScore(string initials, int score)
    {
        ScoreData data = new ScoreData
        {
            api_key = highscoredata.apiKey,
            initials = initials,
            score = score,
            game_id = highscoredata.gameId
        };

        string json = JsonUtility.ToJson(data);
        StartCoroutine(PostScore(json));
    }

    IEnumerator PostScore(string json)
    {
        using UnityWebRequest request = new UnityWebRequest(highscoredata.submitUrl, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("User-Agent", "Mozilla/5.0 (UnityGame)");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Score submitted: " + request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Submit failed: " + request.responseCode + " " + request.downloadHandler.text);
        }
    }
}
// skrevet af: Pelle
// valideret af: TODO valider
