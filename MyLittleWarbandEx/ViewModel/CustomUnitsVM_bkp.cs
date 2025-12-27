using MyLittleWarbandEx;
using NavalDLC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TaleWorlds.CampaignSystem;
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

namespace MyLittleWarbandEx.ViewModel
{
    // Token: 0x0200000E RID: 14
    internal class CustomUnitsVM_bkp : TaleWorlds.Library.ViewModel
    {
        // Token: 0x06000049 RID: 73 RVA: 0x00004684 File Offset: 0x00002884
        public CustomUnitsVM_bkp(string unitId)
        {
            this._unit_id = unitId;
            this._character = Game.Current.ObjectManager.GetObject<CharacterObject>(this._unit_id);
            //Originally this.UnitCharacter = new CharacterViewModel(1);
            this.UnitCharacter = new CharacterViewModel();
            this.UnitCharacter.FillFrom(this._character, -1);
            this.PropertiesList = new MBBindingList<StringItemWithHintVM>();
            this.PropertiesList.Add(CampaignUIHelper.GetCharacterTierData(this._character, true));
            this.PropertiesList.Add(CampaignUIHelper.GetCharacterTypeData(this._character, true));
            this.Skills = new MBBindingList<EncyclopediaSkillVM>();
            this.Skills.Add(new EncyclopediaSkillVM(DefaultSkills.OneHanded, this._character.GetSkillValue(DefaultSkills.OneHanded)));
            this.Skills.Add(new EncyclopediaSkillVM(DefaultSkills.TwoHanded, this._character.GetSkillValue(DefaultSkills.TwoHanded)));
            this.Skills.Add(new EncyclopediaSkillVM(DefaultSkills.Polearm, this._character.GetSkillValue(DefaultSkills.Polearm)));
            this.Skills.Add(new EncyclopediaSkillVM(DefaultSkills.Bow, this._character.GetSkillValue(DefaultSkills.Bow)));
            this.Skills.Add(new EncyclopediaSkillVM(DefaultSkills.Crossbow, this._character.GetSkillValue(DefaultSkills.Crossbow)));
            this.Skills.Add(new EncyclopediaSkillVM(DefaultSkills.Throwing, this._character.GetSkillValue(DefaultSkills.Throwing)));
            this.Skills.Add(new EncyclopediaSkillVM(DefaultSkills.Athletics, this._character.GetSkillValue(DefaultSkills.Athletics)));
            this.Skills.Add(new EncyclopediaSkillVM(DefaultSkills.Riding, this._character.GetSkillValue(DefaultSkills.Riding)));
            this._equipmentSetTextObj = new TextObject("{=vggt7exj}Set {CURINDEX}/{COUNT}", null);
            this.EquipmentSetSelector = new SelectorVM<EncyclopediaUnitEquipmentSetSelectorItemVM>(0, new Action<SelectorVM<EncyclopediaUnitEquipmentSetSelectorItemVM>>(this.OnEquipmentSetChange));
            this.EquipmentSetSelector.ItemList.Clear();
            using (IEnumerator<Equipment> enumerator = this._character.BattleEquipments.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    Equipment equipment = enumerator.Current;
                    bool flag = !this.EquipmentSetSelector.ItemList.Any((EncyclopediaUnitEquipmentSetSelectorItemVM x) => x.EquipmentSet.IsEquipmentEqualTo(equipment));
                    if (flag)
                    {
                        this.EquipmentSetSelector.AddItem(new EncyclopediaUnitEquipmentSetSelectorItemVM(equipment, ""));
                    }
                }
            }
            bool flag2 = this.EquipmentSetSelector.ItemList.Count > 0;
            if (flag2)
            {
                this.EquipmentSetSelector.SelectedIndex = 0;
            }
            this._equipmentSetTextObj.SetTextVariable("CURINDEX", this.EquipmentSetSelector.SelectedIndex + 1);
            this._equipmentSetTextObj.SetTextVariable("COUNT", this.EquipmentSetSelector.ItemList.Count);
            this.EquipmentSetText = this._equipmentSetTextObj.ToString();
            this.Name = new MBBindingList<BindingListStringItem>();
            this.Name.Add(new BindingListStringItem(this._character.Name.ToString()));
            this.Weapon1 = new MBBindingList<CharacterEquipmentItemVM>();
            this.Weapon1.Add(new CharacterEquipmentItemVM(this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][0].Item));
            this.Weapon1Name = new MBBindingList<BindingListStringItem>();
            this.Weapon1Name.Add(new BindingListStringItem("Weapon 1 : " + ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][0].Item == null) ? "None" : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][0].Item.Name.ToString())));
            this.Weapon2 = new MBBindingList<CharacterEquipmentItemVM>();
            this.Weapon2.Add(new CharacterEquipmentItemVM(this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][1].Item));
            this.Weapon2Name = new MBBindingList<BindingListStringItem>();
            this.Weapon2Name.Add(new BindingListStringItem("Weapon 2 : " + ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][1].Item == null) ? "None" : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][1].Item.Name.ToString())));
            this.Weapon3 = new MBBindingList<CharacterEquipmentItemVM>();
            this.Weapon3.Add(new CharacterEquipmentItemVM(this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][2].Item));
            this.Weapon3Name = new MBBindingList<BindingListStringItem>();
            this.Weapon3Name.Add(new BindingListStringItem("Weapon 3 : " + ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][2].Item == null) ? "None" : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][2].Item.Name.ToString())));
            this.Weapon4 = new MBBindingList<CharacterEquipmentItemVM>();
            this.Weapon4.Add(new CharacterEquipmentItemVM(this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][3].Item));
            this.Weapon4Name = new MBBindingList<BindingListStringItem>();
            this.Weapon4Name.Add(new BindingListStringItem("Weapon 4 : " + ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][3].Item == null) ? "None" : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][3].Item.Name.ToString())));
            this.Helmet = new MBBindingList<CharacterEquipmentItemVM>();
            this.Helmet.Add(new CharacterEquipmentItemVM(this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][5].Item));
            this.HelmetName = new MBBindingList<BindingListStringItem>();
            this.HelmetName.Add(new BindingListStringItem("Helmet : " + ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][5].Item == null) ? "None" : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][5].Item.Name.ToString())));
            this.Cape = new MBBindingList<CharacterEquipmentItemVM>();
            this.Cape.Add(new CharacterEquipmentItemVM(this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][9].Item));
            this.CapeName = new MBBindingList<BindingListStringItem>();
            this.CapeName.Add(new BindingListStringItem("Cape : " + ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][9].Item == null) ? "None" : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][9].Item.Name.ToString())));
            this.Chest = new MBBindingList<CharacterEquipmentItemVM>();
            this.Chest.Add(new CharacterEquipmentItemVM(this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][6].Item));
            this.ChestName = new MBBindingList<BindingListStringItem>();
            this.ChestName.Add(new BindingListStringItem("Chest : " + ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][6].Item == null) ? "None" : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][6].Item.Name.ToString())));
            this.Gloves = new MBBindingList<CharacterEquipmentItemVM>();
            this.Gloves.Add(new CharacterEquipmentItemVM(this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][8].Item));
            this.GlovesName = new MBBindingList<BindingListStringItem>();
            this.GlovesName.Add(new BindingListStringItem("Gloves : " + ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][8].Item == null) ? "None" : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][8].Item.Name.ToString())));
            this.Boots = new MBBindingList<CharacterEquipmentItemVM>();
            this.Boots.Add(new CharacterEquipmentItemVM(this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][7].Item));
            this.BootsName = new MBBindingList<BindingListStringItem>();
            this.BootsName.Add(new BindingListStringItem("Boots : " + ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][7].Item == null) ? "None" : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][7].Item.Name.ToString())));
            this.Mount = new MBBindingList<CharacterEquipmentItemVM>();
            this.Mount.Add(new CharacterEquipmentItemVM(this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][10].Item));
            this.MountName = new MBBindingList<BindingListStringItem>();
            this.MountName.Add(new BindingListStringItem("Mount : " + ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][10].Item == null) ? "None" : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][10].Item.Name.ToString())));
            this.Harness = new MBBindingList<CharacterEquipmentItemVM>();
            this.Harness.Add(new CharacterEquipmentItemVM(this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][11].Item));
            this.HarnessName = new MBBindingList<BindingListStringItem>();
            this.HarnessName.Add(new BindingListStringItem("Harness: " + ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][11].Item == null) ? "None" : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][11].Item.Name.ToString())));
            this.GenderString = new MBBindingList<BindingListStringItem>();
            this.GenderString.Add(new BindingListStringItem("Gender : " + (this._character.IsFemale ? "Female" : "Male")));
            this.CultureString = new MBBindingList<BindingListStringItem>();
            this.CultureString.Add(new BindingListStringItem("Culture : " + this._character.Culture.Name.ToString()));
            this.NavalString = new MBBindingList<BindingListStringItem>();
            this.NavalString.Add(new BindingListStringItem("Is Marine : " + this._character.IsNavalSoldier().ToString()));
            bool flag3 = this._character.UpgradeTargets != null && this._character.UpgradeTargets.Length != 0;
            if (flag3)
            {
                this.ImageIdentifier1 = new CharacterImageIdentifierVM(CharacterCode.CreateFrom(this._character.UpgradeTargets[0]));
                this._upgrade_1 = this._character.UpgradeTargets[0];
            }
            else
            {
                this.ImageIdentifier1 = new CharacterImageIdentifierVM(null);
                this._upgrade_1 = null;
            }
            bool flag4 = this._character.UpgradeTargets != null && this._character.UpgradeTargets.Length > 1;
            if (flag4)
            {
                this.ImageIdentifier2 = new CharacterImageIdentifierVM(CharacterCode.CreateFrom(this._character.UpgradeTargets[1]));
                this._upgrade_2 = this._character.UpgradeTargets[1];
            }
            else
            {
                this.ImageIdentifier2 = new CharacterImageIdentifierVM(null);
                this._upgrade_2 = null;
            }
            bool flag5 = Game.Current.ObjectManager.GetObject<CharacterObject>(this._unit_id.Substring(0, this._unit_id.Length - 1)) != null;
            if (flag5)
            {
                this.ImageIdentifier3 = new CharacterImageIdentifierVM(CharacterCode.CreateFrom(Game.Current.ObjectManager.GetObject<CharacterObject>(this._unit_id.Substring(0, this._unit_id.Length - 1))));
                this._root = Game.Current.ObjectManager.GetObject<CharacterObject>(this._unit_id.Substring(0, this._unit_id.Length - 1));
            }
            else
            {
                bool flag6 = !EnyclopediaEditUnitPatch.isCustomTroop(this._character) && this.GetUpgradeRoot() != null;
                if (flag6)
                {
                    this.ImageIdentifier3 = new CharacterImageIdentifierVM(CharacterCode.CreateFrom(this.GetUpgradeRoot()));
                }
                else
                {
                    this.ImageIdentifier3 = new CharacterImageIdentifierVM(null);
                    this._root = null;
                }
            }
            this.PointsLeft = this.pointLeft().ToString();
            this.SkillLevelCap = CustomUnitsBehavior.skillCap(this._unit_id).ToString();
            this.UpgradeButton1 = ((this._upgrade_1 == null) ? "Add" : "Remove");
            this.UpgradeButton2 = ((this._upgrade_2 == null) ? "Add" : "Remove");
            string text = "";
            foreach (int num in CustomUnitsBehavior._filter_tiers)
            {
                bool flag7 = text != "";
                if (flag7)
                {
                    text += ",";
                }
                text += num.ToString();
            }
            this.ItemTierFilter = text;
            string text2 = "";
            bool flag8 = TaleWorlds.Core.Extensions.IsEmpty<CultureObject>(CustomUnitsBehavior._filter_Culture);
            if (flag8)
            {
                text2 = "All";
            }
            else
            {
                foreach (CultureObject cultureObject in CustomUnitsBehavior._filter_Culture)
                {
                    bool flag9 = text2 != "";
                    if (flag9)
                    {
                        text2 += ",";
                    }
                    text2 += cultureObject.Name.ToString();
                }
            }
            this.CultureFilter = text2;
            string text3 = "";
            bool flag10 = TaleWorlds.Core.Extensions.IsEmpty<ItemObject.ItemTypeEnum>(CustomUnitsBehavior._filter_weapon_types);
            if (flag10)
            {
                text3 = "All";
            }
            else
            {
                foreach (ItemObject.ItemTypeEnum itemTypeEnum in CustomUnitsBehavior._filter_weapon_types)
                {
                    bool flag11 = text3 != "";
                    if (flag11)
                    {
                        text3 += ",";
                    }
                    text3 += itemTypeEnum.ToString();
                }
            }
            this.WeaponFilter = text3;
            string text4 = "";
            bool flag12 = TaleWorlds.Core.Extensions.IsEmpty<ArmorComponent.ArmorMaterialTypes>(CustomUnitsBehavior._filter_armour_types);
            if (flag12)
            {
                text4 = "All";
            }
            else
            {
                foreach (ArmorComponent.ArmorMaterialTypes armorMaterialTypes in CustomUnitsBehavior._filter_armour_types)
                {
                    bool flag13 = text4 != "";
                    if (flag13)
                    {
                        text4 += ",";
                    }
                    text4 += armorMaterialTypes.ToString();
                }
            }
            this.ArmourFilter = text4;
            bool disable_gear_restriction = CustomUnitsBehavior._disable_gear_restriction;
            if (disable_gear_restriction)
            {
                this.Disable_1_Brush = "ButtonBrush1";
                this.Disable_1_Text = "ON";
            }
            else
            {
                this.Disable_1_Brush = "ButtonBrush2";
                this.Disable_1_Text = "OFF";
            }
            bool disable_skill_total_restriction = CustomUnitsBehavior._disable_skill_total_restriction;
            if (disable_skill_total_restriction)
            {
                this.Disable_2_Brush = "ButtonBrush1";
                this.Disable_2_Text = "ON";
            }
            else
            {
                this.Disable_2_Brush = "ButtonBrush2";
                this.Disable_2_Text = "OFF";
            }
            bool disable_skill_cap_restriction = CustomUnitsBehavior._disable_skill_cap_restriction;
            if (disable_skill_cap_restriction)
            {
                this.Disable_3_Brush = "ButtonBrush1";
                this.Disable_3_Text = "ON";
            }
            else
            {
                this.Disable_3_Brush = "ButtonBrush2";
                this.Disable_3_Text = "OFF";
            }
            this.IsEnabled = EnyclopediaEditUnitPatch.isCustomTroop(this._character);
        }

        // Token: 0x0600004A RID: 74 RVA: 0x000058FC File Offset: 0x00003AFC
        private CharacterObject GetUpgradeRoot()
        {
            bool flag = this._root != null;
            CharacterObject result;
            if (flag)
            {
                result = this._root;
            }
            else
            {
                foreach (CharacterObject characterObject in Campaign.Current.Characters)
                {
                    bool flag2 = characterObject.UpgradeTargets != null && characterObject.UpgradeTargets.Length != 0;
                    if (flag2)
                    {
                        bool flag3 = characterObject.UpgradeTargets[0] == this._character;
                        if (flag3)
                        {
                            this._root = characterObject;
                            return characterObject;
                        }
                        bool flag4 = characterObject.UpgradeTargets.Length > 1 && characterObject.UpgradeTargets[1] == this._character;
                        if (flag4)
                        {
                            this._root = characterObject;
                            return characterObject;
                        }
                    }
                }
                this._root = null;
                result = null;
            }
            return result;
        }

        // Token: 0x1700000F RID: 15
        // (get) Token: 0x0600004B RID: 75 RVA: 0x000059EC File Offset: 0x00003BEC
        // (set) Token: 0x0600004C RID: 76 RVA: 0x00005A04 File Offset: 0x00003C04
        [DataSourceProperty]
        public string EquipmentSetText
        {
            get
            {
                return this._equipmentSetText;
            }
            set
            {
                bool flag = value != this._equipmentSetText;
                if (flag)
                {
                    this._equipmentSetText = value;
                    base.OnPropertyChangedWithValue<string>(value, "EquipmentSetText");
                }
            }
        }

        // Token: 0x17000010 RID: 16
        // (get) Token: 0x0600004D RID: 77 RVA: 0x00005A38 File Offset: 0x00003C38
        // (set) Token: 0x0600004E RID: 78 RVA: 0x00005A50 File Offset: 0x00003C50
        [DataSourceProperty]
        public EncyclopediaUnitEquipmentSetSelectorItemVM CurrentSelectedEquipmentSet
        {
            get
            {
                return this._currentSelectedEquipmentSet;
            }
            set
            {
                bool flag = value != this._currentSelectedEquipmentSet;
                if (flag)
                {
                    this._currentSelectedEquipmentSet = value;
                    base.OnPropertyChangedWithValue<EncyclopediaUnitEquipmentSetSelectorItemVM>(value, "CurrentSelectedEquipmentSet");
                }
            }
        }

        // Token: 0x17000011 RID: 17
        // (get) Token: 0x0600004F RID: 79 RVA: 0x00005A84 File Offset: 0x00003C84
        // (set) Token: 0x06000050 RID: 80 RVA: 0x00005A9C File Offset: 0x00003C9C
        [DataSourceProperty]
        public SelectorVM<EncyclopediaUnitEquipmentSetSelectorItemVM> EquipmentSetSelector
        {
            get
            {
                return this._equipmentSetSelector;
            }
            set
            {
                bool flag = value != this._equipmentSetSelector;
                if (flag)
                {
                    this._equipmentSetSelector = value;
                    base.OnPropertyChangedWithValue<SelectorVM<EncyclopediaUnitEquipmentSetSelectorItemVM>>(value, "EquipmentSetSelector");
                }
            }
        }

        // Token: 0x17000012 RID: 18
        // (get) Token: 0x06000051 RID: 81 RVA: 0x00005AD0 File Offset: 0x00003CD0
        // (set) Token: 0x06000052 RID: 82 RVA: 0x00005AE8 File Offset: 0x00003CE8
        [DataSourceProperty]
        public bool IsEnabled
        {
            get
            {
                return this._isEnabled;
            }
            set
            {
                bool flag = value != this._isEnabled;
                if (flag)
                {
                    this._isEnabled = value;
                    base.OnPropertyChangedWithValue(value, "IsEnabled");
                }
            }
        }

        // Token: 0x17000013 RID: 19
        // (get) Token: 0x06000053 RID: 83 RVA: 0x00005B1C File Offset: 0x00003D1C
        // (set) Token: 0x06000054 RID: 84 RVA: 0x00005B34 File Offset: 0x00003D34
        [DataSourceProperty]
        public CharacterImageIdentifierVM ImageIdentifier1
        {
            get
            {
                return this._imageIdentifier_1;
            }
            set
            {
                bool flag = value != this._imageIdentifier_1;
                if (flag)
                {
                    this._imageIdentifier_1 = value;
                    base.OnPropertyChangedWithValue<CharacterImageIdentifierVM>(value, "ImageIdentifier1");
                }
            }
        }



        // Token: 0x17000014 RID: 20
        // (get) Token: 0x06000055 RID: 85 RVA: 0x00005B68 File Offset: 0x00003D68
        // (set) Token: 0x06000056 RID: 86 RVA: 0x00005B80 File Offset: 0x00003D80
        [DataSourceProperty]
        public CharacterImageIdentifierVM ImageIdentifier2
        {
            get
            {
                return this._imageIdentifier_2;
            }
            set
            {
                bool flag = value != this._imageIdentifier_2;
                if (flag)
                {
                    this._imageIdentifier_2 = value;
                    base.OnPropertyChangedWithValue<CharacterImageIdentifierVM>(value, "ImageIdentifier2");
                }
            }
        }

        // Token: 0x17000015 RID: 21
        // (get) Token: 0x06000057 RID: 87 RVA: 0x00005BB4 File Offset: 0x00003DB4
        // (set) Token: 0x06000058 RID: 88 RVA: 0x00005BCC File Offset: 0x00003DCC
        [DataSourceProperty]
        public CharacterImageIdentifierVM ImageIdentifier3
        {
            get
            {
                return this._imageIdentifier_3;
            }
            set
            {
                bool flag = value != this._imageIdentifier_3;
                if (flag)
                {
                    this._imageIdentifier_3 = value;
                    base.OnPropertyChangedWithValue<CharacterImageIdentifierVM>(value, "ImageIdentifier3");
                }
            }
        }

        // Token: 0x17000016 RID: 22
        // (get) Token: 0x06000059 RID: 89 RVA: 0x00005C00 File Offset: 0x00003E00
        // (set) Token: 0x0600005A RID: 90 RVA: 0x00005C18 File Offset: 0x00003E18
        [DataSourceProperty]
        public CharacterViewModel UnitCharacter
        {
            get
            {
                return this._unitCharacter;
            }
            set
            {
                bool flag = value != this._unitCharacter;
                if (flag)
                {
                    this._unitCharacter = value;
                    base.OnPropertyChangedWithValue<CharacterViewModel>(value, "UnitCharacter");
                }
            }
        }

        // Token: 0x17000017 RID: 23
        // (get) Token: 0x0600005B RID: 91 RVA: 0x00005C4C File Offset: 0x00003E4C
        // (set) Token: 0x0600005C RID: 92 RVA: 0x00005C64 File Offset: 0x00003E64
        [DataSourceProperty]
        public string Disable_1_Brush
        {
            get
            {
                return this._disable_1_brush;
            }
            set
            {
                bool flag = value != this._disable_1_brush;
                if (flag)
                {
                    this._disable_1_brush = value;
                }
            }
        }

        // Token: 0x17000018 RID: 24
        // (get) Token: 0x0600005D RID: 93 RVA: 0x00005C8C File Offset: 0x00003E8C
        // (set) Token: 0x0600005E RID: 94 RVA: 0x00005CA4 File Offset: 0x00003EA4
        [DataSourceProperty]
        public string Disable_2_Brush
        {
            get
            {
                return this._disable_2_brush;
            }
            set
            {
                bool flag = value != this._disable_2_brush;
                if (flag)
                {
                    this._disable_2_brush = value;
                }
            }
        }

        // Token: 0x17000019 RID: 25
        // (get) Token: 0x0600005F RID: 95 RVA: 0x00005CCC File Offset: 0x00003ECC
        // (set) Token: 0x06000060 RID: 96 RVA: 0x00005CE4 File Offset: 0x00003EE4
        [DataSourceProperty]
        public string Disable_3_Brush
        {
            get
            {
                return this._disable_3_brush;
            }
            set
            {
                bool flag = value != this._disable_3_brush;
                if (flag)
                {
                    this._disable_3_brush = value;
                }
            }
        }

        // Token: 0x1700001A RID: 26
        // (get) Token: 0x06000061 RID: 97 RVA: 0x00005D0C File Offset: 0x00003F0C
        // (set) Token: 0x06000062 RID: 98 RVA: 0x00005D24 File Offset: 0x00003F24
        [DataSourceProperty]
        public string Disable_1_Text
        {
            get
            {
                return this._disable_1_text;
            }
            set
            {
                bool flag = value != this._disable_1_text;
                if (flag)
                {
                    this._disable_1_text = value;
                    base.OnPropertyChangedWithValue<string>(value, "Disable_1_Text");
                }
            }
        }

        // Token: 0x1700001B RID: 27
        // (get) Token: 0x06000063 RID: 99 RVA: 0x00005D58 File Offset: 0x00003F58
        // (set) Token: 0x06000064 RID: 100 RVA: 0x00005D70 File Offset: 0x00003F70
        [DataSourceProperty]
        public string Disable_2_Text
        {
            get
            {
                return this._disable_2_text;
            }
            set
            {
                bool flag = value != this._disable_2_text;
                if (flag)
                {
                    this._disable_2_text = value;
                    base.OnPropertyChangedWithValue<string>(value, "Disable_2_Text");
                }
            }
        }

        // Token: 0x1700001C RID: 28
        // (get) Token: 0x06000065 RID: 101 RVA: 0x00005DA4 File Offset: 0x00003FA4
        // (set) Token: 0x06000066 RID: 102 RVA: 0x00005DBC File Offset: 0x00003FBC
        [DataSourceProperty]
        public string Disable_3_Text
        {
            get
            {
                return this._disable_3_text;
            }
            set
            {
                bool flag = value != this._disable_3_text;
                if (flag)
                {
                    this._disable_3_text = value;
                    base.OnPropertyChangedWithValue<string>(value, "Disable_3_Text");
                }
            }
        }

        // Token: 0x1700001D RID: 29
        // (get) Token: 0x06000067 RID: 103 RVA: 0x00005DF0 File Offset: 0x00003FF0
        // (set) Token: 0x06000068 RID: 104 RVA: 0x00005E08 File Offset: 0x00004008
        [DataSourceProperty]
        public string PointsLeft
        {
            get
            {
                return this._pointsLeft;
            }
            set
            {
                bool flag = value != this._pointsLeft;
                if (flag)
                {
                    this._pointsLeft = value;
                    base.OnPropertyChangedWithValue<string>(value, "PointsLeft");
                }
            }
        }

        // Token: 0x1700001E RID: 30
        // (get) Token: 0x06000069 RID: 105 RVA: 0x00005E3C File Offset: 0x0000403C
        // (set) Token: 0x0600006A RID: 106 RVA: 0x00005E54 File Offset: 0x00004054
        [DataSourceProperty]
        public string ArmourFilter
        {
            get
            {
                return this._armour_filter;
            }
            set
            {
                bool flag = value != this._armour_filter;
                if (flag)
                {
                    this._armour_filter = value;
                    base.OnPropertyChangedWithValue<string>(value, "ArmourFilter");
                }
            }
        }

        // Token: 0x1700001F RID: 31
        // (get) Token: 0x0600006B RID: 107 RVA: 0x00005E88 File Offset: 0x00004088
        // (set) Token: 0x0600006C RID: 108 RVA: 0x00005EA0 File Offset: 0x000040A0
        [DataSourceProperty]
        public string WeaponFilter
        {
            get
            {
                return this._weapon_filter;
            }
            set
            {
                bool flag = value != this._weapon_filter;
                if (flag)
                {
                    this._weapon_filter = value;
                    base.OnPropertyChangedWithValue<string>(value, "WeaponFilter");
                }
            }
        }

        // Token: 0x17000020 RID: 32
        // (get) Token: 0x0600006D RID: 109 RVA: 0x00005ED4 File Offset: 0x000040D4
        // (set) Token: 0x0600006E RID: 110 RVA: 0x00005EEC File Offset: 0x000040EC
        [DataSourceProperty]
        public string CultureFilter
        {
            get
            {
                return this._culture_filter;
            }
            set
            {
                bool flag = value != this._culture_filter;
                if (flag)
                {
                    this._culture_filter = value;
                    base.OnPropertyChangedWithValue<string>(value, "CultureFilter");
                }
            }
        }

        // Token: 0x17000021 RID: 33
        // (get) Token: 0x0600006F RID: 111 RVA: 0x00005F20 File Offset: 0x00004120
        // (set) Token: 0x06000070 RID: 112 RVA: 0x00005F38 File Offset: 0x00004138
        [DataSourceProperty]
        public string ItemTierFilter
        {
            get
            {
                return this._item_tier_filter;
            }
            set
            {
                bool flag = value != this._item_tier_filter;
                if (flag)
                {
                    this._item_tier_filter = value;
                    base.OnPropertyChangedWithValue<string>(value, "ItemTierFilter");
                }
            }
        }

        // Token: 0x17000022 RID: 34
        // (get) Token: 0x06000071 RID: 113 RVA: 0x00005F6C File Offset: 0x0000416C
        // (set) Token: 0x06000072 RID: 114 RVA: 0x00005F84 File Offset: 0x00004184
        [DataSourceProperty]
        public string EquipmentSetTotalCost
        {
            get
            {
                return this._equipment_set_total_cost;
            }
            set
            {
                bool flag = value != this._equipment_set_total_cost;
                if (flag)
                {
                    this._equipment_set_total_cost = value;
                    base.OnPropertyChangedWithValue<string>(value, "EquipmentSetTotalCost");
                }
            }
        }

        // Token: 0x17000023 RID: 35
        // (get) Token: 0x06000073 RID: 115 RVA: 0x00005FB8 File Offset: 0x000041B8
        // (set) Token: 0x06000074 RID: 116 RVA: 0x00005FD0 File Offset: 0x000041D0
        [DataSourceProperty]
        public string SkillLevelCap
        {
            get
            {
                return this._skiil_level_cap;
            }
            set
            {
                bool flag = value != this._skiil_level_cap;
                if (flag)
                {
                    this._skiil_level_cap = value;
                    base.OnPropertyChangedWithValue<string>(value, "SkillLevelCap");
                }
            }
        }

        // Token: 0x17000024 RID: 36
        // (get) Token: 0x06000075 RID: 117 RVA: 0x00006004 File Offset: 0x00004204
        // (set) Token: 0x06000076 RID: 118 RVA: 0x0000601C File Offset: 0x0000421C
        [DataSourceProperty]
        public string UpgradeButton1
        {
            get
            {
                return this._upgrade_button_1;
            }
            set
            {
                bool flag = value != this._upgrade_button_1;
                if (flag)
                {
                    this._upgrade_button_1 = value;
                    base.OnPropertyChangedWithValue<string>(value, "UpgradeButton1");
                }
            }
        }

        // Token: 0x17000025 RID: 37
        // (get) Token: 0x06000077 RID: 119 RVA: 0x00006050 File Offset: 0x00004250
        // (set) Token: 0x06000078 RID: 120 RVA: 0x00006068 File Offset: 0x00004268
        [DataSourceProperty]
        public string UpgradeButton2
        {
            get
            {
                return this._upgrade_button_2;
            }
            set
            {
                bool flag = value != this._upgrade_button_2;
                if (flag)
                {
                    this._upgrade_button_2 = value;
                    base.OnPropertyChangedWithValue<string>(value, "UpgradeButton2");
                }
            }
        }

        // Token: 0x17000026 RID: 38
        // (get) Token: 0x06000079 RID: 121 RVA: 0x0000609C File Offset: 0x0000429C
        // (set) Token: 0x0600007A RID: 122 RVA: 0x000060B4 File Offset: 0x000042B4
        [DataSourceProperty]
        public MBBindingList<EncyclopediaSkillVM> Skills
        {
            get
            {
                return this._skills;
            }
            set
            {
                bool flag = value != this._skills;
                if (flag)
                {
                    this._skills = value;
                    base.OnPropertyChangedWithValue<MBBindingList<EncyclopediaSkillVM>>(value, "Skills");
                }
            }
        }

        // Token: 0x17000027 RID: 39
        // (get) Token: 0x0600007B RID: 123 RVA: 0x000060E8 File Offset: 0x000042E8
        // (set) Token: 0x0600007C RID: 124 RVA: 0x00006100 File Offset: 0x00004300
        [DataSourceProperty]
        public MBBindingList<CharacterEquipmentItemVM> Weapon1
        {
            get
            {
                return this._weapons1;
            }
            set
            {
                bool flag = value != this._weapons1;
                if (flag)
                {
                    this._weapons1 = value;
                    base.OnPropertyChangedWithValue<MBBindingList<CharacterEquipmentItemVM>>(value, "Weapons1");
                }
            }
        }

        // Token: 0x17000028 RID: 40
        // (get) Token: 0x0600007D RID: 125 RVA: 0x00006134 File Offset: 0x00004334
        // (set) Token: 0x0600007E RID: 126 RVA: 0x0000614C File Offset: 0x0000434C
        [DataSourceProperty]
        public MBBindingList<CharacterEquipmentItemVM> Weapon2
        {
            get
            {
                return this._weapons2;
            }
            set
            {
                bool flag = value != this._weapons2;
                if (flag)
                {
                    this._weapons2 = value;
                    base.OnPropertyChangedWithValue<MBBindingList<CharacterEquipmentItemVM>>(value, "Weapons2");
                }
            }
        }

        // Token: 0x17000029 RID: 41
        // (get) Token: 0x0600007F RID: 127 RVA: 0x00006180 File Offset: 0x00004380
        // (set) Token: 0x06000080 RID: 128 RVA: 0x00006198 File Offset: 0x00004398
        [DataSourceProperty]
        public MBBindingList<CharacterEquipmentItemVM> Weapon3
        {
            get
            {
                return this._weapons3;
            }
            set
            {
                bool flag = value != this._weapons3;
                if (flag)
                {
                    this._weapons3 = value;
                    base.OnPropertyChangedWithValue<MBBindingList<CharacterEquipmentItemVM>>(value, "Weapons3");
                }
            }
        }

        // Token: 0x1700002A RID: 42
        // (get) Token: 0x06000081 RID: 129 RVA: 0x000061CC File Offset: 0x000043CC
        // (set) Token: 0x06000082 RID: 130 RVA: 0x000061E4 File Offset: 0x000043E4
        [DataSourceProperty]
        public MBBindingList<CharacterEquipmentItemVM> Weapon4
        {
            get
            {
                return this._weapons4;
            }
            set
            {
                bool flag = value != this._weapons4;
                if (flag)
                {
                    this._weapons4 = value;
                    base.OnPropertyChangedWithValue<MBBindingList<CharacterEquipmentItemVM>>(value, "Weapons4");
                }
            }
        }

        // Token: 0x1700002B RID: 43
        // (get) Token: 0x06000083 RID: 131 RVA: 0x00006218 File Offset: 0x00004418
        // (set) Token: 0x06000084 RID: 132 RVA: 0x00006230 File Offset: 0x00004430
        [DataSourceProperty]
        public MBBindingList<CharacterEquipmentItemVM> Helmet
        {
            get
            {
                return this._helmet;
            }
            set
            {
                bool flag = value != this._helmet;
                if (flag)
                {
                    this._helmet = value;
                    base.OnPropertyChangedWithValue<MBBindingList<CharacterEquipmentItemVM>>(value, "Helmet");
                }
            }
        }

        // Token: 0x1700002C RID: 44
        // (get) Token: 0x06000085 RID: 133 RVA: 0x00006264 File Offset: 0x00004464
        // (set) Token: 0x06000086 RID: 134 RVA: 0x0000627C File Offset: 0x0000447C
        [DataSourceProperty]
        public MBBindingList<CharacterEquipmentItemVM> Cape
        {
            get
            {
                return this._cape;
            }
            set
            {
                bool flag = value != this._cape;
                if (flag)
                {
                    this._cape = value;
                    base.OnPropertyChangedWithValue<MBBindingList<CharacterEquipmentItemVM>>(value, "Cape");
                }
            }
        }

        // Token: 0x1700002D RID: 45
        // (get) Token: 0x06000087 RID: 135 RVA: 0x000062B0 File Offset: 0x000044B0
        // (set) Token: 0x06000088 RID: 136 RVA: 0x000062C8 File Offset: 0x000044C8
        [DataSourceProperty]
        public MBBindingList<CharacterEquipmentItemVM> Chest
        {
            get
            {
                return this._chest;
            }
            set
            {
                bool flag = value != this._chest;
                if (flag)
                {
                    this._chest = value;
                    base.OnPropertyChangedWithValue<MBBindingList<CharacterEquipmentItemVM>>(value, "Chest");
                }
            }
        }

        // Token: 0x1700002E RID: 46
        // (get) Token: 0x06000089 RID: 137 RVA: 0x000062FC File Offset: 0x000044FC
        // (set) Token: 0x0600008A RID: 138 RVA: 0x00006314 File Offset: 0x00004514
        [DataSourceProperty]
        public MBBindingList<CharacterEquipmentItemVM> Gloves
        {
            get
            {
                return this._gloves;
            }
            set
            {
                bool flag = value != this._gloves;
                if (flag)
                {
                    this._gloves = value;
                    base.OnPropertyChangedWithValue<MBBindingList<CharacterEquipmentItemVM>>(value, "Gloves");
                }
            }
        }

        // Token: 0x1700002F RID: 47
        // (get) Token: 0x0600008B RID: 139 RVA: 0x00006348 File Offset: 0x00004548
        // (set) Token: 0x0600008C RID: 140 RVA: 0x00006360 File Offset: 0x00004560
        [DataSourceProperty]
        public MBBindingList<CharacterEquipmentItemVM> Boots
        {
            get
            {
                return this._boots;
            }
            set
            {
                bool flag = value != this._boots;
                if (flag)
                {
                    this._boots = value;
                    base.OnPropertyChangedWithValue<MBBindingList<CharacterEquipmentItemVM>>(value, "Boots");
                }
            }
        }

        // Token: 0x17000030 RID: 48
        // (get) Token: 0x0600008D RID: 141 RVA: 0x00006394 File Offset: 0x00004594
        // (set) Token: 0x0600008E RID: 142 RVA: 0x000063AC File Offset: 0x000045AC
        [DataSourceProperty]
        public MBBindingList<CharacterEquipmentItemVM> Mount
        {
            get
            {
                return this._mount;
            }
            set
            {
                bool flag = value != this._mount;
                if (flag)
                {
                    this._mount = value;
                    base.OnPropertyChangedWithValue<MBBindingList<CharacterEquipmentItemVM>>(value, "Mount");
                }
            }
        }

        // Token: 0x17000031 RID: 49
        // (get) Token: 0x0600008F RID: 143 RVA: 0x000063E0 File Offset: 0x000045E0
        // (set) Token: 0x06000090 RID: 144 RVA: 0x000063F8 File Offset: 0x000045F8
        [DataSourceProperty]
        public MBBindingList<CharacterEquipmentItemVM> Harness
        {
            get
            {
                return this._harness;
            }
            set
            {
                bool flag = value != this._harness;
                if (flag)
                {
                    this._harness = value;
                    base.OnPropertyChangedWithValue<MBBindingList<CharacterEquipmentItemVM>>(value, "Harness");
                }
            }
        }

        // Token: 0x17000032 RID: 50
        // (get) Token: 0x06000091 RID: 145 RVA: 0x0000642C File Offset: 0x0000462C
        // (set) Token: 0x06000092 RID: 146 RVA: 0x00006434 File Offset: 0x00004634
        [DataSourceProperty]
        public MBBindingList<BindingListStringItem> Weapon1Name
        {
            get
            {
                return this._weapon1_name;
            }
            set
            {
                bool flag = value == this._weapon1_name;
                if (!flag)
                {
                    this._weapon1_name = value;
                    base.OnPropertyChanged("Weapon1Name");
                }
            }
        }

        // Token: 0x17000033 RID: 51
        // (get) Token: 0x06000093 RID: 147 RVA: 0x00006464 File Offset: 0x00004664
        // (set) Token: 0x06000094 RID: 148 RVA: 0x0000646C File Offset: 0x0000466C
        public MBBindingList<BindingListStringItem> Weapon2Name
        {
            get
            {
                return this._weapon2_name;
            }
            set
            {
                bool flag = value == this._weapon2_name;
                if (!flag)
                {
                    this._weapon2_name = value;
                    base.OnPropertyChanged("Weapon2Name");
                }
            }
        }

        // Token: 0x17000034 RID: 52
        // (get) Token: 0x06000095 RID: 149 RVA: 0x0000649C File Offset: 0x0000469C
        // (set) Token: 0x06000096 RID: 150 RVA: 0x000064A4 File Offset: 0x000046A4
        public MBBindingList<BindingListStringItem> Weapon3Name
        {
            get
            {
                return this._weapon3_name;
            }
            set
            {
                bool flag = value == this._weapon3_name;
                if (!flag)
                {
                    this._weapon3_name = value;
                    base.OnPropertyChanged("Weapon3Name");
                }
            }
        }

        // Token: 0x17000035 RID: 53
        // (get) Token: 0x06000097 RID: 151 RVA: 0x000064D4 File Offset: 0x000046D4
        // (set) Token: 0x06000098 RID: 152 RVA: 0x000064DC File Offset: 0x000046DC
        public MBBindingList<BindingListStringItem> Weapon4Name
        {
            get
            {
                return this._weapon4_name;
            }
            set
            {
                bool flag = value == this._weapon4_name;
                if (!flag)
                {
                    this._weapon4_name = value;
                    base.OnPropertyChanged("Weapon4Name");
                }
            }
        }

        // Token: 0x17000036 RID: 54
        // (get) Token: 0x06000099 RID: 153 RVA: 0x0000650C File Offset: 0x0000470C
        // (set) Token: 0x0600009A RID: 154 RVA: 0x00006514 File Offset: 0x00004714
        public MBBindingList<BindingListStringItem> HelmetName
        {
            get
            {
                return this._helmet_name;
            }
            set
            {
                bool flag = value == this._helmet_name;
                if (!flag)
                {
                    this._helmet_name = value;
                    base.OnPropertyChanged("HelmetName");
                }
            }
        }

        // Token: 0x17000037 RID: 55
        // (get) Token: 0x0600009B RID: 155 RVA: 0x00006544 File Offset: 0x00004744
        // (set) Token: 0x0600009C RID: 156 RVA: 0x0000654C File Offset: 0x0000474C
        public MBBindingList<BindingListStringItem> CapeName
        {
            get
            {
                return this._cape_name;
            }
            set
            {
                bool flag = value == this._cape_name;
                if (!flag)
                {
                    this._cape_name = value;
                    base.OnPropertyChanged("CapeName");
                }
            }
        }

        // Token: 0x17000038 RID: 56
        // (get) Token: 0x0600009D RID: 157 RVA: 0x0000657C File Offset: 0x0000477C
        // (set) Token: 0x0600009E RID: 158 RVA: 0x00006584 File Offset: 0x00004784
        public MBBindingList<BindingListStringItem> ChestName
        {
            get
            {
                return this._chest_name;
            }
            set
            {
                bool flag = value == this._chest_name;
                if (!flag)
                {
                    this._chest_name = value;
                    base.OnPropertyChanged("ChestName");
                }
            }
        }

        // Token: 0x17000039 RID: 57
        // (get) Token: 0x0600009F RID: 159 RVA: 0x000065B4 File Offset: 0x000047B4
        // (set) Token: 0x060000A0 RID: 160 RVA: 0x000065BC File Offset: 0x000047BC
        public MBBindingList<BindingListStringItem> GlovesName
        {
            get
            {
                return this._gloves_name;
            }
            set
            {
                bool flag = value == this._gloves_name;
                if (!flag)
                {
                    this._gloves_name = value;
                    base.OnPropertyChanged("GlovesName");
                }
            }
        }

        // Token: 0x1700003A RID: 58
        // (get) Token: 0x060000A1 RID: 161 RVA: 0x000065EC File Offset: 0x000047EC
        // (set) Token: 0x060000A2 RID: 162 RVA: 0x000065F4 File Offset: 0x000047F4
        public MBBindingList<BindingListStringItem> BootsName
        {
            get
            {
                return this._boots_name;
            }
            set
            {
                bool flag = value == this._boots_name;
                if (!flag)
                {
                    this._boots_name = value;
                    base.OnPropertyChanged("BootsName");
                }
            }
        }

        // Token: 0x1700003B RID: 59
        // (get) Token: 0x060000A3 RID: 163 RVA: 0x00006624 File Offset: 0x00004824
        // (set) Token: 0x060000A4 RID: 164 RVA: 0x0000662C File Offset: 0x0000482C
        public MBBindingList<BindingListStringItem> MountName
        {
            get
            {
                return this._mount_name;
            }
            set
            {
                bool flag = value == this._mount_name;
                if (!flag)
                {
                    this._mount_name = value;
                    base.OnPropertyChanged("MountName");
                }
            }
        }

        // Token: 0x1700003C RID: 60
        // (get) Token: 0x060000A5 RID: 165 RVA: 0x0000665C File Offset: 0x0000485C
        // (set) Token: 0x060000A6 RID: 166 RVA: 0x00006664 File Offset: 0x00004864
        public MBBindingList<BindingListStringItem> HarnessName
        {
            get
            {
                return this._harness_name;
            }
            set
            {
                bool flag = value == this._harness_name;
                if (!flag)
                {
                    this._harness_name = value;
                    base.OnPropertyChanged("HarnessName");
                }
            }
        }

        // Token: 0x1700003D RID: 61
        // (get) Token: 0x060000A7 RID: 167 RVA: 0x00006694 File Offset: 0x00004894
        // (set) Token: 0x060000A8 RID: 168 RVA: 0x0000669C File Offset: 0x0000489C
        public MBBindingList<BindingListStringItem> GenderString
        {
            get
            {
                return this._gender_string;
            }
            set
            {
                bool flag = value == this._gender_string;
                if (!flag)
                {
                    this._gender_string = value;
                    base.OnPropertyChanged("GenderString");
                }
            }
        }

        // Token: 0x1700003E RID: 62
        // (get) Token: 0x060000A9 RID: 169 RVA: 0x000066CC File Offset: 0x000048CC
        // (set) Token: 0x060000AA RID: 170 RVA: 0x000066D4 File Offset: 0x000048D4
        public MBBindingList<BindingListStringItem> CultureString
        {
            get
            {
                return this._culture_string;
            }
            set
            {
                bool flag = value == this._culture_string;
                if (!flag)
                {
                    this._culture_string = value;
                    base.OnPropertyChanged("CultureString");
                }
            }
        }

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
            this.CultureString = new MBBindingList<BindingListStringItem>();
            this.CultureString.Add(new BindingListStringItem("Culture : " + this._character.Culture.Name.ToString()));
            RefreshValues();
        }

        // Token: 0x1700003F RID: 63
        // (get) Token: 0x060000AB RID: 171 RVA: 0x00006704 File Offset: 0x00004904
        // (set) Token: 0x060000AC RID: 172 RVA: 0x0000670C File Offset: 0x0000490C
        public MBBindingList<BindingListStringItem> Name
        {
            get
            {
                return this._name;
            }
            set
            {
                bool flag = value == this._name;
                if (!flag)
                {
                    this._name = value;
                    base.OnPropertyChanged("Name");
                }
            }
        }

        // Token: 0x17000040 RID: 64
        // (get) Token: 0x060000AD RID: 173 RVA: 0x0000673C File Offset: 0x0000493C
        // (set) Token: 0x060000AE RID: 174 RVA: 0x00006754 File Offset: 0x00004954
        [DataSourceProperty]
        public MBBindingList<StringItemWithHintVM> PropertiesList
        {
            get
            {
                return this._propertiesList;
            }
            set
            {
                bool flag = value != this._propertiesList;
                if (flag)
                {
                    this._propertiesList = value;
                    base.OnPropertyChangedWithValue<MBBindingList<StringItemWithHintVM>>(value, "PropertiesList");
                }
            }
        }

        // Token: 0x060000AF RID: 175 RVA: 0x00006788 File Offset: 0x00004988
        public void Close()
        {
            CustomUnitsBehavior.DeleteVMLayer();
            Campaign.Current.EncyclopediaManager.GoToLink(this._character.EncyclopediaLink);
        }

        // Token: 0x060000B0 RID: 176 RVA: 0x000067AC File Offset: 0x000049AC
        public override void RefreshValues()
        {
            base.RefreshValues();
            this.Name.Clear();
            this.Name.Add(new BindingListStringItem(this._character.Name.ToString()));
            this.PropertiesList.Clear();
            this.PropertiesList.Add(CampaignUIHelper.GetCharacterTierData(this._character, true));
            this.PropertiesList.Add(CampaignUIHelper.GetCharacterTypeData(this._character, true));
            this.Skills.Clear();
            this.Skills.Add(new EncyclopediaSkillVM(DefaultSkills.OneHanded, this._character.GetSkillValue(DefaultSkills.OneHanded)));
            this.Skills.Add(new EncyclopediaSkillVM(DefaultSkills.TwoHanded, this._character.GetSkillValue(DefaultSkills.TwoHanded)));
            this.Skills.Add(new EncyclopediaSkillVM(DefaultSkills.Polearm, this._character.GetSkillValue(DefaultSkills.Polearm)));
            this.Skills.Add(new EncyclopediaSkillVM(DefaultSkills.Bow, this._character.GetSkillValue(DefaultSkills.Bow)));
            this.Skills.Add(new EncyclopediaSkillVM(DefaultSkills.Crossbow, this._character.GetSkillValue(DefaultSkills.Crossbow)));
            this.Skills.Add(new EncyclopediaSkillVM(DefaultSkills.Throwing, this._character.GetSkillValue(DefaultSkills.Throwing)));
            this.Skills.Add(new EncyclopediaSkillVM(DefaultSkills.Athletics, this._character.GetSkillValue(DefaultSkills.Athletics)));
            this.Skills.Add(new EncyclopediaSkillVM(DefaultSkills.Riding, this._character.GetSkillValue(DefaultSkills.Riding)));
            this._equipmentSetTextObj = new TextObject("{=vggt7exj}Set {CURINDEX}/{COUNT}", null);
            this.EquipmentSetSelector = new SelectorVM<EncyclopediaUnitEquipmentSetSelectorItemVM>(0, new Action<SelectorVM<EncyclopediaUnitEquipmentSetSelectorItemVM>>(this.OnEquipmentSetChange));
            this.EquipmentSetSelector.ItemList.Clear();
            using (IEnumerator<Equipment> enumerator = this._character.BattleEquipments.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    Equipment equipment = enumerator.Current;
                    bool flag = !this.EquipmentSetSelector.ItemList.Any((EncyclopediaUnitEquipmentSetSelectorItemVM x) => x.EquipmentSet.IsEquipmentEqualTo(equipment));
                    if (flag)
                    {
                        this.EquipmentSetSelector.AddItem(new EncyclopediaUnitEquipmentSetSelectorItemVM(equipment, ""));
                    }
                }
            }
            bool flag2 = this.EquipmentSetSelector.ItemList.Count > 0;
            if (flag2)
            {
                this.EquipmentSetSelector.SelectedIndex = 0;
            }
            this._equipmentSetTextObj.SetTextVariable("CURINDEX", this.EquipmentSetSelector.SelectedIndex + 1);
            this._equipmentSetTextObj.SetTextVariable("COUNT", this.EquipmentSetSelector.ItemList.Count);
            this.EquipmentSetText = this._equipmentSetTextObj.ToString();
            this.Weapon1.Clear();
            this.Weapon1.Add(new CharacterEquipmentItemVM(this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][0].Item));
            this.Weapon1Name = new MBBindingList<BindingListStringItem>();
            this.Weapon1Name.Add(new BindingListStringItem("Weapon 1 : " + ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][0].Item == null) ? "None" : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][0].Item.Name.ToString())));
            this.Weapon2.Clear();
            this.Weapon2.Add(new CharacterEquipmentItemVM(this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][1].Item));
            this.Weapon2Name = new MBBindingList<BindingListStringItem>();
            this.Weapon2Name.Add(new BindingListStringItem("Weapon 2 : " + ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][1].Item == null) ? "None" : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][1].Item.Name.ToString())));
            this.Weapon3.Clear();
            this.Weapon3.Add(new CharacterEquipmentItemVM(this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][2].Item));
            this.Weapon3Name = new MBBindingList<BindingListStringItem>();
            this.Weapon3Name.Add(new BindingListStringItem("Weapon 3 : " + ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][2].Item == null) ? "None" : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][2].Item.Name.ToString())));
            this.Weapon4.Clear();
            this.Weapon4.Add(new CharacterEquipmentItemVM(this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][3].Item));
            this.Weapon4Name = new MBBindingList<BindingListStringItem>();
            this.Weapon4Name.Add(new BindingListStringItem("Weapon 4 : " + ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][3].Item == null) ? "None" : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][3].Item.Name.ToString())));
            this.Helmet.Clear();
            this.Helmet.Add(new CharacterEquipmentItemVM(this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][5].Item));
            this.HelmetName = new MBBindingList<BindingListStringItem>();
            this.HelmetName.Add(new BindingListStringItem("Helmet : " + ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][5].Item == null) ? "None" : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][5].Item.Name.ToString())));
            this.Cape.Clear();
            this.Cape.Add(new CharacterEquipmentItemVM(this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][9].Item));
            this.CapeName = new MBBindingList<BindingListStringItem>();
            this.CapeName.Add(new BindingListStringItem("Cape : " + ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][9].Item == null) ? "None" : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][9].Item.Name.ToString())));
            this.Chest.Clear();
            this.Chest.Add(new CharacterEquipmentItemVM(this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][6].Item));
            this.ChestName = new MBBindingList<BindingListStringItem>();
            this.ChestName.Add(new BindingListStringItem("Chest : " + ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][6].Item == null) ? "None" : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][6].Item.Name.ToString())));
            this.Gloves.Clear();
            this.Gloves.Add(new CharacterEquipmentItemVM(this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][8].Item));
            this.GlovesName = new MBBindingList<BindingListStringItem>();
            this.GlovesName.Add(new BindingListStringItem("Gloves : " + ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][8].Item == null) ? "None" : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][8].Item.Name.ToString())));
            this.Boots.Clear();
            this.Boots.Add(new CharacterEquipmentItemVM(this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][7].Item));
            this.BootsName = new MBBindingList<BindingListStringItem>();
            this.BootsName.Add(new BindingListStringItem("Boots : " + ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][7].Item == null) ? "None" : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][7].Item.Name.ToString())));
            this.Mount.Clear();
            this.Mount.Add(new CharacterEquipmentItemVM(this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][10].Item));
            this.MountName = new MBBindingList<BindingListStringItem>();
            this.MountName.Add(new BindingListStringItem("Mount : " + ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][10].Item == null) ? "None" : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][10].Item.Name.ToString())));
            this.Harness.Clear();
            this.Harness.Add(new CharacterEquipmentItemVM(this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][11].Item));
            this.HarnessName = new MBBindingList<BindingListStringItem>();
            this.HarnessName.Add(new BindingListStringItem("Harness: " + ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][11].Item == null) ? "None" : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][11].Item.Name.ToString())));
            this.GenderString = new MBBindingList<BindingListStringItem>();
            this.GenderString.Add(new BindingListStringItem("Gender : " + (this._character.IsFemale ? "Female" : "Male")));
            this.CultureString = new MBBindingList<BindingListStringItem>();
            this.CultureString.Add(new BindingListStringItem("Culture : " + this._character.Culture.Name.ToString()));
            this.NavalString = new MBBindingList<BindingListStringItem>();
            this.NavalString.Add(new BindingListStringItem("Naval Soldier : " + this._character.IsNavalSoldier().ToString()));
            this._character = Game.Current.ObjectManager.GetObject<CharacterObject>(this._unit_id);
            //Originally this.UnitCharacter = new CharacterViewModel();
            this.UnitCharacter = new CharacterViewModel();
            this.UnitCharacter.FillFrom(this._character, -1);
            this.PointsLeft = this.pointLeft().ToString();
            this.SkillLevelCap = CustomUnitsBehavior.skillCap(this._unit_id).ToString();
            bool flag3 = this._character.UpgradeTargets != null && this._character.UpgradeTargets.Length != 0;
            if (flag3)
            {
                this.ImageIdentifier1 = new CharacterImageIdentifierVM(CharacterCode.CreateFrom(this._character.UpgradeTargets[0]));
                this._upgrade_1 = this._character.UpgradeTargets[0];
            }
            else
            {
                this.ImageIdentifier1 = new CharacterImageIdentifierVM(null);
                this._upgrade_1 = null;
            }
            bool flag4 = this._character.UpgradeTargets != null && this._character.UpgradeTargets.Length > 1;
            if (flag4)
            {
                this.ImageIdentifier2 = new CharacterImageIdentifierVM(CharacterCode.CreateFrom(this._character.UpgradeTargets[1]));
                this._upgrade_2 = this._character.UpgradeTargets[1];
            }
            else
            {
                this.ImageIdentifier2 = new CharacterImageIdentifierVM(null);
                this._upgrade_2 = null;
            }
            bool flag5 = Game.Current.ObjectManager.GetObject<CharacterObject>(this._unit_id.Substring(0, this._unit_id.Length - 1)) != null;
            if (flag5)
            {
                this.ImageIdentifier3 = new CharacterImageIdentifierVM(CharacterCode.CreateFrom(Game.Current.ObjectManager.GetObject<CharacterObject>(this._unit_id.Substring(0, this._unit_id.Length - 1))));
                this._root = Game.Current.ObjectManager.GetObject<CharacterObject>(this._unit_id.Substring(0, this._unit_id.Length - 1));
            }
            else
            {
                bool flag6 = !EnyclopediaEditUnitPatch.isCustomTroop(this._character) && this.GetUpgradeRoot() != null;
                if (flag6)
                {
                    this.ImageIdentifier3 = new CharacterImageIdentifierVM(CharacterCode.CreateFrom(this.GetUpgradeRoot()));
                }
                else
                {
                    this.ImageIdentifier3 = new CharacterImageIdentifierVM(null);
                    this._root = null;
                }
            }
            this.UpgradeButton1 = ((this._upgrade_1 == null) ? "Add" : "Remove");
            this.UpgradeButton2 = ((this._upgrade_2 == null) ? "Add" : "Remove");
            string text = "";
            foreach (int num in CustomUnitsBehavior._filter_tiers)
            {
                bool flag7 = text != "";
                if (flag7)
                {
                    text += ",";
                }
                text += num.ToString();
            }
            this.ItemTierFilter = text;
            string text2 = "";
            bool flag8 = TaleWorlds.Core.Extensions.IsEmpty<CultureObject>(CustomUnitsBehavior._filter_Culture);
            if (flag8)
            {
                text2 = "All";
            }
            else
            {
                foreach (CultureObject cultureObject in CustomUnitsBehavior._filter_Culture)
                {
                    bool flag9 = text2 != "";
                    if (flag9)
                    {
                        text2 += ",";
                    }
                    text2 += cultureObject.Name.ToString();
                }
            }
            this.CultureFilter = text2;
            string text3 = "";
            bool flag10 = TaleWorlds.Core.Extensions.IsEmpty<ItemObject.ItemTypeEnum>(CustomUnitsBehavior._filter_weapon_types);
            if (flag10)
            {
                text3 = "All";
            }
            else
            {
                foreach (ItemObject.ItemTypeEnum itemTypeEnum in CustomUnitsBehavior._filter_weapon_types)
                {
                    bool flag11 = text3 != "";
                    if (flag11)
                    {
                        text3 += ",";
                    }
                    text3 += itemTypeEnum.ToString();
                }
            }
            this.WeaponFilter = text3;
            string text4 = "";
            bool flag12 = TaleWorlds.Core.Extensions.IsEmpty<ArmorComponent.ArmorMaterialTypes>(CustomUnitsBehavior._filter_armour_types);
            if (flag12)
            {
                text4 = "All";
            }
            else
            {
                foreach (ArmorComponent.ArmorMaterialTypes armorMaterialTypes in CustomUnitsBehavior._filter_armour_types)
                {
                    bool flag13 = text4 != "";
                    if (flag13)
                    {
                        text4 += ",";
                    }
                    text4 += armorMaterialTypes.ToString();
                }
            }
            this.ArmourFilter = text4;
            bool disable_gear_restriction = CustomUnitsBehavior._disable_gear_restriction;
            if (disable_gear_restriction)
            {
                this.Disable_1_Brush = "ButtonBrush1";
                this.Disable_1_Text = "ON";
            }
            else
            {
                this.Disable_1_Brush = "ButtonBrush2";
                this.Disable_1_Text = "OFF";
            }
            bool disable_skill_total_restriction = CustomUnitsBehavior._disable_skill_total_restriction;
            if (disable_skill_total_restriction)
            {
                this.Disable_2_Brush = "ButtonBrush1";
                this.Disable_2_Text = "ON";
            }
            else
            {
                this.Disable_2_Brush = "ButtonBrush2";
                this.Disable_2_Text = "OFF";
            }
            bool disable_skill_cap_restriction = CustomUnitsBehavior._disable_skill_cap_restriction;
            if (disable_skill_cap_restriction)
            {
                this.Disable_3_Brush = "ButtonBrush1";
                this.Disable_3_Text = "ON";
            }
            else
            {
                this.Disable_3_Brush = "ButtonBrush2";
                this.Disable_3_Text = "OFF";
            }
            this.IsEnabled = EnyclopediaEditUnitPatch.isCustomTroop(this._character);
        }

        // Token: 0x060000B1 RID: 177 RVA: 0x00007A1C File Offset: 0x00005C1C
        public int pointLeft()
        {
            return this.getSkillPointsAvalible() - this._character.GetSkillValue(DefaultSkills.OneHanded) - this._character.GetSkillValue(DefaultSkills.TwoHanded) - this._character.GetSkillValue(DefaultSkills.Polearm) - this._character.GetSkillValue(DefaultSkills.Bow) - this._character.GetSkillValue(DefaultSkills.Crossbow) - this._character.GetSkillValue(DefaultSkills.Throwing) - this._character.GetSkillValue(DefaultSkills.Riding) - this._character.GetSkillValue(DefaultSkills.Athletics);
        }

        // Token: 0x060000B2 RID: 178 RVA: 0x00007ABC File Offset: 0x00005CBC
        public int getSkillPointsAvalible()
        {
            bool disable_skill_total_restriction = CustomUnitsBehavior._disable_skill_total_restriction;
            int result;
            if (disable_skill_total_restriction)
            {
                result = 10000;
            }
            else
            {
                switch (this._character.Tier)
                {
                    case 1:
                        result = 80;
                        break;
                    case 2:
                        result = 200;
                        break;
                    case 3:
                        result = 350;
                        break;
                    case 4:
                        result = 500;
                        break;
                    case 5:
                        result = 700;
                        break;
                    case 6:
                        result = 900;
                        break;
                    default:
                        result = 1200;
                        break;
                }
            }
            return result;
        }

        // Token: 0x060000B3 RID: 179 RVA: 0x00007B44 File Offset: 0x00005D44
        private void OnEquipmentSetChange(SelectorVM<EncyclopediaUnitEquipmentSetSelectorItemVM> selector)
        {
            this.CurrentSelectedEquipmentSet = selector.SelectedItem;
            this.UnitCharacter.SetEquipment(this.CurrentSelectedEquipmentSet.EquipmentSet);
            this._equipmentSetTextObj.SetTextVariable("CURINDEX", selector.SelectedIndex + 1);
            this._equipmentSetTextObj.SetTextVariable("COUNT", selector.ItemList.Count);
            this.EquipmentSetText = this._equipmentSetTextObj.ToString();
            int num = 0;
            bool flag = this.Weapon1 != null;
            if (flag)
            {
                this.Weapon1.Clear();
                this.Weapon1.Add(new CharacterEquipmentItemVM(this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][0].Item));
            }
            this.Weapon1Name = new MBBindingList<BindingListStringItem>();
            this.Weapon1Name.Add(new BindingListStringItem("Weapon 1 : " + ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][0].Item == null) ? "None" : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][0].Item.Name.ToString())));
            num += ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][0].Item == null) ? 0 : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][0].Item.Value);
            bool flag2 = this.Weapon2 != null;
            if (flag2)
            {
                this.Weapon2.Clear();
                this.Weapon2.Add(new CharacterEquipmentItemVM(this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][1].Item));
            }
            this.Weapon2Name = new MBBindingList<BindingListStringItem>();
            this.Weapon2Name.Add(new BindingListStringItem("Weapon 2 : " + ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][1].Item == null) ? "None" : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][1].Item.Name.ToString())));
            num += ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][1].Item == null) ? 0 : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][1].Item.Value);
            bool flag3 = this.Weapon3 != null;
            if (flag3)
            {
                this.Weapon3.Clear();
                this.Weapon3.Add(new CharacterEquipmentItemVM(this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][2].Item));
            }
            this.Weapon3Name = new MBBindingList<BindingListStringItem>();
            this.Weapon3Name.Add(new BindingListStringItem("Weapon 3 : " + ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][2].Item == null) ? "None" : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][2].Item.Name.ToString())));
            num += ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][2].Item == null) ? 0 : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][2].Item.Value);
            bool flag4 = this.Weapon4 != null;
            if (flag4)
            {
                this.Weapon4.Clear();
                this.Weapon4.Add(new CharacterEquipmentItemVM(this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][3].Item));
            }
            this.Weapon4Name = new MBBindingList<BindingListStringItem>();
            this.Weapon4Name.Add(new BindingListStringItem("Weapon 4 : " + ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][3].Item == null) ? "None" : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][3].Item.Name.ToString())));
            num += ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][3].Item == null) ? 0 : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][3].Item.Value);
            bool flag5 = this.Helmet != null;
            if (flag5)
            {
                this.Helmet.Clear();
                this.Helmet.Add(new CharacterEquipmentItemVM(this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][5].Item));
            }
            this.HelmetName = new MBBindingList<BindingListStringItem>();
            this.HelmetName.Add(new BindingListStringItem("Helmet : " + ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][5].Item == null) ? "None" : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][5].Item.Name.ToString())));
            num += ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][5].Item == null) ? 0 : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][5].Item.Value);
            bool flag6 = this.Cape != null;
            if (flag6)
            {
                this.Cape.Clear();
                this.Cape.Add(new CharacterEquipmentItemVM(this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][9].Item));
            }
            this.CapeName = new MBBindingList<BindingListStringItem>();
            this.CapeName.Add(new BindingListStringItem("Cape : " + ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][9].Item == null) ? "None" : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][9].Item.Name.ToString())));
            num += ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][9].Item == null) ? 0 : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][9].Item.Value);
            bool flag7 = this.Chest != null;
            if (flag7)
            {
                this.Chest.Clear();
                this.Chest.Add(new CharacterEquipmentItemVM(this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][6].Item));
            }
            this.ChestName = new MBBindingList<BindingListStringItem>();
            this.ChestName.Add(new BindingListStringItem("Chest : " + ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][6].Item == null) ? "None" : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][6].Item.Name.ToString())));
            num += ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][6].Item == null) ? 0 : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][6].Item.Value);
            bool flag8 = this.Gloves != null;
            if (flag8)
            {
                this.Gloves.Clear();
                this.Gloves.Add(new CharacterEquipmentItemVM(this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][8].Item));
            }
            this.GlovesName = new MBBindingList<BindingListStringItem>();
            this.GlovesName.Add(new BindingListStringItem("Gloves : " + ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][8].Item == null) ? "None" : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][8].Item.Name.ToString())));
            num += ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][8].Item == null) ? 0 : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][8].Item.Value);
            bool flag9 = this.Boots != null;
            if (flag9)
            {
                this.Boots.Clear();
                this.Boots.Add(new CharacterEquipmentItemVM(this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][7].Item));
            }
            this.BootsName = new MBBindingList<BindingListStringItem>();
            this.BootsName.Add(new BindingListStringItem("Boots : " + ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][7].Item == null) ? "None" : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][7].Item.Name.ToString())));
            num += ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][7].Item == null) ? 0 : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][7].Item.Value);
            bool flag10 = this.Mount != null;
            if (flag10)
            {
                this.Mount.Clear();
                this.Mount.Add(new CharacterEquipmentItemVM(this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][10].Item));
            }
            this.MountName = new MBBindingList<BindingListStringItem>();
            this.MountName.Add(new BindingListStringItem("Mount : " + ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][10].Item == null) ? "None" : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][10].Item.Name.ToString())));
            num += ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][10].Item == null) ? 0 : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][10].Item.Value);
            bool flag11 = this.Harness != null;
            if (flag11)
            {
                this.Harness.Clear();
                this.Harness.Add(new CharacterEquipmentItemVM(this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][11].Item));
            }
            this.HarnessName = new MBBindingList<BindingListStringItem>();
            this.HarnessName.Add(new BindingListStringItem("Harness: " + ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][11].Item == null) ? "None" : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][11].Item.Name.ToString())));
            this.EquipmentSetTotalCost = "Set Total Cost: " + (num + ((this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][11].Item == null) ? 0 : this._character.BattleEquipments.ToArray<Equipment>()[this.EquipmentSetSelector.SelectedIndex][11].Item.Value)).ToString() + "<img src=\"General\\Icons\\Coin@2x\" extend=\"8\"/>";
        }

        // Token: 0x060000B4 RID: 180 RVA: 0x000089F4 File Offset: 0x00006BF4
        public void OneHandPlus()
        {
           
            bool flag = InputEx.IsKeyDown(29) || InputEx.IsKeyDown(157);
            if (flag)
            {
                CustomUnitsBehavior.UpdateSkill(DefaultSkills.OneHanded, Math.Min(1023, this.pointLeft()), this._unit_id);
            }
            else
            {
                bool flag2 = InputEx.IsKeyDown(42) || InputEx.IsKeyDown(54);
                if (flag2)
                {
                    CustomUnitsBehavior.UpdateSkill(DefaultSkills.OneHanded, Math.Min(5, this.pointLeft()), this._unit_id);
                }
                else
                {
                    CustomUnitsBehavior.UpdateSkill(DefaultSkills.OneHanded, Math.Min(1, this.pointLeft()), this._unit_id);
                }
            }
        }

        // Token: 0x060000B5 RID: 181 RVA: 0x00008A98 File Offset: 0x00006C98
        public void OneHandMinus()
        {
            bool flag = InputEx.IsKeyDown(29) || InputEx.IsKeyDown(157);
            if (flag)
            {
                CustomUnitsBehavior.UpdateSkill(DefaultSkills.OneHanded, -1023, this._unit_id);
            }
            else
            {
                bool flag2 = InputEx.IsKeyDown(42) || InputEx.IsKeyDown(54);
                if (flag2)
                {
                    CustomUnitsBehavior.UpdateSkill(DefaultSkills.OneHanded, -5, this._unit_id);
                }
                else
                {
                    CustomUnitsBehavior.UpdateSkill(DefaultSkills.OneHanded, -1, this._unit_id);
                }
            }
        }

        // Token: 0x060000B6 RID: 182 RVA: 0x00008B1C File Offset: 0x00006D1C
        public void TwoHandPlus()
        {
            bool flag = InputEx.IsKeyDown(29) || InputEx.IsKeyDown(157);
            if (flag)
            {
                CustomUnitsBehavior.UpdateSkill(DefaultSkills.TwoHanded, Math.Min(1023, this.pointLeft()), this._unit_id);
            }
            else
            {
                bool flag2 = InputEx.IsKeyDown(42) || InputEx.IsKeyDown(54);
                if (flag2)
                {
                    CustomUnitsBehavior.UpdateSkill(DefaultSkills.TwoHanded, Math.Min(5, this.pointLeft()), this._unit_id);
                }
                else
                {
                    CustomUnitsBehavior.UpdateSkill(DefaultSkills.TwoHanded, Math.Min(1, this.pointLeft()), this._unit_id);
                }
            }
        }

        // Token: 0x060000B7 RID: 183 RVA: 0x00008BC0 File Offset: 0x00006DC0
        public void TwoHandMinus()
        {
            bool flag = InputEx.IsKeyDown(29) || InputEx.IsKeyDown(157);
            if (flag)
            {
                CustomUnitsBehavior.UpdateSkill(DefaultSkills.TwoHanded, -1023, this._unit_id);
            }
            else
            {
                bool flag2 = InputEx.IsKeyDown(42) || InputEx.IsKeyDown(54);
                if (flag2)
                {
                    CustomUnitsBehavior.UpdateSkill(DefaultSkills.TwoHanded, -5, this._unit_id);
                }
                else
                {
                    CustomUnitsBehavior.UpdateSkill(DefaultSkills.TwoHanded, -1, this._unit_id);
                }
            }
        }

        // Token: 0x060000B8 RID: 184 RVA: 0x00008C44 File Offset: 0x00006E44
        public void PolearmPlus()
        {
            bool flag = InputEx.IsKeyDown(29) || InputEx.IsKeyDown(157);
            if (flag)
            {
                CustomUnitsBehavior.UpdateSkill(DefaultSkills.Polearm, Math.Min(1023, this.pointLeft()), this._unit_id);
            }
            else
            {
                bool flag2 = InputEx.IsKeyDown(42) || InputEx.IsKeyDown(54);
                if (flag2)
                {
                    CustomUnitsBehavior.UpdateSkill(DefaultSkills.Polearm, Math.Min(5, this.pointLeft()), this._unit_id);
                }
                else
                {
                    CustomUnitsBehavior.UpdateSkill(DefaultSkills.Polearm, Math.Min(1, this.pointLeft()), this._unit_id);
                }
            }
        }

        // Token: 0x060000B9 RID: 185 RVA: 0x00008CE8 File Offset: 0x00006EE8
        public void PolearmMinus()
        {
            bool flag = InputEx.IsKeyDown(29) || InputEx.IsKeyDown(157);
            if (flag)
            {
                CustomUnitsBehavior.UpdateSkill(DefaultSkills.Polearm, -1023, this._unit_id);
            }
            else
            {
                bool flag2 = InputEx.IsKeyDown(42) || InputEx.IsKeyDown(54);
                if (flag2)
                {
                    CustomUnitsBehavior.UpdateSkill(DefaultSkills.Polearm, -5, this._unit_id);
                }
                else
                {
                    CustomUnitsBehavior.UpdateSkill(DefaultSkills.Polearm, -1, this._unit_id);
                }
            }
        }

        // Token: 0x060000BA RID: 186 RVA: 0x00008D6C File Offset: 0x00006F6C
        public void BowPlus()
        {
            bool flag = InputEx.IsKeyDown(29) || InputEx.IsKeyDown(157);
            if (flag)
            {
                CustomUnitsBehavior.UpdateSkill(DefaultSkills.Bow, Math.Min(1023, this.pointLeft()), this._unit_id);
            }
            else
            {
                bool flag2 = InputEx.IsKeyDown(42) || InputEx.IsKeyDown(54);
                if (flag2)
                {
                    CustomUnitsBehavior.UpdateSkill(DefaultSkills.Bow, Math.Min(5, this.pointLeft()), this._unit_id);
                }
                else
                {
                    CustomUnitsBehavior.UpdateSkill(DefaultSkills.Bow, Math.Min(1, this.pointLeft()), this._unit_id);
                }
            }
        }

        // Token: 0x060000BB RID: 187 RVA: 0x00008E10 File Offset: 0x00007010
        public void BowMinus()
        {
            bool flag = InputEx.IsKeyDown(29) || InputEx.IsKeyDown(157);
            if (flag)
            {
                CustomUnitsBehavior.UpdateSkill(DefaultSkills.Bow, -1023, this._unit_id);
            }
            else
            {
                bool flag2 = InputEx.IsKeyDown(42) || InputEx.IsKeyDown(54);
                if (flag2)
                {
                    CustomUnitsBehavior.UpdateSkill(DefaultSkills.Bow, -5, this._unit_id);
                }
                else
                {
                    CustomUnitsBehavior.UpdateSkill(DefaultSkills.Bow, -1, this._unit_id);
                }
            }
        }

        // Token: 0x060000BC RID: 188 RVA: 0x00008E94 File Offset: 0x00007094
        public void CrossbowPlus()
        {
            bool flag = InputEx.IsKeyDown(29) || InputEx.IsKeyDown(54);
            if (flag)
            {
                CustomUnitsBehavior.UpdateSkill(DefaultSkills.Crossbow, Math.Min(1023, this.pointLeft()), this._unit_id);
            }
            else
            {
                bool flag2 = InputEx.IsKeyDown(42) || InputEx.IsKeyDown(54);
                if (flag2)
                {
                    CustomUnitsBehavior.UpdateSkill(DefaultSkills.Crossbow, Math.Min(5, this.pointLeft()), this._unit_id);
                }
                else
                {
                    CustomUnitsBehavior.UpdateSkill(DefaultSkills.Crossbow, Math.Min(1, this.pointLeft()), this._unit_id);
                }
            }
        }

        // Token: 0x060000BD RID: 189 RVA: 0x00008F38 File Offset: 0x00007138
        public void CrossbowMinus()
        {
            bool flag = InputEx.IsKeyDown(29) || InputEx.IsKeyDown(157);
            if (flag)
            {
                CustomUnitsBehavior.UpdateSkill(DefaultSkills.Crossbow, -1023, this._unit_id);
            }
            else
            {
                bool flag2 = InputEx.IsKeyDown(42) || InputEx.IsKeyDown(54);
                if (flag2)
                {
                    CustomUnitsBehavior.UpdateSkill(DefaultSkills.Crossbow, -5, this._unit_id);
                }
                else
                {
                    CustomUnitsBehavior.UpdateSkill(DefaultSkills.Crossbow, -1, this._unit_id);
                }
            }
        }

        // Token: 0x060000BE RID: 190 RVA: 0x00008FBC File Offset: 0x000071BC
        public void ThrowingPlus()
        {
            bool flag = InputEx.IsKeyDown(29) || InputEx.IsKeyDown(157);
            if (flag)
            {
                CustomUnitsBehavior.UpdateSkill(DefaultSkills.Throwing, Math.Min(1023, this.pointLeft()), this._unit_id);
            }
            else
            {
                bool flag2 = InputEx.IsKeyDown(42) || InputEx.IsKeyDown(54);
                if (flag2)
                {
                    CustomUnitsBehavior.UpdateSkill(DefaultSkills.Throwing, Math.Min(5, this.pointLeft()), this._unit_id);
                }
                else
                {
                    CustomUnitsBehavior.UpdateSkill(DefaultSkills.Throwing, Math.Min(1, this.pointLeft()), this._unit_id);
                }
            }
        }

        // Token: 0x060000BF RID: 191 RVA: 0x00009060 File Offset: 0x00007260
        public void ThrowingMinus()
        {
            bool flag = InputEx.IsKeyDown(29) || InputEx.IsKeyDown(157);
            if (flag)
            {
                CustomUnitsBehavior.UpdateSkill(DefaultSkills.Throwing, -1023, this._unit_id);
            }
            else
            {
                bool flag2 = InputEx.IsKeyDown(42) || InputEx.IsKeyDown(54);
                if (flag2)
                {
                    CustomUnitsBehavior.UpdateSkill(DefaultSkills.Throwing, -5, this._unit_id);
                }
                else
                {
                    CustomUnitsBehavior.UpdateSkill(DefaultSkills.Throwing, -1, this._unit_id);
                }
            }
        }

        // Token: 0x060000C0 RID: 192 RVA: 0x000090E4 File Offset: 0x000072E4
        public void RidingPlus()
        {
            bool flag = InputEx.IsKeyDown(29) || InputEx.IsKeyDown(157);
            if (flag)
            {
                CustomUnitsBehavior.UpdateSkill(DefaultSkills.Riding, Math.Min(1023, this.pointLeft()), this._unit_id);
            }
            else
            {
                bool flag2 = InputEx.IsKeyDown(42) || InputEx.IsKeyDown(54);
                if (flag2)
                {
                    CustomUnitsBehavior.UpdateSkill(DefaultSkills.Riding, Math.Min(5, this.pointLeft()), this._unit_id);
                }
                else
                {
                    CustomUnitsBehavior.UpdateSkill(DefaultSkills.Riding, Math.Min(1, this.pointLeft()), this._unit_id);
                }
            }
        }

        // Token: 0x060000C1 RID: 193 RVA: 0x00009188 File Offset: 0x00007388
        public void RidingMinus()
        {
            bool flag = InputEx.IsKeyDown(29) || InputEx.IsKeyDown(157);
            if (flag)
            {
                CustomUnitsBehavior.UpdateSkill(DefaultSkills.Riding, -1023, this._unit_id);
            }
            else
            {
                bool flag2 = InputEx.IsKeyDown(42) || InputEx.IsKeyDown(54);
                if (flag2)
                {
                    CustomUnitsBehavior.UpdateSkill(DefaultSkills.Riding, -5, this._unit_id);
                }
                else
                {
                    CustomUnitsBehavior.UpdateSkill(DefaultSkills.Riding, -1, this._unit_id);
                }
            }
        }

        // Token: 0x060000C2 RID: 194 RVA: 0x0000920C File Offset: 0x0000740C
        public void AthleticsPlus()
        {
            bool flag = InputEx.IsKeyDown(29) || InputEx.IsKeyDown(157);
            if (flag)
            {
                CustomUnitsBehavior.UpdateSkill(DefaultSkills.Athletics, Math.Min(1023, this.pointLeft()), this._unit_id);
            }
            else
            {
                bool flag2 = InputEx.IsKeyDown(42) || InputEx.IsKeyDown(54);
                if (flag2)
                {
                    CustomUnitsBehavior.UpdateSkill(DefaultSkills.Athletics, Math.Min(5, this.pointLeft()), this._unit_id);
                }
                else
                {
                    CustomUnitsBehavior.UpdateSkill(DefaultSkills.Athletics, Math.Min(1, this.pointLeft()), this._unit_id);
                }
            }
        }

        // Token: 0x060000C3 RID: 195 RVA: 0x000092B0 File Offset: 0x000074B0
        public void AthleticsMinus()
        {
            bool flag = InputEx.IsKeyDown(29) || InputEx.IsKeyDown(157);
            if (flag)
            {
                CustomUnitsBehavior.UpdateSkill(DefaultSkills.Athletics, -1023, this._unit_id);
            }
            else
            {
                bool flag2 = InputEx.IsKeyDown(42) || InputEx.IsKeyDown(54);
                if (flag2)
                {
                    CustomUnitsBehavior.UpdateSkill(DefaultSkills.Athletics, -5, this._unit_id);
                }
                else
                {
                    CustomUnitsBehavior.UpdateSkill(DefaultSkills.Athletics, -1, this._unit_id);
                }
            }
        }

        // Token: 0x060000C4 RID: 196 RVA: 0x00009334 File Offset: 0x00007534
        public void ChangeWep1()
        {
            CustomUnitsBehavior.UpgradeGear("Wep0", this._unit_id);
        }

        // Token: 0x060000C5 RID: 197 RVA: 0x00009348 File Offset: 0x00007548
        public void ChangeWep2()
        {
            CustomUnitsBehavior.UpgradeGear("Wep1", this._unit_id);
        }

        // Token: 0x060000C6 RID: 198 RVA: 0x0000935C File Offset: 0x0000755C
        public void ChangeWep3()
        {
            CustomUnitsBehavior.UpgradeGear("Wep2", this._unit_id);
        }

        // Token: 0x060000C7 RID: 199 RVA: 0x00009370 File Offset: 0x00007570
        public void ChangeWep4()
        {
            CustomUnitsBehavior.UpgradeGear("Wep3", this._unit_id);
        }

        // Token: 0x060000C8 RID: 200 RVA: 0x00009384 File Offset: 0x00007584
        public void ChangeHelmet()
        {
            CustomUnitsBehavior.UpgradeGear("Head", this._unit_id);
        }

        // Token: 0x060000C9 RID: 201 RVA: 0x00009398 File Offset: 0x00007598
        public void ChangeCape()
        {
            CustomUnitsBehavior.UpgradeGear("Cape", this._unit_id);
        }

        // Token: 0x060000CA RID: 202 RVA: 0x000093AC File Offset: 0x000075AC
        public void ChangeChestArmor()
        {
            CustomUnitsBehavior.UpgradeGear("Body", this._unit_id);
        }

        // Token: 0x060000CB RID: 203 RVA: 0x000093C0 File Offset: 0x000075C0
        public void ChangeGloves()
        {
            CustomUnitsBehavior.UpgradeGear("Gloves", this._unit_id);
        }

        // Token: 0x060000CC RID: 204 RVA: 0x000093D4 File Offset: 0x000075D4
        public void ChangeBoots()
        {
            CustomUnitsBehavior.UpgradeGear("Leg", this._unit_id);
        }

        // Token: 0x060000CD RID: 205 RVA: 0x000093E8 File Offset: 0x000075E8
        public void ChangeMount()
        {
            CustomUnitsBehavior.UpgradeGear("Horse", this._unit_id);
        }

        // Token: 0x060000CE RID: 206 RVA: 0x000093FC File Offset: 0x000075FC
        public void ChangeHarness()
        {
            CustomUnitsBehavior.UpgradeGear("Harness", this._unit_id);
        }

        // Token: 0x060000CF RID: 207 RVA: 0x00009410 File Offset: 0x00007610
        public void Rename()
        {
            CustomUnitsBehavior.rename(this._unit_id);
        }

        // Token: 0x060000D0 RID: 208 RVA: 0x0000941F File Offset: 0x0000761F
        public void ChangeCulture()
        {
            CustomUnitsBehavior.ChangeCulture(this._unit_id);
        }

        // Token: 0x060000D1 RID: 209 RVA: 0x0000942E File Offset: 0x0000762E
        public void ChangeGender()
        {
            CustomUnitsBehavior.ChangeGender(this._unit_id);
        }

        // Token: 0x060000D2 RID: 210 RVA: 0x00009440 File Offset: 0x00007640
        public void Upgrade1Link()
        {
            bool flag = this._upgrade_1 != null;
            if (flag)
            {
                CustomUnitsBehavior.DeleteVMLayer();
                CustomUnitsBehavior.CreateVMLayer(this._upgrade_1.StringId);
            }
        }

        // Token: 0x060000D3 RID: 211 RVA: 0x00009474 File Offset: 0x00007674
        public void Upgrade2Link()
        {
            bool flag = this._upgrade_2 != null;
            if (flag)
            {
                CustomUnitsBehavior.DeleteVMLayer();
                CustomUnitsBehavior.CreateVMLayer(this._upgrade_2.StringId);
            }
        }

        // Token: 0x060000D4 RID: 212 RVA: 0x000094A8 File Offset: 0x000076A8
        public void RootLink()
        {
            bool flag = this._root != null;
            if (flag)
            {
                CustomUnitsBehavior.DeleteVMLayer();
                CustomUnitsBehavior.CreateVMLayer(this._root.StringId);
            }
        }

        // Token: 0x060000D5 RID: 213 RVA: 0x000094DC File Offset: 0x000076DC
        public void UpgradeButtonClick1()
        {
            bool flag = this._upgrade_1 == null;
            if (flag)
            {
                this.AddUpgrade();
            }
            else
            {
                CustomUnitsBehavior.RemoveUpgrade(this._unit_id, this._upgrade_1);
                CustomUnitsBehavior.DeleteVMLayer();
                CustomUnitsBehavior.CreateVMLayer(this._unit_id);
            }
        }

        // Token: 0x060000D6 RID: 214 RVA: 0x00009528 File Offset: 0x00007728
        public void UpgradeButtonClick2()
        {
            bool flag = this._upgrade_2 == null;
            if (flag)
            {
                this.AddUpgrade();
            }
            else
            {
                CustomUnitsBehavior.RemoveUpgrade(this._unit_id, this._upgrade_2);
                CustomUnitsBehavior.DeleteVMLayer();
                CustomUnitsBehavior.CreateVMLayer(this._unit_id);
            }
        }

        // Token: 0x060000D7 RID: 215 RVA: 0x00009574 File Offset: 0x00007774
        public void AddUpgrade()
        {
            CustomUnitsBehavior.AddUpgrade(this._unit_id);
        }

        // Token: 0x060000D8 RID: 216 RVA: 0x00009584 File Offset: 0x00007784
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
            this.RefreshValues();
        }

        // Token: 0x060000D9 RID: 217 RVA: 0x000095F8 File Offset: 0x000077F8
        public void ItemTierChange()
        {
            CustomUnitsBehavior.FilterTiers();
        }

        // Token: 0x060000DA RID: 218 RVA: 0x00009601 File Offset: 0x00007801
        public void FilterCultureClear()
        {
            CustomUnitsBehavior._filter_Culture.Clear();
            this.RefreshValues();
        }

        // Token: 0x060000DB RID: 219 RVA: 0x00009616 File Offset: 0x00007816
        public void FilterCultureChange()
        {
            CustomUnitsBehavior.FilterCultures();
        }

        // Token: 0x060000DC RID: 220 RVA: 0x0000961F File Offset: 0x0000781F
        public void FilterWeaponClear()
        {
            CustomUnitsBehavior._filter_weapon_types.Clear();
            this.RefreshValues();
        }

        // Token: 0x060000DD RID: 221 RVA: 0x00009634 File Offset: 0x00007834
        public void FilterWeaponChange()
        {
            CustomUnitsBehavior.FilterWeapons();
        }

        // Token: 0x060000DE RID: 222 RVA: 0x0000963D File Offset: 0x0000783D
        public void FilterArmourClear()
        {
            CustomUnitsBehavior._filter_armour_types.Clear();
            this.RefreshValues();
        }

        // Token: 0x060000DF RID: 223 RVA: 0x00009652 File Offset: 0x00007852
        public void FilterArmourChange()
        {
            CustomUnitsBehavior.FilterArmour();
        }

        // Token: 0x060000E0 RID: 224 RVA: 0x0000965B File Offset: 0x0000785B
        public void Toggle1()
        {
            CustomUnitsBehavior._disable_gear_restriction = !CustomUnitsBehavior._disable_gear_restriction;
            CustomUnitsBehavior.DeleteVMLayer();
            CustomUnitsBehavior.CreateVMLayer(this._unit_id);
        }

        // Token: 0x060000E1 RID: 225 RVA: 0x0000967D File Offset: 0x0000787D
        public void Toggle2()
        {
            CustomUnitsBehavior._disable_skill_total_restriction = !CustomUnitsBehavior._disable_skill_total_restriction;
            CustomUnitsBehavior.DeleteVMLayer();
            CustomUnitsBehavior.CreateVMLayer(this._unit_id);
        }

        // Token: 0x060000E2 RID: 226 RVA: 0x0000969F File Offset: 0x0000789F
        public void Toggle3()
        {
            CustomUnitsBehavior._disable_skill_cap_restriction = !CustomUnitsBehavior._disable_skill_cap_restriction;
            CustomUnitsBehavior.DeleteVMLayer();
            CustomUnitsBehavior.CreateVMLayer(this._unit_id);
        }

        // Token: 0x060000E3 RID: 227 RVA: 0x000096C1 File Offset: 0x000078C1
        public void CopyTemplate()
        {
            CustomUnitsBehavior.CopyTemplate(this._unit_id);
        }

        // Token: 0x060000E4 RID: 228 RVA: 0x000096D0 File Offset: 0x000078D0
        public void ExecuteBeginHint1()
        {
            bool flag = this._upgrade_1 != null;
            if (flag)
            {
                InformationManager.ShowTooltip(typeof(CharacterObject), new object[]
                {
                    this._upgrade_1
                });
            }
            MBInformationManager.ShowHint("No upgade unit added");
        }

        // Token: 0x060000E5 RID: 229 RVA: 0x00009718 File Offset: 0x00007918
        public void ExecuteBeginHint2()
        {
            bool flag = this._upgrade_2 != null;
            if (flag)
            {
                InformationManager.ShowTooltip(typeof(CharacterObject), new object[]
                {
                    this._upgrade_2
                });
            }
            MBInformationManager.ShowHint("No upgade unit added");
        }

        // Token: 0x060000E6 RID: 230 RVA: 0x00009760 File Offset: 0x00007960
        public void ExecuteBeginHint3()
        {
            bool flag = this._root != null;
            if (flag)
            {
                InformationManager.ShowTooltip(typeof(CharacterObject), new object[]
                {
                    this._root
                });
            }
        }

        private static class InputEx
        {
            public static bool IsKeyDown(int key)
            {
                return Input.IsKeyDown((TaleWorlds.InputSystem.InputKey)key);
            }

        }


        // Token: 0x060000E7 RID: 231 RVA: 0x0000979C File Offset: 0x0000799C
        public void ExecuteEndHint()
        {
            MBInformationManager.HideInformations();
        }

        // Token: 0x04000018 RID: 24
        private CharacterViewModel _unitCharacter;

        // Token: 0x04000019 RID: 25
        private CharacterObject _character;

        // Token: 0x0400001A RID: 26
        private string _unit_id;

        // Token: 0x0400001B RID: 27
        private string _pointsLeft;

        // Token: 0x0400001C RID: 28
        private string _skiil_level_cap;

        // Token: 0x0400001D RID: 29
        private string _item_tier_filter;

        // Token: 0x0400001E RID: 30
        private string _culture_filter;

        // Token: 0x0400001F RID: 31
        private string _weapon_filter;

        // Token: 0x04000020 RID: 32
        private string _armour_filter;

        // Token: 0x04000021 RID: 33
        private string _disable_1_brush;

        // Token: 0x04000022 RID: 34
        private string _disable_2_brush;

        // Token: 0x04000023 RID: 35
        private string _disable_3_brush;

        // Token: 0x04000024 RID: 36
        private string _disable_1_text;

        // Token: 0x04000025 RID: 37
        private string _disable_2_text;

        // Token: 0x04000026 RID: 38
        private string _disable_3_text;

        // Token: 0x04000027 RID: 39
        private string _upgrade_button_1;

        // Token: 0x04000028 RID: 40
        private string _upgrade_button_2;

        // Token: 0x04000029 RID: 41
        private string _equipment_set_total_cost;

        // Token: 0x0400002A RID: 42
        private bool _isEnabled;

        private MBBindingList<BindingListStringItem> _navalStatus;

        // Token: 0x0400002B RID: 43
        private MBBindingList<EncyclopediaSkillVM> _skills;

        // Token: 0x0400002C RID: 44
        private MBBindingList<BindingListStringItem> _name;

        // Token: 0x0400002D RID: 45
        private MBBindingList<StringItemWithHintVM> _propertiesList;

        // Token: 0x0400002E RID: 46
        private MBBindingList<CharacterEquipmentItemVM> _weapons1;

        // Token: 0x0400002F RID: 47
        private MBBindingList<CharacterEquipmentItemVM> _weapons2;

        // Token: 0x04000030 RID: 48
        private MBBindingList<CharacterEquipmentItemVM> _weapons3;

        // Token: 0x04000031 RID: 49
        private MBBindingList<CharacterEquipmentItemVM> _weapons4;

        // Token: 0x04000032 RID: 50
        private MBBindingList<BindingListStringItem> _weapon1_name;

        // Token: 0x04000033 RID: 51
        private MBBindingList<BindingListStringItem> _weapon2_name;

        // Token: 0x04000034 RID: 52
        private MBBindingList<BindingListStringItem> _weapon3_name;

        // Token: 0x04000035 RID: 53
        private MBBindingList<BindingListStringItem> _weapon4_name;

        // Token: 0x04000036 RID: 54
        private MBBindingList<CharacterEquipmentItemVM> _helmet;

        // Token: 0x04000037 RID: 55
        private MBBindingList<CharacterEquipmentItemVM> _cape;

        // Token: 0x04000038 RID: 56
        private MBBindingList<CharacterEquipmentItemVM> _chest;

        // Token: 0x04000039 RID: 57
        private MBBindingList<CharacterEquipmentItemVM> _gloves;

        // Token: 0x0400003A RID: 58
        private MBBindingList<CharacterEquipmentItemVM> _boots;

        // Token: 0x0400003B RID: 59
        private MBBindingList<BindingListStringItem> _helmet_name;

        // Token: 0x0400003C RID: 60
        private MBBindingList<BindingListStringItem> _cape_name;

        // Token: 0x0400003D RID: 61
        private MBBindingList<BindingListStringItem> _chest_name;

        // Token: 0x0400003E RID: 62
        private MBBindingList<BindingListStringItem> _gloves_name;

        // Token: 0x0400003F RID: 63
        private MBBindingList<BindingListStringItem> _boots_name;

        // Token: 0x04000040 RID: 64
        private MBBindingList<CharacterEquipmentItemVM> _mount;

        // Token: 0x04000041 RID: 65
        private MBBindingList<CharacterEquipmentItemVM> _harness;

        // Token: 0x04000042 RID: 66
        private MBBindingList<BindingListStringItem> _mount_name;

        // Token: 0x04000043 RID: 67
        private MBBindingList<BindingListStringItem> _harness_name;

        // Token: 0x04000044 RID: 68
        private MBBindingList<BindingListStringItem> _gender_string;

        // Token: 0x04000045 RID: 69
        private MBBindingList<BindingListStringItem> _culture_string;

        // Token: 0x04000046 RID: 70
        private SelectorVM<EncyclopediaUnitEquipmentSetSelectorItemVM> _equipmentSetSelector;

        // Token: 0x04000047 RID: 71
        private EncyclopediaUnitEquipmentSetSelectorItemVM _currentSelectedEquipmentSet;

        // Token: 0x04000048 RID: 72
        private TextObject _equipmentSetTextObj;

        // Token: 0x04000049 RID: 73
        private string _equipmentSetText;

        // Token: 0x0400004A RID: 74
        private CharacterImageIdentifierVM _imageIdentifier_1;

        // Token: 0x0400004B RID: 75
        private CharacterImageIdentifierVM _imageIdentifier_2;

        // Token: 0x0400004C RID: 76
        private CharacterImageIdentifierVM _imageIdentifier_3;

        // Token: 0x0400004D RID: 77
        private CharacterObject _upgrade_1;

        // Token: 0x0400004E RID: 78
        private CharacterObject _upgrade_2;

        // Token: 0x0400004F RID: 79
        private CharacterObject _root;
    }
}
