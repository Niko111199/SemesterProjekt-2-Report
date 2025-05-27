// Dette script repr�senterer et enkelt UI-element i highscore-listen og bruges til at vise initialer og score.
// Ved hj�lp af TextMeshPro-komponenter bliver teksten sat via SetScore-metoden, som tager initialer og score som input.
// ScoreView instansieres dynamisk af ScoreList og s�ttes ind i UI'et som en visuel repr�sentation af en score entry.

using UnityEngine;

public class ScoreView : MonoBehaviour
{ 
    [SerializeField]private TMPro.TMP_Text initials; 
    [SerializeField]private TMPro.TMP_Text score; 

    public void SetScore(string initials, string score)
    {
        this.initials.text = initials; 
        this.score.text = score; 
    }
}
// skrevet af: Pelle
// valideret af: TODO valider
