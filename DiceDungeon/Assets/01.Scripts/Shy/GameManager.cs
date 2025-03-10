using UnityEngine;

namespace SHY
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData;

        [SerializeField] private BattleManager battleManager;
        [SerializeField] private StageManager stageManager;

        private void Awake()
        {
            playerData = new PlayerData(playerData);
        }

        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                battleManager.Initialize(playerData);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {

            }
        }
    }
}
