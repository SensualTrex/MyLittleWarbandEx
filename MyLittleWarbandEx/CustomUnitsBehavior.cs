
using HarmonyLib;
using HarmonyLib.BUTR.Extensions;
using MCM.Abstractions.Base.Global;
using MyLittleWarbandEx;
using MyLittleWarbandEx.CampaignBehaviors;
using MyLittleWarbandEx.Patches;
using MyLittleWarbandEx.ViewModel;
using NavalDLC;
using StoryMode.GauntletUI.Tutorial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Transactions;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.Recruitment;
using TaleWorlds.Core;
using TaleWorlds.Core.ImageIdentifiers;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Core.ViewModelCollection.ImageIdentifiers;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.Library;
using TaleWorlds.LinQuick;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ObjectSystem;
using TaleWorlds.ScreenSystem;

namespace MyLittleWarbandEx;

internal class CustomUnitsBehavior : CampaignBehaviorBase
{
    
    public static GauntletLayer layer;

    public static GauntletMovieIdentifier gauntletMovie;

    public static CustomUnitsVM customUnitVM;

    public static Dictionary<string, CustomUnit> CustomUnits = new Dictionary<string, CustomUnit>();

    public static List<int> updateSlots;

    public static List<int> _filter_tiers = new List<int>();

    public static List<CultureObject> _filter_Culture = new List<CultureObject>();

    public static List<ItemObject.ItemTypeEnum> _filter_weapon_types = new List<ItemObject.ItemTypeEnum>();
    public static List<WeaponClass> _filter_weapon_classes = new List<WeaponClass>();

    public static List<ArmorComponent.ArmorMaterialTypes> _filter_armour_types = new List<ArmorComponent.ArmorMaterialTypes>();

    private MyLittleWarbandSetting instance = GlobalSettings<MyLittleWarbandSetting>.Instance;

    public static bool _disable_gear_restriction;

    public static bool _disable_skill_total_restriction;

    public static bool _disable_skill_cap_restriction;

