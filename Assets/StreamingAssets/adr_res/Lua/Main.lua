require "Global"
function Main()
	print("Game start .............")
	local uiRoot = GameObject.Find("ui_root")
	print(uiRoot)
	local panel = resMgr.LoadPanelSyn("test/panel_test")
	print(panel)
	panel.transform:SetParent(uiRoot.transform, false)
	--GameObject.Instantiate(panel)
	sceneMgr.LoadScene("Loading");
end