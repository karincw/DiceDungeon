using UnityEngine;

public class HexCoordinates
{
    public static float xOffset = 0.85f, yOffset = 1f;

    public static Vector2Int ConvertPositionToOffset(Vector2 position)
    {
        int x = Mathf.RoundToInt(position.x / xOffset);
        int y = Mathf.RoundToInt(position.y / yOffset);
        return new Vector2Int(x, y);
    }

    public static Vector2 ConvertOffsetToPosition(Vector2Int position)
    {
        float x = position.x * xOffset;
        float y = position.y * yOffset;
        if (Mathf.Abs(position.x) % 2 == 1)
        {
            y += yOffset / 2;
        }
        return new Vector2(x, y);
    }
}
