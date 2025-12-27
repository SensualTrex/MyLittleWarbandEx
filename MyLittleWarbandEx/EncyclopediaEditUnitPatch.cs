using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using MyLittleWarbandEx;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;

namespace MyLittleWarbandEx;

[HarmonyPatch(typeof(EncyclopediaUnitVM), "ExecuteLink")]
internal class EnyclopediaEditUnitPatch
{
    private static bool Prefix(EncyclopediaUnitVM __instance)
    {
        BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
        FieldInfo field = __instance.GetType().GetField("_character", bindingAttr);
        CharacterObject characterObject = (CharacterObject)field.GetValue(__instance);
        if ((InputKey.LeftShift.IsDown() || InputKey.RightShift.IsDown()) && (isCustomTroop(characterObject) || SubModule.FullUnitEditor))
        {
            CustomUnitsBehavior.CreateVMLayer(characterObject.StringId);
            return false;
        }

        return true;
    }

    public static bool isCustomTroop(CharacterObject _character)
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

        return list.Contains(_character);
    }
}