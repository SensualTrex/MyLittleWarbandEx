using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.Core;
using TaleWorlds.Library;
using static TaleWorlds.Core.Equipment;

namespace MyLittleWarbandEx.ViewModel
{
    public class EquipmentCardRowVM : TaleWorlds.Library.ViewModel
        {
        public MBBindingList<EquipmentCardVM> Cards { get; set; }

        public EquipmentCardRowVM(MBBindingList<EquipmentCardVM> cards)
        {
            Cards = cards;
        }

        public EquipmentCardRowVM()
        {
            Cards = new MBBindingList<EquipmentCardVM>();
        }

        public void AddCard(ItemObject item, CharacterObject troop, string equipmentType)
        {
            Cards.Add(new EquipmentCardVM(item, troop, equipmentType));
        }
    }
}
