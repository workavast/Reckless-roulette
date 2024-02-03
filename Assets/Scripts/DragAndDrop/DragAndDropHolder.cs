using UnityEngine;

namespace DragAndDrop
{
    public class DragAndDropHolder
    {
        private GameObject _dragObject;
        
        // public void OnBeginDrag(PointerEventData eventData)
        // {
        //     _isDrag = true;
        // }
        //
        // public void OnEndDrag(PointerEventData eventData)
        // {
        //     GameObject target = eventData.pointerCurrentRaycast.gameObject;
        //     
        //     if (target.TryGetComponent(out GlobalDropZone globalDropZone))
        //         UseCard();    
        //     else
        //         _rectTransform.position = _currentCardLinePosition;
        //     
        //     _isDrag = false;
        // }
        //
        // public void OnDrag(PointerEventData eventData)
        // {
        //     _rectTransform.position = eventData.position;
        // }
    }
}