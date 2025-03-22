using UnityEngine;

[CreateAssetMenu(menuName = "SO/Shy/Stage/None")]
public class StageSO : ScriptableObject
{
    public MapType mapType;
}

public enum MapType
{
    None = 0,
    NormalMob,
    EliteMob,
    BossMob,
    Event,
    Market
}