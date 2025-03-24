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

        [Header("PathFindData")]
        public float G;
        public float H;
        public float F => H + G;
        public int weight = 0;
        public HexTile parentPath = null;

        [SerializeField] private Sprite[] sprites;
        private SpriteRenderer _spriteRenderer;
        private List<HexTile> neighbourTiles;
        public Vector2Int HexCoords => HexCoordinates.ConvertPositionToOffset(transform.position);
        private Dictionary<object, bool> warningDic = new();

        public void SetWarning(object key, bool isAdd = true)
        {
            if (isAdd)
            {
                warningDic.Add(key, true);
            }
            else
            {
                if (warningDic.ContainsKey(key))
                {
                    warningDic.Remove(key);
                }
            }

            if (warningDic.Values.Where(t => t == true).ToList().Count > 0)
                _spriteRenderer.sprite = sprites[(int)HexState.Warning];
            else
                _spriteRenderer.sprite = sprites[(int)HexState.None];

        }

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

        public static List<Agent> GetData(List<HexTile> tiles)
        {
            return tiles.Select(t => t.overAgent).Where(oa => oa != null).ToList();
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
        public List<HexTile> GetNeighbourTiles(Direction dir, AttackType type)
        {
            List<HexTile> tiles = new List<HexTile>();
            int idir = Mathf.Clamp((int)dir, 0, 5);
            Debug.Log(idir);
            switch (type)
            {
                case AttackType.Around:
                    tiles = neighbourTiles;
                    break;
                case AttackType.Front:
                    if (neighbourTiles[idir] != null)
                    {
                        tiles.Add(neighbourTiles[idir]);
                    }
                    break;
                case AttackType.Fan:

                    for (int i = idir - 1; i <= idir + 1; i++)
                    {
                        if (i == -1)
                        {
                            if (neighbourTiles[5] != null)
                            {
                                tiles.Add(neighbourTiles[5]);
                            }
                        }
                        else if (i == 6)
                        {
                            if (neighbourTiles[0] != null)
                            {
                                tiles.Add(neighbourTiles[0]);
                            }
                        }
                        else if (neighbourTiles[i] != null)
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