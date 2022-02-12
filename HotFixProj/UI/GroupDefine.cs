using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFixProj.UI
{
    public class GroupDefine
    {
        public static Dictionary<string, UIGroup> uis = new Dictionary<string, UIGroup>() {
            {"MyFunc1Win",
                new UIGroup(){
                    mLitComs = {
                        new UIComponent("win1", ComPushAction.NotCreate),
                        new UIComponent("win2", ComPushAction.CreateAndHide),
                        new UIComponent("win3", ComPushAction.CreateAndShow),
                    },
                    mPopAction = ComPopAction.Hide
                } },
            {"MyFunc2Win",
                new UIGroup(){ } },
        };
    }
}
