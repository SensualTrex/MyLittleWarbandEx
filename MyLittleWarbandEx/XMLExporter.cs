using MyLittleWarbandEx;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Library;

namespace MyLittleWarbandEx;

internal class XMLExporter : CampaignBehaviorBase
{
    private static string SaveFileName = "";

    public override void RegisterEvents()
    {
        CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, MenuItems);
    }

    private void MenuItems(CampaignGameStarter campaignGameStarter)
    {
        campaignGameStarter.AddGameMenuOption("town_backstreet", "export_basic_tree", "Export Custom Troop Tree To XML File", delegate (MenuCallbackArgs args)
        {
            args.optionLeaveType = GameMenuOption.LeaveType.Escape;
            return true;
        }, delegate
        {
            Name();
        }, isLeave: false, 1);
        campaignGameStarter.AddGameMenuOption("town_backstreet", "import_basic_tree", "Import Custom Troop Tree", delegate (MenuCallbackArgs args)
        {
            args.optionLeaveType = GameMenuOption.LeaveType.Escape;
            return true;
        }, delegate
        {
            Import();
        }, isLeave: false, 1);
    }

    private void Import()
    {
        int num = 0;
        foreach (CharacterObject character in Campaign.Current.Characters)
        {
            if (!character.StringId.StartsWith("_basic_root") && !character.StringId.StartsWith("_elite_root"))
            {
                continue;
            }

            CharacterObject @object = Game.Current.ObjectManager.GetObject<CharacterObject>("copy" + character.StringId);
            if (@object == null)
            {
                continue;
            }

            num++;
            typeof(BasicCharacterObject).GetMethod("SetName", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(character, new object[1] { @object.Name });
            if (character.StringId != "_basic_root" && character.StringId != "_elite_root")
            {
                CharacterObject object2 = Game.Current.ObjectManager.GetObject<CharacterObject>(character.StringId.Substring(0, character.StringId.Length - 1));
                List<CharacterObject> list = new List<CharacterObject>();
                list.Add(character);
                if (object2.UpgradeTargets != null && object2.UpgradeTargets.Length != 0)
                {
                    list.Add(object2.UpgradeTargets[0]);
                }

                typeof(CharacterObject).GetProperty("UpgradeTargets").SetValue(object2, list.ToArray(), null);
                CustomUnitsBehavior.update(object2.StringId, refresh: false);
            }

            CustomUnitsBehavior.CopyCharacter(@object, character);
            CustomUnitsBehavior.update(character.StringId, refresh: false);
        }

        if (num == 0)
        {
            InformationManager.DisplayMessage(new InformationMessage("Failed to load any units.  Check if exported troop tree module is turned on"));
        }
        else
        {
            InformationManager.DisplayMessage(new InformationMessage("Load " + num + " units"));
        }
    }

    private void Name()
    {
        InformationManager.ShowTextInquiry(new TextInquiryData("Save As", "Enter name", isAffirmativeOptionShown: true, isNegativeOptionShown: true, "save", "Cancel", delegate (string s)
        {
            SaveFileName = s;
            string path = Path.Combine(BasePath.Name, string.Concat("Modules/", string.Concat(SaveFileName.Where((char c) => !char.IsWhiteSpace(c)))));
            string path2 = Path.Combine(BasePath.Name, string.Concat("Modules/", string.Concat(SaveFileName.Where((char c) => !char.IsWhiteSpace(c))), "/ModuleData"));
            Directory.CreateDirectory(path);
            Directory.CreateDirectory(path2);
            exportTree();
            createSubModuleXML();
        }, null));
    }

    private void createSubModuleXML()
    {
        string path = Path.Combine(BasePath.Name, string.Concat("Modules/", string.Concat(SaveFileName.Where((char c) => !char.IsWhiteSpace(c))), "/SubModule.xml"));
        string text = "";
        text += "<Module>\n";
        text = text + "\t<Name value=\"" + SaveFileName + "\"/>\n";
        text = string.Concat(text, "\t<Id value=\"", string.Concat(SaveFileName.Where((char c) => !char.IsWhiteSpace(c))), "\"/>\n");
        text += "\t<Version value=\"v1.6.0\"/>\n";
        text += "\t<SingleplayerModule value=\"true\"/>\n";
        text += "\t<MultiplayerModule value=\"false\"/>\n";
        text += "\t<DependedModules>\n";
        text += "\t\t<DependedModule Id=\"Native\"/>\n";
        text += "\t\t<DependedModule Id=\"SandBoxCore\"/>\n";
        text += "\t\t<DependedModule Id=\"Sandbox\"/>\n";
        text += "\t\t<DependedModule Id=\"CustomBattle\"/>\n";
        text += "\t\t<DependedModule Id =\"StoryMode\"/>\n";
        text += "\t</DependedModules>\n";
        text += "\t<SubModules>\n";
        text += "\t</SubModules>\n";
        text += "\t<Xmls>\n";
        text += "\t\t<XmlNode>\n";
        text += "\t\t\t<XmlName id=\"NPCCharacters\" path=\"troops\"/>\n";
        text += "\t\t\t<IncludedGameTypes>\n";
        text += "\t\t\t\t<GameType value=\"Campaign\"/>\n";
        text += "\t\t\t\t<GameType value=\"CampaignStoryMode\"/>\n";
        text += "\t\t\t\t<GameType value=\"CustomGame\"/>\n";
        text += "\t\t\t\t<GameType value =\"EditorGame\"/>\n";
        text += "\t\t\t</IncludedGameTypes>\n";
        text += "\t\t</XmlNode>\n";
        text += "\t</Xmls>\n";
        text += "</Module>\n";
        File.WriteAllText(path, text);
    }

    private void exportTree()
    {
        string text = Path.Combine(BasePath.Name, string.Concat("Modules/", string.Concat(SaveFileName.Where((char c) => !char.IsWhiteSpace(c))), "/ModuleData/troops.xml"));
        InformationManager.DisplayMessage(new InformationMessage("Troop tree exported to " + text));
        string text2 = "";
        text2 += "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\n";
        text2 += "<NPCCharacters>\n";
        foreach (CharacterObject allCharacter in getAllCharacters())
        {
            exportCharacter(allCharacter, ref text2);
        }

        text2 += "</NPCCharacters>\n";
        File.WriteAllText(text, text2);
    }

    public static List<CharacterObject> getAllCharacters()
    {
        List<CharacterObject> list = new List<CharacterObject>();
        Stack<CharacterObject> stack = new Stack<CharacterObject>();
        stack.Push(Game.Current.ObjectManager.GetObject<CharacterObject>("_basic_root"));
        list.Add(Game.Current.ObjectManager.GetObject<CharacterObject>("_basic_root"));
        stack.Push(Game.Current.ObjectManager.GetObject<CharacterObject>("_elite_root"));
        list.Add(Game.Current.ObjectManager.GetObject<CharacterObject>("_elite_root"));
        while (!stack.IsEmpty())
        {
            CharacterObject characterObject = stack.Pop();
            if (characterObject.UpgradeTargets == null || characterObject.UpgradeTargets.Length == 0)
            {
                continue;
            }

            for (int i = 0; i < characterObject.UpgradeTargets.Length; i++)
            {
                if (!list.Contains(characterObject.UpgradeTargets[i]))
                {
                    list.Add(characterObject.UpgradeTargets[i]);
                    stack.Push(characterObject.UpgradeTargets[i]);
                }
            }
        }

        return list;
    }

    private void exportCharacter(CharacterObject character, ref string s)
    {
        s = s + "\t<NPCCharacter id=\"copy" + character.StringId + "\"\n";
        s = s + "\t\tdefault_group=\"" + character.DefaultFormationClass.ToString() + "\"\n";
        s = s + "\t\tlevel=\"" + character.Level + "\"\n";
        s = s + "\t\tname=\"{=" + character.Name.ToString() + "}" + character.Name.ToString() + "\"\n";
        if (character.UpgradeRequiresItemFromCategory != null)
        {
            s = s + "\t\tupgrade_requires=\"ItemCategory." + character.UpgradeRequiresItemFromCategory.StringId + "\"\n";
        }

        s = s + "\t\toccupation=\"" + character.Occupation.ToString() + "\"\n";
        s = s + "\t\tculture=\"Culture." + character.Culture.StringId + "\">\n";
        s += "\t\t<face>\n";
        s = s + "\t\t\t<face_key_template value=\"BodyProperty.villager_" + character.Culture.StringId + "\"/>\n";
        s += "\t\t</face>\n";
        s += "\t\t<skills >\n";
        s = s + "\t\t\t<skill id=\"Athletics\" value=\"" + character.GetSkillValue(DefaultSkills.Athletics) + "\"/>\n";
        s = s + "\t\t\t<skill id=\"Riding\" value=\"" + character.GetSkillValue(DefaultSkills.Riding) + "\"/>\n";
        s = s + "\t\t\t<skill id=\"OneHanded\" value=\"" + character.GetSkillValue(DefaultSkills.OneHanded) + "\"/>\n";
        s = s + "\t\t\t<skill id=\"TwoHanded\" value=\"" + character.GetSkillValue(DefaultSkills.TwoHanded) + "\"/>\n";
        s = s + "\t\t\t<skill id=\"Polearm\" value=\"" + character.GetSkillValue(DefaultSkills.Polearm) + "\"/>\n";
        s = s + "\t\t\t<skill id=\"Bow\" value=\"" + character.GetSkillValue(DefaultSkills.Bow) + "\"/>\n";
        s = s + "\t\t\t<skill id=\"Crossbow\" value=\"" + character.GetSkillValue(DefaultSkills.Crossbow) + "\"/>\n";
        s = s + "\t\t\t<skill id=\"Throwing\" value=\"" + character.GetSkillValue(DefaultSkills.Throwing) + "\"/>\n";
        s += "\t\t</skills>\n";
        if (character.UpgradeTargets != null && character.UpgradeTargets.Length != 0)
        {
            s += "\t\t<upgrade_targets>\n";
            s = s + "\t\t\t<upgrade_target id=\"NPCCharacter.copy" + character.UpgradeTargets[0].StringId + "\"/>\n";
            if (character.UpgradeTargets.Length > 1)
            {
                s = s + "\t\t\t<upgrade_target id=\"NPCCharacter.copy" + character.UpgradeTargets[1].StringId + "\"/>\n";
            }

            s += "\t\t</upgrade_targets>\n";
        }

        s += "\t\t<Equipments>\n";
        List<Equipment> list = character.BattleEquipments.Where((Equipment x) => !x.IsCivilian).ToList();
        List<Equipment> list2 = character.BattleEquipments.Where((Equipment x) => !x.IsCivilian).ToList();
        exportEquipmentRoaster(list[0], ref s, isCivilian: false);
        exportEquipmentRoaster(list[1], ref s, isCivilian: false);
        exportEquipmentRoaster(list[2], ref s, isCivilian: false);
        exportEquipmentRoaster(list[3], ref s, isCivilian: false);
        exportEquipmentRoaster(list2[0], ref s, isCivilian: true);
        s += "\t\t</Equipments>\n";
        s += "\t</NPCCharacter>\n";
    }

    private void exportEquipmentRoaster(Equipment equipment, ref string s, bool isCivilian)
    {
        if (isCivilian)
        {
            s += "\t\t\t<EquipmentRoster civilian=\"true\">\n";
        }
        else
        {
            s += "\t\t\t<EquipmentRoster>\n";
        }

        if (equipment[EquipmentIndex.WeaponItemBeginSlot].Item != null)
        {
            s = s + "\t\t\t\t<equipment slot=\"Item0\" id=\"Item." + equipment[EquipmentIndex.WeaponItemBeginSlot].Item.StringId + "\"/>\n";
        }

        if (equipment[EquipmentIndex.Weapon1].Item != null)
        {
            s = s + "\t\t\t\t<equipment slot=\"Item1\" id=\"Item." + equipment[EquipmentIndex.Weapon1].Item.StringId + "\"/>\n";
        }

        if (equipment[EquipmentIndex.Weapon2].Item != null)
        {
            s = s + "\t\t\t\t<equipment slot=\"Item2\" id=\"Item." + equipment[EquipmentIndex.Weapon2].Item.StringId + "\"/>\n";
        }

        if (equipment[EquipmentIndex.Weapon3].Item != null)
        {
            s = s + "\t\t\t\t<equipment slot=\"Item3\" id=\"Item." + equipment[EquipmentIndex.Weapon3].Item.StringId + "\"/>\n";
        }

        if (equipment[EquipmentIndex.NumAllWeaponSlots].Item != null)
        {
            s = s + "\t\t\t\t<equipment slot=\"Head\" id=\"Item." + equipment[EquipmentIndex.NumAllWeaponSlots].Item.StringId + "\"/>\n";
        }

        if (equipment[EquipmentIndex.Cape].Item != null)
        {
            s = s + "\t\t\t\t<equipment slot=\"Cape\" id=\"Item." + equipment[EquipmentIndex.Cape].Item.StringId + "\"/>\n";
        }

        if (equipment[EquipmentIndex.Body].Item != null)
        {
            s = s + "\t\t\t\t<equipment slot=\"Body\" id=\"Item." + equipment[EquipmentIndex.Body].Item.StringId + "\"/>\n";
        }

        if (equipment[EquipmentIndex.Gloves].Item != null)
        {
            s = s + "\t\t\t\t<equipment slot=\"Gloves\" id=\"Item." + equipment[EquipmentIndex.Gloves].Item.StringId + "\"/>\n";
        }

        if (equipment[EquipmentIndex.Leg].Item != null)
        {
            s = s + "\t\t\t\t<equipment slot=\"Leg\" id=\"Item." + equipment[EquipmentIndex.Leg].Item.StringId + "\"/>\n";
        }

        if (equipment[EquipmentIndex.ArmorItemEndSlot].Item != null)
        {
            s = s + "\t\t\t\t<equipment slot=\"Horse\" id=\"Item." + equipment[EquipmentIndex.ArmorItemEndSlot].Item.StringId + "\"/>\n";
        }

        if (equipment[EquipmentIndex.HorseHarness].Item != null)
        {
            s = s + "\t\t\t\t<equipment slot=\"HorseHarness\" id=\"Item." + equipment[EquipmentIndex.HorseHarness].Item.StringId + "\"/>\n";
        }

        s += "\t\t\t</EquipmentRoster>\n";
    }

    public override void SyncData(IDataStore dataStore)
    {
    }
}