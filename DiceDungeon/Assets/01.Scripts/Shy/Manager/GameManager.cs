using System;
using System.Collections.Generic;
using UnityEngine;

namespace SHY
{
    public enum SceneType
    {
        Stage, Battle
    }

    public class GameManager : MonoBehaviour
    {
        [System.Serializable]
        private class Scene
        {
            public GameObject mapObj;
            public SceneType sceneType;
            public SceneManager manager;
        }

        [SerializeField] private PlayerData playerData;

        [SerializeField] private List<Scene> scenes;

        private void Awake()
        {
            playerData = ScriptableObject.CreateInstance<PlayerData>().Reflect(playerData);

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
                    Debug.Log(playerData);
                    scenes[i].manager.Init(playerData);
                }
            }
        }
    }
}
