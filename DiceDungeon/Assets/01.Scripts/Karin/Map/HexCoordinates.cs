using System.Linq;
using UnityEngine;

namespace karin.HexMap
{
    public class HexCoordinates
    {
        public static float xOffset = 2.3f, yOffset = 1.2f;
        public static Vector2 offsetPos = new Vector2(0, 0.8f);

        private static Vector2[] directionArray =
            {
            new Vector2(-xOffset, 0),
            new Vector2(xOffset, 0),
            new Vector2(-xOffset / 2, yOffset),
            new Vector2(xOffset / 2, yOffset),
            new Vector2(-xOffset / 2, -yOffset),
            new Vector2(xOffset / 2, -yOffset)
        };

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
                    return directionArray[0];
                case Direction.Right:
                    return directionArray[1];
                case Direction.TopLeft:
                    return directionArray[2];
                case Direction.TopRight:
                    return directionArray[3];
                case Direction.BottomLeft:
                    return directionArray[4];
                case Direction.BottomRight:
                    return directionArray[5];
            }
            Debug.LogError("Error Dir");
            return new Vector2(0, 0);
        }

        public static Direction GetVectorToDirection(Vector2 dirVector)
        {
            dirVector = dirVector.normalized;
            if (dirVector == directionArray[0].normalized)
                return Direction.Left;
            if (dirVector == directionArray[1].normalized)
                return Direction.Right;
            if (dirVector == directionArray[2].normalized)
                return Direction.TopLeft;
            if (dirVector == directionArray[3].normalized)
                return Direction.TopRight;
            if (dirVector == directionArray[4].normalized)
                return Direction.BottomLeft;
            if (dirVector == directionArray[5].normalized)
                return Direction.BottomRight;

            return GetNearestDir(dirVector);
        }

        public static Direction GetNearestDir(Vector2 searchVector)
        {
            var directionList = directionArray.ToList();
            var directionVector = directionList.Select(dir => dir.normalized).OrderBy(dir => Vector2.Distance(dir, searchVector)).First();
            return GetVectorToDirection(directionVector);
        }
    }
}
