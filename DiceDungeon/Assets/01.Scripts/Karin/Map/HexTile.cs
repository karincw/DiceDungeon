using System.Collections.Generic;
using UnityEngine;

namespace Karin
{
    public class HexTile : MonoBehaviour
    {
        public Vector2Int HexCoords => HexCoordinates.ConvertPositionToOffset((Vector2)transform.position + HexCoordinates.offsetPos);

        public bool moveAble = true;

        public List<Vector2> GetNeighbourTiles()
        {
            List<Vector2> neighbours = new List<Vector2>();

            Vector2Int myCoords = HexCoords;

            neighbours.Add(new Vector2(myCoords.x - 1, myCoords.y));

            for (int i = myCoords.x; i < myCoords.x + 1; i++)
            {
                for (int j = myCoords.y - 1; j < myCoords.y + 1; j++)
                {
                    neighbours.Add(new Vector2(i, j));
                }
            }

            return neighbours;
        }
    }
}