Util = {}

function Util.SetSpriteSyn(image, atlas_name ,sp_name)
    image.sprite = resMgr:LoadSpriteSyn(atlas_name, sp_name)
end
