using Bannerlord.UIExtenderEx;
using HarmonyLib;
using MCM.Abstractions.Base.Global;
using MyLittleWarbandEx.CampaignBehaviors;
using MyLittleWarbandEx.PatchTools;
using Serilog;
using System;
using System.Collections.Generic;
using System.Runtime;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;


namespace MyLittleWarbandEx
{
    public class SubModule : MBSubModuleBase
    {
        public static readonly string Name = typeof(SubModule).Namespace!;
        public static readonly string DisplayName = new TextObject($"{{=MYz8nKqq}}{Name}").ToString();
        public static readonly string MainHarmonyDomain = "bannerlord." + Name.ToLower();
        public static readonly string CampaignHarmonyDomain = MainHarmonyDomain + ".campaign";

        internal static SubModule Instance { get; set; } = default!;

        private static readonly List<Action> ActionsToExecuteNextTick = new List<Action>();

        public static bool ReplaceAllForPlayer = true;

        private MyLittleWarbandSetting SettingInstance;

        public static bool ClanRecruitCustomTroop;

        public static bool SpawnAtPlayerSettlement;

        public static bool SpawnAtPlayerKingdom;

        public static bool FullUnitEditor;

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();
        }


        public override void OnGameLoaded(Game game, object initializerObject)
        {
            base.OnGameLoaded(game, initializerObject);
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            base.OnGameStart(game, gameStarterObject);
            if (game.GameType is Campaign)
            {
                ((CampaignGameStarter)gameStarterObject).AddBehavior(new TroopsBehavior());
                ((CampaignGameStarter)gameStarterObject).AddBehavior(new CustomUnitsBehavior());
                ((CampaignGameStarter)gameStarterObject).AddBehavior(new XMLExporter());
                SettingInstance = GlobalSettings<MyLittleWarbandSetting>.Instance;
                ClanRecruitCustomTroop = SettingInstance.ClanRecruitCustomTroop;
                SpawnAtPlayerSettlement = SettingInstance.SpawnAtPlayerSettlement;
                SpawnAtPlayerKingdom = SettingInstance.SpawnAtPlayerKingdom;
                FullUnitEditor = SettingInstance.FullUnitEditor;
                PatchManager.ApplyCampaignPatches(CampaignHarmonyDomain);
            }
            
        }

        public static void ExecuteActionOnNextTick(Action action)
        {
            if (action != null)
            {
                ActionsToExecuteNextTick.Add(action);
            }
        }

        protected override void OnApplicationTick(float dt)
        {
            base.OnApplicationTick(dt);
            CheckKeyPressed();
            foreach (Action item in ActionsToExecuteNextTick)
            {
                item();
            }

            ActionsToExecuteNextTick.Clear();
            SettingInstance = GlobalSettings<MyLittleWarbandSetting>.Instance;
            if (SettingInstance != null)
            {
                ClanRecruitCustomTroop = SettingInstance.ClanRecruitCustomTroop;
                SpawnAtPlayerSettlement = SettingInstance.SpawnAtPlayerSettlement;
                SpawnAtPlayerKingdom = SettingInstance.SpawnAtPlayerKingdom;
                FullUnitEditor = SettingInstance.FullUnitEditor;
            }
        }

        public static void CheckKeyPressed()
        {
            if (Input.IsKeyPressed(InputKey.Escape))
            {
                if (EquipmentSelectorBehavior.layer != null)
                {
                    EquipmentSelectorBehavior.DeleteVMLayer();
                }
                else if (CustomUnitsBehavior.layer != null)
                {
                    CustomUnitsBehavior.DeleteVMLayer();
                }
            }
        }

        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            Instance = this;

            //var extender = UIExtender.Create(Name);
            //var assembly = typeof(SubModule).Assembly;
            //extender.Register(assembly);
            //extender.Enable();

            new Harmony("MyLittleWarbandEx").PatchAll(); 
            PatchManager.ApplyMainPatches(MainHarmonyDomain);
        }
    }
}