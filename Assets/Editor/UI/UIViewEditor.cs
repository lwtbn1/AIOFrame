using AIOFrame.UI;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UIEditor
{
    [CustomEditor(typeof(UIView), true)]
    public class UIViewEditor : Editor
    {
        static int CELLIX = 0;
        private UIView uIView;
        public override void OnInspectorGUI()
        {
            uIView = serializedObject.targetObject as UIView;
            if (GUILayout.Button("添加控制的UI元素"))
            {
                CELLIX = 0;
                FindNode(uIView.transform, uIView.uiCells);
            }
            if (GUILayout.Button("清除控制的UI元素"))
            {
                CELLIX = 0;
                uIView.Clear();
            }
        }
        void FindNode(Transform parent, Dictionary<int, UICell> dic)
        {
            if(parent.childCount > 0)
            {
                foreach(Transform p in parent)
                {
                    dic.Add(CELLIX, new UICell() {
                        id = CELLIX,
                        name = p.name,
                        gameObject = p.gameObject
                    });
                    CELLIX++;
                    FindNode(p, dic);
                }
            }
        }
    }
}

