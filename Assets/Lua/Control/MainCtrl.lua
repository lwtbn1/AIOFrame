--region *.lua
--Date
--此文件由[BabeLua]插件自动生成
module("Control/MainCtrl", package.seeall)
MainCtrl = {};
local panelName = "MainPanel"
local this = MainCtrl;
this.luaBehaviour = nil;
local panel = require "View/MainPanel";
function MainCtrl.New()
    return this;
end

function MainCtrl.PushPanel()
    uiMgr:PushPanel("MainPanel",panelName, this, false);
    
end

function MainCtrl.HidePanel()
    print("MainCtrl.HidePanel ................." .. panelName);
    uiMgr:HidePanel(panelName);
end

function MainCtrl.OnExitButtonClick(obj)
    MainCtrl.HidePanel();
end
--启动事件--
function MainCtrl.OnCreate(obj)
    print(obj.name)
    panel.Init(obj);
    this.luaBehaviour = obj:GetComponent(typeof(LuaBehaviour))
    this.luaBehaviour:AddClick(panel.CloseButton,MainCtrl.OnExitButtonClick);
    
    MainCtrl.Enable();
end

function MainCtrl.Enable()
    Util.SetSpriteSyn(panel.img_1,"head","head_portrait_2");
    Util.SetSpriteSyn(panel.img_2,"head","head_portrait_1");
    Util.SetSpriteSyn(panel.img_3,"item","30112004");
    LuaHelper.AddUpdateEvent(MainCtrl.Update, MainCtrl);
end

function MainCtrl.Update()
    print("MainCtrl.Update  ......");
end
return MainCtrl;
--endregion
