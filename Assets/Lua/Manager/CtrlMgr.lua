--region *.lua
--Date
--此文件由[BabeLua]插件自动生成
CtrlMgr = {};
local this = CtrlMgr;
local ctrlList = {};
function CtrlMgr.Init()
    ctrlList[CtrlNames.Main] = require "Control/MainCtrl".New();
end

function CtrlMgr.GetCtrl(name)
    print("getCtrl ".. name)
    return ctrlList[name]
end
--endregion
