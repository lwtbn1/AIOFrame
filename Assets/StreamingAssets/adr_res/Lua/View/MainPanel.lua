--region *.lua
--Date
--此文件由[BabeLua]插件自动生成

MainPanel = {};
local this = MainPanel;
local gameObject = nil;
local transform = nil;

function MainPanel.Init()
    this.mainCtrl = require "Control/MainCtrl";
    this.img_1 = transform:FindChild("img_1"):GetComponent("Image");
    this.img_2 = transform:FindChild("img_2"):GetComponent("Image");
    this.img_3 = transform:FindChild("img_3"):GetComponent("Image");
end
function MainPanel.Awake(obj)
    gameObject = obj;
    transform = gameObject.transform;
    this.Init();
    Util.SetSpriteSyn(this.img_1,"head","head_portrait_2");
    Util.SetSpriteSyn(this.img_2,"head","head_portrait_1");
    Util.SetSpriteSyn(this.img_3,"item","30112004");
end
function MainPanel.OnEnable(obj)


end
function MainPanel.Start(obj)


end
function MainPanel.OnDisable(obj)


end


function MainPanel.OnDestroy(obj)


end

--endregion
