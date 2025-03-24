using UnityEngine;
using UnityEngine.UI;

public class DeadPanel : MonoBehaviour
{
    [SerializeField] private InputReaderSO _inputReader;

    private Button _retryBtn;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _retryBtn = transform.Find("RetryButton").GetComponent<Button>();
        _retryBtn.onClick.AddListener(RetryGame);
    }

    private void OnDestroy()
    {
        _retryBtn.onClick.RemoveAllListeners();
    }

    public void openPanel()
    {
        _inputReader.DisableInput();
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }

    public void RetryGame()
    {
        //다시시작
    }

}
