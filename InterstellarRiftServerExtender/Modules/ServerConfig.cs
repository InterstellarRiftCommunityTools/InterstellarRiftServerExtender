using Game.ClientServer.Classes;
using Game.ClientServer.Classes.Economics;
using Game.Framework;
using Game.Universe;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Game.Configuration;
using Globals = Game.Configuration.Globals;

namespace IRSE.Modules
{
    [Serializable]
    public class ServerConfig
    {
        private static readonly Logger log = (Logger)Logger.Get(MethodBase.GetCurrentMethod().DeclaringType.ToString());
        [JsonIgnore]
        protected static ServerConfig m_singleton = (ServerConfig)null;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Hosting, Description = 100059, EditorType = ConfigOptionAttribute.MenuEditorType.Text, MenuText = 100000)]
        public string ServerName;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Hosting, Description = 100060, EditorType = ConfigOptionAttribute.MenuEditorType.Text, MenuText = 100001)]
        public string MessageOfTheDay;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Hosting, Description = 100061, EditorType = ConfigOptionAttribute.MenuEditorType.NumberText, MenuText = 100002)]
        public int Port;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Hosting, Description = 100062)]
        public uint Flags;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Hosting, Description = 100261, EditorType = ConfigOptionAttribute.MenuEditorType.NumberText, MenuText = 100260)]
        public int MaxPlayers;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Hosting, Description = 100063, EditorType = ConfigOptionAttribute.MenuEditorType.Text, MenuText = 100003)]
        public string GalaxyName;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Hosting, Description = 100064, EditorType = ConfigOptionAttribute.MenuEditorType.HiddenText, MenuText = 100004)]
        public string Password;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Hosting, Description = 100065, EditorType = ConfigOptionAttribute.MenuEditorType.Toggle, MenuText = 100005)]
        public bool AnnounceToMasterServer;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Misc, Description = 100066)]
        public int MaxSpeedupTime;
        public bool MultithreadedSystemUpdate;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Misc, Description = 100067)]
        public int ActiveDronesInTileRemovalThreshold;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.GhostClients, Description = 100068, EditorType = ConfigOptionAttribute.MenuEditorType.Toggle, MenuText = 100006)]
        public bool CreateGhostClients;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.GhostClients, Description = 100069, EditorType = ConfigOptionAttribute.MenuEditorType.Toggle, MenuText = 100007)]
        public bool GhostClientConsoleVisible;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.GhostClients, Description = 100070)]
        public int MaxGhostClientSaveRequestAcknowledgementTimeInSeconds;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.GhostClients, Description = 100071, DescriptionRowSpan = 3)]
        public int GhostClientStartCountThreshold;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.GhostClients)]
        public int GhostClientStartCountResetDurationInSeconds;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.GhostClients)]
        public int GhostClientPreventStartDurationInSeconds;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.GhostClients, Description = 100072)]
        public bool GhostClientStartCountThresholdEnabled;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.GhostClients, Description = 100073)]
        public int GhostClientHeartbeatIntervalInSeconds;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.GhostClients, Description = 100074)]
        public int GhostClientHeartbeatTimeoutInSeconds;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.GhostClients, Description = 100075, EditorType = ConfigOptionAttribute.MenuEditorType.Toggle, MenuText = 100008)]
        public bool GhostClientHeartbeatEnabled;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.GhostClients, Description = 100076)]
        public int MaxTimeToCacheSystemSaveDataInTicks;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Saving, Description = 100077)]
        public float AutoSaveDelay;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Saving, Description = 100078)]
        public float BackupSaveDelay;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Saving, Description = 100079)]
        public int BackupCount;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Saving, Description = 100080)]
        public string BackupsPath;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Database, Description = 100081)]
        public string GameDbPath;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Database, Description = 100082)]
        public string UserDbPath;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Database, Description = 100083)]
        public string ServerDbPath;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Database, Description = 100084)]
        public string WorkshopDbPath;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Respawn, Description = 100085)]
        public int MinimumRespawnCost;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Respawn, Description = 100086)]
        public int MaximumRespawnCost;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Respawn, Description = 100087)]
        public float RespawnCostPerGigameter;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Respawn, Description = 100088)]
        public int OutOfSystemRespawnCost;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Respawn, Description = 100089, DescriptionRowSpan = 2)]
        public int LongRangeRespawnThreshold;
        public int LongRangeRespawnTimerLength;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.AutoStorage, Description = 100090)]
        public int AutoShipStorageInSafeZoneTimerInTicks;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.AutoStorage, Description = 100091)]
        public int AutoShipStorageNotInSafeZoneTimerInTicks;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Skrill, Description = 100092, RequiredRebuild = ConfigOptionAttribute.RebuildType.Rules)]
        public int SkrillGroupSpawnChanceOutOf1000;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Skrill, Description = 100093, RequiredRebuild = ConfigOptionAttribute.RebuildType.Rules)]
        public int SkrillHunterSpawnChanceOutOf1000;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Skrill, Description = 100094, RequiredRebuild = ConfigOptionAttribute.RebuildType.Rules)]
        public int SkrillBomberSpawnChanceOutOf1000;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Skrill, Description = 100095, RequiredRebuild = ConfigOptionAttribute.RebuildType.Rules)]
        public int SkrillDisruptorSpawnChanceOutOf1000;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Skrill, Description = 100096, RequiredRebuild = ConfigOptionAttribute.RebuildType.Rules)]
        public int SkrillGruntMinAmountPerGroup;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Skrill, Description = 100097, RequiredRebuild = ConfigOptionAttribute.RebuildType.Rules)]
        public int SkrillGruntBaseMaxAmountPerGroup;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Skrill, Description = 100098, RequiredRebuild = ConfigOptionAttribute.RebuildType.Rules)]
        public int SkrillHunterMinAmountPerGroup;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Skrill, Description = 100099, RequiredRebuild = ConfigOptionAttribute.RebuildType.Rules)]
        public int SkrillHunterBaseMaxAmountPerGroup;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Skrill, Description = 100100, RequiredRebuild = ConfigOptionAttribute.RebuildType.Rules)]
        public int SkrillBomberMinAmountPerGroup;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Skrill, Description = 100101, RequiredRebuild = ConfigOptionAttribute.RebuildType.Rules)]
        public int SkrillBomberBaseMaxAmountPerGroup;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Skrill, Description = 100102, RequiredRebuild = ConfigOptionAttribute.RebuildType.Rules)]
        public int SkrillDisruptorMinAmountPerGroup;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Skrill, Description = 100103, RequiredRebuild = ConfigOptionAttribute.RebuildType.Rules)]
        public int SkrillDisruptorBaseMaxAmountPerGroup;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.NPCStations, Description = 100104)]
        public float NPCStation_GT_OreAndMetalMarketSpawnChance;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.NPCStations, Description = 100105)]
        public float NPCStation_GT_ProducedResourceMarketSpawnChance;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.NPCStations, Description = 100106)]
        public float NPCStation_GT_RareResourcesMarketSpawnChance;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.NPCStations, Description = 100107)]
        public float NPCStation_Locicorp_LogiCorpPrintingStationSpawnChance;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.NPCStations, Description = 100108)]
        public float NPCStation_S3_NitrogenMiningFacilitySpawnChance;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.NPCStations, Description = 100109)]
        public float NPCStation_S3_AmmunitionFactorySpawnChance;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.NPCStations, Description = 100110)]
        public float NPCStation_S3_WeaponFactorySpawnChance;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.NPCStations, Description = 100111)]
        public float NPCStation_HSC_MiningStationSpawnChance;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.NPCStations, Description = 100112)]
        public float NPCStation_HSC_RefineryStationSpawnChance;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.NPCStations, Description = 100113)]
        public float NPCStation_HSC_ProductionStationSpawnChance;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.NPCStations, Description = 100114)]
        public float NPCStation_VT_VaultronXT22SpawnChance;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.NPCStations, Description = 100115)]
        public float NPCStation_DFT_ScrapTraderSpawnChance;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.NPCStations, Description = 100116)]
        public float NPCStation_DFT_ScrapRefiningSpawnChance;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.NPCStations, Description = 100117)]
        public float NPCStation_DFT_BlackMarketSpawnChance;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.NPCStations, Description = 100118)]
        public float NPCStation_HydroPEX_RiftStationSpawnChance;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.CPU, Description = 100119, EditorType = ConfigOptionAttribute.MenuEditorType.Slider, MaxValue = 300f, MenuText = 100009, MinValue = 50f, Stepsize = 5)]
        public int CpuProviderSmallCpu;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.CPU, Description = 100120, EditorType = ConfigOptionAttribute.MenuEditorType.Slider, MaxValue = 500f, MenuText = 100010, MinValue = 100f, Stepsize = 5)]
        public int CpuProviderMediumCpu;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.CPU, Description = 100121, EditorType = ConfigOptionAttribute.MenuEditorType.Slider, MaxValue = 1000f, MenuText = 100011, MinValue = 250f, Stepsize = 5)]
        public int CpuProviderLargeCpu;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.CPU, Description = 100122, EditorType = ConfigOptionAttribute.MenuEditorType.Slider, MaxValue = 100f, MenuText = 100012, MinValue = 0.0f)]
        public int CpuCostArmorGenerator;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.CPU, Description = 100123, EditorType = ConfigOptionAttribute.MenuEditorType.Slider, MaxValue = 400f, MenuText = 100013, MinValue = 0.0f)]
        public int CpuCostArmorGeneratorMedium;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.CPU, Description = 100124, EditorType = ConfigOptionAttribute.MenuEditorType.Slider, MaxValue = 1000f, MenuText = 100014, MinValue = 0.0f)]
        public int CpuCostArmorGeneratorLarge;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.CPU, Description = 100125, EditorType = ConfigOptionAttribute.MenuEditorType.Slider, MaxValue = 250f, MenuText = 100015, MinValue = 0.0f)]
        public int CpuCostHackingTerminal;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.CPU, Description = 100126, EditorType = ConfigOptionAttribute.MenuEditorType.Slider, MaxValue = 250f, MenuText = 100016, MinValue = 0.0f)]
        public int CpuCostShieldGeneratorSmall;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.CPU, Description = 100127, EditorType = ConfigOptionAttribute.MenuEditorType.Slider, MaxValue = 250f, MenuText = 100017, MinValue = 0.0f)]
        public int CpuCostShieldGeneratorMedium;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.CPU, Description = 100128, EditorType = ConfigOptionAttribute.MenuEditorType.Slider, MaxValue = 250f, MenuText = 100018, MinValue = 0.0f)]
        public int CpuCostShieldGeneratorLarge;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.CPU, Description = 100129, EditorType = ConfigOptionAttribute.MenuEditorType.Slider, MaxValue = 250f, MenuText = 100019, MinValue = 0.0f)]
        public int CpuCostAmmoTank;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.CPU, Description = 100130, EditorType = ConfigOptionAttribute.MenuEditorType.Slider, MaxValue = 250f, MenuText = 100020, MinValue = 0.0f)]
        public int CpuCostEmpGenerator;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.CPU, Description = 100131, EditorType = ConfigOptionAttribute.MenuEditorType.Slider, MaxValue = 250f, MenuText = 100021, MinValue = 0.0f)]
        public int CpuCostMissileLauncher;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.CPU, Description = 100132, EditorType = ConfigOptionAttribute.MenuEditorType.Slider, MaxValue = 250f, MenuText = 100022, MinValue = 0.0f)]
        public int CpuCostMountedTurret;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.CPU, Description = 100133, EditorType = ConfigOptionAttribute.MenuEditorType.Slider, MaxValue = 250f, MenuText = 100023, MinValue = 0.0f)]
        public int CpuCostAutomatedTurret;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.CPU, Description = 100134, EditorType = ConfigOptionAttribute.MenuEditorType.Slider, MaxValue = 250f, MenuText = 100024, MinValue = 0.0f)]
        public int CpuCostFlakCannon;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.CPU, Description = 100135, EditorType = ConfigOptionAttribute.MenuEditorType.Slider, MaxValue = 250f, MenuText = 100025, MinValue = 0.0f)]
        public int CpuCostRailGun;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.CPU, Description = 100136, EditorType = ConfigOptionAttribute.MenuEditorType.Slider, MaxValue = 250f, MenuText = 100026, MinValue = 0.0f)]
        public int CpuCostHeavyRailGun;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.CPU, Description = 100150, EditorType = ConfigOptionAttribute.MenuEditorType.Slider, MaxValue = 250f, MenuText = 100253, MinValue = 0.0f)]
        public int CpuCostLaserArch;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.CPU, Description = 100151, EditorType = ConfigOptionAttribute.MenuEditorType.Slider, MaxValue = 250f, MenuText = 100254, MinValue = 0.0f)]
        public int CpuCostTeslaCoil;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.CPU, Description = 100152, EditorType = ConfigOptionAttribute.MenuEditorType.Slider, MaxValue = 250f, MenuText = 100255, MinValue = 0.0f)]
        public int CpuCostBombTrap;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.AutoWeapons, Description = 100137, EditorType = ConfigOptionAttribute.MenuEditorType.FloatSlider, MaxValue = 5f, MenuText = 100027, MinValue = 0.0f)]
        public float AutomatedTurretDamageMod;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.AutoWeapons, Description = 100138, EditorType = ConfigOptionAttribute.MenuEditorType.FloatSlider, MaxValue = 20f, MenuText = 100028, MinValue = 0.0f)]
        public float AutomatedTurretBaseAttackCooldownMod;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.AutoWeapons, Description = 100139, EditorType = ConfigOptionAttribute.MenuEditorType.FloatSlider, MaxValue = 5f, MenuText = 100029, MinValue = 0.0f)]
        public float AutomatedTurretAttackCooldownPerVelocityMod;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.AutoWeapons, Description = 100140, EditorType = ConfigOptionAttribute.MenuEditorType.FloatSlider, MaxValue = 5f, MenuText = 100030, MinValue = 0.0f)]
        public float AutomatedLaserDamageMod;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.AutoWeapons, Description = 100141, EditorType = ConfigOptionAttribute.MenuEditorType.FloatSlider, MaxValue = 20f, MenuText = 100031, MinValue = 0.0f)]
        public float AutomatedLaserBaseAttackCooldownMod;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.AutoWeapons, Description = 100142, EditorType = ConfigOptionAttribute.MenuEditorType.FloatSlider, MaxValue = 5f, MenuText = 100032, MinValue = 0.0f)]
        public float AutomatedLaserAttackCooldownPerVelocityMod;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Drones, Description = 100143, EditorType = ConfigOptionAttribute.MenuEditorType.Toggle, MenuText = 100033)]
        public bool MineDroneSpawningEnabled;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Drones, Description = 100144, EditorType = ConfigOptionAttribute.MenuEditorType.Toggle, MenuText = 100034)]
        public bool TradeDroneSpawningEnabled;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Drones, Description = 100145, EditorType = ConfigOptionAttribute.MenuEditorType.Toggle, MenuText = 100035)]
        public bool CombatDroneSpawningEnabled;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Drones, Description = 100146, EditorType = ConfigOptionAttribute.MenuEditorType.Toggle, MenuText = 100036)]
        public bool PirateDroneSpawningEnabled;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.AFK, Description = 100147, EditorType = ConfigOptionAttribute.MenuEditorType.Slider, MaxValue = 1000f, MenuText = 100037, MinValue = 0.0f)]
        public int HardenedArmorMaxAmount;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.AFK, Description = 100148, EditorType = ConfigOptionAttribute.MenuEditorType.Slider, MaxValue = 100f, MenuText = 100038, MinValue = 0.0f)]
        public int HardeneArmorDecaySpeed;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.AFK, Description = 100149, EditorType = ConfigOptionAttribute.MenuEditorType.Slider, MaxValue = 7200f, MenuText = 100039, MinValue = 0.0f)]
        public int HardenedArmorOfflineTimeInTicks;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Fines, Description = 100150, EditorType = ConfigOptionAttribute.MenuEditorType.PercSlider, MaxValue = 10f, MenuText = 100040, MinValue = 0.0f)]
        public float DamageFineScale;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Fines, Description = 100151, EditorType = ConfigOptionAttribute.MenuEditorType.PercSlider, MaxValue = 10f, MenuText = 100041, MinValue = 0.0f)]
        public float ContrabandFineScale;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Fines, Description = 100152, EditorType = ConfigOptionAttribute.MenuEditorType.PercSlider, MaxValue = 10f, MenuText = 100042, MinValue = 0.0f)]
        public float GasReleaseFineScale;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Fines, Description = 100153, EditorType = ConfigOptionAttribute.MenuEditorType.PercSlider, MaxValue = 10f, MenuText = 100043, MinValue = 0.0f)]
        public float LoanFineScale;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Fines, Description = 100154, EditorType = ConfigOptionAttribute.MenuEditorType.PercSlider, MaxValue = 10f, MenuText = 100044, MinValue = 0.0f)]
        public float HackingFineScale;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Power, Description = 100155, EditorType = ConfigOptionAttribute.MenuEditorType.PercSlider, MaxValue = 10f, MenuText = 100045, MinValue = 0.0f)]
        public float HydrogenConsumptionScale;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Power, Description = 100156, EditorType = ConfigOptionAttribute.MenuEditorType.PercSlider, MaxValue = 10f, MenuText = 100046, MinValue = 0.0f)]
        public float SolarPowerScale;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Power, Description = 100157, EditorType = ConfigOptionAttribute.MenuEditorType.PercSlider, MaxValue = 10f, MenuText = 100047, MinValue = 0.0f)]
        public float HydrogenPowerScale;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Power, Description = 100158, EditorType = ConfigOptionAttribute.MenuEditorType.PercSlider, MaxValue = 10f, MenuText = 100048, MinValue = 0.0f)]
        public float NuclearPowerScale;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Production, Description = 100159, EditorType = ConfigOptionAttribute.MenuEditorType.PercSlider, MaxValue = 10f, MenuText = 100049, MinValue = 0.0f)]
        public float ExtractorMineSpeedScale;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Production, Description = 100160, EditorType = ConfigOptionAttribute.MenuEditorType.PercSlider, MaxValue = 10f, MenuText = 100050, MinValue = 0.0f)]
        public float RefineryRefineSpeedScale;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Production, Description = 100161, EditorType = ConfigOptionAttribute.MenuEditorType.PercSlider, MaxValue = 10f, MenuText = 100051, MinValue = 0.0f)]
        public float AssemblerAssembleSpeedScale;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Production, Description = 100162, EditorType = ConfigOptionAttribute.MenuEditorType.PercSlider, MaxValue = 10f, MenuText = 100052, MinValue = 0.0f)]
        public float ThreeDPrinterPrintSpeedScale;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.ToolDurability, Description = 100163)]
        public int AutoMineCartridgeDurabilityInTicks;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.ToolDurability, Description = 100164)]
        public int AutoProductionCartridgeDurabilityInTicks;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.ToolDurability, Description = 100165)]
        public int AutoPrintingCartridgeDurabilityInTicks;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.ToolDurability, Description = 100166)]
        public int AutoDroneControlCartridgeDurabilityInTicks;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.ToolDurability, Description = 100167)]
        public int AutoTeleportationCartridgeDurabilityInTicks;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.ToolDurability, Description = 100168)]
        public int RiftGeneratorCartridgeDurabilityInUses;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.ToolDurability, Description = 100169)]
        public int HeatsinkTIDurabilityInTicks;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.ToolDurability, Description = 100170)]
        public int HeatsinkTIIDurabilityInTicks;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.ToolDurability, Description = 100171)]
        public int HeatsinkTIIIDurabilityInTicks;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.ToolDurability, Description = 100172)]
        public int HeatsinkAdvancedDurabilityInTicks;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.ToolDurability, Description = 100173)]
        public int HeatsinkTIHeatCapacity;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.ToolDurability, Description = 100174)]
        public int HeatsinkTIIHeatCapacity;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.ToolDurability, Description = 100175)]
        public int HeatsinkTIIIHeatCapacity;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.ToolDurability, Description = 100176)]
        public int HeatsinkAdvancedHeatCapacity;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.ToolDurability, Description = 100177)]
        public int FocussingCrystalTIDurabilityInTicks;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.ToolDurability, Description = 100178)]
        public int FocussingCrystalTIIDurabilityInTicks;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.ToolDurability, Description = 100179)]
        public int FocussingCrystalTIIIDurabilityInTicks;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.ToolDurability, Description = 100180)]
        public int VaultronPassT0SlotsPerPlayer;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.ToolDurability, Description = 100181)]
        public int VaultronPassTISlotsPerPlayer;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.ToolDurability, Description = 100182)]
        public int VaultronPassTIISlotsPerPlayer;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.ToolDurability, Description = 100183)]
        public int VaultronPassTIIISlotsPerPlayer;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.ToolDurability, Description = 100184)]
        public int PortableBatteryT0MaxPower;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.ToolDurability, Description = 100185)]
        public int PortableBatteryT0ChargeRate;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.ToolDurability, Description = 100186)]
        public int PortableBatteryT0DisChargeRate;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.ToolDurability, Description = 100187)]
        public int PortableBatteryT1MaxPower;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.ToolDurability, Description = 100188)]
        public int PortableBatteryT1ChargeRate;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.ToolDurability, Description = 100189)]
        public int PortableBatteryT1DisChargeRate;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.ToolDurability, Description = 100190)]
        public int PortableBatteryT2MaxPower;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.ToolDurability, Description = 100191)]
        public int PortableBatteryT2ChargeRate;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.ToolDurability, Description = 100192)]
        public int PortableBatteryT2DisChargeRate;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.ToolDurability, Description = 100193)]
        public int PortableBatteryT3MaxPower;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.ToolDurability, Description = 100194)]
        public int PortableBatteryT3ChargeRate;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.ToolDurability, Description = 100195)]
        public int PortableBatteryT3DisChargeRate;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Repair, Description = 100196)]
        public int SmallArmorGeneratorExternalRepairSpeedPerTick;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Repair, Description = 100197)]
        public int MediumArmorGeneratorExternalRepairSpeedPerTick;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Repair, Description = 100198)]
        public int LargeArmorGeneratorExternalRepairSpeedPerTick;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Repair, Description = 100199, DescriptionRowSpan = 2)]
        public int MinDefaultArmorRepairExternalRepairSpeedPerTick;
        public int MaxDefaultArmorRepairExternalRepairSpeedPerTick;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Misc, Description = 100200, EditorType = ConfigOptionAttribute.MenuEditorType.Toggle, MenuText = 100053)]
        public bool RiftCartridgesEnabled;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Misc, Description = 100201, EditorType = ConfigOptionAttribute.MenuEditorType.Slider, MaxValue = 1E+09f, MenuText = 100054, MinValue = 0.0f)]
        public int ShipStorageLimit;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Misc, Description = 100202, EditorType = ConfigOptionAttribute.MenuEditorType.Slider, MaxValue = 1E+09f, MenuText = 100055, MinValue = 0.0f)]
        public int PrivateShipStorageLimit;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Misc, Description = 100203, EditorType = ConfigOptionAttribute.MenuEditorType.PercSlider, MaxValue = 10f, MenuText = 100056, MinValue = 0.0f)]
        public float WarpSpeedScale;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Misc, Description = 100204, EditorType = ConfigOptionAttribute.MenuEditorType.FloatSlider, MaxValue = 400000f, MenuText = 100057, MinValue = 0.0f)]
        public float CrewDisbandTimeInSeconds;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Misc, Description = 100205, Format = "{\"RT_Iron\": {\"StandardPrice\": 10,\"priceDeviant\": 10,\"StockStandard\": 200000,\"StockDeviant\": 150000,\"CanGenerate\": true}}\"")]
        public Dictionary<ResourceTypes, ResourcePriceStats> OverwriteResourcePriceStats;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Misc, Description = 100206, Format = "{\"TT_RepairGun_Ammo\": {\"StandardPrice\": 10,\"priceDeviant\": 10,\"StockStandard\": 50,\"StockDeviant\": 25,\"StockInitialMin\": 1,\"StockInitialMax\": 3,\"AutoGenerateAtStores\": false}}\"")]
        public Dictionary<ToolTypes, ToolPriceStats> OverwriteToolPriceStats;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Misc, Description = 100207)]
        public float UpgradeSalvageArmorPerc;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Misc, Description = 100208)]
        public float DeviceDamageResourceLossThreshold;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Misc, Description = 100209)]
        public float DeviceDamageResourceLossMinScale;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Misc, Description = 100210, Format = "{\"Alpha Ventura\": {\"overrideResourceAbundanty\": {\"RT_IronOre\": {\"v\": 65536}},\"overrideName\": \"Alpha Ventura\"}}")]
        public Dictionary<string, SystemSettings> SpecificSystemSettings;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.UniverseGeneration, Description = 100211)]
        public int MaxUniverseGeneratorGenerationDist;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.UniverseGeneration, Description = 100212)]
        public uint StartUniverseGeneratorSeed;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.UniverseGeneration, Description = 100256)]
        public int Tier0ProtectedSystemChance;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.UniverseGeneration, Description = 100257)]
        public int Tier1ProtectedSystemChance;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.UniverseGeneration, Description = 100258)]
        public int Tier2ProtectedSystemChance;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.UniverseGeneration, Description = 100259)]
        public int Tier3ProtectedSystemChance;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.WelcomePopup, Description = 100213)]
        public bool EnableServerWelcomePopup;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.WelcomePopup, Description = 100214)]
        public string ServerWelcomePopupMessageTitle;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.WelcomePopup, Description = 100215)]
        public string ServerWelcomePopupMessageText;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.WelcomePopup, Description = 100216)]
        public uint[] ServerWelcomePopupMessageTitleColor;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Desync, Description = 100217)]
        public bool DetailedDesyncInfo;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Desync, Description = 100217)]
        public int TrackTimeInTicks;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Desync, Description = 100219)]
        public int ResyncTriesBeforeServerReload;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Desync, Description = 100220)]
        public int TicksAfterSystemLoadForServerReload;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.Desync, Description = 100221, EditorType = ConfigOptionAttribute.MenuEditorType.Toggle, MenuText = 100058)]
        public bool ReconnectCriticalDesyncPlayers;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.StarterShips, Description = 100222)]
        public bool RefreshStarterShipsOnStartup;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.StarterShips, Description = 100223)]
        public Dictionary<FactionTypes, List<FactionStarterShipDetails>> FactionStarterShips;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.DerelictShips, Description = 100224)]
        public bool RefreshDerelictShipsOnStartup;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.DerelictShips, Description = 100223)]
        public List<ServerConfig.DerelictShipDetails> DerelictShipStats;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.RichAsteroids, Description = 100226)]
        public int RichAsteriodMinTier;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.RichAsteroids, Description = 100227)]
        public int RichAsteriodMinTicksForFirstSpawn;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.RichAsteroids, Description = 100228)]
        public int RichAsteriodMaxTicksForFirstSpawn;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.RichAsteroids, Description = 100229)]
        public int RichAsteriodMinTicksBetweenSpawns;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.RichAsteroids, Description = 100230)]
        public int RichAsteriodMaxTicksBetweenSpawns;
        [ConfigOption(Category = ConfigOptionAttribute.CategoryType.RichAsteroids, Description = 100231)]
        public int RichAsteriodMaxAmount;
        public bool OrderDebuggerWriteOutputFiles;
        public int OrderDebuggerWriteToDbFrameCount;
        public bool OrderDebuggerServerWriteNonDiffMessages;

        public static ServerConfig Singleton
        {
            get
            {
                if (ServerConfig.m_singleton == null)
                    ServerConfig.Load();
                return ServerConfig.m_singleton;
            }
            set
            {
                ServerConfig.m_singleton = value;
            }
        }

        public ServerConfig()
        {
            Dictionary<FactionTypes, List<FactionStarterShipDetails>> dictionary = new Dictionary<FactionTypes, List<FactionStarterShipDetails>>();
            int num1 = 1;
            dictionary.Add((FactionTypes)num1, new List<FactionStarterShipDetails>()
      {
        new FactionStarterShipDetails()
        {
          Identifier = "HSC Excavator Mk I",
          Price = 699999L,
          Description = "A small mining vessel, equipped with an extractor and decent cargo capacity.",
          PreviewImageFile = "Textures/OS/ShipImages/str_Miner_01.png",
          Tier = 0,
          CanUseCredit = true
        },
        new FactionStarterShipDetails()
        {
          Identifier = "HSC Excavator Mk II",
          Price = 2999999L,
          Description = "A medium sized mining vessel, equipped with extractor, refineries and other production devices.",
          PreviewImageFile = "Textures/OS/ShipImages/str_Miner_02.png",
          Tier = 1,
          CanUseCredit = false
        }
      });
            int num2 = 6;
            dictionary.Add((FactionTypes)num2, new List<FactionStarterShipDetails>()
      {
        new FactionStarterShipDetails()
        {
          Identifier = "GT Mk IV Hauler",
          Price = 624999L,
          Description = "A small cargo hauler, equipped with plenty cargo pads, and a cargo teleporter.",
          PreviewImageFile = "Textures/OS/ShipImages/str_Hauler_01.png",
          Tier = 0,
          CanUseCredit = true
        },
        new FactionStarterShipDetails()
        {
          Identifier = "GT Mk VI Hauler",
          Price = 2499999L,
          Description = "A medium sized cargo hauler, equipped with large cargo containers, and a cargo teleporter.",
          PreviewImageFile = "Textures/OS/ShipImages/str_Hauler_02.png",
          Tier = 1,
          CanUseCredit = false
        }
      });
            int num3 = 2;
            dictionary.Add((FactionTypes)num3, new List<FactionStarterShipDetails>()
      {
        new FactionStarterShipDetails()
        {
          Identifier = "S3 Peregrine Mk I",
          Price = 599999L,
          Description = "A small combat vessel equipped with multiple weapons and a salvaging unit.",
          PreviewImageFile = "Textures/OS/ShipImages/str_Peregrine_01.png",
          Tier = 0,
          CanUseCredit = true
        },
        new FactionStarterShipDetails()
        {
          Identifier = "S3 Reaper Mk II",
          Price = 4999999L,
          Description = "A medium sized combat vessel equipped with multiple weapons, missile launchers, and salvaging units.",
          PreviewImageFile = "Textures/OS/ShipImages/str_Reaper_01.png",
          Tier = 1,
          CanUseCredit = false
        }
      });
            this.FactionStarterShips = dictionary;
            this.RefreshDerelictShipsOnStartup = true;
            this.DerelictShipStats = new List<ServerConfig.DerelictShipDetails>()
      {
        new ServerConfig.DerelictShipDetails()
        {
          Identifier = "Derelict Hurles Co. Freighter",
          MinSpawnDelay = 6000,
          MaxSpawnDelay = 864000,
          Tier = 0,
          ChanceSelfDestructTriggerOutOf100 = 40,
          TriggerdSelfDestructTimeMin = 6000,
          TriggeredSelfDestructTimeMax = 18000,
          RemoveTime = 72000
        },
        new ServerConfig.DerelictShipDetails()
        {
          Identifier = "Seeker Astra",
          MinSpawnDelay = 6000,
          MaxSpawnDelay = 864000,
          Tier = 2,
          ChanceSelfDestructTriggerOutOf100 = 40,
          TriggerdSelfDestructTimeMin = 12000,
          TriggeredSelfDestructTimeMax = 24000,
          RemoveTime = 72000
        },
        new ServerConfig.DerelictShipDetails()
        {
          Identifier = "Derelict Station",
          MinSpawnDelay = 6000,
          MaxSpawnDelay = 864000,
          Tier = 2,
          ChanceSelfDestructTriggerOutOf100 = 40,
          TriggerdSelfDestructTimeMin = 9600,
          TriggeredSelfDestructTimeMax = 20400,
          RemoveTime = 72000
        },
        new ServerConfig.DerelictShipDetails()
        {
          Identifier = "Habitat 107",
          MinSpawnDelay = 6000,
          MaxSpawnDelay = 864000,
          Tier = 1,
          ChanceSelfDestructTriggerOutOf100 = 40,
          TriggerdSelfDestructTimeMin = 8400,
          TriggeredSelfDestructTimeMax = 19200,
          RemoveTime = 72000
        },
        new ServerConfig.DerelictShipDetails()
        {
          Identifier = "Derelict Excavator",
          MinSpawnDelay = 6000,
          MaxSpawnDelay = 864000,
          Tier = 1,
          ChanceSelfDestructTriggerOutOf100 = 40,
          TriggerdSelfDestructTimeMin = 6000,
          TriggeredSelfDestructTimeMax = 15600,
          RemoveTime = 72000
        },
        new ServerConfig.DerelictShipDetails()
        {
          Identifier = "Cobold",
          MinSpawnDelay = 6000,
          MaxSpawnDelay = 864000,
          Tier = 1,
          ChanceSelfDestructTriggerOutOf100 = 40,
          TriggerdSelfDestructTimeMin = 6000,
          TriggeredSelfDestructTimeMax = 14400,
          RemoveTime = 72000
        }
      };
            this.RichAsteriodMinTier = 2;
            this.RichAsteriodMinTicksForFirstSpawn = 1200;
            this.RichAsteriodMaxTicksForFirstSpawn = 2400;
            this.RichAsteriodMinTicksBetweenSpawns = 24000;
            this.RichAsteriodMaxTicksBetweenSpawns = 48000;
            this.RichAsteriodMaxAmount = 10;
            this.OrderDebuggerWriteToDbFrameCount = 5;
        }

        ~ServerConfig()
        {
            if (this != ServerConfig.m_singleton)
                return;
            this.Save();
        }

        public static void Load()
        {
            if (ServerConfig.m_singleton != null)
                return;
            try
            {
                string path = "server" + ExtenderGlobals.ServerAddition + ".json";
                ServerConfig.log.Info("Loading server config file: " + path, "Gamelogic");
                using (StreamReader streamReader = new StreamReader(path))
                {
                    JsonSerializerSettings settings = new JsonSerializerSettings() { ObjectCreationHandling = ObjectCreationHandling.Replace };
                    ServerConfig.m_singleton = JsonConvert.DeserializeObject<ServerConfig>(streamReader.ReadToEnd(), settings);
                }
            }
            catch
            {
                ServerConfig.log.Info("Could not load server configuration.", "Gamelogic");
                ServerConfig.m_singleton = new ServerConfig();
                ServerConfig.m_singleton.Save();
            }
            if (ServerConfig.m_singleton == null)
            {
                ServerConfig.log.Info("Could not load server configuration.", "Gamelogic");
                ServerConfig.m_singleton = new ServerConfig();
                ServerConfig.m_singleton.Save();
            }
            ServerConfig.i_fixConfig();
        }

        public ServerConfig CreateWorkingCopy()
        {
            try
            {
                string path = "server" + ExtenderGlobals.ServerAddition + ".json";
                ServerConfig.log.Info("Loading server config file: " + path, "Gamelogic");
                using (StreamReader streamReader = new StreamReader(path))
                {
                    JsonSerializerSettings settings = new JsonSerializerSettings() { ObjectCreationHandling = ObjectCreationHandling.Replace };
                    return JsonConvert.DeserializeObject<ServerConfig>(streamReader.ReadToEnd(), settings);
                }
            }
            catch
            {
                Console.WriteLine("Unable to copy server config for edit, returning original");
                return ServerConfig.m_singleton;
            }
        }

        public void Save()
        {
            if (((IEnumerable<string>)Program.CommandLineArgs).Contains<string>("-ghostclient"))
                return;
            for (int index = 0; index < 100; ++index)
            {
                try
                {
                    using (StreamWriter streamWriter = new StreamWriter("server" + ExtenderGlobals.ServerAddition + ".json"))
                    {
                        streamWriter.Write(JsonConvert.SerializeObject((object)this, Formatting.Indented));
                        break;
                    }
                }
                catch
                {
                }
            }
        }

        private static void i_fixConfig()
        {
            ServerConfig serverConfig = new ServerConfig();
            bool flag = false;
            if (ServerConfig.Singleton.RefreshStarterShipsOnStartup)
            {
                foreach (KeyValuePair<FactionTypes, List<FactionStarterShipDetails>> factionStarterShip in serverConfig.FactionStarterShips)
                {
                    if (!ServerConfig.Singleton.FactionStarterShips.ContainsKey(factionStarterShip.Key))
                        ServerConfig.Singleton.FactionStarterShips[factionStarterShip.Key] = factionStarterShip.Value;
                    if (ServerConfig.Singleton.FactionStarterShips.ContainsKey(factionStarterShip.Key))
                    {
                        ServerConfig.Singleton.FactionStarterShips[factionStarterShip.Key].Clear();
                        int index = 0;
                        foreach (FactionStarterShipDetails starterShipDetails1 in serverConfig.FactionStarterShips[factionStarterShip.Key])
                        {
                            FactionStarterShipDetails starterShipDetails2 = factionStarterShip.Value[index];
                            ServerConfig.Singleton.FactionStarterShips[factionStarterShip.Key].Add(starterShipDetails2);
                            ++index;
                        }
                    }
                    flag = true;
                }
            }
            if (ServerConfig.Singleton.RefreshDerelictShipsOnStartup)
            {
                foreach (ServerConfig.DerelictShipDetails derelictShipStat in serverConfig.DerelictShipStats)
                {
                    ServerConfig.DerelictShipDetails entry = derelictShipStat;
                    if (!ServerConfig.Singleton.DerelictShipStats.Contains(entry))
                    {
                        if (ServerConfig.Singleton.DerelictShipStats.Any<ServerConfig.DerelictShipDetails>((Func<ServerConfig.DerelictShipDetails, bool>)(x => x.Identifier == entry.Identifier)))
                            ServerConfig.Singleton.DerelictShipStats.RemoveAll((Predicate<ServerConfig.DerelictShipDetails>)(x => x.Identifier == entry.Identifier));
                        ServerConfig.Singleton.DerelictShipStats.Add(entry);
                        flag = true;
                    }
                }
            }
            if (!flag)
                return;
            ServerConfig.Singleton.Save();
        }

        public struct DerelictShipDetails
        {
            public string Identifier;
            public int MinSpawnDelay;
            public int MaxSpawnDelay;
            public int Tier;
            public int ChanceSelfDestructTriggerOutOf100;
            public int TriggerdSelfDestructTimeMin;
            public int TriggeredSelfDestructTimeMax;
            public int RemoveTime;
        }
    }
}
