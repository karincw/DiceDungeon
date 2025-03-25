using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace karin.HexMap
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
            _destinationPos = endPositions
                .OrderBy(t => Vector2.Distance(t.transform.position, startPos.transform.position))
                .Where(t => t.overAgent == null || t.overAgent == startPos.overAgent).First();
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
                        warningIdx++;
                        if (warningIdx >= 1000)
                        {
                            Debug.LogError("CannotMakeResultRoute in 1000 steps");
                            return null;
                        }
                    }
                    result.Reverse();
                    _closedList.ForEach(t => t.parentPath = null);
                    _openList.ForEach(t => t.parentPath = null);
                    return result;
                }

                AddOpenList(findHex);

                warningIdx++;
                if (warningIdx >= 1000)
                {
                    Debug.LogError("CannotFindPath in 1000 steps");
                    return null;
                }
            }

            Debug.LogError("CannotFindPath");
            return null;
        }

        private void AddOpenList(HexTile tile)
        {
            List<HexTile> tiles = tile.GetNeighbourTiles(Direction.Left, AttackType.Around);
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