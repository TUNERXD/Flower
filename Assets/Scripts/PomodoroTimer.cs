using System;
using TMPro;
using UnityEngine;

public enum TimerPhase { Idle, Focus, Break }

public class PomodoroTimer : MonoBehaviour
{
    [SerializeField] private float focusMinutes = 25f;
    [SerializeField] private float breakMinutes = 5f;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text buttonLabel;
    public event Action OnFocusComplete;
    public event Action OnBreakComplete;

    private float remainingSeconds;
    private TimerPhase phase = TimerPhase.Idle;

    private void Start()
    {
        remainingSeconds = focusMinutes * 60f;
        UpdateDisplay();
        UpdateButtonLabel();
    }

    private void Update()
    {
        if (phase == TimerPhase.Idle)
        {
            return;
        }

        remainingSeconds -= Time.deltaTime;

        if (remainingSeconds > 0f)
        {
            UpdateDisplay();
            return;
        }

        if (phase == TimerPhase.Focus)
        {
            OnFocusComplete?.Invoke();
            StartBreak();
        }
        else
        {
            OnBreakComplete?.Invoke();
            ResetToIdle();
        }
        
        UpdateDisplay();
        UpdateButtonLabel();
    }

    public void OnMainButtonPressed()
    {
        switch (phase)
        {
            case TimerPhase.Idle:  StartFocus();   break;
            default: ResetToIdle(); break;
        }

        UpdateButtonLabel();
    }

    private void StartFocus()
    {
        phase = TimerPhase.Focus;
        remainingSeconds = focusMinutes * 60f;
    }

    private void StartBreak()
    {
        phase = TimerPhase.Break;
        remainingSeconds = breakMinutes * 60f;
    }

    private void ResetToIdle()
    {
        phase = TimerPhase.Idle;
        remainingSeconds = focusMinutes * 60f;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        int minutes = Mathf.FloorToInt(remainingSeconds / 60f);
        int seconds = Mathf.FloorToInt(remainingSeconds % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    private void UpdateButtonLabel()
    {
        buttonLabel.text = phase switch
        {
            TimerPhase.Idle  => "Start Planting",
            TimerPhase.Focus => "Cancel",
            _                => "Skip Break",
        };
    }
}