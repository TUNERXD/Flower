using System;
using UnityEngine;

public enum StageType
{
    SeedFetching = 0,
    Seeding = 1,
    Watering = 2,
    BeesFertilizing = 3,
}

public enum BreakType {
    Sunbathing = 0,
    RestingInSoil = 1,
    RestingInDarkness = 2,
    CultivateSoil = 3,
    TransplantingToGarden= 4,

}

[Serializable]
public class StageData
{
    public StageType type;
    public Sprite sprite;
    public BreakType breakType = BreakType.Sunbathing;
}

[CreateAssetMenu(fileName = "FlowerData", menuName = "Scriptable Objects/FlowerData")]
public class FlowerData : ScriptableObject
{
    public string flowerName;
    public StageData[] stages;
}
