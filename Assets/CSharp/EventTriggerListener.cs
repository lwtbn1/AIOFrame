using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.EventSystems;
using UnityEngine;
class EventTriggerListener : EventTrigger
{
    public Action<PointerEventData> onBeginDrag;
    public Action<PointerEventData> onDrag;
    public Action<PointerEventData> onEndDrag;
    public Action<PointerEventData> onClick;
    public static EventTriggerListener Get(GameObject obj)
    {
        EventTriggerListener listener = obj.GetComponent<EventTriggerListener>();
        if(listener == null)
            listener = obj.AddComponent<EventTriggerListener>();
        return listener;
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (onBeginDrag != null)
            onBeginDrag(eventData);
    }
    public override void OnDrag(PointerEventData eventData)
    {
        if (onDrag != null)
            onDrag(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (onEndDrag != null)
            onEndDrag(eventData);
    }


    public override void OnPointerClick(PointerEventData eventData)
    {
        if (onClick != null)
            onClick(eventData);
    }

}
