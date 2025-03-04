using UnityEngine;

public class MapGenerator : SingleTon<MapGenerator>
{
    [SerializeField] private int _radius;
    [SerializeField] private GameObject _tilePrefab;

    public void Awake()
    {
        for (int i = -_radius; i <= _radius; i++)
        {
            for (int j = -_radius; j <= _radius; j++)
            {
                GameObject tile = Instantiate(_tilePrefab);
                tile.transform.position = HexCoordinates.ConvertOffsetToPosition(new Vector2Int(i, j));
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(HexCoordinates.ConvertPositionToOffset(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
        }

    }
}
