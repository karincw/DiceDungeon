using karin.Charactor;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace karin.HexMap
{
    public class HexTile : MonoBehaviour
    {

        [Header("StateData")]
        public Agent overAgent;
        public bool moveAble = true;
        public bool warning
        {
            get { return _warning; }
            set
            {
                if (value)
                    _spriteRenderer.sprite = sprites[(int)HexState.Warning];
                else
                    _spriteRenderer.sprite = sprites[(int)HexState.None];

                _warning = value;
            }
        }
        private bool _warning = false;
        public int weight = 0;

        [Header("PathFindData")]
        public float G;
        public float H;
        public float F => H + G;
        public HexTile parentPath = null;

        [SerializeField] private Sprite[] sprites;
        private SpriteRenderer _spriteRenderer;
        private List<HexTile> neighbourTiles;
        public Vector2Int HexCoords => HexCoordinates.ConvertPositionToOffset(transform.position);

        private void Awake()
        {
            neighbourTiles = new List<HexTile>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            for (int i = 0; i <= 5; i++)
            {
                var tileCoords = HexCoordinates.GetDirectionToVector((Direction)i);
                var tile = MapManager.Instance.GetTile((Vector2)transform.position + tileCoords);
                if (tile != null)
                    neighbourTiles.Add(tile);
            }
        }

        public List<Agent> GetNeighbourData(Direction dir, AttackType type)
        {
            List<Agent> agents = new List<Agent>();
            switch (type)
            {
                case AttackType.Around:
                    agents = neighbourTiles.Where(t => t.overAgent is not null).Select(t => t.overAgent).ToList();
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
        public List<HexTile> GetNeighbourTiles(Direction dir, AttackType type)
        {
            List<HexTile> tiles = new List<HexTile>();
            switch (type)
            {
                case AttackType.Around:
                    tiles = neighbourTiles;
                    break;
                case AttackType.Front:
                    if (neighbourTiles[(int)dir].overAgent != null)
                    {
                        tiles.Add(neighbourTiles[(int)dir]);
                    }
                    break;
                case AttackType.Fan:

                    for (int i = (int)dir - 1; i <= (int)dir + 1; i++)
                    {
                        if (i == -1)
                        {
                            if (neighbourTiles[5].overAgent != null)
                            {
                                tiles.Add(neighbourTiles[5]);
                            }
                        }
                        if (neighbourTiles[i].overAgent != null)
                        {
                            tiles.Add(neighbourTiles[i]);
                        }
                    }
                    break;
                case AttackType.AllAround:
                    tiles = neighbourTiles;
                    tiles.Add(this);
                    break;
            }
            return tiles;
        }

    }

    public enum HexState
    {
        None = 0,
        Warning = 1
    }

}