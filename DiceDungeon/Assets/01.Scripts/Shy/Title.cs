using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using CanClick = SHY.CanClick;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [SerializeField] private CanvasGroup group;
    [SerializeField] private TextMeshProUGUI[] startBtTxt;
    [SerializeField] private TextMeshProUGUI finBtTxt;

    [SerializeField] private Button startBt;
    [SerializeField] private Button finBt;

    private void Start()
    {
        CanClick.False();

        group.alpha = 0;
        for (int i = 0; i < startBtTxt.Length; i++)
        {
            string mes = "";
            for (int j = 0; j < i; j++)
            {
                mes += "   ";
            }
            startBtTxt[i].text = mes;
        }
        finBtTxt.text = "";
        finBtTxt.gameObject.SetActive(false);

        StartCoroutine(Appear("시작하기", (trm) => StartCoroutine(OnJumping(trm))));
    }

    private IEnumerator Appear(string _mes, UnityAction<Transform> _action)
    {
        yield return new WaitForSeconds(0.7f);

        while (group.alpha < 1)
        {
            group.alpha += 0.1f;
            yield return new WaitForSeconds(0.08f);
        }

        for (int i = 0; i < _mes.Length; i++)
        {
            startBtTxt[i].text += _mes[i];
            yield return new WaitForSeconds(0.25f);
        }

        CanClick.True();

        for (int i = 0; i < _mes.Length; i++)
        {
            _action?.Invoke(startBtTxt[i].transform);
            yield return new WaitForSeconds(0.33f);
        }

        yield return new WaitForSeconds(0.2f);

        _mes = "종료하기";
        finBtTxt.gameObject.SetActive(true);
        for (int i = 0; i < _mes.Length; i++)
        {
            finBtTxt.text += _mes[i];
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator OnJumping(Transform _trm)
    {
        float y = 0, power = 1.4f;
        bool upper = true;

        while (true)
        {
            y += (upper ? 1f : -1f) * power;
            
            if(y <= -10)
            {
                upper = true;
            }
            if (y >= 20)
            {
                upper = false;
            }

            _trm.transform.localPosition = new Vector3(0, y, 0);

            yield return new WaitForSeconds(0.025f);
        }
    }



    public void OnStartBt()
    {
        if (!CanClick.clickAble) return;
        Debug.Log("시작");
        SceneManager.LoadScene("Dice");
    }

    public void OnFinBt()
    {
        if (!CanClick.clickAble) return;
        if (!finBtTxt.gameObject.activeSelf) return;

        Debug.Log("나가기");

        Application.Quit();
    }
}
