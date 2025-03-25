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
        private SceneType nowScene;

        private void Awake()
        {
            playerData = playerData.Reflect();
            //playerData.DiceInit();

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

                if (scenes[i].sceneType == nowScene) scenes[i].manager.Fin();

                scenes[i].mapObj.SetActive(isScene);

                if(isScene)
                {
                    scenes[i].manager.Init(playerData);
                }
            }

            nowScene = _scene;
        }
    }
}
