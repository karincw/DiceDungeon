using karin;
using karin.Charactor;
using UnityEngine;
using UnityEngine.UI;

public class DirectionChanger : MonoBehaviour
{
    private Button _button;
    private Image _showImage;
    [SerializeField]private Agent _target;
    [SerializeField] private Direction _direction;

    [SerializeField, Tooltip("0 : Left\n1 : TopLeft\n2 : TopRight\n3 : Right\n4 : BottomRight\n5 : BottomLeft")]
    private Sprite[] sprites;

    private void OnEnable()
    {
        _button = GetComponent<Button>();
        _showImage = transform.Find("ShowImage").GetComponent<Image>();
        _button.onClick.AddListener(ChangeNextDirection);
        SetDirection(_target.direction);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ChangeNextDirection);
    }

    private void ChangeNextDirection()
    {
        int nextIdx = (int)_direction + 1;
        if (nextIdx > sprites.Length - 1)
        {
            nextIdx -= sprites.Length;
        }
        _direction = (Direction)nextIdx;
        _target.direction = _direction;
        _showImage.sprite = sprites[nextIdx];
    }
    private void SetDirection(Direction dir)
    {
        _direction = dir;
        _showImage.sprite = sprites[(int)dir];
    }
}
