using UnityEngine;

namespace SHY
{
    public static class CanClick
    {
        public static bool clickAble = true;
        public static void False() { Debug.Log("Can Click : false"); clickAble = false; }
        public static void True() { Debug.Log("Can Click : true"); clickAble = true; }
    }
}