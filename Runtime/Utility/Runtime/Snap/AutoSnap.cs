using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boshphelm.Utility
{
    public class AutoSnap : Snap
    {
        protected bool active = false;
        [SerializeField] private float moveSpeed = 8f;
        [SerializeField] private float reachStop = .01f;

        private void Update()
        {
            if (!active) return;
            LerpToTargetPosition(SnappedIndex * -distanceBetweenItems);
        }

        protected void LerpToTargetPosition(float pos)
        {
            float newPos = 0;
            var newPosition = Vector2.zero;
            if (!vertical)
            {
                newPos = Mathf.Lerp(scrollPanel.anchoredPosition.x, pos, Time.deltaTime * moveSpeed);
                newPosition = new Vector2(newPos, scrollPanel.anchoredPosition.y);
                if (Mathf.Abs(newPos - scrollPanel.anchoredPosition.x) < reachStop) active = false;
            }
            else
            {
                newPos = Mathf.Lerp(scrollPanel.anchoredPosition.y, pos, Time.deltaTime * moveSpeed);
                newPosition = new Vector2(scrollPanel.anchoredPosition.x, newPos);
                if (Mathf.Abs(newPos - scrollPanel.anchoredPosition.y) < reachStop) active = false;
            }

            scrollPanel.anchoredPosition = newPosition;
        }

        public override void MoveToIndex(int index)
        {
            base.MoveToIndex(index);

            active = true;
        }

        public override void MoveDirectlyToIndex(int index)
        {
            base.MoveDirectlyToIndex(index);
        }
    }
}