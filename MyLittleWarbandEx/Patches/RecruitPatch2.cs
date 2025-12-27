using HarmonyLib;
using MyLittleWarbandEx;
using MyLittleWarbandEx.CampaignBehaviors;
using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;

namespace MyLittleWarbandEx.Patches
{
    // Token: 0x02000014 RID: 20
    [HarmonyPatch(typeof(PlayerTownVisitCampaignBehavior), "game_menu_recruit_volunteers_on_consequence")]
    internal class RecruitPatch2
    {
        // Token: 0x06000141 RID: 321 RVA: 0x0000C828 File Offset: 0x0000AA28
        private static bool Prefix(MenuCallbackArgs args)
        {
            bool flag = !SubModule.ReplaceAllForPlayer;
            bool result;
            if (flag)
            {
                result = true;
            }
            else
            {
                TroopsBehavior.recruits = new CharacterObject[6, Settlement.CurrentSettlement.Notables.Count];
                int num = 0;
                foreach (Hero hero in Settlement.CurrentSettlement.Notables)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        TroopsBehavior.recruits[i, num] = hero.VolunteerTypes[i];
                        bool flag2 = hero.VolunteerTypes[i] != null && !EnyclopediaEditUnitPatch.isCustomTroop(hero.VolunteerTypes[i]);
                        if (flag2)
                        {
                            bool flag3 = RecruitPatch2.isEliteLine(hero.VolunteerTypes[i]);
                            if (flag3)
                            {
                                hero.VolunteerTypes[i] = RecruitPatch2.tryToLevel(Game.Current.ObjectManager.GetObject<CharacterObject>("_elite_root"), hero.VolunteerTypes[i].Tier);
                            }
                            else
                            {
                                hero.VolunteerTypes[i] = RecruitPatch2.tryToLevel(Game.Current.ObjectManager.GetObject<CharacterObject>("_basic_root"), hero.VolunteerTypes[i].Tier);
                            }
                        }
                    }
                    num++;
                }
                result = false;
            }
            return result;
        }

        // Token: 0x06000142 RID: 322 RVA: 0x0000C9AC File Offset: 0x0000ABAC
        public static CharacterObject tryToLevel(CharacterObject root, int tier)
        {
            CharacterObject characterObject = root;
            while (characterObject.Tier < tier && characterObject.UpgradeTargets != null && characterObject.UpgradeTargets.Length != 0)
            {
                characterObject = Extensions.GetRandomElement<CharacterObject>(characterObject.UpgradeTargets);
            }
            return characterObject;
        }

        // Token: 0x06000143 RID: 323 RVA: 0x0000C9F4 File Offset: 0x0000ABF4
        public static bool isEliteLine(CharacterObject unit)
        {
            List<CharacterObject> list = new List<CharacterObject>();
            Stack<CharacterObject> stack = new Stack<CharacterObject>();
            stack.Push(unit.Culture.EliteBasicTroop);
            list.Add(unit.Culture.EliteBasicTroop);
            while (!Extensions.IsEmpty<CharacterObject>(stack))
            {
                CharacterObject characterObject = stack.Pop();
                bool flag = characterObject.UpgradeTargets != null && characterObject.UpgradeTargets.Length != 0;
                if (flag)
                {
                    for (int i = 0; i < characterObject.UpgradeTargets.Length; i++)
                    {
                        bool flag2 = !list.Contains(characterObject.UpgradeTargets[i]);
                        if (flag2)
                        {
                            list.Add(characterObject.UpgradeTargets[i]);
                            stack.Push(characterObject.UpgradeTargets[i]);
                        }
                    }
                }
            }
            return list.Contains(unit);
        }
    }
}
