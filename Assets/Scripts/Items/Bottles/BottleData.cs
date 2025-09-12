using UnityEngine;

[CreateAssetMenu(fileName = "New Bottle", menuName = "ScriptableObjects/Bottle")]
public class BottleData : ItemData
{
    // === Extra data fields ===
    public enum BottleType { Bottle_01, Bottle_02, Bottle_03 }
    [SerializeField] private BottleType bottleID;

    // === Properties ===
    public BottleType BottleID => bottleID;
}