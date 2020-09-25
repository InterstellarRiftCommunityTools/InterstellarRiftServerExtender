// This file was generated with ServerConfigProperties class
// To allow a PropertyGrid to use IRs fields as properties.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Reflection;
using Game.Configuration;
namespace IRSE.Modules.GameConfig
{ 
  public static class ServerConfigConverter
  {

[Category("Server Settings")]
[Description("The name that will advertise the server in the server list")]
public static System.String ServerName {get{return ServerConfig;} set {ServerConfig.Singleton.ServerName = value;}}


[Category("Server Settings")]
[Description("The message players will see when they connect to the server")]
public static System.String MessageOfTheDay {get{return ServerConfig.Singleton.MessageOfTheDay;} set {ServerConfig.Singleton.MessageOfTheDay = value;}}


[Category("Server Settings")]
[Description("The port used for players to connect to the server")]
public static System.Int32 Port {get{return ServerConfig.Singleton.Port;} set {ServerConfig.Singleton.Port = value;}}


[Category("Server Settings")]
[Description("Specific server settings represented as a bit-field: 1 - creative, 2 - indestructible, 4 - no oxygen life loss, 8 - disable pvp, 16 - disable system events, 32 - ignore personal vault limit, 64 - ignore crew vault limit, 128 - ignore fleet vault limit, 1024 - don't check for positional desyncs, 2048 - don't check for gameplay desyncs")]
public static System.UInt32 Flags {get{return ServerConfig.Singleton.Flags;} set {ServerConfig.Singleton.Flags = value;}}


[Category("Server Settings")]
[Description("The maximum amount of players that can be in the server")]
public static System.Int32 MaxPlayers {get{return ServerConfig.Singleton.MaxPlayers;} set {ServerConfig.Singleton.MaxPlayers = value;}}


[Category("Server Settings")]
[Description("The name of the save that will be used or created for the galaxy")]
public static System.String GalaxyName {get{return ServerConfig.Singleton.GalaxyName;} set {ServerConfig.Singleton.GalaxyName = value;}}


[Category("Server Settings")]
[Description("The password needed to connect to the server, this cannot be changed in the config file, it has to be set using the setPassword command.")]
public static System.String Password {get{return ServerConfig.Singleton.Password;} set {ServerConfig.Singleton.Password = value;}}


[Category("Server Settings")]
[Description("Whether or not the server will appear in the server list")]
public static System.Boolean AnnounceToMasterServer {get{return ServerConfig.Singleton.AnnounceToMasterServer;} set {ServerConfig.Singleton.AnnounceToMasterServer = value;}}


[Category("Miscellaneous")]
[Description("How much faster the server might run if it falls behind")]
public static System.Int32 MaxSpeedupTime {get{return ServerConfig.Singleton.MaxSpeedupTime;} set {ServerConfig.Singleton.MaxSpeedupTime = value;}}


[Category("Miscellaneous")]
[Description("The maximum number of drones at a certain location before the get removed")]
public static System.Int32 ActiveDronesInTileRemovalThreshold {get{return ServerConfig.Singleton.ActiveDronesInTileRemovalThreshold;} set {ServerConfig.Singleton.ActiveDronesInTileRemovalThreshold = value;}}


[Category("Ghost Clients")]
[Description("Whether or not ther server will run separate processes so it can save without stalling.")]
public static System.Boolean CreateGhostClients {get{return ServerConfig.Singleton.CreateGhostClients;} set {ServerConfig.Singleton.CreateGhostClients = value;}}


[Category("Ghost Clients")]
[Description("Whether or not ghost clients are visible to the user in the form of a console window")]
public static System.Boolean GhostClientConsoleVisible {get{return ServerConfig.Singleton.GhostClientConsoleVisible;} set {ServerConfig.Singleton.GhostClientConsoleVisible = value;}}


[Category("Ghost Clients")]
[Description("How long the master server will wait for a save request to a ghost client before handling it as a failed request")]
public static System.Int32 MaxGhostClientSaveRequestAcknowledgementTimeInSeconds {get{return ServerConfig.Singleton.MaxGhostClientSaveRequestAcknowledgementTimeInSeconds;} set {ServerConfig.Singleton.MaxGhostClientSaveRequestAcknowledgementTimeInSeconds = value;}}


[Category("Ghost Clients")]
[Description("How many times a ghost client can be started in '''GhostClientStartCountResetDurationInSeconds''' before being prevented from starting for '''GhostClientPreventStartDurationInSeconds'''")]
public static System.Int32 GhostClientStartCountThreshold {get{return ServerConfig.Singleton.GhostClientStartCountThreshold;} set {ServerConfig.Singleton.GhostClientStartCountThreshold = value;}}


[Category("Ghost Clients")]
[Description("Server Name")]
public static System.Int32 GhostClientStartCountResetDurationInSeconds {get{return ServerConfig.Singleton.GhostClientStartCountResetDurationInSeconds;} set {ServerConfig.Singleton.GhostClientStartCountResetDurationInSeconds = value;}}


[Category("Ghost Clients")]
[Description("Server Name")]
public static System.Int32 GhostClientPreventStartDurationInSeconds {get{return ServerConfig.Singleton.GhostClientPreventStartDurationInSeconds;} set {ServerConfig.Singleton.GhostClientPreventStartDurationInSeconds = value;}}


[Category("Ghost Clients")]
[Description("Whether to enable the ghost client start count threshold")]
public static System.Boolean GhostClientStartCountThresholdEnabled {get{return ServerConfig.Singleton.GhostClientStartCountThresholdEnabled;} set {ServerConfig.Singleton.GhostClientStartCountThresholdEnabled = value;}}


[Category("Ghost Clients")]
[Description("The interval in which heartbeats are sent to ghost clients")]
public static System.Int32 GhostClientHeartbeatIntervalInSeconds {get{return ServerConfig.Singleton.GhostClientHeartbeatIntervalInSeconds;} set {ServerConfig.Singleton.GhostClientHeartbeatIntervalInSeconds = value;}}


[Category("Ghost Clients")]
[Description("After how long a ghost client should be killed when it doesn't respond to heartbeats")]
public static System.Int32 GhostClientHeartbeatTimeoutInSeconds {get{return ServerConfig.Singleton.GhostClientHeartbeatTimeoutInSeconds;} set {ServerConfig.Singleton.GhostClientHeartbeatTimeoutInSeconds = value;}}


[Category("Ghost Clients")]
[Description("Whether to enable ghost client heartbeats")]
public static System.Boolean GhostClientHeartbeatEnabled {get{return ServerConfig.Singleton.GhostClientHeartbeatEnabled;} set {ServerConfig.Singleton.GhostClientHeartbeatEnabled = value;}}


[Category("Ghost Clients")]
[Description("How long to cache save data received from ghost clients, a high number may result in server hiccups when a player logs in or rifts to another system")]
public static System.Int32 MaxTimeToCacheSystemSaveDataInTicks {get{return ServerConfig.Singleton.MaxTimeToCacheSystemSaveDataInTicks;} set {ServerConfig.Singleton.MaxTimeToCacheSystemSaveDataInTicks = value;}}


[Category("Saving")]
[Description("Time between auto saves in seconds")]
public static System.Single AutoSaveDelay {get{return ServerConfig.Singleton.AutoSaveDelay;} set {ServerConfig.Singleton.AutoSaveDelay = value;}}


[Category("Saving")]
[Description("Time between backups in seconds")]
public static System.Single BackupSaveDelay {get{return ServerConfig.Singleton.BackupSaveDelay;} set {ServerConfig.Singleton.BackupSaveDelay = value;}}


[Category("Saving")]
[Description("Max number of backups before the oldest gets removed")]
public static System.Int32 BackupCount {get{return ServerConfig.Singleton.BackupCount;} set {ServerConfig.Singleton.BackupCount = value;}}


[Category("Saving")]
[Description("Location where backups are saved")]
public static System.String BackupsPath {get{return ServerConfig.Singleton.BackupsPath;} set {ServerConfig.Singleton.BackupsPath = value;}}


[Category("Databases")]
[Description("Location of the database that contains ships and stations shipped with the game")]
public static System.String GameDbPath {get{return ServerConfig.Singleton.GameDbPath;} set {ServerConfig.Singleton.GameDbPath = value;}}


[Category("Databases")]
[Description("Location of the database that contains ships created by the user")]
public static System.String UserDbPath {get{return ServerConfig.Singleton.UserDbPath;} set {ServerConfig.Singleton.UserDbPath = value;}}


[Category("Databases")]
[Description("Location of the database that contains ships created by players on the server")]
public static System.String ServerDbPath {get{return ServerConfig.Singleton.ServerDbPath;} set {ServerConfig.Singleton.ServerDbPath = value;}}


[Category("Databases")]
[Description("Location of the database that contains ships downloaded from the steam workshop")]
public static System.String WorkshopDbPath {get{return ServerConfig.Singleton.WorkshopDbPath;} set {ServerConfig.Singleton.WorkshopDbPath = value;}}


[Category("Respawning")]
[Description("Minimum player respawn cost in u-nits")]
public static System.Int32 MinimumRespawnCost {get{return ServerConfig.Singleton.MinimumRespawnCost;} set {ServerConfig.Singleton.MinimumRespawnCost = value;}}


[Category("Respawning")]
[Description("Maximum player respawn cost in u-nits")]
public static System.Int32 MaximumRespawnCost {get{return ServerConfig.Singleton.MaximumRespawnCost;} set {ServerConfig.Singleton.MaximumRespawnCost = value;}}


[Category("Respawning")]
[Description("Respawn price increase per gigameter from the place of player death to the respawner in u-nits")]
public static System.Single RespawnCostPerGigameter {get{return ServerConfig.Singleton.RespawnCostPerGigameter;} set {ServerConfig.Singleton.RespawnCostPerGigameter = value;}}


[Category("Respawning")]
[Description("Cost of player respawn in another system in u-nits")]
public static System.Int32 OutOfSystemRespawnCost {get{return ServerConfig.Singleton.OutOfSystemRespawnCost;} set {ServerConfig.Singleton.OutOfSystemRespawnCost = value;}}


[Category("Respawning")]
[Description("Consecutive player respawns of more than '''LongRangeRespawnThreshold''' meter are prohibited for '''LongRangeRespawnTimerLength''' seconds")]
public static System.Int32 LongRangeRespawnThreshold {get{return ServerConfig.Singleton.LongRangeRespawnThreshold;} set {ServerConfig.Singleton.LongRangeRespawnThreshold = value;}}


[Category("Auto storage")]
[Description("Ships in safe zones will automatically be stored if they are uninhabited for '''AutoShipStorageInSafeZoneTimerInTicks''' ticks (20 ticks/second, server only ticks if there are players in the system)")]
public static System.Int32 AutoShipStorageInSafeZoneTimerInTicks {get{return ServerConfig.Singleton.AutoShipStorageInSafeZoneTimerInTicks;} set {ServerConfig.Singleton.AutoShipStorageInSafeZoneTimerInTicks = value;}}


[Category("Auto storage")]
[Description("Ships not in safe zones will automatically be stored if they are uninhabited for '''AutoShipStorageInSafeZoneTimerInTicks''' ticks (20 ticks/second, server only ticks if there are players in the system)")]
public static System.Int32 AutoShipStorageNotInSafeZoneTimerInTicks {get{return ServerConfig.Singleton.AutoShipStorageNotInSafeZoneTimerInTicks;} set {ServerConfig.Singleton.AutoShipStorageNotInSafeZoneTimerInTicks = value;}}


[Category("Skrill")]
[Description("Chance out of 1000 for skrill encounters")]
public static System.Int32 SkrillGroupSpawnChanceOutOf1000 {get{return ServerConfig.Singleton.SkrillGroupSpawnChanceOutOf1000;} set {ServerConfig.Singleton.SkrillGroupSpawnChanceOutOf1000 = value;}}


[Category("Skrill")]
[Description("Chance out of 1000 for skrill hunters to spawn in skrill encounters in tier 1 systems and higher")]
public static System.Int32 SkrillHunterSpawnChanceOutOf1000 {get{return ServerConfig.Singleton.SkrillHunterSpawnChanceOutOf1000;} set {ServerConfig.Singleton.SkrillHunterSpawnChanceOutOf1000 = value;}}


[Category("Skrill")]
[Description("Chance out of 1000 for skrill bombers to spawn in skrill encounters in tier 2 systems and higher")]
public static System.Int32 SkrillBomberSpawnChanceOutOf1000 {get{return ServerConfig.Singleton.SkrillBomberSpawnChanceOutOf1000;} set {ServerConfig.Singleton.SkrillBomberSpawnChanceOutOf1000 = value;}}


[Category("Skrill")]
[Description("Chance out of 1000 for skrill disruptors to spawn in skrill encounters in tier 3 systems and higher")]
public static System.Int32 SkrillDisruptorSpawnChanceOutOf1000 {get{return ServerConfig.Singleton.SkrillDisruptorSpawnChanceOutOf1000;} set {ServerConfig.Singleton.SkrillDisruptorSpawnChanceOutOf1000 = value;}}


[Category("Skrill")]
[Description("Minimum amount of skrill grunts to spawn per skrill encounter")]
public static System.Int32 SkrillGruntMinAmountPerGroup {get{return ServerConfig.Singleton.SkrillGruntMinAmountPerGroup;} set {ServerConfig.Singleton.SkrillGruntMinAmountPerGroup = value;}}


[Category("Skrill")]
[Description("Maximum amount of skrill grunts to spawn per skrill encounter")]
public static System.Int32 SkrillGruntBaseMaxAmountPerGroup {get{return ServerConfig.Singleton.SkrillGruntBaseMaxAmountPerGroup;} set {ServerConfig.Singleton.SkrillGruntBaseMaxAmountPerGroup = value;}}


[Category("Skrill")]
[Description("Minimum amount of skrill hunters to spawn per skrill encounter, if that encounter contains hunters")]
public static System.Int32 SkrillHunterMinAmountPerGroup {get{return ServerConfig.Singleton.SkrillHunterMinAmountPerGroup;} set {ServerConfig.Singleton.SkrillHunterMinAmountPerGroup = value;}}


[Category("Skrill")]
[Description("Maximum amount of skrill hunters to spawn per skrill encounter, if that encounter contains hunters (increases based on skrill faction influence)")]
public static System.Int32 SkrillHunterBaseMaxAmountPerGroup {get{return ServerConfig.Singleton.SkrillHunterBaseMaxAmountPerGroup;} set {ServerConfig.Singleton.SkrillHunterBaseMaxAmountPerGroup = value;}}


[Category("Skrill")]
[Description("Minimum amount of skrill bombers to spawn per skrill encounter, if that encounter contains bombers")]
public static System.Int32 SkrillBomberMinAmountPerGroup {get{return ServerConfig.Singleton.SkrillBomberMinAmountPerGroup;} set {ServerConfig.Singleton.SkrillBomberMinAmountPerGroup = value;}}


[Category("Skrill")]
[Description("Maximum amount of skrill bombers to spawn per skrill encounter, if that encounter contains bombers (increases based on skrill faction influence)")]
public static System.Int32 SkrillBomberBaseMaxAmountPerGroup {get{return ServerConfig.Singleton.SkrillBomberBaseMaxAmountPerGroup;} set {ServerConfig.Singleton.SkrillBomberBaseMaxAmountPerGroup = value;}}


[Category("Skrill")]
[Description("Minimum amount of skrill disruptors to spawn per skrill encounter, if that encounter contains disruptors")]
public static System.Int32 SkrillDisruptorMinAmountPerGroup {get{return ServerConfig.Singleton.SkrillDisruptorMinAmountPerGroup;} set {ServerConfig.Singleton.SkrillDisruptorMinAmountPerGroup = value;}}


[Category("Skrill")]
[Description("Maximum amount of skrill disruptors to spawn per skrill encounter, if that encounter contains disruptors (increases based on skrill faction influence)")]
public static System.Int32 SkrillDisruptorBaseMaxAmountPerGroup {get{return ServerConfig.Singleton.SkrillDisruptorBaseMaxAmountPerGroup;} set {ServerConfig.Singleton.SkrillDisruptorBaseMaxAmountPerGroup = value;}}


[Category("NPC Stations")]
[Description("Chance for a GT Ore and Metal Market to spawn in a newly discovered system (from 0 to 1) (only affects new systems)")]
public static System.Single NPCStation_GT_OreAndMetalMarketSpawnChance {get{return ServerConfig.Singleton.NPCStation_GT_OreAndMetalMarketSpawnChance;} set {ServerConfig.Singleton.NPCStation_GT_OreAndMetalMarketSpawnChance = value;}}


[Category("NPC Stations")]
[Description("Chance for a GT Produced Resource Market to spawn in a newly discovered system (from 0 to 1) (only affects new systems)")]
public static System.Single NPCStation_GT_ProducedResourceMarketSpawnChance {get{return ServerConfig.Singleton.NPCStation_GT_ProducedResourceMarketSpawnChance;} set {ServerConfig.Singleton.NPCStation_GT_ProducedResourceMarketSpawnChance = value;}}


[Category("NPC Stations")]
[Description("Chance for a GT Rare Resources Market to spawn in a newly discovered system (from 0 to 1) (only affects new systems)")]
public static System.Single NPCStation_GT_RareResourcesMarketSpawnChance {get{return ServerConfig.Singleton.NPCStation_GT_RareResourcesMarketSpawnChance;} set {ServerConfig.Singleton.NPCStation_GT_RareResourcesMarketSpawnChance = value;}}


[Category("NPC Stations")]
[Description("Chance for a Logicorp Printing station to spawn in a newly discovered system (from 0 to 1) (only affects new systems)")]
public static System.Single NPCStation_Locicorp_LogiCorpPrintingStationSpawnChance {get{return ServerConfig.Singleton.NPCStation_Locicorp_LogiCorpPrintingStationSpawnChance;} set {ServerConfig.Singleton.NPCStation_Locicorp_LogiCorpPrintingStationSpawnChance = value;}}


[Category("NPC Stations")]
[Description("Chance for a S3 Nitrogen Mining Facility to spawn in a newly discovered system (from 0 to 1) (only affects new systems)")]
public static System.Single NPCStation_S3_NitrogenMiningFacilitySpawnChance {get{return ServerConfig.Singleton.NPCStation_S3_NitrogenMiningFacilitySpawnChance;} set {ServerConfig.Singleton.NPCStation_S3_NitrogenMiningFacilitySpawnChance = value;}}


[Category("NPC Stations")]
[Description("Chance for a S3 Ammunition Factory to spawn in a newly discovered system (from 0 to 1) (only affects new systems)")]
public static System.Single NPCStation_S3_AmmunitionFactorySpawnChance {get{return ServerConfig.Singleton.NPCStation_S3_AmmunitionFactorySpawnChance;} set {ServerConfig.Singleton.NPCStation_S3_AmmunitionFactorySpawnChance = value;}}


[Category("NPC Stations")]
[Description("Chance for a S3 Weapon factory to spawn in a newly discovered system (from 0 to 1) (only affects new systems)")]
public static System.Single NPCStation_S3_WeaponFactorySpawnChance {get{return ServerConfig.Singleton.NPCStation_S3_WeaponFactorySpawnChance;} set {ServerConfig.Singleton.NPCStation_S3_WeaponFactorySpawnChance = value;}}


[Category("NPC Stations")]
[Description("Chance for a HSC Mining Station to spawn in a newly discovered system (from 0 to 1) (only affects new systems)")]
public static System.Single NPCStation_HSC_MiningStationSpawnChance {get{return ServerConfig.Singleton.NPCStation_HSC_MiningStationSpawnChance;} set {ServerConfig.Singleton.NPCStation_HSC_MiningStationSpawnChance = value;}}


[Category("NPC Stations")]
[Description("Chance for a HSC Refinery Station to spawn in a newly discovered system (from 0 to 1) (only affects new systems)")]
public static System.Single NPCStation_HSC_RefineryStationSpawnChance {get{return ServerConfig.Singleton.NPCStation_HSC_RefineryStationSpawnChance;} set {ServerConfig.Singleton.NPCStation_HSC_RefineryStationSpawnChance = value;}}


[Category("NPC Stations")]
[Description("Chance for a HSC Production station to spawn in a newly discovered system (from 0 to 1) (only affects new systems)")]
public static System.Single NPCStation_HSC_ProductionStationSpawnChance {get{return ServerConfig.Singleton.NPCStation_HSC_ProductionStationSpawnChance;} set {ServerConfig.Singleton.NPCStation_HSC_ProductionStationSpawnChance = value;}}


[Category("NPC Stations")]
[Description("Chance for a Vaultron XT22 to spawn in a newly discovered system (from 0 to 1) (only affects new systems)")]
public static System.Single NPCStation_VT_VaultronXT22SpawnChance {get{return ServerConfig.Singleton.NPCStation_VT_VaultronXT22SpawnChance;} set {ServerConfig.Singleton.NPCStation_VT_VaultronXT22SpawnChance = value;}}


[Category("NPC Stations")]
[Description("Chance for a DFT Scrap Trader to spawn in a newly discovered system (from 0 to 1) (only affects new systems)")]
public static System.Single NPCStation_DFT_ScrapTraderSpawnChance {get{return ServerConfig.Singleton.NPCStation_DFT_ScrapTraderSpawnChance;} set {ServerConfig.Singleton.NPCStation_DFT_ScrapTraderSpawnChance = value;}}


[Category("NPC Stations")]
[Description("Chance for a DFT Scrap Refining Station to spawn in a newly discovered system (from 0 to 1) (only affects new systems)")]
public static System.Single NPCStation_DFT_ScrapRefiningSpawnChance {get{return ServerConfig.Singleton.NPCStation_DFT_ScrapRefiningSpawnChance;} set {ServerConfig.Singleton.NPCStation_DFT_ScrapRefiningSpawnChance = value;}}


[Category("NPC Stations")]
[Description("Chance for a DFT Black Market to spawn in a newly discovered system (from 0 to 1) (only affects new systems)")]
public static System.Single NPCStation_DFT_BlackMarketSpawnChance {get{return ServerConfig.Singleton.NPCStation_DFT_BlackMarketSpawnChance;} set {ServerConfig.Singleton.NPCStation_DFT_BlackMarketSpawnChance = value;}}


[Category("NPC Stations")]
[Description("Chance for a HydroPEX Rift Station to spawn in a newly discovered system (from 0 to 1) (only affects new systems)")]
public static System.Single NPCStation_HydroPEX_RiftStationSpawnChance {get{return ServerConfig.Singleton.NPCStation_HydroPEX_RiftStationSpawnChance;} set {ServerConfig.Singleton.NPCStation_HydroPEX_RiftStationSpawnChance = value;}}


[Category("Combat CPU")]
[Description("Amount of CPU a small CPU Provider provides")]
public static System.Int32 CpuProviderSmallCpu {get{return ServerConfig.Singleton.CpuProviderSmallCpu;} set {ServerConfig.Singleton.CpuProviderSmallCpu = value;}}


[Category("Combat CPU")]
[Description("Amount of CPU a medium CPU Provider provides")]
public static System.Int32 CpuProviderMediumCpu {get{return ServerConfig.Singleton.CpuProviderMediumCpu;} set {ServerConfig.Singleton.CpuProviderMediumCpu = value;}}


[Category("Combat CPU")]
[Description("Amount of CPU a large CPU Provider provides")]
public static System.Int32 CpuProviderLargeCpu {get{return ServerConfig.Singleton.CpuProviderLargeCpu;} set {ServerConfig.Singleton.CpuProviderLargeCpu = value;}}


[Category("Combat CPU")]
[Description("Amount of CPU a small Armor Generator will use")]
public static System.Int32 CpuCostArmorGenerator {get{return ServerConfig.Singleton.CpuCostArmorGenerator;} set {ServerConfig.Singleton.CpuCostArmorGenerator = value;}}


[Category("Combat CPU")]
[Description("Amount of CPU a medium Armor Generator will use")]
public static System.Int32 CpuCostArmorGeneratorMedium {get{return ServerConfig.Singleton.CpuCostArmorGeneratorMedium;} set {ServerConfig.Singleton.CpuCostArmorGeneratorMedium = value;}}


[Category("Combat CPU")]
[Description("Amount of CPU a large Armor Generator will use")]
public static System.Int32 CpuCostArmorGeneratorLarge {get{return ServerConfig.Singleton.CpuCostArmorGeneratorLarge;} set {ServerConfig.Singleton.CpuCostArmorGeneratorLarge = value;}}


[Category("Combat CPU")]
[Description("Amount of CPU a Hacking Terminal will use")]
public static System.Int32 CpuCostHackingTerminal {get{return ServerConfig.Singleton.CpuCostHackingTerminal;} set {ServerConfig.Singleton.CpuCostHackingTerminal = value;}}


[Category("Combat CPU")]
[Description("Amount of CPU a small Shield Generator will use")]
public static System.Int32 CpuCostShieldGeneratorSmall {get{return ServerConfig.Singleton.CpuCostShieldGeneratorSmall;} set {ServerConfig.Singleton.CpuCostShieldGeneratorSmall = value;}}


[Category("Combat CPU")]
[Description("Amount of CPU a medium Shield Generator will use")]
public static System.Int32 CpuCostShieldGeneratorMedium {get{return ServerConfig.Singleton.CpuCostShieldGeneratorMedium;} set {ServerConfig.Singleton.CpuCostShieldGeneratorMedium = value;}}


[Category("Combat CPU")]
[Description("Amount of CPU a large Shield Generator will use")]
public static System.Int32 CpuCostShieldGeneratorLarge {get{return ServerConfig.Singleton.CpuCostShieldGeneratorLarge;} set {ServerConfig.Singleton.CpuCostShieldGeneratorLarge = value;}}


[Category("Combat CPU")]
[Description("Amount of CPU an Ammo Tank will use")]
public static System.Int32 CpuCostAmmoTank {get{return ServerConfig.Singleton.CpuCostAmmoTank;} set {ServerConfig.Singleton.CpuCostAmmoTank = value;}}


[Category("Combat CPU")]
[Description("Amount of CPU an EMP Generator will use")]
public static System.Int32 CpuCostEmpGenerator {get{return ServerConfig.Singleton.CpuCostEmpGenerator;} set {ServerConfig.Singleton.CpuCostEmpGenerator = value;}}


[Category("Combat CPU")]
[Description("Amount of CPU a Missile Launcher will use")]
public static System.Int32 CpuCostMissileLauncher {get{return ServerConfig.Singleton.CpuCostMissileLauncher;} set {ServerConfig.Singleton.CpuCostMissileLauncher = value;}}


[Category("Combat CPU")]
[Description("Amount of CPU a Mounted Turret will use")]
public static System.Int32 CpuCostMountedTurret {get{return ServerConfig.Singleton.CpuCostMountedTurret;} set {ServerConfig.Singleton.CpuCostMountedTurret = value;}}


[Category("Combat CPU")]
[Description("Amount of CPU an Automated Turret will use")]
public static System.Int32 CpuCostAutomatedTurret {get{return ServerConfig.Singleton.CpuCostAutomatedTurret;} set {ServerConfig.Singleton.CpuCostAutomatedTurret = value;}}


[Category("Combat CPU")]
[Description("Amount of CPU a Flak Cannon will use")]
public static System.Int32 CpuCostFlakCannon {get{return ServerConfig.Singleton.CpuCostFlakCannon;} set {ServerConfig.Singleton.CpuCostFlakCannon = value;}}


[Category("Combat CPU")]
[Description("Amount of CPU a Rail Gun will use")]
public static System.Int32 CpuCostRailGun {get{return ServerConfig.Singleton.CpuCostRailGun;} set {ServerConfig.Singleton.CpuCostRailGun = value;}}


[Category("Combat CPU")]
[Description("Amount of CPU a Heavy Rail Gun will use")]
public static System.Int32 CpuCostHeavyRailGun {get{return ServerConfig.Singleton.CpuCostHeavyRailGun;} set {ServerConfig.Singleton.CpuCostHeavyRailGun = value;}}


[Category("Combat CPU")]
[Description("The fines player receives when they deal damage to a protected entity is multiplied by this number")]
public static System.Int32 CpuCostLaserArch {get{return ServerConfig.Singleton.CpuCostLaserArch;} set {ServerConfig.Singleton.CpuCostLaserArch = value;}}


[Category("Combat CPU")]
[Description("The fines player receives when they are caught with contraband is multiplied by this number")]
public static System.Int32 CpuCostTeslaCoil {get{return ServerConfig.Singleton.CpuCostTeslaCoil;} set {ServerConfig.Singleton.CpuCostTeslaCoil = value;}}


[Category("Combat CPU")]
[Description("The fines player receives when they release gas in a safe zone is multiplied by this number")]
public static System.Int32 CpuCostBombTrap {get{return ServerConfig.Singleton.CpuCostBombTrap;} set {ServerConfig.Singleton.CpuCostBombTrap = value;}}


[Category("Automatic Weapons")]
[Description("Amount the damage output of automated turrets will be multiplied with")]
public static System.Single AutomatedTurretDamageMod {get{return ServerConfig.Singleton.AutomatedTurretDamageMod;} set {ServerConfig.Singleton.AutomatedTurretDamageMod = value;}}


[Category("Automatic Weapons")]
[Description("Amount the fire cooldown of automated turrets will be multiplied with")]
public static System.Single AutomatedTurretBaseAttackCooldownMod {get{return ServerConfig.Singleton.AutomatedTurretBaseAttackCooldownMod;} set {ServerConfig.Singleton.AutomatedTurretBaseAttackCooldownMod = value;}}


[Category("Automatic Weapons")]
[Description("Amount the fire cooldown of automated turrets will be multiplied with per velocity")]
public static System.Single AutomatedTurretAttackCooldownPerVelocityMod {get{return ServerConfig.Singleton.AutomatedTurretAttackCooldownPerVelocityMod;} set {ServerConfig.Singleton.AutomatedTurretAttackCooldownPerVelocityMod = value;}}


[Category("Automatic Weapons")]
[Description("Amount the damage output of automated lasers will be multiplied with")]
public static System.Single AutomatedLaserDamageMod {get{return ServerConfig.Singleton.AutomatedLaserDamageMod;} set {ServerConfig.Singleton.AutomatedLaserDamageMod = value;}}


[Category("Automatic Weapons")]
[Description("Amount the fire cooldown of automated lasers will be multiplied with")]
public static System.Single AutomatedLaserBaseAttackCooldownMod {get{return ServerConfig.Singleton.AutomatedLaserBaseAttackCooldownMod;} set {ServerConfig.Singleton.AutomatedLaserBaseAttackCooldownMod = value;}}


[Category("Automatic Weapons")]
[Description("Amount the fire cooldown of automated lasers will be multiplied with per velocity")]
public static System.Single AutomatedLaserAttackCooldownPerVelocityMod {get{return ServerConfig.Singleton.AutomatedLaserAttackCooldownPerVelocityMod;} set {ServerConfig.Singleton.AutomatedLaserAttackCooldownPerVelocityMod = value;}}


[Category("Drones")]
[Description("Indicates Whether or not mine drones will spawn")]
public static System.Boolean MineDroneSpawningEnabled {get{return ServerConfig.Singleton.MineDroneSpawningEnabled;} set {ServerConfig.Singleton.MineDroneSpawningEnabled = value;}}


[Category("Drones")]
[Description("Indicates Whether or not trade drones will spawn")]
public static System.Boolean TradeDroneSpawningEnabled {get{return ServerConfig.Singleton.TradeDroneSpawningEnabled;} set {ServerConfig.Singleton.TradeDroneSpawningEnabled = value;}}


[Category("Drones")]
[Description("Indicates Whether or not combat drones will spawn")]
public static System.Boolean CombatDroneSpawningEnabled {get{return ServerConfig.Singleton.CombatDroneSpawningEnabled;} set {ServerConfig.Singleton.CombatDroneSpawningEnabled = value;}}


[Category("Drones")]
[Description("Indicates Whether or not pirate drones will spawn")]
public static System.Boolean PirateDroneSpawningEnabled {get{return ServerConfig.Singleton.PirateDroneSpawningEnabled;} set {ServerConfig.Singleton.PirateDroneSpawningEnabled = value;}}


[Category("AFK")]
[Description("The maximum amount of Hardened Armor a ship can have")]
public static System.Int32 HardenedArmorMaxAmount {get{return ServerConfig.Singleton.HardenedArmorMaxAmount;} set {ServerConfig.Singleton.HardenedArmorMaxAmount = value;}}


[Category("AFK")]
[Description("The amount of Hardened Armor a ship will lose per tick once it takes damage (20 ticks/second, server only ticks if there are players in the system)")]
public static System.Int32 HardeneArmorDecaySpeed {get{return ServerConfig.Singleton.HardeneArmorDecaySpeed;} set {ServerConfig.Singleton.HardeneArmorDecaySpeed = value;}}


[Category("AFK")]
[Description("The amount of ticks Hardened Armor will remain offline once it is completely drained (20 ticks/second, server only ticks if there are players in the system)")]
public static System.Int32 HardenedArmorOfflineTimeInTicks {get{return ServerConfig.Singleton.HardenedArmorOfflineTimeInTicks;} set {ServerConfig.Singleton.HardenedArmorOfflineTimeInTicks = value;}}


[Category("Fines")]
[Description("The fines player receives when they deal damage to a protected entity is multiplied by this number")]
public static System.Single DamageFineScale {get{return ServerConfig.Singleton.DamageFineScale;} set {ServerConfig.Singleton.DamageFineScale = value;}}


[Category("Fines")]
[Description("The fines player receives when they are caught with contraband is multiplied by this number")]
public static System.Single ContrabandFineScale {get{return ServerConfig.Singleton.ContrabandFineScale;} set {ServerConfig.Singleton.ContrabandFineScale = value;}}


[Category("Fines")]
[Description("The fines player receives when they release gas in a safe zone is multiplied by this number")]
public static System.Single GasReleaseFineScale {get{return ServerConfig.Singleton.GasReleaseFineScale;} set {ServerConfig.Singleton.GasReleaseFineScale = value;}}


[Category("Fines")]
[Description("The amount of u-nits the player has to pay in order to pay off a loan is multiplied by this number")]
public static System.Single LoanFineScale {get{return ServerConfig.Singleton.LoanFineScale;} set {ServerConfig.Singleton.LoanFineScale = value;}}


[Category("Fines")]
[Description("The fines player receive when they try to hack a protected entity is multiplied by this number")]
public static System.Single HackingFineScale {get{return ServerConfig.Singleton.HackingFineScale;} set {ServerConfig.Singleton.HackingFineScale = value;}}


[Category("Power")]
[Description("The amount of hydrogen consumed by Hydrogen Generators is multiplied by this number")]
public static System.Single HydrogenConsumptionScale {get{return ServerConfig.Singleton.HydrogenConsumptionScale;} set {ServerConfig.Singleton.HydrogenConsumptionScale = value;}}


[Category("Power")]
[Description("The amount of power produced by Solar Panels is multiplied by this number")]
public static System.Single SolarPowerScale {get{return ServerConfig.Singleton.SolarPowerScale;} set {ServerConfig.Singleton.SolarPowerScale = value;}}


[Category("Power")]
[Description("The amount of power produced by Hydrogen Generators is multiplied by this number")]
public static System.Single HydrogenPowerScale {get{return ServerConfig.Singleton.HydrogenPowerScale;} set {ServerConfig.Singleton.HydrogenPowerScale = value;}}


[Category("Power")]
[Description("The amount of power produced by Nuclear Power Plants is multiplied by this number")]
public static System.Single NuclearPowerScale {get{return ServerConfig.Singleton.NuclearPowerScale;} set {ServerConfig.Singleton.NuclearPowerScale = value;}}


[Category("Production")]
[Description("The speed at which Extractors mine asteroids is multiplied by this number")]
public static System.Single ExtractorMineSpeedScale {get{return ServerConfig.Singleton.ExtractorMineSpeedScale;} set {ServerConfig.Singleton.ExtractorMineSpeedScale = value;}}


[Category("Production")]
[Description("The speed at which Refineries refine materials is multiplied by this number")]
public static System.Single RefineryRefineSpeedScale {get{return ServerConfig.Singleton.RefineryRefineSpeedScale;} set {ServerConfig.Singleton.RefineryRefineSpeedScale = value;}}


[Category("Production")]
[Description("The speed at which Assemblers produce products is multiplied by this number")]
public static System.Single AssemblerAssembleSpeedScale {get{return ServerConfig.Singleton.AssemblerAssembleSpeedScale;} set {ServerConfig.Singleton.AssemblerAssembleSpeedScale = value;}}


[Category("Production")]
[Description("The speed at which 3d Printers produce items is multiplied by this number")]
public static System.Single ThreeDPrinterPrintSpeedScale {get{return ServerConfig.Singleton.ThreeDPrinterPrintSpeedScale;} set {ServerConfig.Singleton.ThreeDPrinterPrintSpeedScale = value;}}


[Category("Tools")]
[Description("The time Auto Mine Cartridges can be used in ticks (20 ticks/second, server only ticks if there are players in the system), does not affect tools which already exist.")]
public static System.Int32 AutoMineCartridgeDurabilityInTicks {get{return ServerConfig.Singleton.AutoMineCartridgeDurabilityInTicks;} set {ServerConfig.Singleton.AutoMineCartridgeDurabilityInTicks = value;}}


[Category("Tools")]
[Description("The time Auto Production Cartridges can be used in ticks (20 ticks/second, server only ticks if there are players in the system), does not affect tools which already exist.")]
public static System.Int32 AutoProductionCartridgeDurabilityInTicks {get{return ServerConfig.Singleton.AutoProductionCartridgeDurabilityInTicks;} set {ServerConfig.Singleton.AutoProductionCartridgeDurabilityInTicks = value;}}


[Category("Tools")]
[Description("The time Auto Printing Cartridges can be used in ticks (20 ticks/second, server only ticks if there are players in the system), does not affect tools which already exist.")]
public static System.Int32 AutoPrintingCartridgeDurabilityInTicks {get{return ServerConfig.Singleton.AutoPrintingCartridgeDurabilityInTicks;} set {ServerConfig.Singleton.AutoPrintingCartridgeDurabilityInTicks = value;}}


[Category("Tools")]
[Description("The time Auto Drone Control Cartridges can be used in ticks (20 ticks/second, server only ticks if there are players in the system), does not affect tools which already exist.")]
public static System.Int32 AutoDroneControlCartridgeDurabilityInTicks {get{return ServerConfig.Singleton.AutoDroneControlCartridgeDurabilityInTicks;} set {ServerConfig.Singleton.AutoDroneControlCartridgeDurabilityInTicks = value;}}


[Category("Tools")]
[Description("The time Auto Teleportation Cartridges can be used in ticks (20 ticks/second, server only ticks if there are players in the system), does not affect tools which already exist.")]
public static System.Int32 AutoTeleportationCartridgeDurabilityInTicks {get{return ServerConfig.Singleton.AutoTeleportationCartridgeDurabilityInTicks;} set {ServerConfig.Singleton.AutoTeleportationCartridgeDurabilityInTicks = value;}}


[Category("Tools")]
[Description("The amount of times rift generator cartridges can be used, does not affect tools which already exist.")]
public static System.Int32 RiftGeneratorCartridgeDurabilityInUses {get{return ServerConfig.Singleton.RiftGeneratorCartridgeDurabilityInUses;} set {ServerConfig.Singleton.RiftGeneratorCartridgeDurabilityInUses = value;}}


[Category("Tools")]
[Description("The durability of tier 1 Heat Sinks, does not affect tools which already exist.")]
public static System.Int32 HeatsinkTIDurabilityInTicks {get{return ServerConfig.Singleton.HeatsinkTIDurabilityInTicks;} set {ServerConfig.Singleton.HeatsinkTIDurabilityInTicks = value;}}


[Category("Tools")]
[Description("The durability of tier 2 Heat Sinks, does not affect tools which already exist.")]
public static System.Int32 HeatsinkTIIDurabilityInTicks {get{return ServerConfig.Singleton.HeatsinkTIIDurabilityInTicks;} set {ServerConfig.Singleton.HeatsinkTIIDurabilityInTicks = value;}}


[Category("Tools")]
[Description("The durability of tier 3 Heat Sinks, does not affect tools which already exist.")]
public static System.Int32 HeatsinkTIIIDurabilityInTicks {get{return ServerConfig.Singleton.HeatsinkTIIIDurabilityInTicks;} set {ServerConfig.Singleton.HeatsinkTIIIDurabilityInTicks = value;}}


[Category("Tools")]
[Description("The durability of advanced Heat Sinks, does not affect tools which already exist.")]
public static System.Int32 HeatsinkAdvancedDurabilityInTicks {get{return ServerConfig.Singleton.HeatsinkAdvancedDurabilityInTicks;} set {ServerConfig.Singleton.HeatsinkAdvancedDurabilityInTicks = value;}}


[Category("Tools")]
[Description("The heat capacity of tier 1 Heat Sinks, does not affect tools which already exist.")]
public static System.Int32 HeatsinkTIHeatCapacity {get{return ServerConfig.Singleton.HeatsinkTIHeatCapacity;} set {ServerConfig.Singleton.HeatsinkTIHeatCapacity = value;}}


[Category("Tools")]
[Description("The heat capacity of tier 2 Heat Sinks, does not affect tools which already exist.")]
public static System.Int32 HeatsinkTIIHeatCapacity {get{return ServerConfig.Singleton.HeatsinkTIIHeatCapacity;} set {ServerConfig.Singleton.HeatsinkTIIHeatCapacity = value;}}


[Category("Tools")]
[Description("The heat capacity of tier 3 Heat Sinks, does not affect tools which already exist.")]
public static System.Int32 HeatsinkTIIIHeatCapacity {get{return ServerConfig.Singleton.HeatsinkTIIIHeatCapacity;} set {ServerConfig.Singleton.HeatsinkTIIIHeatCapacity = value;}}


[Category("Tools")]
[Description("The heat capacity of advanced Heat Sinks, does not affect tools which already exist.")]
public static System.Int32 HeatsinkAdvancedHeatCapacity {get{return ServerConfig.Singleton.HeatsinkAdvancedHeatCapacity;} set {ServerConfig.Singleton.HeatsinkAdvancedHeatCapacity = value;}}


[Category("Tools")]
[Description("The time tier 1 Focusing Crystals can be used in ticks (20 ticks/second, server only ticks if there are players in the system), does not affect tools which already exist.")]
public static System.Int32 FocussingCrystalTIDurabilityInTicks {get{return ServerConfig.Singleton.FocussingCrystalTIDurabilityInTicks;} set {ServerConfig.Singleton.FocussingCrystalTIDurabilityInTicks = value;}}


[Category("Tools")]
[Description("The time tier 2 Focusing Crystals can be used in ticks (20 ticks/second, server only ticks if there are players in the system), does not affect tools which already exist.")]
public static System.Int32 FocussingCrystalTIIDurabilityInTicks {get{return ServerConfig.Singleton.FocussingCrystalTIIDurabilityInTicks;} set {ServerConfig.Singleton.FocussingCrystalTIIDurabilityInTicks = value;}}


[Category("Tools")]
[Description("The time tier 3 Focusing Crystals can be used in ticks (20 ticks/second, server only ticks if there are players in the system), does not affect tools which already exist.")]
public static System.Int32 FocussingCrystalTIIIDurabilityInTicks {get{return ServerConfig.Singleton.FocussingCrystalTIIIDurabilityInTicks;} set {ServerConfig.Singleton.FocussingCrystalTIIIDurabilityInTicks = value;}}


[Category("Tools")]
[Description("The amount of slots a tier 0 Vaultron pass will add to the vault. In a crew or fleet vault, this number will be multiplied by the number of players in the crew or fleet")]
public static System.Int32 VaultronPassT0SlotsPerPlayer {get{return ServerConfig.Singleton.VaultronPassT0SlotsPerPlayer;} set {ServerConfig.Singleton.VaultronPassT0SlotsPerPlayer = value;}}


[Category("Tools")]
[Description("The amount of slots a tier 1 Vaultron pass will add to the vault. In a crew or fleet vault, this number will be multiplied by the number of players in the crew or fleet")]
public static System.Int32 VaultronPassTISlotsPerPlayer {get{return ServerConfig.Singleton.VaultronPassTISlotsPerPlayer;} set {ServerConfig.Singleton.VaultronPassTISlotsPerPlayer = value;}}


[Category("Tools")]
[Description("The amount of slots a tier 2 Vaultron pass will add to the vault. In a crew or fleet vault, this number will be multiplied by the number of players in the crew or fleet")]
public static System.Int32 VaultronPassTIISlotsPerPlayer {get{return ServerConfig.Singleton.VaultronPassTIISlotsPerPlayer;} set {ServerConfig.Singleton.VaultronPassTIISlotsPerPlayer = value;}}


[Category("Tools")]
[Description("The amount of slots a tier 3 Vaultron pass will add to the vault. In a crew or fleet vault, this number will be multiplied by the number of players in the crew or fleet")]
public static System.Int32 VaultronPassTIIISlotsPerPlayer {get{return ServerConfig.Singleton.VaultronPassTIIISlotsPerPlayer;} set {ServerConfig.Singleton.VaultronPassTIIISlotsPerPlayer = value;}}


[Category("Tools")]
[Description("The amount of power a T0 portable battery can hold.")]
public static System.Int32 PortableBatteryT0MaxPower {get{return ServerConfig.Singleton.PortableBatteryT0MaxPower;} set {ServerConfig.Singleton.PortableBatteryT0MaxPower = value;}}


[Category("Tools")]
[Description("The amount of power a T0 portable battery can add to its storage per frame.")]
public static System.Int32 PortableBatteryT0ChargeRate {get{return ServerConfig.Singleton.PortableBatteryT0ChargeRate;} set {ServerConfig.Singleton.PortableBatteryT0ChargeRate = value;}}


[Category("Tools")]
[Description("The amount of power a T0 portable battery can discharge per frame.")]
public static System.Int32 PortableBatteryT0DisChargeRate {get{return ServerConfig.Singleton.PortableBatteryT0DisChargeRate;} set {ServerConfig.Singleton.PortableBatteryT0DisChargeRate = value;}}


[Category("Tools")]
[Description("The amount of power a T1 portable battery can hold.")]
public static System.Int32 PortableBatteryT1MaxPower {get{return ServerConfig.Singleton.PortableBatteryT1MaxPower;} set {ServerConfig.Singleton.PortableBatteryT1MaxPower = value;}}


[Category("Tools")]
[Description("The amount of power a T1 portable battery can add to its storage per frame.")]
public static System.Int32 PortableBatteryT1ChargeRate {get{return ServerConfig.Singleton.PortableBatteryT1ChargeRate;} set {ServerConfig.Singleton.PortableBatteryT1ChargeRate = value;}}


[Category("Tools")]
[Description("The amount of power a T1 portable battery can discharge per frame.")]
public static System.Int32 PortableBatteryT1DisChargeRate {get{return ServerConfig.Singleton.PortableBatteryT1DisChargeRate;} set {ServerConfig.Singleton.PortableBatteryT1DisChargeRate = value;}}


[Category("Tools")]
[Description("The amount of power a T2 portable battery can hold.")]
public static System.Int32 PortableBatteryT2MaxPower {get{return ServerConfig.Singleton.PortableBatteryT2MaxPower;} set {ServerConfig.Singleton.PortableBatteryT2MaxPower = value;}}


[Category("Tools")]
[Description("The amount of power a T2 portable battery can add to its storage per frame.")]
public static System.Int32 PortableBatteryT2ChargeRate {get{return ServerConfig.Singleton.PortableBatteryT2ChargeRate;} set {ServerConfig.Singleton.PortableBatteryT2ChargeRate = value;}}


[Category("Tools")]
[Description("The amount of power a T2 portable battery can discharge per frame.")]
public static System.Int32 PortableBatteryT2DisChargeRate {get{return ServerConfig.Singleton.PortableBatteryT2DisChargeRate;} set {ServerConfig.Singleton.PortableBatteryT2DisChargeRate = value;}}


[Category("Tools")]
[Description("The amount of power a T3 portable battery can hold.")]
public static System.Int32 PortableBatteryT3MaxPower {get{return ServerConfig.Singleton.PortableBatteryT3MaxPower;} set {ServerConfig.Singleton.PortableBatteryT3MaxPower = value;}}


[Category("Tools")]
[Description("The amount of power a T3 portable battery can add to its storage per frame.")]
public static System.Int32 PortableBatteryT3ChargeRate {get{return ServerConfig.Singleton.PortableBatteryT3ChargeRate;} set {ServerConfig.Singleton.PortableBatteryT3ChargeRate = value;}}


[Category("Tools")]
[Description("The amount of power a T3 portable battery can discharge per frame.")]
public static System.Int32 PortableBatteryT3DisChargeRate {get{return ServerConfig.Singleton.PortableBatteryT3DisChargeRate;} set {ServerConfig.Singleton.PortableBatteryT3DisChargeRate = value;}}


[Category("Repair")]
[Description("The amount of armor small armor generators will gain per tick when repaired by any type of drone. (20 ticks/second)")]
public static System.Int32 SmallArmorGeneratorExternalRepairSpeedPerTick {get{return ServerConfig.Singleton.SmallArmorGeneratorExternalRepairSpeedPerTick;} set {ServerConfig.Singleton.SmallArmorGeneratorExternalRepairSpeedPerTick = value;}}


[Category("Repair")]
[Description("The amount of armor medium armor generators will gain per tick when repaired by any type of drone. (20 ticks/second)")]
public static System.Int32 MediumArmorGeneratorExternalRepairSpeedPerTick {get{return ServerConfig.Singleton.MediumArmorGeneratorExternalRepairSpeedPerTick;} set {ServerConfig.Singleton.MediumArmorGeneratorExternalRepairSpeedPerTick = value;}}


[Category("Repair")]
[Description("The amount of armor large armor generators will gain per tick when repaired by any type of drone. (20 ticks/second)")]
public static System.Int32 LargeArmorGeneratorExternalRepairSpeedPerTick {get{return ServerConfig.Singleton.LargeArmorGeneratorExternalRepairSpeedPerTick;} set {ServerConfig.Singleton.LargeArmorGeneratorExternalRepairSpeedPerTick = value;}}


[Category("Repair")]
[Description("When repaired by any type of drone, ships will gain anywhere between [MinDefaultArmorRepairExternalRepairSpeedPerTick] and [MaxDefaultArmorRepairExternalRepairSpeedPerTick] per tick based on their size. (20 ticks/second)")]
public static System.Int32 MinDefaultArmorRepairExternalRepairSpeedPerTick {get{return ServerConfig.Singleton.MinDefaultArmorRepairExternalRepairSpeedPerTick;} set {ServerConfig.Singleton.MinDefaultArmorRepairExternalRepairSpeedPerTick = value;}}


[Category("Miscellaneous")]
[Description("Indicates rift cartridges are required to open rifts to systems of tier 1 or higher")]
public static System.Boolean RiftCartridgesEnabled {get{return ServerConfig.Singleton.RiftCartridgesEnabled;} set {ServerConfig.Singleton.RiftCartridgesEnabled = value;}}


[Category("Miscellaneous")]
[Description("The maximum mass the store-o-tron will allow people to store")]
public static System.Int32 ShipStorageLimit {get{return ServerConfig.Singleton.ShipStorageLimit;} set {ServerConfig.Singleton.ShipStorageLimit = value;}}


[Category("Miscellaneous")]
[Description("The maximum mass the private ship storage hangar will allow people to store")]
public static System.Int32 PrivateShipStorageLimit {get{return ServerConfig.Singleton.PrivateShipStorageLimit;} set {ServerConfig.Singleton.PrivateShipStorageLimit = value;}}


[Category("Miscellaneous")]
[Description("The speed of all warp levels is multiplied by this number")]
public static System.Single WarpSpeedScale {get{return ServerConfig.Singleton.WarpSpeedScale;} set {ServerConfig.Singleton.WarpSpeedScale = value;}}


[Category("Miscellaneous")]
[Description("The amount of time (in seconds) all crew members of a crew have to be offline before the crew disbands")]
public static System.Single CrewDisbandTimeInSeconds {get{return ServerConfig.Singleton.CrewDisbandTimeInSeconds;} set {ServerConfig.Singleton.CrewDisbandTimeInSeconds = value;}}


[Category("Miscellaneous")]
[Description("Sets the global trade stats of resources.")]
public static System.Collections.Generic.Dictionary`2[Game.ClientServer.Classes.Economics.ResourceTypes,Game.ClientServer.Classes.Economics.ResourcePriceStats] OverwriteResourcePriceStats {get{return ServerConfig.Singleton.OverwriteResourcePriceStats;} set {ServerConfig.Singleton.OverwriteResourcePriceStats = value;}}


[Category("Miscellaneous")]
[Description("Sets the global trade stats of tools.")]
public static System.Collections.Generic.Dictionary`2[Game.ClientServer.Classes.Economics.ToolTypes,Game.ClientServer.Classes.Economics.ToolPriceStats] OverwriteToolPriceStats {get{return ServerConfig.Singleton.OverwriteToolPriceStats;} set {ServerConfig.Singleton.OverwriteToolPriceStats = value;}}


[Category("Miscellaneous")]
[Description("The amount of damage a ship can have in order for it to be upgraded or salvaged.")]
public static System.Single UpgradeSalvageArmorPerc {get{return ServerConfig.Singleton.UpgradeSalvageArmorPerc;} set {ServerConfig.Singleton.UpgradeSalvageArmorPerc = value;}}


[Category("Miscellaneous")]
[Description("The amount of damage a device needs before it will yield less resources in a salvage or upgrade operation. 0 = never lose resources, 1 = only don't lose resources if it is completely healthy")]
public static System.Single DeviceDamageResourceLossThreshold {get{return ServerConfig.Singleton.DeviceDamageResourceLossThreshold;} set {ServerConfig.Singleton.DeviceDamageResourceLossThreshold = value;}}


[Category("Miscellaneous")]
[Description("The amount of resources a device will yield if it is completely broken in a salvage or upgrade operation. 0 = don't yield any resources, 1 = yield all resources regardless of damage.")]
public static System.Single DeviceDamageResourceLossMinScale {get{return ServerConfig.Singleton.DeviceDamageResourceLossMinScale;} set {ServerConfig.Singleton.DeviceDamageResourceLossMinScale = value;}}


[Category("Miscellaneous")]
[Description("Sets custom settings for specific systems.")]
public static System.Collections.Generic.Dictionary`2[System.String,Game.Universe.SystemSettings] SpecificSystemSettings {get{return ServerConfig.Singleton.SpecificSystemSettings;} set {ServerConfig.Singleton.SpecificSystemSettings = value;}}


[Category("Generation")]
[Description("Indicates how far the universe will generate from a point between the starter systems. 0 = unlimited")]
public static System.Int32 MaxUniverseGeneratorGenerationDist {get{return ServerConfig.Singleton.MaxUniverseGeneratorGenerationDist;} set {ServerConfig.Singleton.MaxUniverseGeneratorGenerationDist = value;}}


[Category("Generation")]
[Description("The random seed for universe generator")]
public static System.UInt32 StartUniverseGeneratorSeed {get{return ServerConfig.Singleton.StartUniverseGeneratorSeed;} set {ServerConfig.Singleton.StartUniverseGeneratorSeed = value;}}


[Category("Generation")]
[Description("The chance a Tier 0 system is protected (PvE). 0 = always PvP, 100 = always PvE")]
public static System.Int32 Tier0ProtectedSystemChance {get{return ServerConfig.Singleton.Tier0ProtectedSystemChance;} set {ServerConfig.Singleton.Tier0ProtectedSystemChance = value;}}


[Category("Generation")]
[Description("The chance a Tier 1 system is protected (PvE). 0 = always PvP, 100 = always PvE")]
public static System.Int32 Tier1ProtectedSystemChance {get{return ServerConfig.Singleton.Tier1ProtectedSystemChance;} set {ServerConfig.Singleton.Tier1ProtectedSystemChance = value;}}


[Category("Generation")]
[Description("The chance a Tier 2 system is protected (PvE). 0 = always PvP, 100 = always PvE")]
public static System.Int32 Tier2ProtectedSystemChance {get{return ServerConfig.Singleton.Tier2ProtectedSystemChance;} set {ServerConfig.Singleton.Tier2ProtectedSystemChance = value;}}


[Category("Generation")]
[Description("The chance a Tier 3 system is protected (PvE). 0 = always PvP, 100 = always PvE")]
public static System.Int32 Tier3ProtectedSystemChance {get{return ServerConfig.Singleton.Tier3ProtectedSystemChance;} set {ServerConfig.Singleton.Tier3ProtectedSystemChance = value;}}


[Category("Welcome message")]
[Description("Indicates whether or not players will be greeted with a welcome popup when they connect to the server")]
public static System.Boolean EnableServerWelcomePopup {get{return ServerConfig.Singleton.EnableServerWelcomePopup;} set {ServerConfig.Singleton.EnableServerWelcomePopup = value;}}


[Category("Welcome message")]
[Description("The title of the welcome popup people will see when they connect to the server")]
public static System.String ServerWelcomePopupMessageTitle {get{return ServerConfig.Singleton.ServerWelcomePopupMessageTitle;} set {ServerConfig.Singleton.ServerWelcomePopupMessageTitle = value;}}


[Category("Welcome message")]
[Description("The body text of the welcome popup people will see when they connect to the server")]
public static System.String ServerWelcomePopupMessageText {get{return ServerConfig.Singleton.ServerWelcomePopupMessageText;} set {ServerConfig.Singleton.ServerWelcomePopupMessageText = value;}}


[Category("Welcome message")]
[Description("The color of the title of the welcome popup people will see when they connect to the server")]
public static System.UInt32[] ServerWelcomePopupMessageTitleColor {get{return ServerConfig.Singleton.ServerWelcomePopupMessageTitleColor;} set {ServerConfig.Singleton.ServerWelcomePopupMessageTitleColor = value;}}


[Category("Desyncs")]
[Description("Setting used by developers to debug desyncs")]
public static System.Boolean DetailedDesyncInfo {get{return ServerConfig.Singleton.DetailedDesyncInfo;} set {ServerConfig.Singleton.DetailedDesyncInfo = value;}}


[Category("Desyncs")]
[Description("Setting used by developers to debug desyncs")]
public static System.Int32 TrackTimeInTicks {get{return ServerConfig.Singleton.TrackTimeInTicks;} set {ServerConfig.Singleton.TrackTimeInTicks = value;}}


[Category("Desyncs")]
[Description("Amount of times the server tries to resync a player before the server reloads the system the desyncing player is in.")]
public static System.Int32 ResyncTriesBeforeServerReload {get{return ServerConfig.Singleton.ResyncTriesBeforeServerReload;} set {ServerConfig.Singleton.ResyncTriesBeforeServerReload = value;}}


[Category("Desyncs")]
[Description("The amount of ticks the server will not reload, even if players are desyncing. (20 ticks/second, server only ticks if there are players in the system)")]
public static System.Int32 TicksAfterSystemLoadForServerReload {get{return ServerConfig.Singleton.TicksAfterSystemLoadForServerReload;} set {ServerConfig.Singleton.TicksAfterSystemLoadForServerReload = value;}}


[Category("Desyncs")]
[Description("Indicates Whether or not players will automatically be reconnected when they desync")]
public static System.Boolean ReconnectCriticalDesyncPlayers {get{return ServerConfig.Singleton.ReconnectCriticalDesyncPlayers;} set {ServerConfig.Singleton.ReconnectCriticalDesyncPlayers = value;}}


[Category("Starter Ships")]
[Description("Resets the starterships to the default values, removing all custom starter ships")]
public static System.Boolean RefreshStarterShipsOnStartup {get{return ServerConfig.Singleton.RefreshStarterShipsOnStartup;} set {ServerConfig.Singleton.RefreshStarterShipsOnStartup = value;}}


[Category("Starter Ships")]
[Description("Specifics of starter ships.")]
public static System.Collections.Generic.Dictionary`2[Game.ClientServer.Classes.FactionTypes,System.Collections.Generic.List`1[Game.ClientServer.Classes.FactionStarterShipDetails]] FactionStarterShips {get{return ServerConfig.Singleton.FactionStarterShips;} set {ServerConfig.Singleton.FactionStarterShips = value;}}


[Category("Derelict Ships")]
[Description("Resets the Derelict ships to the default values, removing all custom ships")]
public static System.Boolean RefreshDerelictShipsOnStartup {get{return ServerConfig.Singleton.RefreshDerelictShipsOnStartup;} set {ServerConfig.Singleton.RefreshDerelictShipsOnStartup = value;}}


[Category("Derelict Ships")]
[Description("Specifics of starter ships.")]
public static System.Collections.Generic.List`1[Game.Configuration.ServerConfig+DerelictShipDetails] DerelictShipStats {get{return ServerConfig.Singleton.DerelictShipStats;} set {ServerConfig.Singleton.DerelictShipStats = value;}}


[Category("Rich Asteroids")]
[Description("The minimum tier a system needs to be for it to spawn rich asteroids")]
public static System.Int32 RichAsteriodMinTier {get{return ServerConfig.Singleton.RichAsteriodMinTier;} set {ServerConfig.Singleton.RichAsteriodMinTier = value;}}


[Category("Rich Asteroids")]
[Description("The minimum amount of time it takes for the first rich asteroid to spawn in ticks (20 ticks/second, server only ticks if there are players in the system)")]
public static System.Int32 RichAsteriodMinTicksForFirstSpawn {get{return ServerConfig.Singleton.RichAsteriodMinTicksForFirstSpawn;} set {ServerConfig.Singleton.RichAsteriodMinTicksForFirstSpawn = value;}}


[Category("Rich Asteroids")]
[Description("The maximum amount of time it takes for the first rich asteroid to spawn in ticks (20 ticks/second, server only ticks if there are players in the system)")]
public static System.Int32 RichAsteriodMaxTicksForFirstSpawn {get{return ServerConfig.Singleton.RichAsteriodMaxTicksForFirstSpawn;} set {ServerConfig.Singleton.RichAsteriodMaxTicksForFirstSpawn = value;}}


[Category("Rich Asteroids")]
[Description("The minimum amount of time it takes for consecutive rich asteroid to spawn in ticks (20 ticks/second, server only ticks if there are players in the system)")]
public static System.Int32 RichAsteriodMinTicksBetweenSpawns {get{return ServerConfig.Singleton.RichAsteriodMinTicksBetweenSpawns;} set {ServerConfig.Singleton.RichAsteriodMinTicksBetweenSpawns = value;}}


[Category("Rich Asteroids")]
[Description("The maximum amount of time it takes for consecutive rich asteroid to spawn in ticks (20 ticks/second, server only ticks if there are players in the system)")]
public static System.Int32 RichAsteriodMaxTicksBetweenSpawns {get{return ServerConfig.Singleton.RichAsteriodMaxTicksBetweenSpawns;} set {ServerConfig.Singleton.RichAsteriodMaxTicksBetweenSpawns = value;}}


[Category("Rich Asteroids")]
[Description("The maximum amount of rich asteroids that can exist in a system simultaneously.")]
public static System.Int32 RichAsteriodMaxAmount {get{return ServerConfig.Singleton.RichAsteriodMaxAmount;} set {ServerConfig.Singleton.RichAsteriodMaxAmount = value;}}

  }
}

