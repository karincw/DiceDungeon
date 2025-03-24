using UnityEngine;

public class StageCamera : SingleTon<StageCamera>
{
    [SerializeField] private Transform stageScene;
    [SerializeField] private float wheelSpeed = 5f;
    public bool canUse = false;

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
        yValue = Mathf.Min(yValue, yLimit * 5.1f);


        stageScene.localPosition = new Vector3(stageScene.localPosition.x, yValue);
    }
}
