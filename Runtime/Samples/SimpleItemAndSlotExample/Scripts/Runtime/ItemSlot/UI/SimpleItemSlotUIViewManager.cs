using System.Collections.Generic;
using UnityEngine;

namespace Boshphelm.Sample.SimpleItemAndSlot
{
    public class SimpleItemSlotUIViewManager
    {
        private readonly Transform _slotParent;
        private readonly GameObject _simpleItemSlotPrefab;

        private readonly Dictionary<SimpleItemSlot, SimpleItemSlotUIView> _simpleItemSlots = new Dictionary<SimpleItemSlot, SimpleItemSlotUIView>();

        public SimpleItemSlotUIViewManager(Transform slotParent, GameObject simpleItemSlotPrefab)
        {
            _slotParent = slotParent;
            _simpleItemSlotPrefab = simpleItemSlotPrefab;
        }

        public void CreateSlotUI(SimpleItemSlot slot)
        {
            var newSlotUI = Object.Instantiate(_simpleItemSlotPrefab, _slotParent); // TODO: Make Pool For It.

            var simpleItemSlotUIView = newSlotUI.GetComponent<SimpleItemSlotUIView>();
            simpleItemSlotUIView.Initialize(slot);

            _simpleItemSlots.Add(slot, simpleItemSlotUIView);
        }

        public void RemoveSlotUIBySlot(SimpleItemSlot slot)
        {
            if (!_simpleItemSlots.TryGetValue(slot, out var simpleItemSlotUIView)) return;

            Object.Destroy(simpleItemSlotUIView.gameObject); // TODO: Release To Pool
            _simpleItemSlots.Remove(slot);
        }

        private void ClearExistingSlots()
        {
            foreach (var simpleItemSlot in _simpleItemSlots)
            {
                var slotView = simpleItemSlot.Value;
                Object.Destroy(slotView.gameObject);
            }

            _simpleItemSlots.Clear();
        }
    }
}
