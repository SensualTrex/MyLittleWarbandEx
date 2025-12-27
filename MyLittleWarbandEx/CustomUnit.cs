
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using MyLittleWarbandEx;
using NetworkMessages.FromServer;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;
using TaleWorlds.SaveSystem;

namespace MyLittleWarbandEx;

internal class CustomUnit
{
    private string _id;

    private Equipment[] _equimpent;

    private int _level;

    private int _athletics;

    private int _riding;

    private int _one_hand;

    private int _two_hand;

    private int _polearm;

    private int _bow;

    private int _throwing;

    private int _crossbow;

    private string _name;

    private CultureObject _culture;

    private bool _is_female;

    private CharacterObject _upgrade_target_1;

    private CharacterObject _upgrade_target_2;

    [SaveableProperty(1)]
    public string Id
    {
        get
        {
            return _id;
        }
        set
        {
            _id = value;
        }
    }

    [SaveableProperty(2)]
    public int Level
    {
        get
        {
            return _level;
        }
        set
        {
            _level = value;
        }
    }

    [SaveableProperty(3)]
    public Equipment[] Equipment
    {
        get
        {
            return _equimpent;
        }
        set
        {
            _equimpent = value;
        }
    }

    [SaveableProperty(4)]
    public int Athletics
    {
        get
        {
            return _athletics;
        }
        set
        {
            _athletics = value;
        }
    }

    [SaveableProperty(5)]
    public int Riding
    {
        get
        {
            return _riding;
        }
        set
        {
            _riding = value;
        }
    }

    [SaveableProperty(6)]
    public int OneHand
    {
        get
        {
            return _one_hand;
        }
        set
        {
            _one_hand = value;
        }
    }

    [SaveableProperty(7)]
    public int TwoHand
    {
        get
        {
            return _two_hand;
        }
        set
        {
            _two_hand = value;
        }
    }

    [SaveableProperty(8)]
    public int Polearm
    {
        get
        {
            return _polearm;
        }
        set
        {
            _polearm = value;
        }
    }

    [SaveableProperty(9)]
    public int Bow
    {
        get
        {
            return _bow;
        }
        set
        {
            _bow = value;
        }
    }

    [SaveableProperty(10)]
    public int Throwing
    {
        get
        {
            return _throwing;
        }
        set
        {
            _throwing = value;
        }
    }

    [SaveableProperty(11)]
    public int Crossbow
    {
        get
        {
            return _crossbow;
        }
        set
        {
            _crossbow = value;
        }
    }

