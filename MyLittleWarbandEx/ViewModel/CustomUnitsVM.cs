using MyLittleWarbandEx;
using MyLittleWarbandEx.ViewModel;
using NavalDLC;
using System;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Core.ViewModelCollection.ImageIdentifiers;
using TaleWorlds.Core.ViewModelCollection.Selector;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace MyLittleWarbandEx;

internal class CustomUnitsVM : FilterableViewModel
{
    private CharacterViewModel _unitCharacter;

    private CharacterObject _character;

    private MBBindingList<BindingListStringItem> _navalStatus;

    private string _unit_id;

    private string _pointsLeft;

    private string _skiil_level_cap;

    private string _item_tier_filter;

    private string _culture_filter;

    private string _weapon_filter;

    private string _armour_filter;

    private string _disable_1_brush;

    private string _disable_2_brush;

    private string _disable_3_brush;

    private string _disable_1_text;

    private string _disable_2_text;

    private string _disable_3_text;

    private string _upgrade_button_1;

    private string _upgrade_button_2;

    private string _equipment_set_total_cost;

    private bool _isEnabled;

    private MBBindingList<EncyclopediaSkillVM> _skills;

    private MBBindingList<BindingListStringItem> _name;

    private MBBindingList<StringItemWithHintVM> _propertiesList;

    private MBBindingList<CharacterEquipmentItemVM> _weapons1;

    private MBBindingList<CharacterEquipmentItemVM> _weapons2;

    private MBBindingList<CharacterEquipmentItemVM> _weapons3;

    private MBBindingList<CharacterEquipmentItemVM> _weapons4;

    private MBBindingList<BindingListStringItem> _weapon1_name;

    private MBBindingList<BindingListStringItem> _weapon2_name;

    private MBBindingList<BindingListStringItem> _weapon3_name;

    private MBBindingList<BindingListStringItem> _weapon4_name;

    private MBBindingList<CharacterEquipmentItemVM> _helmet;

    private MBBindingList<CharacterEquipmentItemVM> _cape;

    private MBBindingList<CharacterEquipmentItemVM> _chest;

    private MBBindingList<CharacterEquipmentItemVM> _gloves;

    private MBBindingList<CharacterEquipmentItemVM> _boots;

    private MBBindingList<BindingListStringItem> _helmet_name;

    private MBBindingList<BindingListStringItem> _cape_name;

    private MBBindingList<BindingListStringItem> _chest_name;

    private MBBindingList<BindingListStringItem> _gloves_name;

    private MBBindingList<BindingListStringItem> _boots_name;

    private MBBindingList<CharacterEquipmentItemVM> _mount;

    private MBBindingList<CharacterEquipmentItemVM> _harness;

    private MBBindingList<BindingListStringItem> _mount_name;

    private MBBindingList<BindingListStringItem> _harness_name;

    private MBBindingList<BindingListStringItem> _gender_string;

    private MBBindingList<BindingListStringItem> _culture_string;

    private SelectorVM<EncyclopediaUnitEquipmentSetSelectorItemVM> _equipmentSetSelector;

    private EncyclopediaUnitEquipmentSetSelectorItemVM _currentSelectedEquipmentSet;

    private TextObject _equipmentSetTextObj;

    private string _equipmentSetText;

    private CharacterImageIdentifierVM _imageIdentifier_1;

    private CharacterImageIdentifierVM _imageIdentifier_2;

    private CharacterImageIdentifierVM _imageIdentifier_3;

    private CharacterObject _upgrade_1;

    private CharacterObject _upgrade_2;

    private CharacterObject _root;

    [DataSourceProperty]
    public string EquipmentSetText
    {
        get
        {
            return _equipmentSetText;
        }
        set
        {
            if (value != _equipmentSetText)
            {
                _equipmentSetText = value;
                OnPropertyChangedWithValue(value, "EquipmentSetText");
            }
        }
    }

    [DataSourceProperty]
    public EncyclopediaUnitEquipmentSetSelectorItemVM CurrentSelectedEquipmentSet
    {
        get
        {
            return _currentSelectedEquipmentSet;
        }
        set
        {
            if (value != _currentSelectedEquipmentSet)
            {
                _currentSelectedEquipmentSet = value;
                OnPropertyChangedWithValue(value, "CurrentSelectedEquipmentSet");
            }
        }
    }

    [DataSourceProperty]
    public SelectorVM<EncyclopediaUnitEquipmentSetSelectorItemVM> EquipmentSetSelector
    {
        get
        {
            return _equipmentSetSelector;
        }
        set
        {
            if (value != _equipmentSetSelector)
            {
                _equipmentSetSelector = value;
                OnPropertyChangedWithValue(value, "EquipmentSetSelector");
            }
        }
    }

    [DataSourceProperty]
    public bool IsEnabled
    {
        get
        {
            return _isEnabled;
        }
        set
        {
            if (value != _isEnabled)
            {
                _isEnabled = value;
                OnPropertyChangedWithValue(value, "IsEnabled");
            }
        }
    }

    [DataSourceProperty]
    public CharacterImageIdentifierVM ImageIdentifier1
    {
        get
        {
            return _imageIdentifier_1;
        }
        set
        {
            if (value != _imageIdentifier_1)
            {
                _imageIdentifier_1 = value;
                OnPropertyChangedWithValue(value, "ImageIdentifier1");
            }
        }
    }

    [DataSourceProperty]
    public CharacterImageIdentifierVM ImageIdentifier2
    {
        get
        {
            return _imageIdentifier_2;
        }
        set
        {
            if (value != _imageIdentifier_2)
            {
                _imageIdentifier_2 = value;
                OnPropertyChangedWithValue(value, "ImageIdentifier2");
            }
        }
    }

    [DataSourceProperty]
    public CharacterImageIdentifierVM ImageIdentifier3
    {
        get
        {
            return _imageIdentifier_3;
        }
        set
        {
            if (value != _imageIdentifier_3)
            {
                _imageIdentifier_3 = value;
                OnPropertyChangedWithValue(value, "ImageIdentifier3");
            }
        }
    }

    [DataSourceProperty]
    public CharacterViewModel UnitCharacter
    {
        get
        {
            return _unitCharacter;
        }
        set
        {
            if (value != _unitCharacter)
            {
                _unitCharacter = value;
                OnPropertyChangedWithValue(value, "UnitCharacter");
            }
        }
    }

    [DataSourceProperty]
    public string Disable_1_Brush
    {
        get
        {
            return _disable_1_brush;
        }
        set
        {
            if (value != _disable_1_brush)
            {
                _disable_1_brush = value;
            }
        }
    }

    [DataSourceProperty]
    public string Disable_2_Brush
    {
        get
        {
            return _disable_2_brush;
        }
        set
        {
            if (value != _disable_2_brush)
            {
                _disable_2_brush = value;
            }
        }
    }

    [DataSourceProperty]
    public string Disable_3_Brush
    {
        get
        {
            return _disable_3_brush;
        }
        set
        {
            if (value != _disable_3_brush)
            {
                _disable_3_brush = value;
            }
        }
    }

    [DataSourceProperty]
    public string Disable_1_Text
    {
        get
        {
            return _disable_1_text;
        }
        set
        {
            if (value != _disable_1_text)
            {
                _disable_1_text = value;
                OnPropertyChangedWithValue(value, "Disable_1_Text");
            }
        }
    }

    [DataSourceProperty]
    public string Disable_2_Text
    {
        get
        {
            return _disable_2_text;
        }
        set
        {
            if (value != _disable_2_text)
            {
                _disable_2_text = value;
                OnPropertyChangedWithValue(value, "Disable_2_Text");
            }
        }
    }

    [DataSourceProperty]
    public string Disable_3_Text
    {
        get
        {
            return _disable_3_text;
        }
        set
        {
            if (value != _disable_3_text)
            {
                _disable_3_text = value;
                OnPropertyChangedWithValue(value, "Disable_3_Text");
            }
        }
    }

    [DataSourceProperty]
    public string PointsLeft
    {
        get
        {
            return _pointsLeft;
        }
        set
        {
            if (value != _pointsLeft)
            {
                _pointsLeft = value;
                OnPropertyChangedWithValue(value, "PointsLeft");
            }
        }
    }

