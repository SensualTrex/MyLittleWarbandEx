using MyLittleWarbandEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.SaveSystem;

namespace MyLittleWarbandEx
{
    public class SaveDefiner : SaveableTypeDefiner
    {
        public SaveDefiner()
            : base(1436500005)
        {
        }

        protected override void DefineClassTypes()
        {
            AddClassDefinition(typeof(CustomUnit), 1);
        }

        protected override void DefineContainerDefinitions()
        {
            ConstructContainerDefinition(typeof(Dictionary<string, CustomUnit>));
        }
    }
}



