using System.Collections.Generic;
using UnityEngine;

namespace karin.HexMap
{
    public class MapManager : SingleTon<MapManager>
    {
        [Header("MapGenerate")]
        [SerializeField] private int _radius;
        [SerializeField] private GameObject _tilePrefab;

        [Header("Value")]
        public List<HexTile> tiles;


        private void Awake()
        {
            for (int i = 0; i < tiles.Count; i++)
            {
                if (tiles[i] == null)
                    tiles.Remove(tiles[i]);
            }
        }

        [ContextMenu("MakeMap")]
        public void MakeMap()
        {
            tiles = new List<HexTile>();
            for (int i = -_radius; i <= _radius; i++)
            {
                for (int j = -_radius; j <= _radius; j++)
                {
                    GameObject tile = Instantiate(_tilePrefab);
                    tile.transform.parent = transform;
                    tile.transform.position = HexCoordinates.ConvertOffsetToPosition(new Vector2Int(i, j));
                    tiles.Add(tile.GetComponent<HexTile>());
                }
            }
        }

        public HexTile GetTile(Vector2Int hexCoords) => MapManager.Instance.tiles.Find(tile => tile.HexCoords == hexCoords);
        public HexTile GetTile(Vector2 position) => MapManager.Instance.tiles.Find(tile => tile.HexCoords == HexCoordinates.ConvertPositionToOffset(position));

    }
}