using System.Collections.Generic;
using UnityEngine;

namespace Boshphelm.Sample.SimpleItemAndSlot
{
    public class SimpleItemSlotUIViewManager
    {
        private readonly Transform _slotParent;
        private readonly GameObject _simpleItemSlotPrefab;

        private readonly List<SimpleItemSlotUIView> _slotViews = new List<SimpleItemSlotUIView>();

        public SimpleItemSlotUIViewManager(Transform slotParent, GameObject simpleItemSlotPrefab)
        {
            _slotParent = slotParent;
            _simpleItemSlotPrefab = simpleItemSlotPrefab;
        }

        public void Initialize(List<SimpleItemSlot> itemSlots)
        {
            ClearExistingSlots();

            for (int i = 0; i < itemSlots.Count; i++)
            {
                CreateSlotUI(itemSlots[i]);
            }
        }

        private void CreateSlotUI(SimpleItemSlot slot)
        {
            var newSlotUI = Object.Instantiate(_simpleItemSlotPrefab, _slotParent);
            var simpleItemSlotUIView = newSlotUI.GetComponent<SimpleItemSlotUIView>();
            simpleItemSlotUIView.Initialize(slot);
            _slotViews.Add(simpleItemSlotUIView);
        }

        private void ClearExistingSlots()
        {
            for (int i = _slotViews.Count - 1; i >= 0; i--)
            {
                var slotView = _slotViews[i];
                Object.Destroy(slotView.gameObject);
                _slotViews.RemoveAt(i);
            }

            _slotViews.Clear();
        }
    }
}