    private static CultureObject _emptyCulture;
    private static CultureObject EmptyCulture {
        get {
            if (_emptyCulture == null) {
                _emptyCulture = new CultureObject();
                _emptyCulture.StringId = "None";
                var propInfo = typeof(BasicCultureObject).GetProperty("Name", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                propInfo.SetValue(_emptyCulture, new TextObject("None"));
            }
            return _emptyCulture;
        }
        set
        {
            _emptyCulture = value;
        }
    }
    public static void CreateVMLayer(string unitId)
    {
        if (layer == null)
        {
            
            layer = new GauntletLayer("CustomUnits",1000);
            if (customUnitVM == null)
            {
                customUnitVM = new CustomUnitsVM(unitId);
            }

            customUnitVM.RefreshValues();
            gauntletMovie = layer.LoadMovie("CustomUnits", customUnitVM);
            
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
        customUnitVM = null;
    }

    public static void UpgradeGear(string equipmentType, string unitId)
    {
        List<InquiryElement> list = new List<InquiryElement>();
        CharacterObject @object = Game.Current.ObjectManager.GetObject<CharacterObject>(unitId);
        //objects
        List<Equipment> list2 = @object.BattleEquipments.ToList();
        for (int i = 0; i < list2.Count; i++)
        {   
            list.Add(new InquiryElement(i, i + 1 + " : " + ((list2[i][ToEquipmentSlot(equipmentType)].Item == null) ? "Empty" : list2[i][ToEquipmentSlot(equipmentType)].Item.Name.ToString()), (list2[i][ToEquipmentSlot(equipmentType)].Item == null) ? new ItemImageIdentifier(null) : new ItemImageIdentifier(list2[i][ToEquipmentSlot(equipmentType)].Item)));
        }

        MBInformationManager.ShowMultiSelectionInquiry(new MultiSelectionInquiryData("Select variartions sets to change", "", list, isExitShown: true, 1, list2.Count, "Continue", null, delegate (List<InquiryElement> args)
        {
            if (args == null || args.Any())
            {
                InformationManager.HideInquiry();
                updateSlots = new List<int>();
                foreach (InquiryElement arg in args)
                {
                    updateSlots.Add((int)arg.Identifier);
                }

                SubModule.ExecuteActionOnNextTick(delegate
                {
                    UpgradeGear2(equipmentType, unitId);
                });
            }
        }, null));
    }

    internal static void CopyTemplate(string unitId)
    {
        List<InquiryElement> list = new List<InquiryElement>();
        CharacterObject editedUnit = Game.Current.ObjectManager.GetObject<CharacterObject>(unitId);
        foreach (CharacterObject character in Campaign.Current.Characters)
        {
            if (character != null && !character.IsTemplate && !character.HiddenInEncyclopedia && character.HeroObject == null && (character.Tier <= editedUnit.Tier || _disable_gear_restriction) && (character.Occupation == Occupation.Soldier || character.Occupation == Occupation.Mercenary || character.Occupation == Occupation.Bandit || character.Occupation == Occupation.Gangster || character.Occupation == Occupation.CaravanGuard))
            { 
               
                list.Add(new InquiryElement(character, character.Name.ToString(), new CharacterImageIdentifier(CharacterCode.CreateFrom(character)), isEnabled: true, GetHint(character)));
            }
        }

        list.Sort(Compare2);
        MBInformationManager.ShowMultiSelectionInquiry(new MultiSelectionInquiryData("Select template to copy from", "", list, isExitShown: true, 1, 1, "Continue", null, delegate (List<InquiryElement> args)
        {
            if (args == null || args.Any())
            {
                InformationManager.HideInquiry();
                CharacterObject template = args.Select((InquiryElement element) => element.Identifier as CharacterObject).First();
                SubModule.ExecuteActionOnNextTick(delegate
                {
                    CopyCharacter(template, editedUnit);
                    update(unitId);
                });
            }
        }, null));
    }

    private static string GetHint(CharacterObject character)
    {
        string text = "";
        text = text + "level : " + character.Level + "\n";
        text = text + "tier : " + character.Tier + "\n";
        text = text + "culture : " + character.Culture.ToString() + "\n";
        text = text + "one handed : " + character.GetSkillValue(DefaultSkills.OneHanded) + "\n";
        text = text + "two handed : " + character.GetSkillValue(DefaultSkills.TwoHanded) + "\n";
        text = text + "polearm : " + character.GetSkillValue(DefaultSkills.Polearm) + "\n";
        text = text + "bow : " + character.GetSkillValue(DefaultSkills.Bow) + "\n";
        text = text + "crossbow : " + character.GetSkillValue(DefaultSkills.Crossbow) + "\n";
        text = text + "throwing : " + character.GetSkillValue(DefaultSkills.Throwing) + "\n";
        text = text + "riding : " + character.GetSkillValue(DefaultSkills.Riding) + "\n";
        return text + "athletics : " + character.GetSkillValue(DefaultSkills.Athletics) + "\n";
    }

    public static void UpgradeGear2(string equipmentType, string unitId)
    {
        List<ItemObject> list = new List<ItemObject>();
        CharacterObject @object = Game.Current.ObjectManager.GetObject<CharacterObject>(unitId);
        foreach (ItemObject objectType in MBObjectManager.Instance.GetObjectTypeList<ItemObject>())
        {
            if (getRequiredItemType(ToEquipmentSlot(equipmentType)).Contains(objectType.Type) && ((int)objectType.Tier <= @object.Tier || _disable_gear_restriction) && MatchesFilter(objectType) && !objectType.IsCraftedByPlayer)
            {
                list.Add(objectType);
            }
        }

        EquipmentSelectorBehavior.CreateVMLayer(list, @object, equipmentType);
    }

    private static bool MatchesFilter(ItemObject item)
    {
        return MatchesFilter(item, _filter_tiers, _filter_Culture, _filter_weapon_classes, _filter_armour_types);
    }

    public static bool MatchesFilter(ItemObject item, List<int> FilterTiers, List<CultureObject> FilterCulture, List<WeaponClass> FilterWeaponTypes, List<ArmorComponent.ArmorMaterialTypes> FilterArmourTypes)
    {
        bool flag = FilterTiers.IsEmpty() || FilterTiers.Contains((int)(item.Tier + 1));
        bool flag2 = FilterCulture.IsEmpty() || (item.Culture != null && FilterCulture.Contains(item.Culture)) || (FilterCulture.Contains(EmptyCulture) && item.Culture == null );

        bool flag3 = FilterWeaponTypes.IsEmpty();
        if (!flag3)
        {
            if (item.WeaponComponent != null)
            {
                foreach (var WeaponComponent in item.WeaponComponent.Weapons)
                {
                    flag3 = (flag3 || FilterWeaponTypes.Contains(WeaponComponent.WeaponClass));
                }
            }
        }

        bool flag4 = FilterArmourTypes.IsEmpty() || item == null || item.ArmorComponent == null || FilterArmourTypes.Contains(item.ArmorComponent.MaterialType);
        return flag && flag2 && flag3 && flag4;
    }

    internal static void FilterWeaponsPopup(FilterableViewModel viewModel = null)
    {
        var weaponClasses = Enum.GetNames(typeof(TaleWorlds.Core.WeaponClass)).ToList();
        List<WeaponClass> localWeaponsClasses = new List<WeaponClass>();

        List<ItemObject.ItemTypeEnum> localWeaponsList = new List<ItemObject.ItemTypeEnum>();
        List<InquiryElement> list = new List<InquiryElement>();
        List<InquiryElement> WeaponClassList = new List<InquiryElement>();
        foreach (var weaponClass in weaponClasses)
        {
            WeaponClassList.Add(new InquiryElement(weaponClass, weaponClass, null));
        }

        MBInformationManager.ShowMultiSelectionInquiry(new MultiSelectionInquiryData("Filter by weapon type", "", WeaponClassList, isExitShown: true, 1, list.Count, "Filter", "Clear", delegate (List<InquiryElement> args)
        {
            if (args == null || args.Any())
            {
                InformationManager.HideInquiry();
                IEnumerable<string> enumerable = args.Select((InquiryElement element) => element.Identifier as string);
                viewModel.FilterWeaponTypes.Clear();
                foreach (string item in enumerable)
                {
                    viewModel.FilterWeaponTypes.Add(stringToWeaponClass(item));
                }
                viewModel.RefreshData();
            }
        }, delegate (List<InquiryElement> args)
        {
            if (args == null || args.Any())
            {
                localWeaponsClasses.Clear();
                viewModel.RefreshData();
            }
        }));
    }

    public static void FilterWeapons()
    {
        FilterWeaponsPopup(customUnitVM);
    }

    internal static void FilterArmourPopup(FilterableViewModel viewModel = null)
    {
        List<ArmorComponent.ArmorMaterialTypes> localArmourTypes = new List<ArmorComponent.ArmorMaterialTypes>();
        List<InquiryElement> list = new List<InquiryElement>();
        list.Add(new InquiryElement("Chainmail", "Chainmail", null));
        list.Add(new InquiryElement("Cloth", "Cloth", null));
        list.Add(new InquiryElement("Leather", "Leather", null));
        list.Add(new InquiryElement("Plate", "Plate", null));
        MBInformationManager.ShowMultiSelectionInquiry(new MultiSelectionInquiryData("Filter by armour type", "", list, isExitShown: true, 1, list.Count, "Continue", null, delegate (List<InquiryElement> args)
        {
            if (args == null || args.Any())
            {
                InformationManager.HideInquiry();
                IEnumerable<string> enumerable = args.Select((InquiryElement element) => element.Identifier as string);
                localArmourTypes.Clear();
                foreach (string item in enumerable)
                {
                    localArmourTypes.Add(stringToArmourType(item));
                }

                viewModel.RefreshValues();
            }
        }, null));
    }

    public static void FilterArmour()
    {
        FilterArmourPopup(customUnitVM);
    }

    private static ArmorComponent.ArmorMaterialTypes stringToArmourType(string s)
    {
        return s switch
        {
            "Chainmail" => ArmorComponent.ArmorMaterialTypes.Chainmail,
            "Cloth" => ArmorComponent.ArmorMaterialTypes.Cloth,
            "Leather" => ArmorComponent.ArmorMaterialTypes.Leather,
            "Plate" => ArmorComponent.ArmorMaterialTypes.Plate,
            _ => ArmorComponent.ArmorMaterialTypes.None,
        };
    }

    private static TaleWorlds.Core.WeaponClass stringToWeaponClass(string s)
    {
        WeaponClass weaponClass;

        // TryParse<TEnum>(string value, bool ignoreCase, out TEnum result)
        if(Enum.TryParse<WeaponClass>(s, true, out weaponClass))
        {
            return weaponClass;
        }
        return TaleWorlds.Core.WeaponClass.Undefined;
    }

    private static ItemObject.ItemTypeEnum stringToWeaponType(string s)
    {
        return s switch
        {
            "Arrows" => ItemObject.ItemTypeEnum.Arrows,
            "Bolts" => ItemObject.ItemTypeEnum.Bolts,
            "Bow" => ItemObject.ItemTypeEnum.Bow,
            "Bullets" => ItemObject.ItemTypeEnum.Bullets,
            "Crossbow" => ItemObject.ItemTypeEnum.Crossbow,
            "Musket" => ItemObject.ItemTypeEnum.Musket,
            "One Handed Weapon" => ItemObject.ItemTypeEnum.OneHandedWeapon,
            "Pistol" => ItemObject.ItemTypeEnum.Pistol,
            "Polearm" => ItemObject.ItemTypeEnum.Polearm,
            "Shield" => ItemObject.ItemTypeEnum.Shield,
            "Thrown" => ItemObject.ItemTypeEnum.Thrown,
            "Two Handed Weapon" => ItemObject.ItemTypeEnum.TwoHandedWeapon,
            _ => ItemObject.ItemTypeEnum.Invalid,
        };
    }

    public static void FilterCultures()
    {
        FilterTiersPopup(customUnitVM);
    }

    public static void FilterCulturesPopup(FilterableViewModel viewModel = null)
    {       
        List<CultureObject> list = new List<CultureObject>();
        List<InquiryElement> list2 = new List<InquiryElement>();
        List<CultureObject> filterList = list;
        var isSelected = false;
        foreach (Kingdom kingdom in Campaign.Current.Kingdoms)
        {
            if (kingdom != null && !list.Contains(kingdom.Culture))
            {
                list.Add(kingdom.Culture);
                list2.Add(new InquiryElement(kingdom.Culture, kingdom.Culture.Name.ToString(), new BannerImageIdentifier(kingdom.Banner)));
            }
        }

        list.Add(EmptyCulture);
        list2.Add(new InquiryElement(EmptyCulture, "None", new BannerImageIdentifier(null)));

        MBInformationManager.ShowMultiSelectionInquiry(new MultiSelectionInquiryData("Filter by equipment culture", "", list2, isExitShown: true, 1, list2.Count, "Continue", null, delegate (List<InquiryElement> args)
        {
            if (args == null || args.Any())
            {
                InformationManager.HideInquiry();
                IEnumerable<CultureObject> enumerable = args.Select((InquiryElement element) => element.Identifier as CultureObject);
                viewModel.FilterCulture.Clear();
                //filterList.Clear();
                foreach (CultureObject item in enumerable)
                {
                    viewModel.FilterCulture.Add(item);
                }
                viewModel.RefreshData();
            }
        }, null));
    }

    public static void FilterTiers()
    {
        FilterTiersPopup(customUnitVM);
    }

    public static void FilterTiersPopup(FilterableViewModel viewModel = null)
    {
        var filterList = new List<int>();
        List<InquiryElement> list = new List<InquiryElement>();
        list.Add(new InquiryElement("0", "0", null));
        list.Add(new InquiryElement("1", "1", null));
        list.Add(new InquiryElement("2", "2", null));
        list.Add(new InquiryElement("3", "3", null));
        list.Add(new InquiryElement("4", "4", null));
        list.Add(new InquiryElement("5", "5", null));
        list.Add(new InquiryElement("6", "6", null));
        MBInformationManager.ShowMultiSelectionInquiry(new MultiSelectionInquiryData("Filter by equipment tier", "", list, isExitShown: true, 1, 6, "Continue", null, delegate (List<InquiryElement> args)
        {
            if (args == null || args.Any())
            {
                InformationManager.HideInquiry();
                IEnumerable<string> enumerable = args.Select((InquiryElement element) => element.Identifier as string);
                viewModel.FilterTiers.Clear();
                foreach (string item in enumerable)
                {
                    viewModel.FilterTiers.Add(int.Parse(item));
                }

                viewModel.RefreshData();
            }
        }, null));
    }



    private static bool isWeaponType(ItemObject.ItemTypeEnum type)
    {
        if ((uint)(type - 2) <= 8u || (uint)(type - 16) <= 2u)
        {
            return true;
        }

        return false;
    }

    public static void UpgradeGearFinalize(EquipmentIndex equipmentIndex, ItemObject item, string unitId)
    {
        foreach (int updateSlot in updateSlots)
        {
            ChangeUnitEquipment((int)equipmentIndex, Game.Current.ObjectManager.GetObject<CharacterObject>(unitId), item, updateSlot);
        }

        update(unitId);
    }

    public static void update(string unitId, bool refresh = true)
    {
        SetDefaultGroup(unitId);
        if (refresh)
        {
            customUnitVM.RefreshValues();
        }

        if (CustomUnits.TryGetValue(unitId, out var value))
        {
            value.SaveUpdate();
        }
        else
        {
            CustomUnits.Add(unitId, new CustomUnit(unitId));
        }

        if (CustomUnits.TryGetValue(unitId + "0", out var value2))
        {
            value2.checkHorseNeeded();
        }

        if (CustomUnits.TryGetValue(unitId + "1", out var value3))
        {
            value3.checkHorseNeeded();
        }
    }

    public static void ToggleMarineStatus(string unitId)
    {
        CharacterObject @object = Game.Current.ObjectManager.GetObject<CharacterObject>(unitId);
        int MarineStatus = @object.IsNavalSoldier() ? 0 : 1;
        var @ObjectRef = AccessTools2.FieldRefAccess<CharacterObject, PropertyOwner<TraitObject>>("_characterTraits");
        PropertyOwner<TraitObject> CharacterTraits = @ObjectRef.Invoke(@object);
        CharacterTraits.SetPropertyValue(DefaultTraits.NavalSoldier, MarineStatus);
        Traverse.Create(@object).Property("_characterTraits").SetValue(CharacterTraits);
    }

    private static void ChangeUnitEquipment(int slot, CharacterObject character, ItemObject item, int set, bool isCivilian = false)
    {
        var Equipment = character.Equipment;
        List<Equipment> list = character.BattleEquipments.Where((Equipment x) => x.IsCivilian).ToList();
        List<Equipment> list2 = character.BattleEquipments.Where((Equipment x) => !x.IsCivilian).ToList();
        EquipmentElement value = ((item == null) ? default(EquipmentElement) : new EquipmentElement(item));
        if (isCivilian)
        {
            list[set][slot] = value;
        }
        else
        {
            list2[set][slot] = value;
        }

        list2.AddRange(list);
        UpdateSelectedUnitEquipment(character, list2);
    }

    private static void UpdateSelectedUnitEquipment(CharacterObject character, List<Equipment> equipments)
    {
        MBEquipmentRoster mBEquipmentRoster = new MBEquipmentRoster();
        ((FieldInfo)GetInstanceField(mBEquipmentRoster, "_equipments")).SetValue(mBEquipmentRoster, new MBList<Equipment>(equipments));
        ((FieldInfo)GetInstanceField((BasicCharacterObject)character, "_equipmentRoster")).SetValue(character, mBEquipmentRoster);
        character.InitializeEquipmentsOnLoad(character);
    }

    internal static object GetInstanceField<T>(T instance, string fieldName)
    {
        BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
        return typeof(T).GetField(fieldName, bindingAttr);
    }

    private static int Compare2(InquiryElement x, InquiryElement y)
    {
        if (x == null || x.Identifier == null)
        {
            return -1;
        }

        if (y == null || y.Identifier == null)
        {
            return 1;
        }

        return string.Compare(((CharacterObject)x.Identifier).Name.ToString(), ((CharacterObject)y.Identifier).Name.ToString());
    }

    public static void UpdateSkill(SkillObject skill, int amount, string unitId)
    {
        CharacterObject @object = Game.Current.ObjectManager.GetObject<CharacterObject>(unitId);
        MBCharacterSkills mBCharacterSkills = MBObjectManager.Instance.CreateObject<MBCharacterSkills>(@object.StringId);
        mBCharacterSkills.Skills.SetPropertyValue(DefaultSkills.Crossbow, @object.GetSkillValue(DefaultSkills.Crossbow));
        mBCharacterSkills.Skills.SetPropertyValue(DefaultSkills.Bow, @object.GetSkillValue(DefaultSkills.Bow));
        mBCharacterSkills.Skills.SetPropertyValue(DefaultSkills.Throwing, @object.GetSkillValue(DefaultSkills.Throwing));
        mBCharacterSkills.Skills.SetPropertyValue(DefaultSkills.OneHanded, @object.GetSkillValue(DefaultSkills.OneHanded));
        mBCharacterSkills.Skills.SetPropertyValue(DefaultSkills.TwoHanded, @object.GetSkillValue(DefaultSkills.TwoHanded));
        mBCharacterSkills.Skills.SetPropertyValue(DefaultSkills.Polearm, @object.GetSkillValue(DefaultSkills.Polearm));
        mBCharacterSkills.Skills.SetPropertyValue(DefaultSkills.Athletics, @object.GetSkillValue(DefaultSkills.Athletics));
        mBCharacterSkills.Skills.SetPropertyValue(DefaultSkills.Riding, @object.GetSkillValue(DefaultSkills.Riding));
        mBCharacterSkills.Skills.SetPropertyValue(skill, Math.Min(skillCap(unitId), (@object.GetSkillValue(skill) + amount > 0) ? (@object.GetSkillValue(skill) + amount) : 0));
        FieldInfo field = @object.GetType().GetField("DefaultCharacterSkills", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        if (null != field)
        {
            field.SetValue(@object, mBCharacterSkills);
        }

        update(unitId);
    }

    public static int skillCap(string unitId)
    {
        if (_disable_skill_cap_restriction)
        {
            return 1023;
        }

        CharacterObject @object = Game.Current.ObjectManager.GetObject<CharacterObject>(unitId);
        switch (@object.Tier)
        {
            case 0:
            case 1:
                return 25;
            case 2:
                return 50;
            case 3:
                return 90;
            case 4:
                return 120;
            case 5:
                return 170;
            case 6:
                return 260;
            default:
                return 330;
        }
    }

    public static void rename(string unitId)
    {
        InformationManager.ShowTextInquiry(new TextInquiryData("Rename", "Enter new name", isAffirmativeOptionShown: true, isNegativeOptionShown: true, "Procede", "Cancel", delegate (string s)
        {
            newName(s, unitId);
        }, null));
    }

    private static void newName(string s, string unitId)
    {
        CharacterObject @object = Game.Current.ObjectManager.GetObject<CharacterObject>(unitId);
        typeof(BasicCharacterObject).GetMethod("SetName", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(@object, new object[1]
        {
            new TextObject(s)
        });
        update(unitId);
    }

    public static void ChangeCulture(string unitId)
    {
        List<CultureObject> list = new List<CultureObject>();
        List<InquiryElement> list2 = new List<InquiryElement>();
        foreach (Kingdom kingdom in Campaign.Current.Kingdoms)
        {
            if (kingdom != null && !list.Contains(kingdom.Culture))
            {
                list.Add(kingdom.Culture);
               ;
                list2.Add(new InquiryElement(kingdom.Culture, kingdom.Culture.Name.ToString(), new BannerImageIdentifier(kingdom.Banner)));
            }
        }

        MBInformationManager.ShowMultiSelectionInquiry(new MultiSelectionInquiryData("Select Culture", "", list2, isExitShown: true, 1, 1, "Continue", null, delegate (List<InquiryElement> args)
        {
            if (args == null || args.Any())
            {
                InformationManager.HideInquiry();
                CultureObject culture = args.Select((InquiryElement element) => element.Identifier as CultureObject).First();
                SubModule.ExecuteActionOnNextTick(delegate
                {
                    SetCulture(culture, unitId);
                });
            }
        }, null));
    }

    private static void SetCulture(CultureObject culture, string unitId)
    {
        CharacterObject @object = Game.Current.ObjectManager.GetObject<CharacterObject>(unitId);
        Traverse.Create(@object).Property("Culture").SetValue(culture);
        updateAppearance(@object);
        update(unitId);
    }

    public static void ChangeGender(string unitId)
    {
        CharacterObject @object = Game.Current.ObjectManager.GetObject<CharacterObject>(unitId);
        @object.IsFemale = !@object.IsFemale;
        updateAppearance(@object);
        update(unitId);
    }

    public static void AddUpgrade(string unitId)
    {
        update(unitId);
        CharacterObject @object = Game.Current.ObjectManager.GetObject<CharacterObject>(unitId);
        List<CharacterObject> list = new List<CharacterObject>();
        if (@object.UpgradeTargets != null)
        {
            for (int i = 0; i < @object.UpgradeTargets.Length; i++)
            {
                list.Add(@object.UpgradeTargets[i]);
            }
        }

        if (list.Count > 1)
        {
            return;
        }

        if (Game.Current.ObjectManager.GetObject<CharacterObject>(unitId + "0") != null && !list.Contains(Game.Current.ObjectManager.GetObject<CharacterObject>(unitId + "0")))
        {
            CharacterObject object2 = Game.Current.ObjectManager.GetObject<CharacterObject>(unitId + "0");
            CopyCharacter(@object, object2);
            list.Add(object2);
            update(unitId + "0");
        }
        else
        {
            if (Game.Current.ObjectManager.GetObject<CharacterObject>(unitId + "1") == null || list.Contains(Game.Current.ObjectManager.GetObject<CharacterObject>(unitId + "1")))
            {
                InformationManager.DisplayMessage(new InformationMessage("This unit is max tier"));
                return;
            }

            CharacterObject object3 = Game.Current.ObjectManager.GetObject<CharacterObject>(unitId + "1");
            CopyCharacter(@object, object3);
            list.Add(object3);
            update(unitId + "1");
        }

        typeof(CharacterObject).GetProperty("UpgradeTargets").SetValue(@object, list.ToArray(), null);
        update(unitId);
    }

    public static void RemoveUpgrade(string unitId, CharacterObject upgrade)
    {
        CharacterObject @object = Game.Current.ObjectManager.GetObject<CharacterObject>(unitId);
        List<CharacterObject> list = new List<CharacterObject>();
        for (int i = 0; i < @object.UpgradeTargets.Length; i++)
        {
            list.Add(@object.UpgradeTargets[i]);
        }

        list.Remove(upgrade);
        typeof(CharacterObject).GetProperty("UpgradeTargets").SetValue(@object, (list.Count > 0) ? ((object?)list.ToArray()) : ((object?)new CharacterObject[0]), null);
        update(unitId);
    }

    public static void SetDefaultGroup(string unitId)
    {
        CharacterObject @object = Game.Current.ObjectManager.GetObject<CharacterObject>(unitId);
        int DefaultFormation = 0;
        var newFormationClass = FormationClass.Infantry;

        if (isRanged(@object) && !isMounted(@object))
        {
            DefaultFormation = 1;
            newFormationClass = FormationClass.Ranged;
        }
        else if (isRanged(@object) && isMounted(@object))
        {
            DefaultFormation = 2;
            newFormationClass = FormationClass.HorseArcher;
        }
        else if (!isRanged(@object) && isMounted(@object))
        {
            DefaultFormation = 3;
            newFormationClass = FormationClass.Cavalry;
        }
        Traverse.Create(@object).Property("DefaultFormationClass").SetValue(newFormationClass);
        @object.DefaultFormationGroup = DefaultFormation;
    }

    private static bool isMounted(CharacterObject @object)
    {
        return (@object.Equipment[10].Item != null);
    }

    private static bool isRanged(CharacterObject character)
    {
        for (int i = 0; i < 4; i++)
        {
            ItemObject item = character.Equipment[i].Item;
            if (item != null && (item.ItemType == ItemObject.ItemTypeEnum.Bow || item.ItemType == ItemObject.ItemTypeEnum.Crossbow))
            {
                return true;
            }
        }

        return false;
    }

    public static void CopyCharacter(CharacterObject orginalCharacter, CharacterObject newCharacter)
    {
        EquipmentIndex[] array = new EquipmentIndex[11]
        {
            EquipmentIndex.WeaponItemBeginSlot,
            EquipmentIndex.Weapon1,
            EquipmentIndex.Weapon2,
            EquipmentIndex.Weapon3,
            EquipmentIndex.NumAllWeaponSlots,
            EquipmentIndex.Cape,
            EquipmentIndex.Body,
            EquipmentIndex.Gloves,
            EquipmentIndex.Leg,
            EquipmentIndex.ArmorItemEndSlot,
            EquipmentIndex.HorseHarness
        };
        if (CustomUnits.TryGetValue(orginalCharacter.StringId, out var value))
        {
            EquipmentIndex[] array2 = array;
            foreach (EquipmentIndex equipmentIndex in array2)
            {
                for (int j = 0; j < value.Equipment.Length; j++)
                {
                    if (value.Equipment[j] != null)
                    {
                        if (value.Equipment[j].GetEquipmentFromSlot(equipmentIndex).Item != null && value.Equipment[j].GetEquipmentFromSlot(equipmentIndex).Item.Name != null)
                        {
                            ChangeUnitEquipment((int)equipmentIndex, newCharacter, value.Equipment[j].GetEquipmentFromSlot(equipmentIndex).Item, j);
                        }
                        else
                        {
                            ChangeUnitEquipment((int)equipmentIndex, newCharacter, null, j);
                        }
                    }
                }
            }

            typeof(CharacterObject).GetProperty("UpgradeTargets").SetValue(newCharacter, new CharacterObject[0], null);
        }
        else
        {
            value = new CustomUnit(orginalCharacter.StringId);
           
            EquipmentIndex[] array3 = array;
            foreach (EquipmentIndex equipmentIndex2 in array3)
            {
                for (int l = 0; l < newCharacter.BattleEquipments.Where((Equipment x) => !x.IsCivilian).ToList().Count; l++)
                {
                    if (value.Equipment[l % value.Equipment.Length] != null)
                    {
                        if (value.Equipment[l % value.Equipment.Length].GetEquipmentFromSlot(equipmentIndex2).Item != null && value.Equipment[l % value.Equipment.Length].GetEquipmentFromSlot(equipmentIndex2).Item.Name != null)
                        {
                            ChangeUnitEquipment((int)equipmentIndex2, newCharacter, value.Equipment[l % value.Equipment.Length].GetEquipmentFromSlot(equipmentIndex2).Item, l);
                        }
                        else
                        {
                            ChangeUnitEquipment((int)equipmentIndex2, newCharacter, null, l);
                        }
                    }
                }
            }
        }

        MBCharacterSkills mBCharacterSkills = MBObjectManager.Instance.CreateObject<MBCharacterSkills>(newCharacter.StringId);
        mBCharacterSkills.Skills.SetPropertyValue(DefaultSkills.Crossbow, value.Crossbow);
        mBCharacterSkills.Skills.SetPropertyValue(DefaultSkills.Bow, value.Bow);
        mBCharacterSkills.Skills.SetPropertyValue(DefaultSkills.Throwing, value.Throwing);
        mBCharacterSkills.Skills.SetPropertyValue(DefaultSkills.OneHanded, value.OneHand);
        mBCharacterSkills.Skills.SetPropertyValue(DefaultSkills.TwoHanded, value.TwoHand);
        mBCharacterSkills.Skills.SetPropertyValue(DefaultSkills.Polearm, value.Polearm);
        mBCharacterSkills.Skills.SetPropertyValue(DefaultSkills.Athletics, value.Athletics);
        mBCharacterSkills.Skills.SetPropertyValue(DefaultSkills.Riding, value.Riding);
        FieldInfo field = newCharacter.GetType().GetField("CharacterSkills", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        if (null != field)
        {
            field.SetValue(newCharacter, mBCharacterSkills);
        }

        newCharacter.IsFemale = orginalCharacter.IsFemale;
        // Harmony Traverse to copy the culture
        var origCulture = orginalCharacter.Culture;
        Traverse.Create(newCharacter).Property("Culture").SetValue(origCulture);
        
        AccessTools.Property(typeof(CharacterObject), "BodyPropertyRange").SetValue(newCharacter, orginalCharacter.BodyPropertyRange, null);
    }

    private static void updateAppearance(CharacterObject character)
    {
        if ((character.IsFemale ? character.Culture.Townswoman : character.Culture.Townsman) != null)
        {
            MBBodyProperty value = (character.IsFemale ? character.Culture.FemaleDancer.BodyPropertyRange : character.Culture.BasicTroop.BodyPropertyRange);
            AccessTools.Property(typeof(CharacterObject), "BodyPropertyRange").SetValue(character, value, null);
        }
        else
        {
            MBBodyProperty value2 = (character.IsFemale ? Game.Current.ObjectManager.GetObject<CharacterObject>("female_dancer_empire").BodyPropertyRange : Game.Current.ObjectManager.GetObject<CharacterObject>("imperial_recruit").BodyPropertyRange);
            AccessTools.Property(typeof(CharacterObject), "BodyPropertyRange").SetValue(character, value2, null);
        }
    }

    public static EquipmentIndex ToEquipmentSlot(string equipment)
    {
        return equipment switch
        {
            "Wep0" => EquipmentIndex.WeaponItemBeginSlot,
            "Wep1" => EquipmentIndex.Weapon1,
            "Wep2" => EquipmentIndex.Weapon2,
            "Wep3" => EquipmentIndex.Weapon3,
            "Head" => EquipmentIndex.NumAllWeaponSlots,
            "Cape" => EquipmentIndex.Cape,
            "Body" => EquipmentIndex.Body,
            "Gloves" => EquipmentIndex.Gloves,
            "Leg" => EquipmentIndex.Leg,
            "Horse" => EquipmentIndex.ArmorItemEndSlot,
            "Harness" => EquipmentIndex.HorseHarness,
            _ => EquipmentIndex.None,
        };
    }

    private static List<ItemObject.ItemTypeEnum> getRequiredItemType(EquipmentIndex equipmentIndex)
    {
        List<ItemObject.ItemTypeEnum> list = new List<ItemObject.ItemTypeEnum>();
        if (equipmentIndex == EquipmentIndex.WeaponItemBeginSlot || equipmentIndex == EquipmentIndex.Weapon1 || equipmentIndex == EquipmentIndex.Weapon2 || equipmentIndex == EquipmentIndex.Weapon3)
        {
            list.Add(ItemObject.ItemTypeEnum.Arrows);
            list.Add(ItemObject.ItemTypeEnum.Bolts);
            list.Add(ItemObject.ItemTypeEnum.Bow);
            list.Add(ItemObject.ItemTypeEnum.Bullets);
            list.Add(ItemObject.ItemTypeEnum.Crossbow);
            list.Add(ItemObject.ItemTypeEnum.Musket);
            list.Add(ItemObject.ItemTypeEnum.OneHandedWeapon);
            list.Add(ItemObject.ItemTypeEnum.Pistol);
            list.Add(ItemObject.ItemTypeEnum.Polearm);
            list.Add(ItemObject.ItemTypeEnum.Shield);
            list.Add(ItemObject.ItemTypeEnum.Thrown);
            list.Add(ItemObject.ItemTypeEnum.TwoHandedWeapon);
        }
        else
        {
            switch (equipmentIndex)
            {
                case EquipmentIndex.NumAllWeaponSlots:
                    list.Add(ItemObject.ItemTypeEnum.HeadArmor);
                    break;
                case EquipmentIndex.Cape:
                    list.Add(ItemObject.ItemTypeEnum.Cape);
                    break;
                case EquipmentIndex.Body:
                    list.Add(ItemObject.ItemTypeEnum.BodyArmor);
                    break;
                case EquipmentIndex.Gloves:
                    list.Add(ItemObject.ItemTypeEnum.HandArmor);
                    break;
                case EquipmentIndex.Leg:
                    list.Add(ItemObject.ItemTypeEnum.LegArmor);
                    break;
                case EquipmentIndex.ArmorItemEndSlot:
                    list.Add(ItemObject.ItemTypeEnum.Horse);
                    break;
                case EquipmentIndex.HorseHarness:
                    list.Add(ItemObject.ItemTypeEnum.HorseHarness);
                    break;
            }
        }

        return list;
    }

    public override void RegisterEvents()
    {
        CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, GameStart);
        CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, MenuItems);
    }

    private void MenuItems(CampaignGameStarter campaignGameSystemStarter)
    {
        campaignGameSystemStarter.AddGameMenuOption("town", "recruit_custom_volunteers", "Switch recruitment type", delegate (MenuCallbackArgs args)
        {
            args.optionLeaveType = GameMenuOption.LeaveType.Recruit;
            return true;
        }, delegate
        {
           
            if (SubModule.ReplaceAllForPlayer)
            {
                InformationManager.DisplayMessage(new InformationMessage("Recruitment type set to native troops"));
            }
            else
            {
                InformationManager.DisplayMessage(new InformationMessage("Recruitment type set to custom troops"));
            }

            SubModule.ReplaceAllForPlayer = !SubModule.ReplaceAllForPlayer;
        }, isLeave: false, 5);
        campaignGameSystemStarter.AddGameMenuOption("village", "recruit_custom_volunteers", "Switch recruitment type", delegate (MenuCallbackArgs args)
        {
            args.optionLeaveType = GameMenuOption.LeaveType.Recruit;
            return true;
        }, delegate
        {
            if (SubModule.ReplaceAllForPlayer)
            {
                InformationManager.DisplayMessage(new InformationMessage("Recruitment type set to native troops"));
            }
            else
            {
                InformationManager.DisplayMessage(new InformationMessage("Recruitment type set to custom troops"));
            }

            SubModule.ReplaceAllForPlayer = !SubModule.ReplaceAllForPlayer;
        }, isLeave: false, 2);
    }

    private void GameStart(CampaignGameStarter campaignStarter)
    {
        if (CustomUnits == null)
        {
            CustomUnits = new Dictionary<string, CustomUnit>();
        }

        foreach (KeyValuePair<string, CustomUnit> customUnit in CustomUnits)
        {
            if (customUnit.Value.Id.Contains("_basic_root") || customUnit.Value.Id.Contains("_elite_root") || instance.FullUnitEditor)
            {
                customUnit.Value.Update();
            }
        }

        InitializeEquipmentFilterSettings();
    }

    private void InitializeEquipmentFilterSettings()
    {
        _filter_tiers.Clear();
        _filter_tiers.Add(0);
        _filter_tiers.Add(1);
        _filter_tiers.Add(2);
        _filter_tiers.Add(3);
        _filter_tiers.Add(4);
        _filter_tiers.Add(5);
        _filter_tiers.Add(6);
    }

    public override void SyncData(IDataStore dataStore)
    {
        dataStore.SyncData("_custom_units", ref CustomUnits);
        dataStore.SyncData("_gear_restriction", ref _disable_gear_restriction);
        dataStore.SyncData("_skill_total_restriction", ref _disable_skill_total_restriction);
        dataStore.SyncData("_skill_cap_restriction", ref _disable_skill_cap_restriction);
    }

}