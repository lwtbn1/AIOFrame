require "Define"
require "Common/Util"
require "Manager/CtrlMgr"
require "Manager/ViewMgr"

function Main()
    CtrlMgr.Init();
    ViewMgr.Init();


    local mainCtrl = CtrlMgr.GetCtrl(CtrlNames.Main);
    mainCtrl.PushPanel();
    

end

