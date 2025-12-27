using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.Recruitment;
using TaleWorlds.Core;
using TaleWorlds.GauntletUI;

namespace MyLittleWarbandEx.CampaignBehaviors
{
    internal class TroopsBehavior : CampaignBehaviorBase
    {
        public static CharacterObject[,] recruits;
        public override void RegisterEvents()
        {
            CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, AddCustomCultures);
        }

        public static void AddCustomCultures(CampaignGameStarter gameStarter)
        {
            
        }

        public override void SyncData(IDataStore dataStore)
        {
            
        }
    }
}
