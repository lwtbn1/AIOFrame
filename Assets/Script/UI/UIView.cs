using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AIOFrame.UI
{
    [ExecuteInEditMode]
    public class UIView : MonoBehaviour
    {
        public UICell[] lit;
        [SerializeField]
        public Dictionary<int, UICell> uiCells = new Dictionary<int, UICell>();
        private Dictionary<string, int> name2Id = new Dictionary<string, int>();
        public void CacheIx(string name, int id)
        {
            name2Id.Add(name, id);
        }
        public void Clear()
        {
            uiCells.Clear();
            name2Id.Clear();
        }
        public void SetImage(int id, int resId)
        {

        }
    }
}

