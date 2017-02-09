--region *.lua
--Date
--此文件由[BabeLua]插件自动生成
MainCtrl = {};
local panelName = "MainPanel"
local this = MainCtrl;
local luaBehaviour;

function MainCtrl.New()
    return this;
end

function MainCtrl.PushPanel()
    uiMgr:PushPanel("MainPanel",panelName, this.OnCreate, false);
end

function MainCtrl.HidePanel()
    uiMgr:HidePanel(panelName);
end
--启动事件--
function MainCtrl.OnCreate(obj)
    print(obj.name)
    luaBehaviour = obj:AddComponent(typeof(LuaBehaviour))
    
end


--endregion
