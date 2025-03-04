using System;
using UnityEngine;

namespace Karin
{

    public class EventManager : SingleTon<EventManager>
    {
        public event Action<AttackData>  AttackEvent;
        public event Action<ShieldData>  ShieldEvent;
        public event Action<MoveData>    MoveEvent;
        public event Action<BuffData>    BuffEvent;

    }

}