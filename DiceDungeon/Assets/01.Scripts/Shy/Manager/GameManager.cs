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
            playerData = new PlayerData(playerData);
            StagePlayer.Instance.fin += SceneChange;
        }

        private void Start()
        {
            SceneChange(SceneType.Stage);
        }

        private void SceneChange(SceneType _scene)
        {
            for (int i = 0; i < scenes.Count; i++)
            {
                bool isScene = scenes[i].sceneType == _scene;

                scenes[i].mapObj.SetActive(isScene);

                if(isScene)
                {
                    scenes[i].manager.Init(playerData);
                }
            }
        }
    }
}
