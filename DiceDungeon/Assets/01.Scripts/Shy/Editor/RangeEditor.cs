using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RangeSO))]
[CanEditMultipleObjects]
public class RangeEditor : Editor
{
    private RangeSO range;
    private int[] arrX = { 4, 5, 6, 7, 6, 5, 4 };

    private void OnEnable()
    {
        range = (RangeSO)target;
    }

    public override void OnInspectorGUI()
    {
        for (int y = 0; y < 7; y++)
        {
            EditorGUILayout.BeginHorizontal();

            GUILayout.Space(10 * (10 - arrX[y]));

            for (int x = 0; x < arrX[y]; x++)
            {
                GUILayout.Space(5);
                bool oldValue = range.arr[y, x]; // 기존 값 저장
                bool newValue = EditorGUILayout.Toggle(oldValue, GUILayout.Width(10));

                if (oldValue != newValue) // 값이 변경된 경우
                {
                    range.arr[y, x] = newValue;
                    EditorUtility.SetDirty(target); // 변경 사항 저장
                }
            }

            EditorGUILayout.EndHorizontal();
        }


        base.OnInspectorGUI();
    }
}
