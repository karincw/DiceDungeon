using UnityEngine;

namespace SHY
{
    public abstract class SceneManager : MonoBehaviour
    {
        public virtual void Init(PlayerData _data) { }
        public virtual void Fin() { }
    }
}
