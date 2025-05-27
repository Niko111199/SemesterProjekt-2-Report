// Dette script er et ScriptableObject, der indeholder API-information og endpoints til et highscore-system.
// Det bruges til at gemme og tilgå credentials som API-nøgle, spil-ID og URL'er til at indsende og hente highscores.

using UnityEngine;

[CreateAssetMenu(fileName = "HighscoreData", menuName = "Scriptable Objects/HighscoreData")]
public class HighscoreData : ScriptableObject
{
    [Header("API Credentials")]
    [Tooltip("The unique API key for this game")]
    public string apiKey;

    [Tooltip("The unique identifier for this game")]
    public string gameId;

    [Header("Endpoints")]
    [Tooltip("Base URL for submitting scores")]
    public string submitUrl = "https://gamecraft.dk/leaderboard/submit_score.php";

    [Tooltip("Base URL for retrieving leaderboard")]
    public string leaderboardUrl = "https://gamecraft.dk/leaderboard/leaderboard.php";
}
// skrevet af: Pelle
// valideret af: TODO valider
