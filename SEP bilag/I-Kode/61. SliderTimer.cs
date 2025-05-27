//dette script er en del af KingClicker minigame, dette script holder styr på tiden spilleren har

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
            gameUI.SetActive(true); // Aktivér UI, når timeren starter
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
        gameUI.SetActive(false); // Deaktiver UI, når timeren slutter
        isTimerRunning = false; // Stop timeren
        InitializeTimer(); // Nulstil timeren, så den er klar til næste gang
    }
}

//skrevet af: Nikoalj Bræmer
//Valideret af: Victor