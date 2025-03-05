using UnityEngine;

namespace Karin.HexMap
{
    public class HexCoordinates
    {
        public static float xOffset = 2.3f, yOffset = 1.2f;
        public static Vector2 offsetPos = new Vector2(0, 1.8f);

        public static Vector2Int ConvertPositionToOffset(Vector2 position)
        {
            int y = Mathf.RoundToInt((position.y - offsetPos.y) / yOffset);
            float posx = position.x;
            if (Mathf.Abs(y) % 2 == 1)
            {
                posx = position.x - xOffset / 2;
            }
            int x = Mathf.RoundToInt((posx - offsetPos.x) / xOffset);

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

        public static Vector2 GetDirection(Direction dir)
        {
            switch (dir)
            {
                case Direction.Left:
                    return new Vector2(-xOffset, 0);
                case Direction.Right:
                    return new Vector2(xOffset, 0);
                case Direction.TopLeft:
                    return new Vector2(-xOffset / 2, yOffset);
                case Direction.TopRight:
                    return new Vector2(xOffset / 2, yOffset);
                case Direction.BottomLeft:
                    return new Vector2(-xOffset / 2, -yOffset);
                case Direction.BottomRight:
                    return new Vector2(xOffset / 2, -yOffset);
            }
            Debug.LogError("Error Vector");
            return new Vector2(0, 0);
        }
    }
}
