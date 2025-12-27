using HarmonyLib;
using MyLittleWarbandEx;
using MyLittleWarbandEx.Patches;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;

namespace MyLittleWarbandEx.Patches
{
    // Token: 0x0200000C RID: 12
    [HarmonyPatch(typeof(RecruitmentCampaignBehavior), "UpdateVolunteersOfNotablesInSettlement")]
    internal class RecruitProductionPatch
    {
        // Token: 0x06000045 RID: 69 RVA: 0x000044CB File Offset: 0x000026CB
        private static void Postfix(Settlement settlement)
        {
            RecruitProductionPatch.fix(settlement);
        }

        // Token: 0x06000046 RID: 70 RVA: 0x000044D8 File Offset: 0x000026D8
        public static void fix(Settlement settlement)
        {
            foreach (Hero hero in settlement.Notables)
            {
                bool flag = SubModule.SpawnAtPlayerSettlement && settlement.OwnerClan != null && settlement.OwnerClan == Hero.MainHero.Clan;
                bool flag2 = SubModule.SpawnAtPlayerKingdom && settlement.OwnerClan != null && settlement.OwnerClan.Kingdom != null && settlement.OwnerClan.Kingdom.Leader == Hero.MainHero;
                bool flag3 = hero.CanHaveRecruits && (flag || flag2);
                if (flag3)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        bool flag4 = hero.VolunteerTypes[i] != null && !EnyclopediaEditUnitPatch.isCustomTroop(hero.VolunteerTypes[i]);
                        if (flag4)
                        {
                            bool flag5 = RecruitPatch2.isEliteLine(hero.VolunteerTypes[i]);
                            if (flag5)
                            {
                                hero.VolunteerTypes[i] = RecruitPatch2.tryToLevel(Game.Current.ObjectManager.GetObject<CharacterObject>("_elite_root"), hero.VolunteerTypes[i].Tier);
                            }
                            else
                            {
                                hero.VolunteerTypes[i] = RecruitPatch2.tryToLevel(Game.Current.ObjectManager.GetObject<CharacterObject>("_basic_root"), hero.VolunteerTypes[i].Tier);
                            }
                        }
                    }
                }
            }
        }
    }
}
