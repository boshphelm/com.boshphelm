using System;
using System.Collections.Generic;
using UnityEngine;

namespace boshphelm.Utility
{
    public abstract class Snap : MonoBehaviour
    {
        [SerializeField] protected List<RectTransform> snapItems;
        [SerializeField] protected RectTransform scrollPanel;

        [SerializeField] private RectTransform _center;

        [SerializeField] protected bool vertical;

        protected float distanceBetweenItems;
        public int ItemCount => snapItems.Count;

        private int _snappedIndex;

        public int SnappedIndex
        {
            get => _snappedIndex;
            set
            {
                _snappedIndex = Mathf.Clamp(value, 0, ItemCount - 1);
                OnSnappedIndexUpdated.Invoke(_snappedIndex);
            }
        }

        public Vector2 SnappedIndexPosition => snapPositionCalculator.GetSnappedIndexPosition(SnappedIndex, distanceBetweenItems);

        protected SnapPositionCalculator snapPositionCalculator;


        public Action<int> OnSnappedIndexUpdated = _ => { };

        protected void Initialize()
        {
            snapPositionCalculator = new SnapPositionCalculator(vertical, scrollPanel, distanceBetweenItems);

            if (ItemCount < 2)
            {
                Debug.LogError("SNAPPED ITEM COUNT CANNOT BE LOWER THAN 2");
                return;
            }

            distanceBetweenItems = snapPositionCalculator.AnchoredDistance(snapItems[0], snapItems[1]);
        }

        protected void AddItem(RectTransform newItem) => snapItems.Add(newItem);

        public virtual void MoveToIndex(int index)
        {
            SnappedIndex = index;
        }

        public virtual void MoveToNextIndex()
        {
            SnappedIndex++;
        }

        public virtual void MoveToPreviousIndex()
        {
            SnappedIndex--;
        }

        public virtual void MoveDirectlyToIndex(int index)
        {
            var targetPosition = Vector2.zero;

            SnappedIndex = index;

            var position = SnappedIndex * -distanceBetweenItems;

            scrollPanel.anchoredPosition = snapPositionCalculator.GetTargetPositionOnScrollPanel(position); //targetPosition;
        }


        protected int FindClosestItemIndex()
        {
            var closestIndex = 0;
            var closestDistance = Mathf.Infinity;

            for (var i = 0; i < snapItems.Count; i++)
            {
                var distance = snapPositionCalculator.Distance(_center, snapItems[i]);

                if (distance > closestDistance) continue;

                closestDistance = distance;
                closestIndex = i;
            }

            return closestIndex;
        }
    }

    public class SnapPositionCalculator
    {
        private readonly bool _vertical;
        private readonly RectTransform _scrollPanel;
        private readonly float _distanceBetweenItems;

        public SnapPositionCalculator(bool vertical, RectTransform scrollPanel, float distanceBetweenItems)
        {
            _vertical = vertical;
            _scrollPanel = scrollPanel;
            _distanceBetweenItems = distanceBetweenItems;
        }

        public float AnchoredDistance(RectTransform a, RectTransform b) =>
            _vertical
                ? Mathf.Abs(a.anchoredPosition.y - b.anchoredPosition.y)
                : Mathf.Abs(a.anchoredPosition.x - b.anchoredPosition.x);

        public float Distance(Transform a, Transform b) =>
            _vertical
                ? Mathf.Abs(a.position.y - b.position.y)
                : Mathf.Abs(a.position.x - b.position.x);

        public Vector2 GetTargetPositionOnScrollPanel(float position) =>
            _vertical
                ? new Vector2(_scrollPanel.anchoredPosition.x, position)
                : new Vector2(position, _scrollPanel.anchoredPosition.y);

        public Vector2 GetSnappedIndexPosition(int snappedIndex, float distanceBetweenItems)
        {
            var position = snappedIndex * -distanceBetweenItems;
            return _vertical
                ? new Vector2(_scrollPanel.anchoredPosition.x, position)
                : new Vector2(position, _scrollPanel.anchoredPosition.y);
        }
    }
}