using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.ViewModels;

using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.Overlay;
using TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.Recruitment;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.GauntletUI;

namespace MyLittleWarbandEx.ViewModelMixin
{
    [ViewModelMixin]
    [UsedImplicitly]
    internal sealed class RecruitmentVMMixin : BaseViewModelMixin<RecruitmentVM>
    {
        private RecruitmentVM @base;
        public RecruitmentVMMixin(RecruitmentVM vm) : base(vm)
        {
            @base = vm;
        }

        public override void OnFinalize()
        {
            base.OnFinalize();
        }


        public override void OnRefresh()
        {

            base.OnRefresh();
            
        }

    }
}
