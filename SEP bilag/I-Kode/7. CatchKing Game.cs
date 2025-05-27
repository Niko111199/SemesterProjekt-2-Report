//dette script håndtere alt logic for at få nogen knapper fra 2 objct pools til at falde ned over skærmen

using System.Collections.Generic;
using UnityEngine;

public class CatchKingGame : MonoBehaviour
{
    [SerializeField] private RectTransform pointButtonPrefab;
    [SerializeField] private RectTransform losingButtonPrefab;
    [SerializeField] private RectTransform canvas;
    [SerializeField] private int poolSize = 10;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private float speed = 200f;

    private Queue<RectTransform> pointButtonPool = new Queue<RectTransform>();
    private Queue<RectTransform> losingButtonPool = new Queue<RectTransform>();

    private List<RectTransform> activeButtons = new List<RectTransform>();

    private float screenHeight;
    private float screenWidth;

    private void Start()
    {
        screenHeight = canvas.rect.height;
        screenWidth = canvas.rect.width;

        InitializePool(pointButtonPrefab, pointButtonPool);
        InitializePool(losingButtonPrefab, losingButtonPool);

        InvokeRepeating(nameof(SpawnButton), 0f, spawnInterval);
    }

    private void Update()
    {
        MoveActiveButtons();
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(SpawnButton));
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(SpawnButton), 0f, spawnInterval);
    }

    private void InitializePool(RectTransform prefab, Queue<RectTransform> pool)
    {
        for (int i = 0; i < poolSize; i++)
        {
            RectTransform button = Instantiate(prefab, canvas);
            button.gameObject.SetActive(false);
            pool.Enqueue(button);
        }
    }

    private void SpawnButton()
    {
        Queue<RectTransform> selectedPool = Random.Range(0, 2) == 0 ? pointButtonPool : losingButtonPool;

        if (selectedPool.Count > 0)
        {
            RectTransform button = selectedPool.Dequeue();
            button.gameObject.SetActive(true);

            float randomX = Random.Range(-screenWidth / 2f, screenWidth / 2f);
            button.anchoredPosition = new Vector2(randomX, screenHeight / 2f);

            activeButtons.Add(button);
        }
    }

    private void MoveActiveButtons()
    {
        for (int i = activeButtons.Count - 1; i >= 0; i--)
        {
            RectTransform button = activeButtons[i];

            //Elsker hvordan tyngdekraft bliver kodet når vi har en hel physics simulation engine, smukt XD
            button.anchoredPosition -= new Vector2(0, speed * Time.deltaTime);

            if (button.anchoredPosition.y < -screenHeight / 2f)
            {
                ReturnButtonToPool(button);
                activeButtons.RemoveAt(i);
            }
        }
    }

    public void ReturnButtonToPool(RectTransform button)
    {
        button.gameObject.SetActive(false);

        if (button.name.Contains(pointButtonPrefab.name))
            pointButtonPool.Enqueue(button);
        else if (button.name.Contains(losingButtonPrefab.name))
            losingButtonPool.Enqueue(button);
        else
            Debug.LogWarning("Ukjent knap-type: " + button.name);
    }

    public void ReturnAllButtonsToPool()
    {
        foreach (var button in activeButtons)
        {
            button.gameObject.SetActive(false);

            if (button.name.Contains(pointButtonPrefab.name))
                pointButtonPool.Enqueue(button);
            else if (button.name.Contains(losingButtonPrefab.name))
                losingButtonPool.Enqueue(button);
        }

        activeButtons.Clear();
    }

    public void LoseGame()
    {
        gameObject.SetActive(false);
    }
}

//skrevet af Nikolaj Bræmer
//Valideret af: Victor