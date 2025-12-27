using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;

namespace MyLittleWarbandEx.ViewModel
{
    public class FilterableViewModel : TaleWorlds.Library.ViewModel
    {
        private List<int> _filter_tiers = new List<int>();
        private List<CultureObject> _filter_Culture = new List<CultureObject>();
        private List<WeaponClass> _filter_weapon_classes = new List<WeaponClass>();
        private List<ArmorComponent.ArmorMaterialTypes> _filter_armour_types = new List<ArmorComponent.ArmorMaterialTypes>();
        public FilterableViewModel() { 
        
        }

        public virtual void RefreshData()
        {
            
        }

        [DataSourceProperty]
        public List<int> FilterTiers
        {
            get
            {
                return _filter_tiers;
            }set
            {
                if (_filter_tiers != value)
                {
                    _filter_tiers = value;
                }
            }
        }

        [DataSourceProperty]
        public List<WeaponClass> FilterWeaponTypes
        {
            get
            {
                return _filter_weapon_classes;
            }
            set
            {
                if (_filter_weapon_classes != value)
                {
                    _filter_weapon_classes = value;
                }
            }
        }

        [DataSourceProperty]
        public List<CultureObject> FilterCulture
        {
            get
            {
                return _filter_Culture;
            }
            set
            {
                if (_filter_Culture != value)
                {
                    _filter_Culture = value;
                }
            }
        }

    }
}
