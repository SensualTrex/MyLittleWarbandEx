using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v1;
using MCM.Abstractions.Base.Global;

namespace MyLittleWarbandEx;

internal class MyLittleWarbandSetting : AttributeGlobalSettings<MyLittleWarbandSetting>
{
    public override string Id => "MyLittleWarbandSetting";

    public override string DisplayName => "My Little Warband Setting";

    public override string FolderName => "MyLittleWarbandSetting";

    public override string FormatType => "json";

    [SettingProperty("Spawn At Player Settlement", HintText = "Do custom troops spawn at settlements owned by the player clan?  AI lords visiting player settlements can hire these troops", RequireRestart = false)]
    [SettingPropertyGroup("Setting")]
    public bool SpawnAtPlayerSettlement { get; set; } = false;


    [SettingProperty("Spawn At Player Kingdom", HintText = "Do custom troops spawn at settlements in the kingdom lead by the player clan?  AI lords visiting settlements in player lead kingdom can hire these troops", RequireRestart = false)]
    [SettingPropertyGroup("Setting")]
    public bool SpawnAtPlayerKingdom { get; set; } = false;


    [SettingProperty("Full Unit Editor", HintText = "Can non-custom (vanilla, added by other mods) troops be edited?", RequireRestart = false)]
    [SettingPropertyGroup("Setting")]
    public bool FullUnitEditor { get; set; } = false;


    [SettingProperty("Clan Recruit Custom Troop", HintText = "Do parties part of the player clan recruit custom troops at settlements instead of the default troops?", RequireRestart = false)]
    [SettingPropertyGroup("Setting")]
    public bool ClanRecruitCustomTroop { get; set; } = false;

}