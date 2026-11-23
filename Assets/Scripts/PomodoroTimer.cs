using System;
using NUnit.Framework;
using TMPro;
using UnityEngine;

public class PomodoroTimer : MonoBehaviour
{
    [SerializeField] private float sessionMinutes = 25f;
    [SerializeField] private TMP_Text timerText;

    public event Action OnSessionComplete;

    private float remainingSeconds;
    private bool isRunning;

    private void Start()
    {
        remainingSeconds = sessionMinutes * 60f;
        UpdateDisplay();
    }

    public void StartSession()
    {
        if (isRunning) return;              // ignore extra clicks mid-session
        remainingSeconds = sessionMinutes * 60f;
        isRunning = true;
    }

    private void Update()
    {
        if (!isRunning) return;

        remainingSeconds -= Time.deltaTime;

        if (remainingSeconds <= 0f)
        {
            remainingSeconds = 0f;
            isRunning = false;
            Debug.Log("Session complete!");
            OnSessionComplete?.Invoke();
        }

        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        int minutes = Mathf.FloorToInt(remainingSeconds / 60f);
        int seconds = Mathf.FloorToInt(remainingSeconds % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}