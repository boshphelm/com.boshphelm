using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Boshphelm.Utility
{
    public class ScrollSnap : Snap, IEndDragHandler, IBeginDragHandler
    {
        [SerializeField] private HorizontalLayoutGroup _horizontalLayoutGroup;
        [SerializeField] private GameObject _previousButtonGO;

        [SerializeField] private GameObject _nextButtonGO;

        [SerializeField] private float _lerpSpeed = 8f;

        private bool _isDragging;
        private bool _active;

        private void Awake()
        {
            _horizontalLayoutGroup.CalculateLayoutInputHorizontal();
        }

        private void Start()
        {
            Setup();
        }

        public void Setup()
        {
            Initialize();

            SetNextPreviousButtonStatus();
        }

        private void Update()
        {
            if (snapItems.Count < 2) return;

            distanceBetweenItems = snapPositionCalculator.AnchoredDistance(snapItems[0], snapItems[1]);

            if (!_active) return;
            if (!_isDragging) LerpToSnappedIndexPosition();
        }

        protected void LerpToSnappedIndexPosition()
        {
            var newPosition = Vector2.Lerp(scrollPanel.anchoredPosition, SnappedIndexPosition, Time.deltaTime * _lerpSpeed);

            var sqrDistance = Vector2.SqrMagnitude(newPosition - scrollPanel.anchoredPosition);

            var closeEnoughToStopLerp = sqrDistance < .01f;
            _active = !closeEnoughToStopLerp;

            scrollPanel.anchoredPosition = newPosition;
        }

        public override void MoveToNextIndex()
        {
            base.MoveToNextIndex();

            SetNextPreviousButtonStatus();
            _isDragging = false;
            _active = true;
        }

        public override void MoveToPreviousIndex()
        {
            base.MoveToPreviousIndex();

            SetNextPreviousButtonStatus();
            _isDragging = false;
            _active = true;
        }

        public override void MoveDirectlyToIndex(int index)
        {
            base.MoveDirectlyToIndex(index);

            SetNextPreviousButtonStatus();
        }

        public override void MoveToIndex(int index)
        {
            base.MoveToIndex(index);

            SetNextPreviousButtonStatus();
            _isDragging = false;
            _active = true;
        }


        #region DragEvents

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isDragging = true;
            _active = false;
        }

        public void OnEndDrag(PointerEventData data)
        {
            SnappedIndex = FindClosestItemIndex();
            SetNextPreviousButtonStatus();
            _isDragging = false;
            _active = true;
        }

        #endregion

        private void SetNextPreviousButtonStatus()
        {
            if (SnappedIndex == 0)
            {
                _nextButtonGO?.SetActive(true);
                _previousButtonGO?.SetActive(false);
            }
            else if (SnappedIndex == ItemCount - 1)
            {
                _nextButtonGO?.SetActive(false);
                _previousButtonGO?.SetActive(true);
            }
            else
            {
                _nextButtonGO?.SetActive(true);
                _previousButtonGO?.SetActive(true);
            }
        }
    }
}