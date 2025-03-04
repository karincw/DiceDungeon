using UnityEngine;

public class Hex : MonoBehaviour
{
    public Vector2Int HexCoords => HexCoordinates.ConvertPositionToOffset(transform.position);
}
