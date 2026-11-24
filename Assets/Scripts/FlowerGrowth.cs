using TMPro;
using UnityEngine;

public class FlowerGrowth : MonoBehaviour
{
    [SerializeField] private PomodoroTimer timer;
    [SerializeField] private FlowerData currentFlower;
    [SerializeField] private TMP_Text stageText;

    private int currentStageIndex = 0;

    private void OnEnable()
    {
        timer.OnFocusComplete += HandleFocusComplete;
        timer.OnBreakComplete += HandleBreakComplete;
    }

    private void OnDisable()
    {
        timer.OnFocusComplete -= HandleFocusComplete;
        timer.OnBreakComplete -= HandleBreakComplete;
    }

    private void Start()
    {
        UpdateStageText();
    }

    private void HandleFocusComplete()
    {
        StageData stage = currentFlower.stages[currentStageIndex];
        stageText.text = stage.breakType.ToString();
    }

    private void HandleBreakComplete()
    {
        currentStageIndex++;

        if (currentStageIndex >= currentFlower.stages.Length)
        {
            Debug.Log(currentFlower.flowerName + " bloomed!");
            currentStageIndex = 0;
        }

        UpdateStageText();
    }

    private void UpdateStageText()
    {
        StageData stage = currentFlower.stages[currentStageIndex];
        stageText.text = stage.type.ToString();
    }
}