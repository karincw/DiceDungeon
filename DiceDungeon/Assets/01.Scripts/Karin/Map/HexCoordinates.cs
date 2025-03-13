using UnityEngine;

namespace karin.HexMap
{
    public class HexCoordinates
    {
        public static float xOffset = 2.3f, yOffset = 1.2f;
        public static Vector2 offsetPos = new Vector2(0, 0.8f);

        public static Direction InvertDirection(Direction dir) => dir - 3 >= 0 ? dir - 3 : dir + 3;

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

        public static Vector2 GetDirectionToVector(Direction dir)
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
            Debug.LogError("Error Dir");
            return new Vector2(0, 0);
        }

        public static Direction GetVectorToDirection(Vector2 dirVector)
        {
            dirVector = dirVector.normalized;
            if (dirVector == new Vector2(-xOffset, 0).normalized)
                return Direction.Left;
            if (dirVector == new Vector2(xOffset, 0).normalized)
                return Direction.Right;
            if (dirVector == new Vector2(-xOffset / 2, yOffset).normalized)
                return Direction.TopLeft;
            if (dirVector == new Vector2(xOffset / 2, yOffset).normalized)
                return Direction.TopRight;
            if (dirVector == new Vector2(-xOffset / 2, -yOffset).normalized)
                return Direction.BottomLeft;
            if (dirVector == new Vector2(xOffset / 2, -yOffset).normalized)
                return Direction.BottomRight;

            Debug.LogError("Error Vector");
            return Direction.Left;
        }
    }
}
