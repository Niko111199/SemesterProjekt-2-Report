//dette script er en del af KingClicker minigame, dette script holder styr p� tiden spilleren har

using UnityEngine;
using UnityEngine.UI;

public class SliderTimer : MonoBehaviour
{
    [SerializeField] private Slider timerSlider;
    [SerializeField] private float countdownTime = 10f;
    [SerializeField] private GameObject gameUI;

    private float currentTime;
    private bool isTimerRunning = false;

    private void Update()
    {
        if (isTimerRunning)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                if (timerSlider != null)
                {
                    timerSlider.value = currentTime;
                }
            }
            else
            {
                currentTime = 0;
                OnTimerEnd();
            }
        }
    }

    public void RunTime()
    {
        if (!isTimerRunning)
        {
            InitializeTimer();
            isTimerRunning = true; // Start timeren
            gameUI.SetActive(true); // Aktiv�r UI, n�r timeren starter
        }
    }

    private void InitializeTimer()
    {
        if (timerSlider != null)
        {
            timerSlider.maxValue = countdownTime;
            timerSlider.value = countdownTime;
            timerSlider.interactable = false;
        }

        currentTime = countdownTime;
    }

    private void OnTimerEnd()
    {
        Debug.Log("Timeren er slut!");
        gameUI.SetActive(false); // Deaktiver UI, n�r timeren slutter
        isTimerRunning = false; // Stop timeren
        InitializeTimer(); // Nulstil timeren, s� den er klar til n�ste gang
    }
}

//skrevet af: Nikoalj Br�mer
//Valideret af: Victor