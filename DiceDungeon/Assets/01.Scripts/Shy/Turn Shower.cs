using UnityEngine;

public class TurnShower : MonoBehaviour
{
    [SerializeField] private Transform p;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            for (int i = 0; i < p.childCount; i++)
            {
                if(p.GetChild(i).gameObject.activeSelf)
                {
                    p.GetChild(i).gameObject.SetActive(false);
                    break;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            for (int i = 0; i < p.childCount; i++)
            {
                if (!p.GetChild(i).gameObject.activeSelf)
                {
                    p.GetChild(i).gameObject.SetActive(true);
                    break;
                }
            }
        }
    }
}
