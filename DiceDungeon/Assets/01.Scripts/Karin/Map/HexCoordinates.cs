using UnityEngine;

namespace Karin
{
    public class HexCoordinates
    {
        public static float xOffset = 2.3f, yOffset = 1.2f;
        public static Vector2 offsetPos = new Vector2(0, 1.8f);

        public static Vector2Int ConvertPositionToOffset(Vector2 position)
        {
            int x = Mathf.RoundToInt(position.x - offsetPos.x / xOffset);
            int y = Mathf.RoundToInt(position.y - offsetPos.y / yOffset);
            return new Vector2Int(x, y);
        }

        public static Vector2 ConvertOffsetToPosition(Vector2Int position)
        {
            float x = position.x * xOffset;
            float y = position.y * yOffset;
            if (Mathf.Abs(position.y) % 2 == 1)
            {
                x += xOffset / 2;
            }
            return new Vector2(x + offsetPos.x, y + offsetPos.y);
        }

        public static Vector2Int GetDirection(Direction dir)
        {
            switch (dir)
            {
                case Direction.Left:
                    return new Vector2Int(-1, 0);
                case Direction.Right:
                    return new Vector2Int(1, 0);
                case Direction.TopLeft:
                    return new Vector2Int(-1, 1);
                case Direction.TopRight:
                    return new Vector2Int(0, 1);
                case Direction.BottomLeft:
                    return new Vector2Int(-1, -1);
                case Direction.BottomRight:
                    return new Vector2Int(0, -1);
            }
            Debug.LogError("Error Vector");
            return new Vector2Int(0, 0);
        }
    }
}
