using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace karin.Inventory
{

    public class UIOpenManager : MonoSingleton<UIOpenManager>
    {
        public List<UIOpener> LeftOpener, RightOpener, TopOpener, BottomOpener;

        [SerializeField] private InputReaderSO _inputReader;
        public UnityEvent OpenInventory;

        private void Awake()
        {
            _inputReader.OnOpenInventoryEvent += HandleInventory;
        }

        private void OnDestroy()
        {
            _inputReader.OnOpenInventoryEvent -= HandleInventory;
        }

        public void HandleInventory()
        {
            OpenInventory?.Invoke();
        }

        public void OpenDefencer(UIOpener opener, Position position, bool state = false)
        {
            List<UIOpener> openers = null;
            switch (position)
            {
                case Position.Left:
                    openers = LeftOpener;
                    break;
                case Position.Right:
                    openers = RightOpener;
                    break;
                case Position.Top:
                    openers = TopOpener;
                    break;
                case Position.Bottom:
                    openers = BottomOpener;
                    break;
            }

            openers.ForEach(o =>
            {
                if (o != opener)
                {
                    o.BtnInteract(state);
                }
            });

        }

        public void ListSetup(Position position)
        {
            List<UIOpener> openers = null;
            switch (position)
            {
                case Position.Left:
                    openers = LeftOpener;
                    break;
                case Position.Right:
                    openers = RightOpener;
                    break;
                case Position.Top:
                    openers = TopOpener;
                    break;
                case Position.Bottom:
                    openers = BottomOpener;
                    break;
            }

            openers.ForEach(o =>
            {
                o.BtnInteract(true);
            });

        }

        public void CloseAll()
        {
            LeftOpener.ForEach(t => t.CloseAll());
            RightOpener.ForEach(t => t.CloseAll());
            TopOpener.ForEach(t => t.CloseAll());
            BottomOpener.ForEach(t => t.CloseAll());
        }

    }
}