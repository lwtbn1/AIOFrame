--region *.lua
--Date
--此文件由[BabeLua]插件自动生成
MainCtrl = {};
local this = MainCtrl;
this.panelName = "MainPanel";
this.luaBehaviour = nil;
this.panel = require "View/MainPanel";

function MainCtrl.New()
    return this;
end

function MainCtrl.PushPanel()
    uiMgr:PushPanel("MainPanel",this.panelName, this, false);
    
end

function MainCtrl.HidePanel()
    print("MainCtrl.HidePanel ................." .. this.panelName);
    uiMgr:HidePanel(panelName);
end
--启动事件--
function MainCtrl.OnCreate(obj)
    print(obj.name)
    this.luaBehaviour = obj:GetComponent(typeof(LuaBehaviour))
    this.luaBehaviour:AddClick(this.panel.ui.CloseButton,MainCtrl.OnExitButtonClick);
    
    MainCtrl.Enable();
end

function MainCtrl.OnExitButtonClick(obj)
    MainCtrl.HidePanel();
end

function MainCtrl.Enable()
    Util.SetSpriteSyn(this.panel.ui.img_1,"head","head_portrait_2");
    Util.SetSpriteSyn(this.panel.ui.img_2,"head","head_portrait_1");
    Util.SetSpriteSyn(this.panel.ui.img_3,"item","30112004");
    --LuaHelper.AddUpdateEvent(MainCtrl.Update, MainCtrl);
end

function MainCtrl.Update()
    print("MainCtrl.Update  ......");
end
--endregion