    [SaveableProperty(12)]
    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }

    [SaveableProperty(16)]
    public CultureObject Culture
    {
        get
        {
            return _culture;
        }
        set
        {
            _culture = value;
        }
    }

    [SaveableProperty(17)]
    public bool IsFemale
    {
        get
        {
            return _is_female;
        }
        set
        {
            _is_female = value;
        }
    }

    [SaveableProperty(18)]
    public CharacterObject UpgradeTarget1
    {
        get
        {
            return _upgrade_target_1;
        }
        set
        {
            _upgrade_target_1 = value;
        }
    }

    [SaveableProperty(19)]
    public CharacterObject UpgradeTarget2
    {
        get
        {
            return _upgrade_target_2;
        }
        set
        {
            _upgrade_target_2 = value;
        }
    }

    public CustomUnit(string id)
    {
        _id = id;
        SaveUpdate();
    }

    public void SaveUpdate()
    {
        CharacterObject @object = Game.Current.ObjectManager.GetObject<CharacterObject>(_id);
        
        List<Equipment> list = @object.BattleEquipments.ToList();
        _equimpent = new Equipment[list.Count];
        int num = 0;
        foreach (Equipment item in list)
        {
            _equimpent[num] = item;
            num++;
        }

        _name = @object.Name.ToString();
        _level = @object.Level;
        _athletics = @object.GetSkillValue(DefaultSkills.Athletics);
        _riding = @object.GetSkillValue(DefaultSkills.Riding);
        _one_hand = @object.GetSkillValue(DefaultSkills.OneHanded);
        _two_hand = @object.GetSkillValue(DefaultSkills.TwoHanded);
        _polearm = @object.GetSkillValue(DefaultSkills.Polearm);
        _bow = @object.GetSkillValue(DefaultSkills.Bow);
        _throwing = @object.GetSkillValue(DefaultSkills.Throwing);
        _crossbow = @object.GetSkillValue(DefaultSkills.Crossbow);
        _culture = @object.Culture;
        _is_female = @object.IsFemale;
        if (@object.UpgradeTargets != null)
        {
            if (@object.UpgradeTargets.Length != 0)
            {
                _upgrade_target_1 = @object.UpgradeTargets[0];
            }
            else
            {
                _upgrade_target_1 = null;
            }

            if (@object.UpgradeTargets.Length > 1)
            {
                _upgrade_target_2 = @object.UpgradeTargets[1];
            }
            else
            {
                _upgrade_target_2 = null;
            }
        }

        checkHorseNeeded();
    }

    public void Update()
    {
        
        CharacterObject @object = Game.Current.ObjectManager.GetObject<CharacterObject>(_id);

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
        EquipmentIndex[] array2 = array;
        foreach (EquipmentIndex equipmentIndex in array2)
        {
            for (int j = 0; j < _equimpent.Length; j++)
            {
                if (_equimpent[j] != null)
                {
                    if (_equimpent[j].GetEquipmentFromSlot(equipmentIndex).Item != null && _equimpent[j].GetEquipmentFromSlot(equipmentIndex).Item.Name != null)
                    {
                        ChangeUnitEquipment((int)equipmentIndex, @object, _equimpent[j].GetEquipmentFromSlot(equipmentIndex).Item, j);
                    }
                    else
                    {
                        ChangeUnitEquipment((int)equipmentIndex, @object, null, j);
                    }
                }
            }
        }

        MBCharacterSkills mBCharacterSkills = MBObjectManager.Instance.CreateObject<MBCharacterSkills>(@object.StringId);
        mBCharacterSkills.Skills.SetPropertyValue(DefaultSkills.Crossbow, Crossbow);
        mBCharacterSkills.Skills.SetPropertyValue(DefaultSkills.Bow, Bow);
        mBCharacterSkills.Skills.SetPropertyValue(DefaultSkills.Throwing, Throwing);
        mBCharacterSkills.Skills.SetPropertyValue(DefaultSkills.OneHanded, OneHand);
        mBCharacterSkills.Skills.SetPropertyValue(DefaultSkills.TwoHanded, TwoHand);
        mBCharacterSkills.Skills.SetPropertyValue(DefaultSkills.Polearm, Polearm);
        mBCharacterSkills.Skills.SetPropertyValue(DefaultSkills.Athletics, Athletics);
        mBCharacterSkills.Skills.SetPropertyValue(DefaultSkills.Riding, Riding);
        FieldInfo field = @object.GetType().GetField("DefaultCharacterSkills", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        if (null != field)
        {
            field.SetValue(@object, mBCharacterSkills);
        }

        typeof(BasicCharacterObject).GetMethod("SetName", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(@object, new object[1]
        {
            new TextObject(_name)
        });
        @object.Level = Level;
        @object.IsFemale = IsFemale;
        if (Culture != null)
        {
            Traverse.Create(@object).Property("Culture").SetValue(Culture);
        }
        else
        {
            Culture = @object.Culture;
        }

        if ((IsFemale ? Culture.Townswoman : Culture.Townsman) != null)
        {
            AccessTools.Property(typeof(CharacterObject), "BodyPropertyRange").SetValue(@object, IsFemale ? Culture.Townswoman.BodyPropertyRange : Culture.Townsman.BodyPropertyRange, null);
        }
        else
        {
            AccessTools.Property(typeof(CharacterObject), "BodyPropertyRange").SetValue(@object, IsFemale ? Game.Current.ObjectManager.GetObject<CharacterObject>("townswoman_empire").BodyPropertyRange : Game.Current.ObjectManager.GetObject<CharacterObject>("townsman_empire").BodyPropertyRange, null);
        }

        int num = 0;
        if (_upgrade_target_1 != null)
        {
            num++;
        }

        if (_upgrade_target_2 != null)
        {
            num++;
        }

        if (num > 0)
        {
            int num2 = 0;
            CharacterObject[] array3 = new CharacterObject[num];
            if (_upgrade_target_1 != null)
            {
                array3[num2] = _upgrade_target_1;
                num2++;
            }

            if (_upgrade_target_2 != null)
            {
                array3[num2] = _upgrade_target_2;
            }

            typeof(CharacterObject).GetProperty("UpgradeTargets").SetValue(@object, array3, null);
        }

        CustomUnitsBehavior.SetDefaultGroup(_id);
        checkHorseNeeded();
    }

    public void checkHorseNeeded()
    {
        CharacterObject @object = Game.Current.ObjectManager.GetObject<CharacterObject>(_id);
        if (EnyclopediaEditUnitPatch.isCustomTroop(@object))
        {
            CharacterObject object2 = Game.Current.ObjectManager.GetObject<CharacterObject>(@object.StringId.Substring(0, @object.StringId.Length - 1));
            if (object2 != null && @object.Equipment[EquipmentIndex.ArmorItemEndSlot].Item != null && @object.Equipment[EquipmentIndex.ArmorItemEndSlot].Item.ItemCategory == DefaultItemCategories.WarHorse && (object2.Equipment[EquipmentIndex.ArmorItemEndSlot].Item == null || object2.Equipment[EquipmentIndex.ArmorItemEndSlot].Item.ItemCategory != DefaultItemCategories.WarHorse))
            {
                typeof(CharacterObject).GetProperty("UpgradeRequiresItemFromCategory").SetValue(@object, DefaultItemCategories.WarHorse, null);
            }
            else if (object2 != null && @object.Equipment[EquipmentIndex.ArmorItemEndSlot].Item != null && @object.Equipment[EquipmentIndex.ArmorItemEndSlot].Item.ItemCategory == DefaultItemCategories.Horse && object2.Equipment[EquipmentIndex.ArmorItemEndSlot].Item == null)
            {
                typeof(CharacterObject).GetProperty("UpgradeRequiresItemFromCategory").SetValue(@object, DefaultItemCategories.Horse, null);
            }
            else
            {
                typeof(CharacterObject).GetProperty("UpgradeRequiresItemFromCategory").SetValue(@object, null, null);
            }
        }
    }

    private void ChangeUnitEquipment(int slot, CharacterObject character, ItemObject item, int set)
    {
        List<Equipment> list = null;
        if (character != null && character.CivilianEquipments != null)
        {
            list = character.CivilianEquipments.ToList();
        }

        List<Equipment> list2 = null;
        if (character != null && character.BattleEquipments != null)
        {
            list2 = character.BattleEquipments.ToList();
        }

        if (list2 == null || set >= list2.Count)
        {
            return;
        }

        EquipmentElement value = ((item == null) ? default(EquipmentElement) : new EquipmentElement(item));
        if (list2 != null)
        {
            list2[set][slot] = value;
            if (list != null)
            {
                list2.AddRange(list);
            }
        }

        UpdateSelectedUnitEquipment(character, list2);
    }

    private void UpdateSelectedUnitEquipment(CharacterObject character, List<Equipment> equipments)
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
}