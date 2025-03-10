using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Karin.HexMap
{
    public class Pathfinder
    {
        private List<HexTile> _openList;
        private List<HexTile> _closedList;

        private HexTile _destinationPos;

        public List<Vector2> PathFind(HexTile startPos, HexTile endPos)
        {
            _openList = new List<HexTile>();
            _closedList = new List<HexTile>();
            _destinationPos = endPos;
            _openList.Add(startPos);

            while (_openList.Count > 0)
            {
                int warningIdx = 0;
                HexTile findHex = _openList.First();

                if (findHex == _destinationPos)
                {
                    var nextRoute = _destinationPos;
                    List<Vector2> result = new List<Vector2>();
                    while (nextRoute != null)
                    {
                        result.Add(nextRoute.transform.position);
                        nextRoute = nextRoute.parentPath;
                    }
                    result.Reverse();
                    return result;
                }

                AddOpenList(findHex);

                warningIdx++;
                if (warningIdx >= 1000)
                {
                    break;
                }
            }
            Debug.LogError("CannotFindPath in 1000 steps");
            return null;
        }

        public List<Vector2> PathFind(HexTile startPos, List<HexTile> endPositions)
        {
            _openList = new List<HexTile>();
            _closedList = new List<HexTile>();
            _destinationPos = endPositions.OrderBy(t => Vector2.Distance(t.transform.position, startPos.transform.position)).First();
            _openList.Add(startPos);

            while (_openList.Count > 0)
            {
                int warningIdx = 0;
                HexTile findHex = _openList.First();

                if (findHex == _destinationPos)
                {
                    var nextRoute = _destinationPos;
                    List<Vector2> result = new List<Vector2>();
                    while (nextRoute != null)
                    {
                        result.Add(nextRoute.transform.position);
                        nextRoute = nextRoute.parentPath;
                    }
                    result.Reverse();
                    return result;
                }

                AddOpenList(findHex);

                warningIdx++;
                if (warningIdx >= 1000)
                {
                    break;
                }
            }
            Debug.LogError("CannotFindPath in 1000 steps");
            return null;
        }

        private void AddOpenList(HexTile tile)
        {
            Debug.Log($"AddOpenList > {tile}");
            List<HexTile> tiles = tile.GetNeighbourTile;
            for (int i = 0; i < tiles.Count; i++)
            {
                if (tiles[i] != null && !_openList.Contains(tiles[i]) && !_closedList.Contains(tiles[i]) && tiles[i].moveAble)
                {
                    tiles[i].G = tile.G + 1 + tiles[i].weight;
                    tiles[i].H = Vector2.Distance(_destinationPos.transform.position, tiles[i].transform.position);
                    tiles[i].parentPath = tile;
                    _openList.Add(tiles[i]);
                }
            }
            _closedList.Add(tile);
            _openList.Remove(tile);
            _openList = _openList.OrderBy(T => T.F).ToList();
        }

    }

}