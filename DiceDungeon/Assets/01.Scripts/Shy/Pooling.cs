using System.Collections.Generic;
using UnityEngine;

namespace SHY
{
    public enum PoolEnum
    {
        None,
        LR, //LineRenderer
        StageUI,
        TurnShower
    }

    public class Pooling : SingleTon<Pooling>
    {
        [System.Serializable]
        private class PoolData
        {
            public PoolEnum callName = PoolEnum.None;
            public GameObject obj;
            public int spawnCnt;
        }

        private Dictionary<PoolEnum, PoolData> dic = new Dictionary<PoolEnum, PoolData>();
        [SerializeField] private List<PoolData> poolDatas;

        private void Awake()
        {
            for (int i = 0; i < poolDatas.Count; i++)
            {
                for (int j = 0; j < poolDatas[i].spawnCnt; j++) SpawnItem(poolDatas[i]);
                dic.Add(poolDatas[i].callName, poolDatas[i]);
            }
        }

        private GameObject SpawnItem(PoolData _data)
        {
            GameObject obj = Instantiate(_data.obj, transform);
            obj.SetActive(false);
            obj.name = _data.callName.ToString();
            return obj;
        }

        public GameObject GetItem(PoolEnum _itemEnum, Transform _parent, bool _active = true)
        {
            GameObject obj;
            try { obj = transform.Find(_itemEnum.ToString()).gameObject; }
            catch { obj = SpawnItem(dic[_itemEnum]); }
            
            if (_parent != null) obj.transform.SetParent(_parent);

            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            obj.SetActive(_active);
            return obj;
        }

        public void ReturnItem(GameObject _obj)
        {
            _obj.SetActive(false);
            _obj.transform.SetParent(transform);
        }
    }
}