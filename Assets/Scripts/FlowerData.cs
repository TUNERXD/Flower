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
    CultivateSoil = 0,
    RestingInSoil = 1,
    Sunbathing = 2,
    TransplantingToGarden= 3,
    RestingInDarkness = 4,

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
