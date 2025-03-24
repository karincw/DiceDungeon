using UnityEngine;

namespace SHY
{
    public class StageCamera : SingleTon<StageCamera>
    {
        [SerializeField] private Transform stageScene;
        [SerializeField] private float wheelSpeed = 5f;
        public bool canUse = false;

        internal float min;

        private float yLimit;

        private void Awake()
        {
            yLimit = stageScene.localPosition.y;
        }

        private void Update()
        {
            float w = -Input.GetAxisRaw("Mouse ScrollWheel") * wheelSpeed;
            float yValue = stageScene.localPosition.y + w;
            yValue = Mathf.Max(yValue, yLimit);
            yValue = Mathf.Min(yValue, yLimit * min);


            stageScene.localPosition = new Vector3(stageScene.localPosition.x, yValue);
        }
    }
}