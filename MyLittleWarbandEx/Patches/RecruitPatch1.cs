using HarmonyLib;
using MyLittleWarbandEx.CampaignBehaviors;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.Recruitment;

namespace MyLittleWarbandEx.Patches
{
    // Token: 0x02000013 RID: 19
    [HarmonyPatch(typeof(RecruitmentVM), "Deactivate")]
    internal class RecruitPatch1
    {
        // Token: 0x0600013F RID: 319 RVA: 0x0000C754 File Offset: 0x0000A954
        private static void Postfix()
        {
            bool flag = !SubModule.ReplaceAllForPlayer;
            if (!flag)
            {
                int num = 0;
                foreach (Hero hero in Settlement.CurrentSettlement.Notables)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        bool flag2 = hero.VolunteerTypes[i] != null && TroopsBehavior.recruits != null && TroopsBehavior.recruits[i, num] != null;
                        if (flag2)
                        {
                            hero.VolunteerTypes[i] = TroopsBehavior.recruits[i, num];
                        }
                    }
                    num++;
                }
            }
        }
    }
}
