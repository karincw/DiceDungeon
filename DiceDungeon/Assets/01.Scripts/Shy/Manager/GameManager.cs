using System;
using System.Collections.Generic;
using UnityEngine;

namespace SHY
{
    public enum SceneType
    {
        Stage, Battle
    }

    public class GameManager : SingleTon<GameManager>
    {
        [System.Serializable]
        private class Scene
        {
            public GameObject mapObj;
            public SceneType sceneType;
            public SceneManager manager;
        }

        public PlayerData playerData;

        [SerializeField] private List<Scene> scenes;

        private void Awake()
        {
            playerData = ScriptableObject.CreateInstance<PlayerData>().Reflect(playerData);

            StagePlayer.Instance.fin += SceneChange;
        }

        private void Start()
        {
            if(onStart)
            SceneChange(debuggingScene);
        }

        #region Debuging
        [Header("Alpha 1"), SerializeField] private SceneType debuggingScene;
        public bool onStart = true;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) SceneChange(debuggingScene);
        }
        #endregion

        public void SceneChange(SceneType _scene)
        {
            for (int i = 0; i < scenes.Count; i++)
            {
                bool isScene = scenes[i].sceneType == _scene;

                scenes[i].mapObj.SetActive(isScene);

                if(isScene)
                {
                    Debug.Log(playerData);
                    scenes[i].manager.Init(playerData);
                }
            }
        }
    }
}
