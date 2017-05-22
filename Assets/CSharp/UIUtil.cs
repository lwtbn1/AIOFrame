using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUtil  {

    public static void PanelSetParent(GameObject parent, GameObject child)
    {
        RectTransform childRect = child.GetComponent<RectTransform>();
        RectTransform parentRect = parent.GetComponent<RectTransform>();
        childRect.SetParent(parentRect, false);
        childRect.sizeDelta = Vector2.zero;
        childRect.anchoredPosition = Vector3.zero;
        childRect.localScale = Vector3.one;
    }

    public static GameObject FindInChild(GameObject obj, string name)
    {
        if (obj != null && obj.name == name)
            return obj;
        else
        {
            for (var i = 0; i < obj.transform.childCount; i++)
            {
                GameObject target =FindInChild(obj.transform.GetChild(i).gameObject, name);
                if (target != null)
                    return target;
            }
        }
        return null;
    }
}