    [DataSourceProperty]
    public string ArmourFilter
    {
        get
        {
            return _armour_filter;
        }
        set
        {
            if (value != _armour_filter)
            {
                _armour_filter = value;
                OnPropertyChangedWithValue(value, "ArmourFilter");
            }
        }
    }

    [DataSourceProperty]
    public string WeaponFilter
    {
        get
        {
            return _weapon_filter;
        }
        set
        {
            if (value != _weapon_filter)
            {
                _weapon_filter = value;
                OnPropertyChangedWithValue(value, "WeaponFilter");
            }
        }
    }

    [DataSourceProperty]
    public string CultureFilter
    {
        get
        {
            return _culture_filter;
        }
        set
        {
            if (value != _culture_filter)
            {
                _culture_filter = value;
                OnPropertyChangedWithValue(value, "CultureFilter");
            }
        }
    }

    [DataSourceProperty]
    public string ItemTierFilter
    {
        get
        {
            return _item_tier_filter;
        }
        set
        {
            if (value != _item_tier_filter)
            {
                _item_tier_filter = value;
                OnPropertyChangedWithValue(value, "ItemTierFilter");
            }
        }
    }

    [DataSourceProperty]
    public string EquipmentSetTotalCost
    {
        get
        {
            return _equipment_set_total_cost;
        }
        set
        {
            if (value != _equipment_set_total_cost)
            {
                _equipment_set_total_cost = value;
                OnPropertyChangedWithValue(value, "EquipmentSetTotalCost");
            }
        }
    }

    [DataSourceProperty]
    public string SkillLevelCap
    {
        get
        {
            return _skiil_level_cap;
        }
        set
        {
            if (value != _skiil_level_cap)
            {
                _skiil_level_cap = value;
                OnPropertyChangedWithValue(value, "SkillLevelCap");
            }
        }
    }

    [DataSourceProperty]
    public string UpgradeButton1
    {
        get
        {
            return _upgrade_button_1;
        }
        set
        {
            if (value != _upgrade_button_1)
            {
                _upgrade_button_1 = value;
                OnPropertyChangedWithValue(value, "UpgradeButton1");
            }
        }
    }

    [DataSourceProperty]
    public string UpgradeButton2
    {
        get
        {
            return _upgrade_button_2;
        }
        set
        {
            if (value != _upgrade_button_2)
            {
                _upgrade_button_2 = value;
                OnPropertyChangedWithValue(value, "UpgradeButton2");
            }
        }
    }

    [DataSourceProperty]
    public MBBindingList<EncyclopediaSkillVM> Skills
    {
        get
        {
            return _skills;
        }
        set
        {
            if (value != _skills)
            {
                _skills = value;
                OnPropertyChangedWithValue(value, "Skills");
            }
        }
    }

    [DataSourceProperty]
    public MBBindingList<CharacterEquipmentItemVM> Weapon1
    {
        get
        {
            return _weapons1;
        }
        set
        {
            if (value != _weapons1)
            {
                _weapons1 = value;
                OnPropertyChangedWithValue(value, "Weapons1");
            }
        }
    }

    [DataSourceProperty]
    public MBBindingList<CharacterEquipmentItemVM> Weapon2
    {
        get
        {
            return _weapons2;
        }
        set
        {
            if (value != _weapons2)
            {
                _weapons2 = value;
                OnPropertyChangedWithValue(value, "Weapons2");
            }
        }
    }

    [DataSourceProperty]
    public MBBindingList<CharacterEquipmentItemVM> Weapon3
    {
        get
        {
            return _weapons3;
        }
        set
        {
            if (value != _weapons3)
            {
                _weapons3 = value;
                OnPropertyChangedWithValue(value, "Weapons3");
            }
        }
    }

    [DataSourceProperty]
    public MBBindingList<CharacterEquipmentItemVM> Weapon4
    {
        get
        {
            return _weapons4;
        }
        set
        {
            if (value != _weapons4)
            {
                _weapons4 = value;
                OnPropertyChangedWithValue(value, "Weapons4");
            }
        }
    }

    [DataSourceProperty]
    public MBBindingList<CharacterEquipmentItemVM> Helmet
    {
        get
        {
            return _helmet;
        }
        set
        {
            if (value != _helmet)
            {
                _helmet = value;
                OnPropertyChangedWithValue(value, "Helmet");
            }
        }
    }

    [DataSourceProperty]
    public MBBindingList<CharacterEquipmentItemVM> Cape
    {
        get
        {
            return _cape;
        }
        set
        {
            if (value != _cape)
            {
                _cape = value;
                OnPropertyChangedWithValue(value, "Cape");
            }
        }
    }

    [DataSourceProperty]
    public MBBindingList<CharacterEquipmentItemVM> Chest
    {
        get
        {
            return _chest;
        }
        set
        {
            if (value != _chest)
            {
                _chest = value;
                OnPropertyChangedWithValue(value, "Chest");
            }
        }
    }

    [DataSourceProperty]
    public MBBindingList<CharacterEquipmentItemVM> Gloves
    {
        get
        {
            return _gloves;
        }
        set
        {
            if (value != _gloves)
            {
                _gloves = value;
                OnPropertyChangedWithValue(value, "Gloves");
            }
        }
    }

    [DataSourceProperty]
    public MBBindingList<CharacterEquipmentItemVM> Boots
    {
        get
        {
            return _boots;
        }
        set
        {
            if (value != _boots)
            {
                _boots = value;
                OnPropertyChangedWithValue(value, "Boots");
            }
        }
    }

    [DataSourceProperty]
    public MBBindingList<CharacterEquipmentItemVM> Mount
    {
        get
        {
            return _mount;
        }
        set
        {
            if (value != _mount)
            {
                _mount = value;
                OnPropertyChangedWithValue(value, "Mount");
            }
        }
    }

    [DataSourceProperty]
    public MBBindingList<CharacterEquipmentItemVM> Harness
    {
        get
        {
            return _harness;
        }
        set
        {
            if (value != _harness)
            {
                _harness = value;
                OnPropertyChangedWithValue(value, "Harness");
            }
        }
    }

    [DataSourceProperty]
    public MBBindingList<BindingListStringItem> Weapon1Name
    {
        get
        {
            return _weapon1_name;
        }
        set
        {
            if (value != _weapon1_name)
            {
                _weapon1_name = value;
                OnPropertyChanged("Weapon1Name");
            }
        }
    }

    public MBBindingList<BindingListStringItem> Weapon2Name
    {
        get
        {
            return _weapon2_name;
        }
        set
        {
            if (value != _weapon2_name)
            {
                _weapon2_name = value;
                OnPropertyChanged("Weapon2Name");
            }
        }
    }

    public MBBindingList<BindingListStringItem> Weapon3Name
    {
        get
        {
            return _weapon3_name;
        }
        set
        {
            if (value != _weapon3_name)
            {
                _weapon3_name = value;
                OnPropertyChanged("Weapon3Name");
            }
        }
    }

    public MBBindingList<BindingListStringItem> Weapon4Name
    {
        get
        {
            return _weapon4_name;
        }
        set
        {
            if (value != _weapon4_name)
            {
                _weapon4_name = value;
                OnPropertyChanged("Weapon4Name");
            }
        }
    }

    public MBBindingList<BindingListStringItem> HelmetName
    {
        get
        {
            return _helmet_name;
        }
        set
        {
            if (value != _helmet_name)
            {
                _helmet_name = value;
                OnPropertyChanged("HelmetName");
            }
        }
    }

    public MBBindingList<BindingListStringItem> CapeName
    {
        get
        {
            return _cape_name;
        }
        set
        {
            if (value != _cape_name)
            {
                _cape_name = value;
                OnPropertyChanged("CapeName");
            }
        }
    }

    public MBBindingList<BindingListStringItem> ChestName
    {
        get
        {
            return _chest_name;
        }
        set
        {
            if (value != _chest_name)
            {
                _chest_name = value;
                OnPropertyChanged("ChestName");
            }
        }
    }

    public MBBindingList<BindingListStringItem> GlovesName
    {
        get
        {
            return _gloves_name;
        }
        set
        {
            if (value != _gloves_name)
            {
                _gloves_name = value;
                OnPropertyChanged("GlovesName");
            }
        }
    }

