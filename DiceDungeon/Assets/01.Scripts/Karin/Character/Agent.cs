using UnityEngine;

namespace Karin
{
    public class Agent : MonoBehaviour
    {
        private void Start()
        {
            MoveEnd();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                MoveData moveData = new MoveData();
                moveData.who = this;
                moveData.direction = Direction.TopLeft;
                moveData.distance = 1;
                EventManager.Instance.MoveEvent?.Invoke(moveData);
            }
        }

        public void MoveEnd()
        {
            var nowTile = MapManager.Instance.GetTile(transform.position);
            nowTile.moveAble = false;
        }

        public void MoveStart()
        {
            var nowTile = MapManager.Instance.GetTile(transform.position);
            nowTile.moveAble = true;
        }
    }
}