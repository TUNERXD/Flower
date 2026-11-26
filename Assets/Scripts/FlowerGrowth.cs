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

    private string GetFocusText(StageType stageType, string flowerName)
    {
        return stageType switch
        {
            StageType.SeedFetching         => $"Fetching {flowerName} seeds",
            StageType.Seeding              => $"Planting {flowerName} seeds",
            StageType.Watering             => $"Watering {flowerName}",
            StageType.BeesFertilizing      => $"Bees are fertilizing the {flowerName}",
            _                              => stageType.ToString(),
        };
    }

    private string GetBreakText(BreakType breakType, string flowerName)
    {
        return breakType switch
        {
            BreakType.CultivateSoil         => "Cultivating soil",
            BreakType.Sunbathing            => $"{flowerName} is sunbathing",
            BreakType.RestingInSoil         => $"{flowerName} is resting in the soil",
            BreakType.RestingInDarkness     => $"{flowerName} is resting in the darkness",
            BreakType.TransplantingToGarden => $"Transplanting {flowerName} to your garden",
            _                               => breakType.ToString(),
        };
    }

    private void HandleFocusComplete()
    {
        UpdateBreakText();
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
        stageText.text = GetFocusText(stage.type, currentFlower.flowerName);
    }

    private void UpdateBreakText()
    {
        StageData stage = currentFlower.stages[currentStageIndex];
        stageText.text = GetBreakText(stage.breakType, currentFlower.flowerName);
    }
}