--region *.lua
--Date
--此文件由[BabeLua]插件自动生成


ViewMgr = {};
local this = ViewMgr;

function ViewMgr.Init()
    require ("View/" .. tostring(PanelNames.Main));
end

--endregion
