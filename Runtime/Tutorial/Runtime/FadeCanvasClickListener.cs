using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Boshphelm.Tutorial
{
    public class FadeCanvasClickListener : MonoBehaviour, IPointerDownHandler
    {
        public event Action onPointerDown;

        public void OnPointerDown(PointerEventData eventData)
        {
            onPointerDown?.Invoke();
        }
    }
}