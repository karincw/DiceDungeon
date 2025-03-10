using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RangeSO))]
public class RangeEditor : Editor
{
    private RangeSO range;
    private int[] _arrX;

    private void OnEnable()
    {
        range = (RangeSO)target;
        _arrX = range.arrX;
    }

    public override void OnInspectorGUI()
    {
        int p = 0;

        for (int y = 0; y < 7; y++)
        {
            EditorGUILayout.BeginHorizontal();

            GUILayout.Space(10 * (10 - _arrX[y]));

            for (int x = 0; x < _arrX[y]; x++)
            {
                GUILayout.Space(5);
                range.arr[p] = EditorGUILayout.Toggle(range.arr[p], GUILayout.Width(10));

                p++;
            }

            EditorGUILayout.EndHorizontal();
        }

        EditorUtility.SetDirty(target);
        serializedObject.ApplyModifiedProperties();

        base.OnInspectorGUI();
    }
}
