using HarmonyLib;
using MyLittleWarbandEx.PatchTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace MyLittleWarbandEx.Patches
{
    internal sealed class BaseCharacterObjectPatch : PatchClass<BaseCharacterObjectPatch>
    {

        protected override IEnumerable<Patch> Prepare()
        {
            return new Patch[]
            {
                
            };
        }


    }
}
