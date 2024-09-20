using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Logic.UI
{
    public class PointerInputObserver : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public Action PointerDowned;
        public Action<PointerEventData> PointerDownedWithData;
        public Action PointerUped;
        public Action<PointerEventData> PointerUpedWithData;

        public void OnPointerDown(PointerEventData eventData)
        {
            PointerDowned?.Invoke();
            PointerDownedWithData?.Invoke(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            PointerUped?.Invoke();
            PointerUpedWithData?.Invoke(eventData);
        }
    }
}