    public MBBindingList<BindingListStringItem> BootsName
    {
        get
        {
            return _boots_name;
        }
        set
        {
            if (value != _boots_name)
            {
                _boots_name = value;
                OnPropertyChanged("BootsName");
            }
        }
    }

    public MBBindingList<BindingListStringItem> MountName
    {
        get
        {
            return _mount_name;
        }
        set
        {
            if (value != _mount_name)
            {
                _mount_name = value;
                OnPropertyChanged("MountName");
            }
        }
    }

    public MBBindingList<BindingListStringItem> HarnessName
    {
        get
        {
            return _harness_name;
        }
        set
        {
            if (value != _harness_name)
            {
                _harness_name = value;
                OnPropertyChanged("HarnessName");
            }
        }
    }

    public MBBindingList<BindingListStringItem> GenderString
    {
        get
        {
            return _gender_string;
        }
        set
        {
            if (value != _gender_string)
            {
                _gender_string = value;
                OnPropertyChanged("GenderString");
            }
        }
    }

    public MBBindingList<BindingListStringItem> CultureString
    {
        get
        {
            return _culture_string;
        }
        set
        {
            if (value != _culture_string)
            {
                _culture_string = value;
                OnPropertyChanged("CultureString");
            }
        }
    }

    public MBBindingList<BindingListStringItem> Name
    {
        get
        {
            return _name;
        }
        set
        {
            if (value != _name)
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
    }

    [DataSourceProperty]
    public MBBindingList<StringItemWithHintVM> PropertiesList
    {
        get
        {
            return _propertiesList;
        }
        set
        {
            if (value != _propertiesList)
            {
                _propertiesList = value;
                OnPropertyChangedWithValue(value, "PropertiesList");
            }
        }
    }

    public override void RefreshData()
    {
        this.RefreshValues();
    }

    public CustomUnitsVM(string unitId)
    {
        _unit_id = unitId;
        _character = Game.Current.ObjectManager.GetObject<CharacterObject>(_unit_id);
        UnitCharacter = new CharacterViewModel(CharacterViewModel.StanceTypes.EmphasizeFace);
        UnitCharacter.FillFrom(_character);
        PropertiesList = new MBBindingList<StringItemWithHintVM>();
        PropertiesList.Add(CampaignUIHelper.GetCharacterTierData(_character, isBig: true));
        PropertiesList.Add(CampaignUIHelper.GetCharacterTypeData(_character, isBig: true));
        Skills = new MBBindingList<EncyclopediaSkillVM>();
        Skills.Add(new EncyclopediaSkillVM(DefaultSkills.OneHanded, _character.GetSkillValue(DefaultSkills.OneHanded)));
        Skills.Add(new EncyclopediaSkillVM(DefaultSkills.TwoHanded, _character.GetSkillValue(DefaultSkills.TwoHanded)));
        Skills.Add(new EncyclopediaSkillVM(DefaultSkills.Polearm, _character.GetSkillValue(DefaultSkills.Polearm)));
        Skills.Add(new EncyclopediaSkillVM(DefaultSkills.Bow, _character.GetSkillValue(DefaultSkills.Bow)));
        Skills.Add(new EncyclopediaSkillVM(DefaultSkills.Crossbow, _character.GetSkillValue(DefaultSkills.Crossbow)));
        Skills.Add(new EncyclopediaSkillVM(DefaultSkills.Throwing, _character.GetSkillValue(DefaultSkills.Throwing)));
        Skills.Add(new EncyclopediaSkillVM(DefaultSkills.Athletics, _character.GetSkillValue(DefaultSkills.Athletics)));
        Skills.Add(new EncyclopediaSkillVM(DefaultSkills.Riding, _character.GetSkillValue(DefaultSkills.Riding)));
        _equipmentSetTextObj = new TextObject("{=vggt7exj}Set {CURINDEX}/{COUNT}");
        EquipmentSetSelector = new SelectorVM<EncyclopediaUnitEquipmentSetSelectorItemVM>(0, OnEquipmentSetChange);
        EquipmentSetSelector.ItemList.Clear();
        foreach (Equipment equipment in _character.BattleEquipments)
        {
            if (!EquipmentSetSelector.ItemList.Any((EncyclopediaUnitEquipmentSetSelectorItemVM x) => x.EquipmentSet.IsEquipmentEqualTo(equipment)))
            {
                EquipmentSetSelector.AddItem(new EncyclopediaUnitEquipmentSetSelectorItemVM(equipment));
            }
        }

        if (EquipmentSetSelector.ItemList.Count > 0)
        {
            EquipmentSetSelector.SelectedIndex = 0;
        }

        _equipmentSetTextObj.SetTextVariable("CURINDEX", EquipmentSetSelector.SelectedIndex + 1);
        _equipmentSetTextObj.SetTextVariable("COUNT", EquipmentSetSelector.ItemList.Count);
        EquipmentSetText = _equipmentSetTextObj.ToString();
        Name = new MBBindingList<BindingListStringItem>();
        Name.Add(new BindingListStringItem(_character.Name.ToString()));
        Weapon1 = new MBBindingList<CharacterEquipmentItemVM>();
        Weapon1.Add(new CharacterEquipmentItemVM(_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.WeaponItemBeginSlot].Item));
        Weapon1Name = new MBBindingList<BindingListStringItem>();
        Weapon1Name.Add(new BindingListStringItem("Weapon 1 : " + ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.WeaponItemBeginSlot].Item == null) ? "None" : _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.WeaponItemBeginSlot].Item.Name.ToString())));
        Weapon2 = new MBBindingList<CharacterEquipmentItemVM>();
        Weapon2.Add(new CharacterEquipmentItemVM(_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Weapon1].Item));
        Weapon2Name = new MBBindingList<BindingListStringItem>();
        Weapon2Name.Add(new BindingListStringItem("Weapon 2 : " + ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Weapon1].Item == null) ? "None" : _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Weapon1].Item.Name.ToString())));
        Weapon3 = new MBBindingList<CharacterEquipmentItemVM>();
        Weapon3.Add(new CharacterEquipmentItemVM(_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Weapon2].Item));
        Weapon3Name = new MBBindingList<BindingListStringItem>();
        Weapon3Name.Add(new BindingListStringItem("Weapon 3 : " + ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Weapon2].Item == null) ? "None" : _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Weapon2].Item.Name.ToString())));
        Weapon4 = new MBBindingList<CharacterEquipmentItemVM>();
        Weapon4.Add(new CharacterEquipmentItemVM(_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Weapon3].Item));
        Weapon4Name = new MBBindingList<BindingListStringItem>();
        Weapon4Name.Add(new BindingListStringItem("Weapon 4 : " + ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Weapon3].Item == null) ? "None" : _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Weapon3].Item.Name.ToString())));
        Helmet = new MBBindingList<CharacterEquipmentItemVM>();
        Helmet.Add(new CharacterEquipmentItemVM(_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.NumAllWeaponSlots].Item));
        HelmetName = new MBBindingList<BindingListStringItem>();
        HelmetName.Add(new BindingListStringItem("Helmet : " + ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.NumAllWeaponSlots].Item == null) ? "None" : _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.NumAllWeaponSlots].Item.Name.ToString())));
        Cape = new MBBindingList<CharacterEquipmentItemVM>();
        Cape.Add(new CharacterEquipmentItemVM(_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Cape].Item));
        CapeName = new MBBindingList<BindingListStringItem>();
        CapeName.Add(new BindingListStringItem("Cape : " + ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Cape].Item == null) ? "None" : _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Cape].Item.Name.ToString())));
        Chest = new MBBindingList<CharacterEquipmentItemVM>();
        Chest.Add(new CharacterEquipmentItemVM(_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Body].Item));
        ChestName = new MBBindingList<BindingListStringItem>();
        ChestName.Add(new BindingListStringItem("Chest : " + ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Body].Item == null) ? "None" : _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Body].Item.Name.ToString())));
        Gloves = new MBBindingList<CharacterEquipmentItemVM>();
        Gloves.Add(new CharacterEquipmentItemVM(_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Gloves].Item));
        GlovesName = new MBBindingList<BindingListStringItem>();
        GlovesName.Add(new BindingListStringItem("Gloves : " + ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Gloves].Item == null) ? "None" : _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Gloves].Item.Name.ToString())));
        Boots = new MBBindingList<CharacterEquipmentItemVM>();
        Boots.Add(new CharacterEquipmentItemVM(_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Leg].Item));
        BootsName = new MBBindingList<BindingListStringItem>();
        BootsName.Add(new BindingListStringItem("Boots : " + ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Leg].Item == null) ? "None" : _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Leg].Item.Name.ToString())));
        Mount = new MBBindingList<CharacterEquipmentItemVM>();
        Mount.Add(new CharacterEquipmentItemVM(_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.ArmorItemEndSlot].Item));
        MountName = new MBBindingList<BindingListStringItem>();
        MountName.Add(new BindingListStringItem("Mount : " + ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.ArmorItemEndSlot].Item == null) ? "None" : _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.ArmorItemEndSlot].Item.Name.ToString())));
        Harness = new MBBindingList<CharacterEquipmentItemVM>();
        Harness.Add(new CharacterEquipmentItemVM(_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.HorseHarness].Item));
        HarnessName = new MBBindingList<BindingListStringItem>();
        HarnessName.Add(new BindingListStringItem("Harness: " + ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.HorseHarness].Item == null) ? "None" : _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.HorseHarness].Item.Name.ToString())));
        GenderString = new MBBindingList<BindingListStringItem>();
        GenderString.Add(new BindingListStringItem("Gender : " + (_character.IsFemale ? "Female" : "Male")));
        CultureString = new MBBindingList<BindingListStringItem>();
        CultureString.Add(new BindingListStringItem("Culture : " + _character.Culture.Name.ToString()));
        NavalString = new MBBindingList<BindingListStringItem>();
        NavalString.Add(new BindingListStringItem("Naval Soldier : " + this._character.IsNavalSoldier().ToString()));
        if (_character.UpgradeTargets != null && _character.UpgradeTargets.Length != 0)
        {
            ImageIdentifier1 = new CharacterImageIdentifierVM(CharacterCode.CreateFrom(_character.UpgradeTargets[0]));
            _upgrade_1 = _character.UpgradeTargets[0];
        }
        else
        {
            ImageIdentifier1 = new CharacterImageIdentifierVM(null);
            _upgrade_1 = null;
        }

        if (_character.UpgradeTargets != null && _character.UpgradeTargets.Length > 1)
        {
            ImageIdentifier2 = new CharacterImageIdentifierVM(CharacterCode.CreateFrom(_character.UpgradeTargets[1]));
            _upgrade_2 = _character.UpgradeTargets[1];
        }
        else
        {
            ImageIdentifier2 = new CharacterImageIdentifierVM(null);
            _upgrade_2 = null;
        }

        if (Game.Current.ObjectManager.GetObject<CharacterObject>(_unit_id.Substring(0, _unit_id.Length - 1)) != null)
        {
            ImageIdentifier3 = new CharacterImageIdentifierVM(CharacterCode.CreateFrom(Game.Current.ObjectManager.GetObject<CharacterObject>(_unit_id.Substring(0, _unit_id.Length - 1))));
            _root = Game.Current.ObjectManager.GetObject<CharacterObject>(_unit_id.Substring(0, _unit_id.Length - 1));
        }
        else if (!EnyclopediaEditUnitPatch.isCustomTroop(_character) && GetUpgradeRoot() != null)
        {
            ImageIdentifier3 = new CharacterImageIdentifierVM(CharacterCode.CreateFrom(GetUpgradeRoot()));
        }
        else
        {
            ImageIdentifier3 = new CharacterImageIdentifierVM(null);
            _root = null;
        }

        PointsLeft = pointLeft().ToString();
        SkillLevelCap = CustomUnitsBehavior.skillCap(_unit_id).ToString();
        UpgradeButton1 = ((_upgrade_1 == null) ? "Add" : "Remove");
        UpgradeButton2 = ((_upgrade_2 == null) ? "Add" : "Remove");
        string text = "";
        foreach (int filter_tier in CustomUnitsBehavior._filter_tiers)
        {
            if (text != "")
            {
                text += ",";
            }

            text += filter_tier;
        }

        ItemTierFilter = text;
        string text2 = "";
        if (CustomUnitsBehavior._filter_Culture.IsEmpty())
        {
            text2 = "All";
        }
        else
        {
            foreach (CultureObject item in CustomUnitsBehavior._filter_Culture)
            {
                if (text2 != "")
                {
                    text2 += ",";
                }

                text2 += item.Name.ToString();
            }
        }

        CultureFilter = text2;
        string text3 = "";
        if (CustomUnitsBehavior._filter_weapon_types.IsEmpty())
        {
            text3 = "All";
        }
        else
        {
            foreach (ItemObject.ItemTypeEnum filter_weapon_type in CustomUnitsBehavior._filter_weapon_types)
            {
                if (text3 != "")
                {
                    text3 += ",";
                }

                text3 += filter_weapon_type;
            }
        }

        WeaponFilter = text3;
        string text4 = "";
        if (CustomUnitsBehavior._filter_armour_types.IsEmpty())
        {
            text4 = "All";
        }
        else
        {
            foreach (ArmorComponent.ArmorMaterialTypes filter_armour_type in CustomUnitsBehavior._filter_armour_types)
            {
                if (text4 != "")
                {
                    text4 += ",";
                }

                text4 += filter_armour_type;
            }
        }

        ArmourFilter = text4;
        if (CustomUnitsBehavior._disable_gear_restriction)
        {
            Disable_1_Brush = "ButtonBrush1";
            Disable_1_Text = "ON";
        }
        else
        {
            Disable_1_Brush = "ButtonBrush2";
            Disable_1_Text = "OFF";
        }

        if (CustomUnitsBehavior._disable_skill_total_restriction)
        {
            Disable_2_Brush = "ButtonBrush1";
            Disable_2_Text = "ON";
        }
        else
        {
            Disable_2_Brush = "ButtonBrush2";
            Disable_2_Text = "OFF";
        }

        if (CustomUnitsBehavior._disable_skill_cap_restriction)
        {
            Disable_3_Brush = "ButtonBrush1";
            Disable_3_Text = "ON";
        }
        else
        {
            Disable_3_Brush = "ButtonBrush2";
            Disable_3_Text = "OFF";
        }

        IsEnabled = EnyclopediaEditUnitPatch.isCustomTroop(_character);
    }

    private CharacterObject GetUpgradeRoot()
    {
        if (_root != null)
        {
            return _root;
        }

        foreach (CharacterObject character in Campaign.Current.Characters)
        {
            if (character.UpgradeTargets != null && character.UpgradeTargets.Length != 0)
            {
                if (character.UpgradeTargets[0] == _character)
                {
                    _root = character;
                    return character;
                }

                if (character.UpgradeTargets.Length > 1 && character.UpgradeTargets[1] == _character)
                {
                    _root = character;
                    return character;
                }
            }
        }

        _root = null;
        return null;
    }

    public void Close()
    {
        CustomUnitsBehavior.DeleteVMLayer();
        Campaign.Current.EncyclopediaManager.GoToLink(_character.EncyclopediaLink);
    }

    public override void RefreshValues()
    {
        base.RefreshValues();
        //var MyHero = Hero.MainHero;
        Name.Clear();
        Name.Add(new BindingListStringItem(_character.Name.ToString()));
        PropertiesList.Clear();
        PropertiesList.Add(CampaignUIHelper.GetCharacterTierData(_character, isBig: true));
        PropertiesList.Add(CampaignUIHelper.GetCharacterTypeData(_character, isBig: true));
        Skills.Clear();
        Skills.Add(new EncyclopediaSkillVM(DefaultSkills.OneHanded, _character.GetSkillValue(DefaultSkills.OneHanded)));
        Skills.Add(new EncyclopediaSkillVM(DefaultSkills.TwoHanded, _character.GetSkillValue(DefaultSkills.TwoHanded)));
        Skills.Add(new EncyclopediaSkillVM(DefaultSkills.Polearm, _character.GetSkillValue(DefaultSkills.Polearm)));
        Skills.Add(new EncyclopediaSkillVM(DefaultSkills.Bow, _character.GetSkillValue(DefaultSkills.Bow)));
        Skills.Add(new EncyclopediaSkillVM(DefaultSkills.Crossbow, _character.GetSkillValue(DefaultSkills.Crossbow)));
        Skills.Add(new EncyclopediaSkillVM(DefaultSkills.Throwing, _character.GetSkillValue(DefaultSkills.Throwing)));
        Skills.Add(new EncyclopediaSkillVM(DefaultSkills.Athletics, _character.GetSkillValue(DefaultSkills.Athletics)));
        Skills.Add(new EncyclopediaSkillVM(DefaultSkills.Riding, _character.GetSkillValue(DefaultSkills.Riding)));
        _equipmentSetTextObj = new TextObject("{=vggt7exj}Set {CURINDEX}/{COUNT}");
        EquipmentSetSelector = new SelectorVM<EncyclopediaUnitEquipmentSetSelectorItemVM>(0, OnEquipmentSetChange);
        EquipmentSetSelector.ItemList.Clear();
        foreach (Equipment equipment in _character.BattleEquipments)
        {
            if (!EquipmentSetSelector.ItemList.Any((EncyclopediaUnitEquipmentSetSelectorItemVM x) => x.EquipmentSet.IsEquipmentEqualTo(equipment)))
            {
                EquipmentSetSelector.AddItem(new EncyclopediaUnitEquipmentSetSelectorItemVM(equipment));
            }
        }

        if (EquipmentSetSelector.ItemList.Count > 0)
        {
            EquipmentSetSelector.SelectedIndex = 0;
        }

        _equipmentSetTextObj.SetTextVariable("CURINDEX", EquipmentSetSelector.SelectedIndex + 1);
        _equipmentSetTextObj.SetTextVariable("COUNT", EquipmentSetSelector.ItemList.Count);
        EquipmentSetText = _equipmentSetTextObj.ToString();
        Weapon1.Clear();
        Weapon1.Add(new CharacterEquipmentItemVM(_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.WeaponItemBeginSlot].Item));
        Weapon1Name = new MBBindingList<BindingListStringItem>();
        Weapon1Name.Add(new BindingListStringItem("Weapon 1 : " + ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.WeaponItemBeginSlot].Item == null) ? "None" : _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.WeaponItemBeginSlot].Item.Name.ToString())));
        Weapon2.Clear();
        Weapon2.Add(new CharacterEquipmentItemVM(_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Weapon1].Item));
        Weapon2Name = new MBBindingList<BindingListStringItem>();
        Weapon2Name.Add(new BindingListStringItem("Weapon 2 : " + ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Weapon1].Item == null) ? "None" : _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Weapon1].Item.Name.ToString())));
        Weapon3.Clear();
        Weapon3.Add(new CharacterEquipmentItemVM(_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Weapon2].Item));
        Weapon3Name = new MBBindingList<BindingListStringItem>();
        Weapon3Name.Add(new BindingListStringItem("Weapon 3 : " + ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Weapon2].Item == null) ? "None" : _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Weapon2].Item.Name.ToString())));
        Weapon4.Clear();
        Weapon4.Add(new CharacterEquipmentItemVM(_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Weapon3].Item));
        Weapon4Name = new MBBindingList<BindingListStringItem>();
        Weapon4Name.Add(new BindingListStringItem("Weapon 4 : " + ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Weapon3].Item == null) ? "None" : _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Weapon3].Item.Name.ToString())));
        Helmet.Clear();
        Helmet.Add(new CharacterEquipmentItemVM(_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.NumAllWeaponSlots].Item));
        HelmetName = new MBBindingList<BindingListStringItem>();
        HelmetName.Add(new BindingListStringItem("Helmet : " + ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.NumAllWeaponSlots].Item == null) ? "None" : _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.NumAllWeaponSlots].Item.Name.ToString())));
        Cape.Clear();
        Cape.Add(new CharacterEquipmentItemVM(_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Cape].Item));
        CapeName = new MBBindingList<BindingListStringItem>();
        CapeName.Add(new BindingListStringItem("Cape : " + ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Cape].Item == null) ? "None" : _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Cape].Item.Name.ToString())));
        Chest.Clear();
        Chest.Add(new CharacterEquipmentItemVM(_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Body].Item));
        ChestName = new MBBindingList<BindingListStringItem>();
        ChestName.Add(new BindingListStringItem("Chest : " + ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Body].Item == null) ? "None" : _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Body].Item.Name.ToString())));
        Gloves.Clear();
        Gloves.Add(new CharacterEquipmentItemVM(_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Gloves].Item));
        GlovesName = new MBBindingList<BindingListStringItem>();
        GlovesName.Add(new BindingListStringItem("Gloves : " + ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Gloves].Item == null) ? "None" : _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Gloves].Item.Name.ToString())));
        Boots.Clear();
        Boots.Add(new CharacterEquipmentItemVM(_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Leg].Item));
        BootsName = new MBBindingList<BindingListStringItem>();
        BootsName.Add(new BindingListStringItem("Boots : " + ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Leg].Item == null) ? "None" : _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Leg].Item.Name.ToString())));
        Mount.Clear();
        Mount.Add(new CharacterEquipmentItemVM(_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.ArmorItemEndSlot].Item));
        MountName = new MBBindingList<BindingListStringItem>();
        MountName.Add(new BindingListStringItem("Mount : " + ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.ArmorItemEndSlot].Item == null) ? "None" : _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.ArmorItemEndSlot].Item.Name.ToString())));
        Harness.Clear();
        Harness.Add(new CharacterEquipmentItemVM(_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.HorseHarness].Item));
        HarnessName = new MBBindingList<BindingListStringItem>();
        HarnessName.Add(new BindingListStringItem("Harness: " + ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.HorseHarness].Item == null) ? "None" : _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.HorseHarness].Item.Name.ToString())));
        GenderString = new MBBindingList<BindingListStringItem>();
        GenderString.Add(new BindingListStringItem("Gender : " + (_character.IsFemale ? "Female" : "Male")));
        CultureString = new MBBindingList<BindingListStringItem>();
        CultureString.Add(new BindingListStringItem("Culture : " + _character.Culture.Name.ToString()));
        NavalString = new MBBindingList<BindingListStringItem>();
        NavalString.Add(new BindingListStringItem("Naval Soldier : " + this._character.IsNavalSoldier().ToString()));
        _character = Game.Current.ObjectManager.GetObject<CharacterObject>(_unit_id);
        UnitCharacter = new CharacterViewModel(CharacterViewModel.StanceTypes.EmphasizeFace);
        UnitCharacter.FillFrom(_character);
        PointsLeft = pointLeft().ToString();
        SkillLevelCap = CustomUnitsBehavior.skillCap(_unit_id).ToString();
        if (_character.UpgradeTargets != null && _character.UpgradeTargets.Length != 0)
        {
            ImageIdentifier1 = new CharacterImageIdentifierVM(CharacterCode.CreateFrom(_character.UpgradeTargets[0]));
            _upgrade_1 = _character.UpgradeTargets[0];
        }
        else
        {
            ImageIdentifier1 = new CharacterImageIdentifierVM(null);
            _upgrade_1 = null;
        }

        if (_character.UpgradeTargets != null && _character.UpgradeTargets.Length > 1)
        {
            ImageIdentifier2 = new CharacterImageIdentifierVM(CharacterCode.CreateFrom(_character.UpgradeTargets[1]));
            _upgrade_2 = _character.UpgradeTargets[1];
        }
        else
        {
            ImageIdentifier2 = new CharacterImageIdentifierVM(null);
            _upgrade_2 = null;
        }

        if (Game.Current.ObjectManager.GetObject<CharacterObject>(_unit_id.Substring(0, _unit_id.Length - 1)) != null)
        {
            ImageIdentifier3 = new CharacterImageIdentifierVM(CharacterCode.CreateFrom(Game.Current.ObjectManager.GetObject<CharacterObject>(_unit_id.Substring(0, _unit_id.Length - 1))));
            _root = Game.Current.ObjectManager.GetObject<CharacterObject>(_unit_id.Substring(0, _unit_id.Length - 1));
        }
        else if (!EnyclopediaEditUnitPatch.isCustomTroop(_character) && GetUpgradeRoot() != null)
        {
            ImageIdentifier3 = new CharacterImageIdentifierVM(CharacterCode.CreateFrom(GetUpgradeRoot()));
        }
        else
        {
            ImageIdentifier3 = new CharacterImageIdentifierVM(null);
            _root = null;
        }

        UpgradeButton1 = ((_upgrade_1 == null) ? "Add" : "Remove");
        UpgradeButton2 = ((_upgrade_2 == null) ? "Add" : "Remove");
        string text = "";
        foreach (int filter_tier in CustomUnitsBehavior._filter_tiers)
        {
            if (text != "")
            {
                text += ",";
            }

            text += filter_tier;
        }

        ItemTierFilter = text;
        string text2 = "";
        if (CustomUnitsBehavior._filter_Culture.IsEmpty())
        {
            text2 = "All";
        }
        else
        {
            foreach (CultureObject item in CustomUnitsBehavior._filter_Culture)
            {
                if (text2 != "")
                {
                    text2 += ",";
                }

                text2 += item.Name.ToString();
            }
        }

        CultureFilter = text2;
        string text3 = "";
        if (CustomUnitsBehavior._filter_weapon_types.IsEmpty())
        {
            text3 = "All";
        }
        else
        {
            foreach (ItemObject.ItemTypeEnum filter_weapon_type in CustomUnitsBehavior._filter_weapon_types)
            {
                if (text3 != "")
                {
                    text3 += ",";
                }

                text3 += filter_weapon_type;
            }
        }

        WeaponFilter = text3;
        string text4 = "";
        if (CustomUnitsBehavior._filter_armour_types.IsEmpty())
        {
            text4 = "All";
        }
        else
        {
            foreach (ArmorComponent.ArmorMaterialTypes filter_armour_type in CustomUnitsBehavior._filter_armour_types)
            {
                if (text4 != "")
                {
                    text4 += ",";
                }

                text4 += filter_armour_type;
            }
        }

        ArmourFilter = text4;
        if (CustomUnitsBehavior._disable_gear_restriction)
        {
            Disable_1_Brush = "ButtonBrush1";
            Disable_1_Text = "ON";
        }
        else
        {
            Disable_1_Brush = "ButtonBrush2";
            Disable_1_Text = "OFF";
        }

        if (CustomUnitsBehavior._disable_skill_total_restriction)
        {
            Disable_2_Brush = "ButtonBrush1";
            Disable_2_Text = "ON";
        }
        else
        {
            Disable_2_Brush = "ButtonBrush2";
            Disable_2_Text = "OFF";
        }

        if (CustomUnitsBehavior._disable_skill_cap_restriction)
        {
            Disable_3_Brush = "ButtonBrush1";
            Disable_3_Text = "ON";
        }
        else
        {
            Disable_3_Brush = "ButtonBrush2";
            Disable_3_Text = "OFF";
        }

        IsEnabled = EnyclopediaEditUnitPatch.isCustomTroop(_character);
    }

    public int pointLeft()
    {
        return getSkillPointsAvalible() - _character.GetSkillValue(DefaultSkills.OneHanded) - _character.GetSkillValue(DefaultSkills.TwoHanded) - _character.GetSkillValue(DefaultSkills.Polearm) - _character.GetSkillValue(DefaultSkills.Bow) - _character.GetSkillValue(DefaultSkills.Crossbow) - _character.GetSkillValue(DefaultSkills.Throwing) - _character.GetSkillValue(DefaultSkills.Riding) - _character.GetSkillValue(DefaultSkills.Athletics);
    }

    public int getSkillPointsAvalible()
    {
        if (CustomUnitsBehavior._disable_skill_total_restriction)
        {
            return 10000;
        }

        return _character.Tier switch
        {
            1 => 80,
            2 => 200,
            3 => 350,
            4 => 500,
            5 => 700,
            6 => 900,
            _ => 1200,
        };
    }

    private void OnEquipmentSetChange(SelectorVM<EncyclopediaUnitEquipmentSetSelectorItemVM> selector)
    {
        CurrentSelectedEquipmentSet = selector.SelectedItem;
        UnitCharacter.SetEquipment(CurrentSelectedEquipmentSet.EquipmentSet);
        _equipmentSetTextObj.SetTextVariable("CURINDEX", selector.SelectedIndex + 1);
        _equipmentSetTextObj.SetTextVariable("COUNT", selector.ItemList.Count);
        EquipmentSetText = _equipmentSetTextObj.ToString();
        int num = 0;
        if (Weapon1 != null)
        {
            Weapon1.Clear();
            Weapon1.Add(new CharacterEquipmentItemVM(_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.WeaponItemBeginSlot].Item));
        }

        Weapon1Name = new MBBindingList<BindingListStringItem>();
        Weapon1Name.Add(new BindingListStringItem("Weapon 1 : " + ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.WeaponItemBeginSlot].Item == null) ? "None" : _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.WeaponItemBeginSlot].Item.Name.ToString())));
        num += ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.WeaponItemBeginSlot].Item != null) ? _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.WeaponItemBeginSlot].Item.Value : 0);
        if (Weapon2 != null)
        {
            Weapon2.Clear();
            Weapon2.Add(new CharacterEquipmentItemVM(_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Weapon1].Item));
        }

        Weapon2Name = new MBBindingList<BindingListStringItem>();
        Weapon2Name.Add(new BindingListStringItem("Weapon 2 : " + ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Weapon1].Item == null) ? "None" : _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Weapon1].Item.Name.ToString())));
        num += ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Weapon1].Item != null) ? _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Weapon1].Item.Value : 0);
        if (Weapon3 != null)
        {
            Weapon3.Clear();
            Weapon3.Add(new CharacterEquipmentItemVM(_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Weapon2].Item));
        }

        Weapon3Name = new MBBindingList<BindingListStringItem>();
        Weapon3Name.Add(new BindingListStringItem("Weapon 3 : " + ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Weapon2].Item == null) ? "None" : _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Weapon2].Item.Name.ToString())));
        num += ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Weapon2].Item != null) ? _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Weapon2].Item.Value : 0);
        if (Weapon4 != null)
        {
            Weapon4.Clear();
            Weapon4.Add(new CharacterEquipmentItemVM(_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Weapon3].Item));
        }

        Weapon4Name = new MBBindingList<BindingListStringItem>();
        Weapon4Name.Add(new BindingListStringItem("Weapon 4 : " + ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Weapon3].Item == null) ? "None" : _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Weapon3].Item.Name.ToString())));
        num += ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Weapon3].Item != null) ? _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Weapon3].Item.Value : 0);
        if (Helmet != null)
        {
            Helmet.Clear();
            Helmet.Add(new CharacterEquipmentItemVM(_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.NumAllWeaponSlots].Item));
        }

        HelmetName = new MBBindingList<BindingListStringItem>();
        HelmetName.Add(new BindingListStringItem("Helmet : " + ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.NumAllWeaponSlots].Item == null) ? "None" : _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.NumAllWeaponSlots].Item.Name.ToString())));
        num += ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.NumAllWeaponSlots].Item != null) ? _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.NumAllWeaponSlots].Item.Value : 0);
        if (Cape != null)
        {
            Cape.Clear();
            Cape.Add(new CharacterEquipmentItemVM(_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Cape].Item));
        }

        CapeName = new MBBindingList<BindingListStringItem>();
        CapeName.Add(new BindingListStringItem("Cape : " + ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Cape].Item == null) ? "None" : _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Cape].Item.Name.ToString())));
        num += ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Cape].Item != null) ? _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Cape].Item.Value : 0);
        if (Chest != null)
        {
            Chest.Clear();
            Chest.Add(new CharacterEquipmentItemVM(_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Body].Item));
        }

        ChestName = new MBBindingList<BindingListStringItem>();
        ChestName.Add(new BindingListStringItem("Chest : " + ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Body].Item == null) ? "None" : _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Body].Item.Name.ToString())));
        num += ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Body].Item != null) ? _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Body].Item.Value : 0);
        if (Gloves != null)
        {
            Gloves.Clear();
            Gloves.Add(new CharacterEquipmentItemVM(_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Gloves].Item));
        }

        GlovesName = new MBBindingList<BindingListStringItem>();
        GlovesName.Add(new BindingListStringItem("Gloves : " + ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Gloves].Item == null) ? "None" : _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Gloves].Item.Name.ToString())));
        num += ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Gloves].Item != null) ? _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Gloves].Item.Value : 0);
        if (Boots != null)
        {
            Boots.Clear();
            Boots.Add(new CharacterEquipmentItemVM(_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Leg].Item));
        }

        BootsName = new MBBindingList<BindingListStringItem>();
        BootsName.Add(new BindingListStringItem("Boots : " + ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Leg].Item == null) ? "None" : _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Leg].Item.Name.ToString())));
        num += ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Leg].Item != null) ? _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.Leg].Item.Value : 0);
        if (Mount != null)
        {
            Mount.Clear();
            Mount.Add(new CharacterEquipmentItemVM(_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.ArmorItemEndSlot].Item));
        }

        MountName = new MBBindingList<BindingListStringItem>();
        MountName.Add(new BindingListStringItem("Mount : " + ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.ArmorItemEndSlot].Item == null) ? "None" : _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.ArmorItemEndSlot].Item.Name.ToString())));
        num += ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.ArmorItemEndSlot].Item != null) ? _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.ArmorItemEndSlot].Item.Value : 0);
        if (Harness != null)
        {
            Harness.Clear();
            Harness.Add(new CharacterEquipmentItemVM(_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.HorseHarness].Item));
        }

        HarnessName = new MBBindingList<BindingListStringItem>();
        HarnessName.Add(new BindingListStringItem("Harness: " + ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.HorseHarness].Item == null) ? "None" : _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.HorseHarness].Item.Name.ToString())));
        EquipmentSetTotalCost = "Set Total Cost: " + (num + ((_character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.HorseHarness].Item != null) ? _character.BattleEquipments.ToArray()[EquipmentSetSelector.SelectedIndex][EquipmentIndex.HorseHarness].Item.Value : 0)) + "<img src=\"General\\Icons\\Coin@2x\" extend=\"8\"/>";
    }

    [DataSourceProperty]
    public MBBindingList<BindingListStringItem> NavalString
    {
        get
        {
            return this._navalStatus;
        }
        set
        {
            bool flag = value == this._navalStatus;
            if (!flag)
            {
                this._navalStatus = value;
                base.OnPropertyChanged("NavalString");
            }
        }
    }

 
    public void ChangeNavalSoldier()
    {
        CustomUnitsBehavior.ToggleMarineStatus(this._unit_id);
        this.NavalString = new MBBindingList<BindingListStringItem>();
        this.NavalString.Add(new BindingListStringItem("Naval Soldier : " + this._character.IsNavalSoldier().ToString()));
        RefreshValues();
    }
    public void OneHandPlus()
    {
        if (Input.IsKeyDown(InputKey.LeftControl) || Input.IsKeyDown(InputKey.RightControl))
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.OneHanded, Math.Min(1023, pointLeft()), _unit_id);
        }
        else if (Input.IsKeyDown(InputKey.LeftShift) || Input.IsKeyDown(InputKey.RightShift))
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.OneHanded, Math.Min(5, pointLeft()), _unit_id);
        }
        else
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.OneHanded, Math.Min(1, pointLeft()), _unit_id);
        }
    }

    public void OneHandMinus()
    {
        if (Input.IsKeyDown(InputKey.LeftControl) || Input.IsKeyDown(InputKey.RightControl))
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.OneHanded, -1023, _unit_id);
        }
        else if (Input.IsKeyDown(InputKey.LeftShift) || Input.IsKeyDown(InputKey.RightShift))
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.OneHanded, -5, _unit_id);
        }
        else
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.OneHanded, -1, _unit_id);
        }
    }

    public void TwoHandPlus()
    {
        if (Input.IsKeyDown(InputKey.LeftControl) || Input.IsKeyDown(InputKey.RightControl))
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.TwoHanded, Math.Min(1023, pointLeft()), _unit_id);
        }
        else if (Input.IsKeyDown(InputKey.LeftShift) || Input.IsKeyDown(InputKey.RightShift))
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.TwoHanded, Math.Min(5, pointLeft()), _unit_id);
        }
        else
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.TwoHanded, Math.Min(1, pointLeft()), _unit_id);
        }
    }

    public void TwoHandMinus()
    {
        if (Input.IsKeyDown(InputKey.LeftControl) || Input.IsKeyDown(InputKey.RightControl))
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.TwoHanded, -1023, _unit_id);
        }
        else if (Input.IsKeyDown(InputKey.LeftShift) || Input.IsKeyDown(InputKey.RightShift))
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.TwoHanded, -5, _unit_id);
        }
        else
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.TwoHanded, -1, _unit_id);
        }
    }

    public void PolearmPlus()
    {
        if (Input.IsKeyDown(InputKey.LeftControl) || Input.IsKeyDown(InputKey.RightControl))
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.Polearm, Math.Min(1023, pointLeft()), _unit_id);
        }
        else if (Input.IsKeyDown(InputKey.LeftShift) || Input.IsKeyDown(InputKey.RightShift))
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.Polearm, Math.Min(5, pointLeft()), _unit_id);
        }
        else
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.Polearm, Math.Min(1, pointLeft()), _unit_id);
        }
    }

    public void PolearmMinus()
    {
        if (Input.IsKeyDown(InputKey.LeftControl) || Input.IsKeyDown(InputKey.RightControl))
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.Polearm, -1023, _unit_id);
        }
        else if (Input.IsKeyDown(InputKey.LeftShift) || Input.IsKeyDown(InputKey.RightShift))
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.Polearm, -5, _unit_id);
        }
        else
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.Polearm, -1, _unit_id);
        }
    }

    public void BowPlus()
    {
        if (Input.IsKeyDown(InputKey.LeftControl) || Input.IsKeyDown(InputKey.RightControl))
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.Bow, Math.Min(1023, pointLeft()), _unit_id);
        }
        else if (Input.IsKeyDown(InputKey.LeftShift) || Input.IsKeyDown(InputKey.RightShift))
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.Bow, Math.Min(5, pointLeft()), _unit_id);
        }
        else
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.Bow, Math.Min(1, pointLeft()), _unit_id);
        }
    }

    public void BowMinus()
    {
        if (Input.IsKeyDown(InputKey.LeftControl) || Input.IsKeyDown(InputKey.RightControl))
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.Bow, -1023, _unit_id);
        }
        else if (Input.IsKeyDown(InputKey.LeftShift) || Input.IsKeyDown(InputKey.RightShift))
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.Bow, -5, _unit_id);
        }
        else
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.Bow, -1, _unit_id);
        }
    }

    public void CrossbowPlus()
    {
        if (Input.IsKeyDown(InputKey.LeftControl) || Input.IsKeyDown(InputKey.RightShift))
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.Crossbow, Math.Min(1023, pointLeft()), _unit_id);
        }
        else if (Input.IsKeyDown(InputKey.LeftShift) || Input.IsKeyDown(InputKey.RightShift))
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.Crossbow, Math.Min(5, pointLeft()), _unit_id);
        }
        else
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.Crossbow, Math.Min(1, pointLeft()), _unit_id);
        }
    }

    public void CrossbowMinus()
    {
        if (Input.IsKeyDown(InputKey.LeftControl) || Input.IsKeyDown(InputKey.RightControl))
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.Crossbow, -1023, _unit_id);
        }
        else if (Input.IsKeyDown(InputKey.LeftShift) || Input.IsKeyDown(InputKey.RightShift))
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.Crossbow, -5, _unit_id);
        }
        else
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.Crossbow, -1, _unit_id);
        }
    }

    public void ThrowingPlus()
    {
        if (Input.IsKeyDown(InputKey.LeftControl) || Input.IsKeyDown(InputKey.RightControl))
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.Throwing, Math.Min(1023, pointLeft()), _unit_id);
        }
        else if (Input.IsKeyDown(InputKey.LeftShift) || Input.IsKeyDown(InputKey.RightShift))
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.Throwing, Math.Min(5, pointLeft()), _unit_id);
        }
        else
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.Throwing, Math.Min(1, pointLeft()), _unit_id);
        }
    }

    public void ThrowingMinus()
    {
        if (Input.IsKeyDown(InputKey.LeftControl) || Input.IsKeyDown(InputKey.RightControl))
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.Throwing, -1023, _unit_id);
        }
        else if (Input.IsKeyDown(InputKey.LeftShift) || Input.IsKeyDown(InputKey.RightShift))
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.Throwing, -5, _unit_id);
        }
        else
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.Throwing, -1, _unit_id);
        }
    }

    public void RidingPlus()
    {
        if (Input.IsKeyDown(InputKey.LeftControl) || Input.IsKeyDown(InputKey.RightControl))
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.Riding, Math.Min(1023, pointLeft()), _unit_id);
        }
        else if (Input.IsKeyDown(InputKey.LeftShift) || Input.IsKeyDown(InputKey.RightShift))
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.Riding, Math.Min(5, pointLeft()), _unit_id);
        }
        else
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.Riding, Math.Min(1, pointLeft()), _unit_id);
        }
    }

    public void RidingMinus()
    {
        if (Input.IsKeyDown(InputKey.LeftControl) || Input.IsKeyDown(InputKey.RightControl))
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.Riding, -1023, _unit_id);
        }
        else if (Input.IsKeyDown(InputKey.LeftShift) || Input.IsKeyDown(InputKey.RightShift))
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.Riding, -5, _unit_id);
        }
        else
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.Riding, -1, _unit_id);
        }
    }

    public void AthleticsPlus()
    {
        if (Input.IsKeyDown(InputKey.LeftControl) || Input.IsKeyDown(InputKey.RightControl))
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.Athletics, Math.Min(1023, pointLeft()), _unit_id);
        }
        else if (Input.IsKeyDown(InputKey.LeftShift) || Input.IsKeyDown(InputKey.RightShift))
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.Athletics, Math.Min(5, pointLeft()), _unit_id);
        }
        else
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.Athletics, Math.Min(1, pointLeft()), _unit_id);
        }
    }

    public void AthleticsMinus()
    {
        if (Input.IsKeyDown(InputKey.LeftControl) || Input.IsKeyDown(InputKey.RightControl))
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.Athletics, -1023, _unit_id);
        }
        else if (Input.IsKeyDown(InputKey.LeftShift) || Input.IsKeyDown(InputKey.RightShift))
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.Athletics, -5, _unit_id);
        }
        else
        {
            CustomUnitsBehavior.UpdateSkill(DefaultSkills.Athletics, -1, _unit_id);
        }
    }

    public void ChangeWep1()
    {
        CustomUnitsBehavior.UpgradeGear("Wep0", _unit_id);
    }

    public void ChangeWep2()
    {
        CustomUnitsBehavior.UpgradeGear("Wep1", _unit_id);
    }

    public void ChangeWep3()
    {
        CustomUnitsBehavior.UpgradeGear("Wep2", _unit_id);
    }

    public void ChangeWep4()
    {
        CustomUnitsBehavior.UpgradeGear("Wep3", _unit_id);
    }

    public void ChangeHelmet()
    {
        CustomUnitsBehavior.UpgradeGear("Head", _unit_id);
    }

    public void ChangeCape()
    {
        CustomUnitsBehavior.UpgradeGear("Cape", _unit_id);
    }

    public void ChangeChestArmor()
    {
        CustomUnitsBehavior.UpgradeGear("Body", _unit_id);
    }

    public void ChangeGloves()
    {
        CustomUnitsBehavior.UpgradeGear("Gloves", _unit_id);
    }

    public void ChangeBoots()
    {
        CustomUnitsBehavior.UpgradeGear("Leg", _unit_id);
    }

    public void ChangeMount()
    {
        CustomUnitsBehavior.UpgradeGear("Horse", _unit_id);
    }

    public void ChangeHarness()
    {
        CustomUnitsBehavior.UpgradeGear("Harness", _unit_id);
    }

    public void Rename()
    {
        CustomUnitsBehavior.rename(_unit_id);
    }

    public void ChangeCulture()
    {
        CustomUnitsBehavior.ChangeCulture(_unit_id);
    }

    public void ChangeGender()
    {
        CustomUnitsBehavior.ChangeGender(_unit_id);
    }

    public void Upgrade1Link()
    {
        if (_upgrade_1 != null)
        {
            CustomUnitsBehavior.DeleteVMLayer();
            CustomUnitsBehavior.CreateVMLayer(_upgrade_1.StringId);
        }
    }

    public void Upgrade2Link()
    {
        if (_upgrade_2 != null)
        {
            CustomUnitsBehavior.DeleteVMLayer();
            CustomUnitsBehavior.CreateVMLayer(_upgrade_2.StringId);
        }
    }

    public void RootLink()
    {
        if (_root != null)
        {
            CustomUnitsBehavior.DeleteVMLayer();
            CustomUnitsBehavior.CreateVMLayer(_root.StringId);
        }
    }

    public void UpgradeButtonClick1()
    {
        if (_upgrade_1 == null)
        {
            AddUpgrade();
            return;
        }

        CustomUnitsBehavior.RemoveUpgrade(_unit_id, _upgrade_1);
        CustomUnitsBehavior.DeleteVMLayer();
        CustomUnitsBehavior.CreateVMLayer(_unit_id);
    }

    public void UpgradeButtonClick2()
    {
        if (_upgrade_2 == null)
        {
            AddUpgrade();
            return;
        }

        CustomUnitsBehavior.RemoveUpgrade(_unit_id, _upgrade_2);
        CustomUnitsBehavior.DeleteVMLayer();
        CustomUnitsBehavior.CreateVMLayer(_unit_id);
    }

    public void AddUpgrade()
    {
        CustomUnitsBehavior.AddUpgrade(_unit_id);
    }

    public void ItemTierClear()
    {
        CustomUnitsBehavior._filter_tiers.Clear();
        CustomUnitsBehavior._filter_tiers.Add(0);
        CustomUnitsBehavior._filter_tiers.Add(1);
        CustomUnitsBehavior._filter_tiers.Add(2);
        CustomUnitsBehavior._filter_tiers.Add(3);
        CustomUnitsBehavior._filter_tiers.Add(4);
        CustomUnitsBehavior._filter_tiers.Add(5);
        CustomUnitsBehavior._filter_tiers.Add(6);
        RefreshValues();
    }

    public void ItemTierChange()
    {
        CustomUnitsBehavior.FilterTiers();
    }

    public void FilterCultureClear()
    {
        CustomUnitsBehavior._filter_Culture.Clear();
        RefreshValues();
    }

    public void FilterCultureChange()
    {
        CustomUnitsBehavior.FilterCulturesPopup();
    }

    public void FilterWeaponClear()
    {
        CustomUnitsBehavior._filter_weapon_types.Clear();
        RefreshValues();
    }

    public void FilterWeaponChange()
    {
        CustomUnitsBehavior.FilterWeapons();
    }

    public void FilterArmourClear()
    {
        CustomUnitsBehavior._filter_armour_types.Clear();
        RefreshValues();
    }

    public void FilterArmourChange()
    {
        CustomUnitsBehavior.FilterArmour();
    }

    public void Toggle1()
    {
        CustomUnitsBehavior._disable_gear_restriction = !CustomUnitsBehavior._disable_gear_restriction;
        CustomUnitsBehavior.DeleteVMLayer();
        CustomUnitsBehavior.CreateVMLayer(_unit_id);
    }

    public void Toggle2()
    {
        CustomUnitsBehavior._disable_skill_total_restriction = !CustomUnitsBehavior._disable_skill_total_restriction;
        CustomUnitsBehavior.DeleteVMLayer();
        CustomUnitsBehavior.CreateVMLayer(_unit_id);
    }

    public void Toggle3()
    {
        CustomUnitsBehavior._disable_skill_cap_restriction = !CustomUnitsBehavior._disable_skill_cap_restriction;
        CustomUnitsBehavior.DeleteVMLayer();
        CustomUnitsBehavior.CreateVMLayer(_unit_id);
    }

    public void CopyTemplate()
    {
        CustomUnitsBehavior.CopyTemplate(_unit_id);
    }

    public void ExecuteBeginHint1()
    {
        if (_upgrade_1 != null)
        {
            InformationManager.ShowTooltip(typeof(CharacterObject), _upgrade_1);
        }

        MBInformationManager.ShowHint("No upgade unit added");
    }

    public void ExecuteBeginHint2()
    {
        if (_upgrade_2 != null)
        {
            InformationManager.ShowTooltip(typeof(CharacterObject), _upgrade_2);
        }

        MBInformationManager.ShowHint("No upgade unit added");
    }

    public void ExecuteBeginHint3()
    {
        if (_root != null)
        {
            InformationManager.ShowTooltip(typeof(CharacterObject), _root);
        }
    }

    public void ExecuteEndHint()
    {
        MBInformationManager.HideInformations();
    }
}