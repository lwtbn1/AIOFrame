using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HotFixProj.UI.UIStack;

namespace HotFixProj.UI
{
    public class UIGroup
    {
        public List<UIComponent> mLitComs;
        public E_UIState mCurState = E_UIState.Default;
        public bool mCloseDestroy = false;
        public ComPopAction mPopAction;
        public void OnPush()
        {

        }

        public void OnPop()
        {

        }
    }

    public class UIComponent
    {
        public UIComponent(string name, ComPushAction pushAction)
        {
            mName = name;
            mPushAction = pushAction;
        }
        public readonly string mName;
        public readonly ComPushAction mPushAction;
        
    }

    public enum ComPushAction
    {
        NotCreate,
        CreateAndHide,
        CreateAndShow
    }

    public enum ComPopAction
    {
        Destroy,
        Hide
    }
}
