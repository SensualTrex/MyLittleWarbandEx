using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.Library;
using TaleWorlds.ScreenSystem;
using MyLittleWarbandEx.ViewModel;


namespace MyLittleWarbandEx.CampaignBehaviors;

public class EquipmentSelectorBehavior : CampaignBehaviorBase
{
    public static GauntletLayer layer;

    public static GauntletMovieIdentifier gauntletMovie;

    public static EquipmentSelectorVM equipmentSelectorVM;

    public static void CreateVMLayer(List<ItemObject> list, CharacterObject troop, string equipmentType)
    {
        if (layer == null)
        {
            layer = new GauntletLayer("EquipmentSelection" ,1001, false);
            if (equipmentSelectorVM == null)
            {
                equipmentSelectorVM = new EquipmentSelectorVM(list, troop, equipmentType);
            }

            
            gauntletMovie = layer.LoadMovie("EquipmentSelection", equipmentSelectorVM);
            equipmentSelectorVM.RefreshValues();
            layer.InputRestrictions.SetInputRestrictions();
            ScreenManager.TopScreen.AddLayer(layer);
            layer.IsFocusLayer = true;
           
            ScreenManager.TrySetFocus(layer);

            
        }
    }

    public static void DeleteVMLayer()
    {
        ScreenBase topScreen = ScreenManager.TopScreen;
        if (layer != null)
        {
            layer.InputRestrictions.ResetInputRestrictions();
            layer.IsFocusLayer = false;
            if (gauntletMovie != null)
            {
                layer.ReleaseMovie(gauntletMovie);
            }

            topScreen.RemoveLayer(layer);
        }

        layer = null;
        gauntletMovie = null;
        equipmentSelectorVM = null;
    }

    public override void RegisterEvents()
    {
    }



    public override void SyncData(IDataStore dataStore)
    {
        var x = dataStore;
        var i = 0;
    }
}