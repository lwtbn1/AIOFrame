--region *.lua
--Date
--此文件由[BabeLua]插件自动生成
module("View/MainPanel", package.seeall)
MainPanel = {};
local this = MainPanel;
local gameObject = nil;
local transform = nil;

function MainPanel.Init(obj)
    gameObject = obj;
    transform = gameObject.transform;

    this.img_1 = transform:FindChild("img_1"):GetComponent("Image");
    this.img_2 = transform:FindChild("img_2"):GetComponent("Image");
    this.img_3 = transform:FindChild("img_3"):GetComponent("Image");
    this.CloseButton = transform:FindChild("CloseButton").gameObject;

    print("MainPanel.Awake............");
end

function MainPanel.OnEnable(obj)


end
function MainPanel.Start(obj)


end
function MainPanel.OnDisable(obj)


end


function MainPanel.OnDestroy(obj)


end

return MainPanel;
--endregion
