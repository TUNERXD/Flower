using System;
using TMPro;
using UnityEngine;

public enum TimerPhase {
    FocusReady = 0,
    Focus = 1,
    BreakReady = 2,
    Break = 3,
}

public class PomodoroTimer : MonoBehaviour
{
    [SerializeField] private float focusMinutes = 25f;
    [SerializeField] private float breakMinutes = 5f;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text buttonLabel;
    public event Action OnFocusComplete;
    public event Action OnBreakComplete;

    private float remainingSeconds;
    private TimerPhase phase = TimerPhase.FocusReady;

    private void Start()
    {
        SetPhase(phase);
    }

    private void Update()
    {

        if (phase == TimerPhase.BreakReady || phase == TimerPhase.FocusReady) return ;

        remainingSeconds -= Time.deltaTime;

        if (remainingSeconds > 0f)
        {
            UpdateDisplay();
            return;
        }

        if (phase == TimerPhase.Focus)
        {
            OnFocusComplete?.Invoke();
            SetPhase(TimerPhase.BreakReady);
        }
        else
        {
            EndBreak();
        }
    }

    public void OnMainButtonPressed()
    {
        switch (phase)
        {
            case TimerPhase.FocusReady:  SetPhase(TimerPhase.Focus);    break;
            case TimerPhase.Focus: SetPhase(TimerPhase.FocusReady);     break;
            case TimerPhase.BreakReady: SetPhase(TimerPhase.Break);     break;
            case TimerPhase.Break: EndBreak();                          break;
        }
    }

    private void SetPhase(TimerPhase newPhase)
    {
        phase = newPhase;

        if (phase == TimerPhase.FocusReady || phase == TimerPhase.Focus)
        {
            remainingSeconds = focusMinutes * 60f;
        }
        else
        {
            remainingSeconds = breakMinutes * 60f;
        }

        UpdateDisplay();
        UpdateButtonLabel();
    }

    private void EndBreak()
    {
        OnBreakComplete?.Invoke(); 
        SetPhase(TimerPhase.FocusReady);
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
            TimerPhase.FocusReady   => "Start Session",
            TimerPhase.Focus        => "Cancel",
            TimerPhase.BreakReady   => "Start Break",
            _                       => "Skip Break",
        };
    }
}