

using SandBox.View.Map;

using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.Core;

namespace MyLittleWarbandEx.CampaignBehaviors
{
    internal sealed class UIBehavior : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            CampaignEvents.TickEvent.AddNonSerializedListener(this, AddUIElements);
        }

        private void AddUIElements(float obj)
        {
        }

        public override void SyncData(IDataStore dataStore)
        {
        }
    }
}