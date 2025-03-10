using Karin.Charactor;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Karin.HexMap
{
    public class HexTile : MonoBehaviour
    {

        [Header("StateData")]
        public Agent overAgent;
        public bool moveAble = true;
        public int weight = 0;

        [Header("PathFindData")]
        public float G;
        public float H;
        public float F => H + G;
        public HexTile parentPath = null;

        private List<HexTile> neighbourTiles;
        public Vector2Int HexCoords => HexCoordinates.ConvertPositionToOffset(transform.position);
        public List<HexTile> GetNeighbourTile => neighbourTiles;

        private void Awake()
        {
            neighbourTiles = new List<HexTile>();
            for (int i = 0; i <= 5; i++)
            {
                var tileCoords = HexCoordinates.GetDirection((Direction)i);
                neighbourTiles.Add(MapManager.Instance.GetTile((Vector2)transform.position + tileCoords));
            }
        }

        public List<Agent> GetNeighbourData(Direction dir, AttackType type)
        {
            List<Agent> agents = new List<Agent>();
            switch (type)
            {
                case AttackType.Around:
                    agents = neighbourTiles.Where(t => t.overAgent != null).Select(t => t.overAgent).ToList();
                    break;
                case AttackType.Front:
                    if (neighbourTiles[(int)dir].overAgent != null)
                    {
                        agents.Add(neighbourTiles[(int)dir].overAgent);
                    }
                    break;
                case AttackType.Fan:

                    for (int i = (int)dir - 1; i <= (int)dir + 1; i++)
                    {
                        if (i == -1)
                        {
                            if (neighbourTiles[5].overAgent != null)
                            {
                                agents.Add(neighbourTiles[5].overAgent);
                            }
                        }
                        if (neighbourTiles[i].overAgent != null)
                        {
                            agents.Add(neighbourTiles[i].overAgent);
                        }
                    }
                    break;
                case AttackType.AllAround:
                    agents = neighbourTiles.Where(t => t.overAgent != null).Select(t => t.overAgent).ToList();
                    if (overAgent != null)
                    {
                        agents.Add(overAgent);
                    }
                    break;
            }
            return agents;
        }

    }
}