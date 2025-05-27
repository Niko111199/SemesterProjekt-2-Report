//dette script er til at detecte om spilleren rammer en pipe i flappy king minigamet

using UnityEngine;

public class PipeTouched : MonoBehaviour
{
    private GameObject gameUI;
    private RectTransform player;
    [SerializeField] private RectTransform pipe;
    private PipeMover pipeMover;

    private void Start()
    {
        gameUI = FindFirstObjectByType<FlappyKingGame>().gameUI;
        player = FindFirstObjectByType<FlappyKingGame>().Player;
        pipeMover = FindFirstObjectByType<PipeMover>();
    }
    private void Update()
    {

        if(RectOverlaps(player, pipe))
        {
            Debug.Log("Game Over");
            gameUI.SetActive(false);
            pipeMover.ReturnPipeToPool(pipe.transform.parent.gameObject);
        }
    }

    bool RectOverlaps(RectTransform a, RectTransform b)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(a, b.position) ||
               RectTransformUtility.RectangleContainsScreenPoint(b, a.position);
    }
}

//skrevet af Nikolaj Brærmer
//valideret af: TODO mangler validering