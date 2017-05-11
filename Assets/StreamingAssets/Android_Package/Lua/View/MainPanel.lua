--region *.lua
--Date
--此文件由[BabeLua]插件自动生成
MainPanel = {};
local this = MainPanel;
this.ui = {};
this.gameObject = nil;
this.transform = nil;

function MainPanel.Awake(obj)
    this.gameObject = obj;
    this.transform = obj.transform;

    this.ui.img_1 = this.transform:FindChild("img_1"):GetComponent("Image");
    this.ui.img_2 = this.transform:FindChild("img_2"):GetComponent("Image");
    this.ui.img_3 = this.transform:FindChild("img_3"):GetComponent("Image");
    this.ui.CloseButton = this.transform:FindChild("CloseButton").gameObject;
    print("MainPanel.Awake.................");
    
end


function MainPanel.OnEnable(obj)


end
function MainPanel.Start(obj)


end
function MainPanel.OnDisable(obj)
    print("mainPanel OnDisable ......");
end


function MainPanel.OnDestroy(obj)
    print("mainPanel OnDisable ......");
end

return this;
--endregion
