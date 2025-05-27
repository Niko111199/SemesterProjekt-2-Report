//dette scirpt er til at give score til spilleren når de passerer ignnem rørene i flappy king minigamet

using UnityEngine;

public class Pipescore : MonoBehaviour
{
    [SerializeField] private RectTransform Score;
    private RectTransform Player;
    private GoldManager gold;
    private bool hasScored = false;

    private void Start()
    {
        Player = FindFirstObjectByType<FlappyKingGame>().Player;
        gold = GoldManager.GetInstance();
    }

    private void Update()
    {
        if(!hasScored && RectOverlaps(Player, Score))
        {
            gold.AddGold(5);
            hasScored = true;
        }
    }

    bool RectOverlaps(RectTransform a, RectTransform b)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(a, b.position) ||
               RectTransformUtility.RectangleContainsScreenPoint(b, a.position);
    }
}

//Note, det er så cursed med den måde at tjekke for collision på haha.

//skrevet af Nikolaj Bræmer
//Valideret af: Victor