using karin;
using karin.Charactor;
using UnityEngine;
using UnityEngine.UI;

public class DirectionChanger : MonoBehaviour
{
    private GameObject _dirSelector;
    private Image _showArrow;

    [SerializeField]private Agent _target;
    [SerializeField] private Direction _direction;

    [SerializeField, Tooltip("0 : Left\n1 : TopLeft\n2 : TopRight\n3 : Right\n4 : BottomRight\n5 : BottomLeft")]
    private Sprite[] sprites;

    private bool childOn = false;

    private void OnEnable()
    {
        _dirSelector = transform.Find("Dir Selector").gameObject;
        _showArrow = transform.Find("Show Dir").GetComponent<Image>();
        SetDirection(_target.direction);
    }

    public void ChangeNextDirection()
    {
        childOn = !childOn;

        _dirSelector.SetActive(childOn);
       

        //int nextIdx = (int)_direction + 1;
        //if (nextIdx > sprites.Length - 1)
        //{
        //    nextIdx -= sprites.Length;
        //}
        //_direction = (Direction)nextIdx;
        //_target.direction = _direction;
        //_showImage.sprite = sprites[nextIdx];
    }

    public void SetDirection(int _dir) => SetDirection((Direction)_dir);

    public void SetDirection(Direction dir)
    {
        _direction = dir;
        _target.direction = dir;
        _dirSelector.SetActive(false);
        _showArrow.sprite = sprites[(int)dir];
    }
}
