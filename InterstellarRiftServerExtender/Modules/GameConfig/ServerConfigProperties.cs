//gv1.0.0.10
//ev0.0.3.10 Branch: master
// This file was generated with ServerConfigConverter class
// To allow a PropertyGrid to use IRs fields as properties.
using Game.Configuration;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

namespace IRSE.Modules.GameConfig
{
    public class ServerConfigProperties
    {
        private static ServerConfigProperties m_instance; 
        public static ServerConfigProperties Instance => m_instance == null ? m_instance = new ServerConfigProperties() : m_instance;

        public ServerConfigProperties()
        {
        }

        #region Manual Properties

        [Category("Welcome message")]
        [Description("The color of the title of the welcome popup people will see when they connect to the server")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
        public Color ServerWelcomePopupMessageTitleColor
        {
            get
            {
                uint[] color = ServerConfig.Singleton.ServerWelcomePopupMessageTitleColor;
                return Color.FromArgb(0, (int)color[0], (int)color[1], (int)color[2]);
            }
            set
            {
                uint[] color = new uint[] { value.R, value.G, value.B };
                ServerConfig.Singleton.ServerWelcomePopupMessageTitleColor = color;
            }
        }

        [Category("Miscellaneous")]
        [Description("Sets the global trade stats of resources.")]
        public static System.Collections.Generic.Dictionary<Game.ClientServer.Classes.Economics.ResourceTypes, Game.ClientServer.Classes.Economics.ResourcePriceStats> OverwriteResourcePriceStats
        {
            get { return ServerConfig.Singleton.OverwriteResourcePriceStats; }
            set { ServerConfig.Singleton.OverwriteResourcePriceStats = value; }
        }

        [Category("Miscellaneous")]
        [Description("Sets the global trade stats of tools.")]
        public static System.Collections.Generic.Dictionary<Game.ClientServer.Classes.Economics.ToolTypes, Game.ClientServer.Classes.Economics.ToolPriceStats> OverwriteToolPriceStats
        { get { return ServerConfig.Singleton.OverwriteToolPriceStats; } set { ServerConfig.Singleton.OverwriteToolPriceStats = value; } }

        [Category("Miscellaneous")]
        [Description("Sets custom settings for specific systems.")]
        public static System.Collections.Generic.Dictionary<System.String, Game.Universe.SystemSettings> SpecificSystemSettings
        {
            get { return ServerConfig.Singleton.SpecificSystemSettings; }
            set { ServerConfig.Singleton.SpecificSystemSettings = value; }
        }

        [Category("Starter Ships")]
        [Description("Specifics of starter ships.")]
        public static System.Collections.Generic.Dictionary<Game.ClientServer.Classes.FactionTypes, System.Collections.Generic.List<Game.ClientServer.Classes.FactionStarterShipDetails>> FactionStarterShips
        {
            get { return ServerConfig.Singleton.FactionStarterShips; }
            set { ServerConfig.Singleton.FactionStarterShips = value; }
        }

        #endregion Manual Properties

        //---<STARTGEN>---
        [DisplayName("Server Name")]
        [Category("Server Settings")]
        [Description("The name that will advertise the server in the server list")]
        public System.String ServerName
        { get { return ServerConfig.Singleton.ServerName; } set { ServerConfig.Singleton.ServerName = value; } }

        [DisplayName("Message Of The Day")]
        [Category("Server Settings")]
        [Description("The message players will see when they connect to the server")]
        public System.String MessageOfTheDay
        { get { return ServerConfig.Singleton.MessageOfTheDay; } set { ServerConfig.Singleton.MessageOfTheDay = value; } }

        [DisplayName("Port")]
        [Category("Server Settings")]
        [Description("The port used for players to connect to the server")]
        public System.Int32 Port
        { get { return ServerConfig.Singleton.Port; } set { ServerConfig.Singleton.Port = value; } }

        [DisplayName("Flags")]
        [Category("Server Settings")]
        [Description("Specific server settings represented as a bit-field: 1 - creative, 2 - indestructible, 4 - no oxygen life loss, 8 - disable pvp, 16 - disable system events, 32 - ignore personal vault limit, 64 - ignore crew vault limit, 128 - ignore fleet vault limit, 1024 - don't check for positional desyncs, 2048 - don't check for gameplay desyncs")]
        public System.UInt32 Flags
        { get { return ServerConfig.Singleton.Flags; } set { ServerConfig.Singleton.Flags = value; } }

        [DisplayName("Max Players")]
        [Category("Server Settings")]
        [Description("The maximum amount of players that can be in the server")]
        public System.Int32 MaxPlayers
        { get { return ServerConfig.Singleton.MaxPlayers; } set { ServerConfig.Singleton.MaxPlayers = value; } }

        [DisplayName("Galaxy Name")]
        [Category("Server Settings")]
        [Description("The name of the save that will be used or created for the galaxy")]
        public System.String GalaxyName
        { get { return ServerConfig.Singleton.GalaxyName; } set { ServerConfig.Singleton.GalaxyName = value; } }

        [DisplayName("Password")]
        [Category("Server Settings")]
        [Description("The password needed to connect to the server, this cannot be changed in the config file, it has to be set using the setPassword command.")]
        public System.String Password
        { get { return ServerConfig.Singleton.Password; } set { ServerConfig.Singleton.Password = value; } }

        [DisplayName("Announce To Master Server")]
        [Category("Server Settings")]
        [Description("Whether or not the server will appear in the server list")]
        public System.Boolean AnnounceToMasterServer
        { get { return ServerConfig.Singleton.AnnounceToMasterServer; } set { ServerConfig.Singleton.AnnounceToMasterServer = value; } }

        [DisplayName("Max Speedup Time")]
        [Category("Miscellaneous")]
        [Description("How much faster the server might run if it falls behind")]
        public System.Int32 MaxSpeedupTime
        { get { return ServerConfig.Singleton.MaxSpeedupTime; } set { ServerConfig.Singleton.MaxSpeedupTime = value; } }

        [DisplayName("Active Drones In Tile Removal Threshold")]
        [Category("Miscellaneous")]
        [Description("The maximum number of drones at a certain location before the get removed")]
        public System.Int32 ActiveDronesInTileRemovalThreshold
        { get { return ServerConfig.Singleton.ActiveDronesInTileRemovalThreshold; } set { ServerConfig.Singleton.ActiveDronesInTileRemovalThreshold = value; } }

        [DisplayName("Create Ghost Clients")]
        [Category("Ghost Clients")]
        [Description("(DISABLED. IRSE STARTS THEM) Whether or not the server will run separate processes so it can save without stalling.")]
        public System.Boolean CreateGhostClients
        { get { return ServerConfig.Singleton.CreateGhostClients; } set { ServerConfig.Singleton.CreateGhostClients = value; } }

        [DisplayName("Ghost Client Console Visible")]
        [Category("Ghost Clients")]
        [Description("Whether or not ghost clients are visible to the user in the form of a console window")]
        public System.Boolean GhostClientConsoleVisible
        { get { return ServerConfig.Singleton.GhostClientConsoleVisible; } set { ServerConfig.Singleton.GhostClientConsoleVisible = value; } }

        [DisplayName("Max Ghost Client Save Request Acknowledgement Time In Seconds")]
        [Category("Ghost Clients")]
        [Description("How long the master server will wait for a save request to a ghost client before handling it as a failed request")]
        public System.Int32 MaxGhostClientSaveRequestAcknowledgementTimeInSeconds
        { get { return ServerConfig.Singleton.MaxGhostClientSaveRequestAcknowledgementTimeInSeconds; } set { ServerConfig.Singleton.MaxGhostClientSaveRequestAcknowledgementTimeInSeconds = value; } }

        [DisplayName("Ghost Client Start Count Threshold")]
        [Category("Ghost Clients")]
        [Description("How many times a ghost client can be started in '''GhostClientStartCountResetDurationInSeconds''' before being prevented from starting for '''GhostClientPreventStartDurationInSeconds'''")]
        public System.Int32 GhostClientStartCountThreshold
        { get { return ServerConfig.Singleton.GhostClientStartCountThreshold; } set { ServerConfig.Singleton.GhostClientStartCountThreshold = value; } }

        [DisplayName("Ghost Client Start Count Reset Duration In Seconds")]
        [Category("Ghost Clients")]
        [Description("Server Name")]
        public System.Int32 GhostClientStartCountResetDurationInSeconds
        { get { return ServerConfig.Singleton.GhostClientStartCountResetDurationInSeconds; } set { ServerConfig.Singleton.GhostClientStartCountResetDurationInSeconds = value; } }

        [DisplayName("Ghost Client Prevent Start Duration In Seconds")]
        [Category("Ghost Clients")]
        [Description("Server Name")]
        public System.Int32 GhostClientPreventStartDurationInSeconds
        { get { return ServerConfig.Singleton.GhostClientPreventStartDurationInSeconds; } set { ServerConfig.Singleton.GhostClientPreventStartDurationInSeconds = value; } }

        [DisplayName("Ghost Client Start Count Threshold Enabled")]
        [Category("Ghost Clients")]
        [Description("Whether to enable the ghost client start count threshold")]
        public System.Boolean GhostClientStartCountThresholdEnabled
        { get { return ServerConfig.Singleton.GhostClientStartCountThresholdEnabled; } set { ServerConfig.Singleton.GhostClientStartCountThresholdEnabled = value; } }

        [DisplayName("Ghost Client Heartbeat Interval In Seconds")]
        [Category("Ghost Clients")]
        [Description("The interval in which heartbeats are sent to ghost clients")]
        public System.Int32 GhostClientHeartbeatIntervalInSeconds
        { get { return ServerConfig.Singleton.GhostClientHeartbeatIntervalInSeconds; } set { ServerConfig.Singleton.GhostClientHeartbeatIntervalInSeconds = value; } }

        [DisplayName("Ghost Client Heartbeat Timeout In Seconds")]
        [Category("Ghost Clients")]
        [Description("After how long a ghost client should be killed when it doesn't respond to heartbeats")]
        public System.Int32 GhostClientHeartbeatTimeoutInSeconds
        { get { return ServerConfig.Singleton.GhostClientHeartbeatTimeoutInSeconds; } set { ServerConfig.Singleton.GhostClientHeartbeatTimeoutInSeconds = value; } }

        [DisplayName("Ghost Client Heartbeat Enabled")]
        [Category("Ghost Clients")]
        [Description("Whether to enable ghost client heartbeats")]
        public System.Boolean GhostClientHeartbeatEnabled
        { get { return ServerConfig.Singleton.GhostClientHeartbeatEnabled; } set { ServerConfig.Singleton.GhostClientHeartbeatEnabled = value; } }

        [DisplayName("Max Time To Cache System Save Data In Ticks")]
        [Category("Ghost Clients")]
        [Description("How long to cache save data received from ghost clients, a high number may result in server hiccups when a player logs in or rifts to another system")]
        public System.Int32 MaxTimeToCacheSystemSaveDataInTicks
        { get { return ServerConfig.Singleton.MaxTimeToCacheSystemSaveDataInTicks; } set { ServerConfig.Singleton.MaxTimeToCacheSystemSaveDataInTicks = value; } }

        [DisplayName("Auto Save Delay")]
        [Category("Saving")]
        [Description("Time between auto saves in seconds")]
        public System.Single AutoSaveDelay
        { get { return ServerConfig.Singleton.AutoSaveDelay; } set { ServerConfig.Singleton.AutoSaveDelay = value; } }

        [DisplayName("Backup Save Delay")]
        [Category("Saving")]
        [Description("Time between backups in seconds")]
        public System.Single BackupSaveDelay
        { get { return ServerConfig.Singleton.BackupSaveDelay; } set { ServerConfig.Singleton.BackupSaveDelay = value; } }

        [DisplayName("Backup Count")]
        [Category("Saving")]
        [Description("Max number of backups before the oldest gets removed")]
        public System.Int32 BackupCount
        { get { return ServerConfig.Singleton.BackupCount; } set { ServerConfig.Singleton.BackupCount = value; } }

        [DisplayName("Backups Path")]
        [Category("Saving")]
        [Description("Location where backups are saved")]
        public System.String BackupsPath
        { get { return ServerConfig.Singleton.BackupsPath; } set { ServerConfig.Singleton.BackupsPath = value; } }

        [DisplayName("Game Db Path")]
        [Category("Databases")]
        [Description("Location of the database that contains ships and stations shipped with the game")]
        public System.String GameDbPath
        { get { return ServerConfig.Singleton.GameDbPath; } set { ServerConfig.Singleton.GameDbPath = value; } }

        [DisplayName("User Db Path")]
        [Category("Databases")]
        [Description("Location of the database that contains ships created by the user")]
        public System.String UserDbPath
        { get { return ServerConfig.Singleton.UserDbPath; } set { ServerConfig.Singleton.UserDbPath = value; } }

        [DisplayName("Server Db Path")]
        [Category("Databases")]
        [Description("Location of the database that contains ships created by players on the server")]
        public System.String ServerDbPath
        { get { return ServerConfig.Singleton.ServerDbPath; } set { ServerConfig.Singleton.ServerDbPath = value; } }

        [DisplayName("Workshop Db Path")]
        [Category("Databases")]
        [Description("Location of the database that contains ships downloaded from the steam workshop")]
        public System.String WorkshopDbPath
        { get { return ServerConfig.Singleton.WorkshopDbPath; } set { ServerConfig.Singleton.WorkshopDbPath = value; } }

        [DisplayName("Minimum Respawn Cost")]
        [Category("Respawning")]
        [Description("Minimum player respawn cost in u-nits")]
        public System.Int32 MinimumRespawnCost
        { get { return ServerConfig.Singleton.MinimumRespawnCost; } set { ServerConfig.Singleton.MinimumRespawnCost = value; } }

        [DisplayName("Maximum Respawn Cost")]
        [Category("Respawning")]
        [Description("Maximum player respawn cost in u-nits")]
        public System.Int32 MaximumRespawnCost
        { get { return ServerConfig.Singleton.MaximumRespawnCost; } set { ServerConfig.Singleton.MaximumRespawnCost = value; } }

        [DisplayName("Respawn Cost Per Gigameter")]
        [Category("Respawning")]
        [Description("Respawn price increase per gigameter from the place of player death to the respawner in u-nits")]
        public System.Single RespawnCostPerGigameter
        { get { return ServerConfig.Singleton.RespawnCostPerGigameter; } set { ServerConfig.Singleton.RespawnCostPerGigameter = value; } }

        [DisplayName("Out Of System Respawn Cost")]
        [Category("Respawning")]
        [Description("Cost of player respawn in another system in u-nits")]
        public System.Int32 OutOfSystemRespawnCost
        { get { return ServerConfig.Singleton.OutOfSystemRespawnCost; } set { ServerConfig.Singleton.OutOfSystemRespawnCost = value; } }

        [DisplayName("Long Range Respawn Threshold")]
        [Category("Respawning")]
        [Description("Consecutive player respawns of more than '''LongRangeRespawnThreshold''' meter are prohibited for '''LongRangeRespawnTimerLength''' seconds")]
        public System.Int32 LongRangeRespawnThreshold
        { get { return ServerConfig.Singleton.LongRangeRespawnThreshold; } set { ServerConfig.Singleton.LongRangeRespawnThreshold = value; } }

        [DisplayName("Auto Ship Storage In Safe Zone Timer In Ticks")]
        [Category("Auto storage")]
        [Description("Ships in safe zones will automatically be stored if they are uninhabited for '''AutoShipStorageInSafeZoneTimerInTicks''' ticks (20 ticks/second, server only ticks if there are players in the system)")]
        public System.Int32 AutoShipStorageInSafeZoneTimerInTicks
        { get { return ServerConfig.Singleton.AutoShipStorageInSafeZoneTimerInTicks; } set { ServerConfig.Singleton.AutoShipStorageInSafeZoneTimerInTicks = value; } }

        [DisplayName("Auto Ship Storage Not In Safe Zone Timer In Ticks")]
        [Category("Auto storage")]
        [Description("Ships not in safe zones will automatically be stored if they are uninhabited for '''AutoShipStorageInSafeZoneTimerInTicks''' ticks (20 ticks/second, server only ticks if there are players in the system)")]
        public System.Int32 AutoShipStorageNotInSafeZoneTimerInTicks
        { get { return ServerConfig.Singleton.AutoShipStorageNotInSafeZoneTimerInTicks; } set { ServerConfig.Singleton.AutoShipStorageNotInSafeZoneTimerInTicks = value; } }

        [DisplayName("Skrill Group Spawn Chance Out Of1000")]
        [Category("Skrill")]
        [Description("Chance out of 1000 for skrill encounters")]
        public System.Int32 SkrillGroupSpawnChanceOutOf1000
        { get { return ServerConfig.Singleton.SkrillGroupSpawnChanceOutOf1000; } set { ServerConfig.Singleton.SkrillGroupSpawnChanceOutOf1000 = value; } }

        [DisplayName("Skrill Hunter Spawn Chance Out Of1000")]
        [Category("Skrill")]
        [Description("Chance out of 1000 for skrill hunters to spawn in skrill encounters in tier 1 systems and higher")]
        public System.Int32 SkrillHunterSpawnChanceOutOf1000
        { get { return ServerConfig.Singleton.SkrillHunterSpawnChanceOutOf1000; } set { ServerConfig.Singleton.SkrillHunterSpawnChanceOutOf1000 = value; } }

        [DisplayName("Skrill Bomber Spawn Chance Out Of1000")]
        [Category("Skrill")]
        [Description("Chance out of 1000 for skrill bombers to spawn in skrill encounters in tier 2 systems and higher")]
        public System.Int32 SkrillBomberSpawnChanceOutOf1000
        { get { return ServerConfig.Singleton.SkrillBomberSpawnChanceOutOf1000; } set { ServerConfig.Singleton.SkrillBomberSpawnChanceOutOf1000 = value; } }

        [DisplayName("Skrill Disruptor Spawn Chance Out Of1000")]
        [Category("Skrill")]
        [Description("Chance out of 1000 for skrill disruptors to spawn in skrill encounters in tier 3 systems and higher")]
        public System.Int32 SkrillDisruptorSpawnChanceOutOf1000
        { get { return ServerConfig.Singleton.SkrillDisruptorSpawnChanceOutOf1000; } set { ServerConfig.Singleton.SkrillDisruptorSpawnChanceOutOf1000 = value; } }

        [DisplayName("Skrill Grunt Min Amount Per Group")]
        [Category("Skrill")]
        [Description("Minimum amount of skrill grunts to spawn per skrill encounter")]
        public System.Int32 SkrillGruntMinAmountPerGroup
        { get { return ServerConfig.Singleton.SkrillGruntMinAmountPerGroup; } set { ServerConfig.Singleton.SkrillGruntMinAmountPerGroup = value; } }

        [DisplayName("Skrill Grunt Base Max Amount Per Group")]
        [Category("Skrill")]
        [Description("Maximum amount of skrill grunts to spawn per skrill encounter")]
        public System.Int32 SkrillGruntBaseMaxAmountPerGroup
        { get { return ServerConfig.Singleton.SkrillGruntBaseMaxAmountPerGroup; } set { ServerConfig.Singleton.SkrillGruntBaseMaxAmountPerGroup = value; } }

        [DisplayName("Skrill Hunter Min Amount Per Group")]
        [Category("Skrill")]
        [Description("Minimum amount of skrill hunters to spawn per skrill encounter, if that encounter contains hunters")]
        public System.Int32 SkrillHunterMinAmountPerGroup
        { get { return ServerConfig.Singleton.SkrillHunterMinAmountPerGroup; } set { ServerConfig.Singleton.SkrillHunterMinAmountPerGroup = value; } }

        [DisplayName("Skrill Hunter Base Max Amount Per Group")]
        [Category("Skrill")]
        [Description("Maximum amount of skrill hunters to spawn per skrill encounter, if that encounter contains hunters (increases based on skrill faction influence)")]
        public System.Int32 SkrillHunterBaseMaxAmountPerGroup
        { get { return ServerConfig.Singleton.SkrillHunterBaseMaxAmountPerGroup; } set { ServerConfig.Singleton.SkrillHunterBaseMaxAmountPerGroup = value; } }

        [DisplayName("Skrill Bomber Min Amount Per Group")]
        [Category("Skrill")]
        [Description("Minimum amount of skrill bombers to spawn per skrill encounter, if that encounter contains bombers")]
        public System.Int32 SkrillBomberMinAmountPerGroup
        { get { return ServerConfig.Singleton.SkrillBomberMinAmountPerGroup; } set { ServerConfig.Singleton.SkrillBomberMinAmountPerGroup = value; } }

        [DisplayName("Skrill Bomber Base Max Amount Per Group")]
        [Category("Skrill")]
        [Description("Maximum amount of skrill bombers to spawn per skrill encounter, if that encounter contains bombers (increases based on skrill faction influence)")]
        public System.Int32 SkrillBomberBaseMaxAmountPerGroup
        { get { return ServerConfig.Singleton.SkrillBomberBaseMaxAmountPerGroup; } set { ServerConfig.Singleton.SkrillBomberBaseMaxAmountPerGroup = value; } }

        [DisplayName("Skrill Disruptor Min Amount Per Group")]
        [Category("Skrill")]
        [Description("Minimum amount of skrill disruptors to spawn per skrill encounter, if that encounter contains disruptors")]
        public System.Int32 SkrillDisruptorMinAmountPerGroup
        { get { return ServerConfig.Singleton.SkrillDisruptorMinAmountPerGroup; } set { ServerConfig.Singleton.SkrillDisruptorMinAmountPerGroup = value; } }

        [DisplayName("Skrill Disruptor Base Max Amount Per Group")]
        [Category("Skrill")]
        [Description("Maximum amount of skrill disruptors to spawn per skrill encounter, if that encounter contains disruptors (increases based on skrill faction influence)")]
        public System.Int32 SkrillDisruptorBaseMaxAmountPerGroup
        { get { return ServerConfig.Singleton.SkrillDisruptorBaseMaxAmountPerGroup; } set { ServerConfig.Singleton.SkrillDisruptorBaseMaxAmountPerGroup = value; } }

        [DisplayName("NPC Station_ G T_ Ore And Metal Market Spawn Chance")]
        [Category("NPC Stations")]
        [Description("Chance for a GT Ore and Metal Market to spawn in a newly discovered system (from 0 to 1) (only affects new systems)")]
        public System.Single NPCStation_GT_OreAndMetalMarketSpawnChance
        { get { return ServerConfig.Singleton.NPCStation_GT_OreAndMetalMarketSpawnChance; } set { ServerConfig.Singleton.NPCStation_GT_OreAndMetalMarketSpawnChance = value; } }

        [DisplayName("NPC Station_ G T_ Produced Resource Market Spawn Chance")]
        [Category("NPC Stations")]
        [Description("Chance for a GT Produced Resource Market to spawn in a newly discovered system (from 0 to 1) (only affects new systems)")]
        public System.Single NPCStation_GT_ProducedResourceMarketSpawnChance
        { get { return ServerConfig.Singleton.NPCStation_GT_ProducedResourceMarketSpawnChance; } set { ServerConfig.Singleton.NPCStation_GT_ProducedResourceMarketSpawnChance = value; } }

        [DisplayName("NPC Station_ G T_ Rare Resources Market Spawn Chance")]
        [Category("NPC Stations")]
        [Description("Chance for a GT Rare Resources Market to spawn in a newly discovered system (from 0 to 1) (only affects new systems)")]
        public System.Single NPCStation_GT_RareResourcesMarketSpawnChance
        { get { return ServerConfig.Singleton.NPCStation_GT_RareResourcesMarketSpawnChance; } set { ServerConfig.Singleton.NPCStation_GT_RareResourcesMarketSpawnChance = value; } }

        [DisplayName("NPC Station_ Locicorp_ Logi Corp Printing Station Spawn Chance")]
        [Category("NPC Stations")]
        [Description("Chance for a Logicorp Printing station to spawn in a newly discovered system (from 0 to 1) (only affects new systems)")]
        public System.Single NPCStation_Locicorp_LogiCorpPrintingStationSpawnChance
        { get { return ServerConfig.Singleton.NPCStation_Locicorp_LogiCorpPrintingStationSpawnChance; } set { ServerConfig.Singleton.NPCStation_Locicorp_LogiCorpPrintingStationSpawnChance = value; } }

        [DisplayName("NPC Station_ S3_ Nitrogen Mining Facility Spawn Chance")]
        [Category("NPC Stations")]
        [Description("Chance for a S3 Nitrogen Mining Facility to spawn in a newly discovered system (from 0 to 1) (only affects new systems)")]
        public System.Single NPCStation_S3_NitrogenMiningFacilitySpawnChance
        { get { return ServerConfig.Singleton.NPCStation_S3_NitrogenMiningFacilitySpawnChance; } set { ServerConfig.Singleton.NPCStation_S3_NitrogenMiningFacilitySpawnChance = value; } }

        [DisplayName("NPC Station_ S3_ Ammunition Factory Spawn Chance")]
        [Category("NPC Stations")]
        [Description("Chance for a S3 Ammunition Factory to spawn in a newly discovered system (from 0 to 1) (only affects new systems)")]
        public System.Single NPCStation_S3_AmmunitionFactorySpawnChance
        { get { return ServerConfig.Singleton.NPCStation_S3_AmmunitionFactorySpawnChance; } set { ServerConfig.Singleton.NPCStation_S3_AmmunitionFactorySpawnChance = value; } }

        [DisplayName("NPC Station_ S3_ Weapon Factory Spawn Chance")]
        [Category("NPC Stations")]
        [Description("Chance for a S3 Weapon factory to spawn in a newly discovered system (from 0 to 1) (only affects new systems)")]
        public System.Single NPCStation_S3_WeaponFactorySpawnChance
        { get { return ServerConfig.Singleton.NPCStation_S3_WeaponFactorySpawnChance; } set { ServerConfig.Singleton.NPCStation_S3_WeaponFactorySpawnChance = value; } }

        [DisplayName("NPC Station_ HS C_ Mining Station Spawn Chance")]
        [Category("NPC Stations")]
        [Description("Chance for a HSC Mining Station to spawn in a newly discovered system (from 0 to 1) (only affects new systems)")]
        public System.Single NPCStation_HSC_MiningStationSpawnChance
        { get { return ServerConfig.Singleton.NPCStation_HSC_MiningStationSpawnChance; } set { ServerConfig.Singleton.NPCStation_HSC_MiningStationSpawnChance = value; } }

        [DisplayName("NPC Station_ HS C_ Refinery Station Spawn Chance")]
        [Category("NPC Stations")]
        [Description("Chance for a HSC Refinery Station to spawn in a newly discovered system (from 0 to 1) (only affects new systems)")]
        public System.Single NPCStation_HSC_RefineryStationSpawnChance
        { get { return ServerConfig.Singleton.NPCStation_HSC_RefineryStationSpawnChance; } set { ServerConfig.Singleton.NPCStation_HSC_RefineryStationSpawnChance = value; } }

        [DisplayName("NPC Station_ HS C_ Production Station Spawn Chance")]
        [Category("NPC Stations")]
        [Description("Chance for a HSC Production station to spawn in a newly discovered system (from 0 to 1) (only affects new systems)")]
        public System.Single NPCStation_HSC_ProductionStationSpawnChance
        { get { return ServerConfig.Singleton.NPCStation_HSC_ProductionStationSpawnChance; } set { ServerConfig.Singleton.NPCStation_HSC_ProductionStationSpawnChance = value; } }

        [DisplayName("NPC Station_ V T_ Vaultron X T22 Spawn Chance")]
        [Category("NPC Stations")]
        [Description("Chance for a Vaultron XT22 to spawn in a newly discovered system (from 0 to 1) (only affects new systems)")]
        public System.Single NPCStation_VT_VaultronXT22SpawnChance
        { get { return ServerConfig.Singleton.NPCStation_VT_VaultronXT22SpawnChance; } set { ServerConfig.Singleton.NPCStation_VT_VaultronXT22SpawnChance = value; } }

        [DisplayName("NPC Station_ DF T_ Scrap Trader Spawn Chance")]
        [Category("NPC Stations")]
        [Description("Chance for a DFT Scrap Trader to spawn in a newly discovered system (from 0 to 1) (only affects new systems)")]
        public System.Single NPCStation_DFT_ScrapTraderSpawnChance
        { get { return ServerConfig.Singleton.NPCStation_DFT_ScrapTraderSpawnChance; } set { ServerConfig.Singleton.NPCStation_DFT_ScrapTraderSpawnChance = value; } }

        [DisplayName("NPC Station_ DF T_ Scrap Refining Spawn Chance")]
        [Category("NPC Stations")]
        [Description("Chance for a DFT Scrap Refining Station to spawn in a newly discovered system (from 0 to 1) (only affects new systems)")]
        public System.Single NPCStation_DFT_ScrapRefiningSpawnChance
        { get { return ServerConfig.Singleton.NPCStation_DFT_ScrapRefiningSpawnChance; } set { ServerConfig.Singleton.NPCStation_DFT_ScrapRefiningSpawnChance = value; } }

        [DisplayName("NPC Station_ DF T_ Black Market Spawn Chance")]
        [Category("NPC Stations")]
        [Description("Chance for a DFT Black Market to spawn in a newly discovered system (from 0 to 1) (only affects new systems)")]
        public System.Single NPCStation_DFT_BlackMarketSpawnChance
        { get { return ServerConfig.Singleton.NPCStation_DFT_BlackMarketSpawnChance; } set { ServerConfig.Singleton.NPCStation_DFT_BlackMarketSpawnChance = value; } }

        [DisplayName("NPC Station_ Hydro PE X_ Rift Station Spawn Chance")]
        [Category("NPC Stations")]
        [Description("Chance for a HydroPEX Rift Station to spawn in a newly discovered system (from 0 to 1) (only affects new systems)")]
        public System.Single NPCStation_HydroPEX_RiftStationSpawnChance
        { get { return ServerConfig.Singleton.NPCStation_HydroPEX_RiftStationSpawnChance; } set { ServerConfig.Singleton.NPCStation_HydroPEX_RiftStationSpawnChance = value; } }

        [DisplayName("Cpu Provider Small Cpu")]
        [Category("Combat CPU")]
        [Description("Amount of CPU a small CPU Provider provides")]
        public System.Int32 CpuProviderSmallCpu
        { get { return ServerConfig.Singleton.CpuProviderSmallCpu; } set { ServerConfig.Singleton.CpuProviderSmallCpu = value; } }

        [DisplayName("Cpu Provider Medium Cpu")]
        [Category("Combat CPU")]
        [Description("Amount of CPU a medium CPU Provider provides")]
        public System.Int32 CpuProviderMediumCpu
        { get { return ServerConfig.Singleton.CpuProviderMediumCpu; } set { ServerConfig.Singleton.CpuProviderMediumCpu = value; } }

        [DisplayName("Cpu Provider Large Cpu")]
        [Category("Combat CPU")]
        [Description("Amount of CPU a large CPU Provider provides")]
        public System.Int32 CpuProviderLargeCpu
        { get { return ServerConfig.Singleton.CpuProviderLargeCpu; } set { ServerConfig.Singleton.CpuProviderLargeCpu = value; } }

        [DisplayName("Cpu Cost Armor Generator")]
        [Category("Combat CPU")]
        [Description("Amount of CPU a small Armor Generator will use")]
        public System.Int32 CpuCostArmorGenerator
        { get { return ServerConfig.Singleton.CpuCostArmorGenerator; } set { ServerConfig.Singleton.CpuCostArmorGenerator = value; } }

        [DisplayName("Cpu Cost Armor Generator Medium")]
        [Category("Combat CPU")]
        [Description("Amount of CPU a medium Armor Generator will use")]
        public System.Int32 CpuCostArmorGeneratorMedium
        { get { return ServerConfig.Singleton.CpuCostArmorGeneratorMedium; } set { ServerConfig.Singleton.CpuCostArmorGeneratorMedium = value; } }

        [DisplayName("Cpu Cost Armor Generator Large")]
        [Category("Combat CPU")]
        [Description("Amount of CPU a large Armor Generator will use")]
        public System.Int32 CpuCostArmorGeneratorLarge
        { get { return ServerConfig.Singleton.CpuCostArmorGeneratorLarge; } set { ServerConfig.Singleton.CpuCostArmorGeneratorLarge = value; } }

        [DisplayName("Cpu Cost Hacking Terminal")]
        [Category("Combat CPU")]
        [Description("Amount of CPU a Hacking Terminal will use")]
        public System.Int32 CpuCostHackingTerminal
        { get { return ServerConfig.Singleton.CpuCostHackingTerminal; } set { ServerConfig.Singleton.CpuCostHackingTerminal = value; } }

        [DisplayName("Cpu Cost Shield Generator Small")]
        [Category("Combat CPU")]
        [Description("Amount of CPU a small Shield Generator will use")]
        public System.Int32 CpuCostShieldGeneratorSmall
        { get { return ServerConfig.Singleton.CpuCostShieldGeneratorSmall; } set { ServerConfig.Singleton.CpuCostShieldGeneratorSmall = value; } }

        [DisplayName("Cpu Cost Shield Generator Medium")]
        [Category("Combat CPU")]
        [Description("Amount of CPU a medium Shield Generator will use")]
        public System.Int32 CpuCostShieldGeneratorMedium
        { get { return ServerConfig.Singleton.CpuCostShieldGeneratorMedium; } set { ServerConfig.Singleton.CpuCostShieldGeneratorMedium = value; } }

        [DisplayName("Cpu Cost Shield Generator Large")]
        [Category("Combat CPU")]
        [Description("Amount of CPU a large Shield Generator will use")]
        public System.Int32 CpuCostShieldGeneratorLarge
        { get { return ServerConfig.Singleton.CpuCostShieldGeneratorLarge; } set { ServerConfig.Singleton.CpuCostShieldGeneratorLarge = value; } }

        [DisplayName("Cpu Cost Ammo Tank")]
        [Category("Combat CPU")]
        [Description("Amount of CPU an Ammo Tank will use")]
        public System.Int32 CpuCostAmmoTank
        { get { return ServerConfig.Singleton.CpuCostAmmoTank; } set { ServerConfig.Singleton.CpuCostAmmoTank = value; } }

        [DisplayName("Cpu Cost Emp Generator")]
        [Category("Combat CPU")]
        [Description("Amount of CPU an EMP Generator will use")]
        public System.Int32 CpuCostEmpGenerator
        { get { return ServerConfig.Singleton.CpuCostEmpGenerator; } set { ServerConfig.Singleton.CpuCostEmpGenerator = value; } }

        [DisplayName("Cpu Cost Missile Launcher")]
        [Category("Combat CPU")]
        [Description("Amount of CPU a Missile Launcher will use")]
        public System.Int32 CpuCostMissileLauncher
        { get { return ServerConfig.Singleton.CpuCostMissileLauncher; } set { ServerConfig.Singleton.CpuCostMissileLauncher = value; } }

        [DisplayName("Cpu Cost Mounted Turret")]
        [Category("Combat CPU")]
        [Description("Amount of CPU a Mounted Turret will use")]
        public System.Int32 CpuCostMountedTurret
        { get { return ServerConfig.Singleton.CpuCostMountedTurret; } set { ServerConfig.Singleton.CpuCostMountedTurret = value; } }

        [DisplayName("Cpu Cost Automated Turret")]
        [Category("Combat CPU")]
        [Description("Amount of CPU an Automated Turret will use")]
        public System.Int32 CpuCostAutomatedTurret
        { get { return ServerConfig.Singleton.CpuCostAutomatedTurret; } set { ServerConfig.Singleton.CpuCostAutomatedTurret = value; } }

        [DisplayName("Cpu Cost Flak Cannon")]
        [Category("Combat CPU")]
        [Description("Amount of CPU a Flak Cannon will use")]
        public System.Int32 CpuCostFlakCannon
        { get { return ServerConfig.Singleton.CpuCostFlakCannon; } set { ServerConfig.Singleton.CpuCostFlakCannon = value; } }

        [DisplayName("Cpu Cost Rail Gun")]
        [Category("Combat CPU")]
        [Description("Amount of CPU a Rail Gun will use")]
        public System.Int32 CpuCostRailGun
        { get { return ServerConfig.Singleton.CpuCostRailGun; } set { ServerConfig.Singleton.CpuCostRailGun = value; } }

        [DisplayName("Cpu Cost Heavy Rail Gun")]
        [Category("Combat CPU")]
        [Description("Amount of CPU a Heavy Rail Gun will use")]
        public System.Int32 CpuCostHeavyRailGun
        { get { return ServerConfig.Singleton.CpuCostHeavyRailGun; } set { ServerConfig.Singleton.CpuCostHeavyRailGun = value; } }

        [DisplayName("Cpu Cost Laser Arch")]
        [Category("Combat CPU")]
        [Description("The fines player receives when they deal damage to a protected entity is multiplied by this number")]
        public System.Int32 CpuCostLaserArch
        { get { return ServerConfig.Singleton.CpuCostLaserArch; } set { ServerConfig.Singleton.CpuCostLaserArch = value; } }

        [DisplayName("Cpu Cost Tesla Coil")]
        [Category("Combat CPU")]
        [Description("The fines player receives when they are caught with contraband is multiplied by this number")]
        public System.Int32 CpuCostTeslaCoil
        { get { return ServerConfig.Singleton.CpuCostTeslaCoil; } set { ServerConfig.Singleton.CpuCostTeslaCoil = value; } }

        [DisplayName("Cpu Cost Bomb Trap")]
        [Category("Combat CPU")]
        [Description("The fines player receives when they release gas in a safe zone is multiplied by this number")]
        public System.Int32 CpuCostBombTrap
        { get { return ServerConfig.Singleton.CpuCostBombTrap; } set { ServerConfig.Singleton.CpuCostBombTrap = value; } }

        [DisplayName("Automated Turret Damage Mod")]
        [Category("Automatic Weapons")]
        [Description("Amount the damage output of automated turrets will be multiplied with")]
        public System.Single AutomatedTurretDamageMod
        { get { return ServerConfig.Singleton.AutomatedTurretDamageMod; } set { ServerConfig.Singleton.AutomatedTurretDamageMod = value; } }

        [DisplayName("Automated Turret Base Attack Cooldown Mod")]
        [Category("Automatic Weapons")]
        [Description("Amount the fire cooldown of automated turrets will be multiplied with")]
        public System.Single AutomatedTurretBaseAttackCooldownMod
        { get { return ServerConfig.Singleton.AutomatedTurretBaseAttackCooldownMod; } set { ServerConfig.Singleton.AutomatedTurretBaseAttackCooldownMod = value; } }

        [DisplayName("Automated Turret Attack Cooldown Per Velocity Mod")]
        [Category("Automatic Weapons")]
        [Description("Amount the fire cooldown of automated turrets will be multiplied with per velocity")]
        public System.Single AutomatedTurretAttackCooldownPerVelocityMod
        { get { return ServerConfig.Singleton.AutomatedTurretAttackCooldownPerVelocityMod; } set { ServerConfig.Singleton.AutomatedTurretAttackCooldownPerVelocityMod = value; } }

        [DisplayName("Automated Laser Damage Mod")]
        [Category("Automatic Weapons")]
        [Description("Amount the damage output of automated lasers will be multiplied with")]
        public System.Single AutomatedLaserDamageMod
        { get { return ServerConfig.Singleton.AutomatedLaserDamageMod; } set { ServerConfig.Singleton.AutomatedLaserDamageMod = value; } }

        [DisplayName("Automated Laser Base Attack Cooldown Mod")]
        [Category("Automatic Weapons")]
        [Description("Amount the fire cooldown of automated lasers will be multiplied with")]
        public System.Single AutomatedLaserBaseAttackCooldownMod
        { get { return ServerConfig.Singleton.AutomatedLaserBaseAttackCooldownMod; } set { ServerConfig.Singleton.AutomatedLaserBaseAttackCooldownMod = value; } }

        [DisplayName("Automated Laser Attack Cooldown Per Velocity Mod")]
        [Category("Automatic Weapons")]
        [Description("Amount the fire cooldown of automated lasers will be multiplied with per velocity")]
        public System.Single AutomatedLaserAttackCooldownPerVelocityMod
        { get { return ServerConfig.Singleton.AutomatedLaserAttackCooldownPerVelocityMod; } set { ServerConfig.Singleton.AutomatedLaserAttackCooldownPerVelocityMod = value; } }

        [DisplayName("Mine Drone Spawning Enabled")]
        [Category("Drones")]
        [Description("Indicates Whether or not mine drones will spawn")]
        public System.Boolean MineDroneSpawningEnabled
        { get { return ServerConfig.Singleton.MineDroneSpawningEnabled; } set { ServerConfig.Singleton.MineDroneSpawningEnabled = value; } }

        [DisplayName("Trade Drone Spawning Enabled")]
        [Category("Drones")]
        [Description("Indicates Whether or not trade drones will spawn")]
        public System.Boolean TradeDroneSpawningEnabled
        { get { return ServerConfig.Singleton.TradeDroneSpawningEnabled; } set { ServerConfig.Singleton.TradeDroneSpawningEnabled = value; } }

        [DisplayName("Combat Drone Spawning Enabled")]
        [Category("Drones")]
        [Description("Indicates Whether or not combat drones will spawn")]
        public System.Boolean CombatDroneSpawningEnabled
        { get { return ServerConfig.Singleton.CombatDroneSpawningEnabled; } set { ServerConfig.Singleton.CombatDroneSpawningEnabled = value; } }

        [DisplayName("Pirate Drone Spawning Enabled")]
        [Category("Drones")]
        [Description("Indicates Whether or not pirate drones will spawn")]
        public System.Boolean PirateDroneSpawningEnabled
        { get { return ServerConfig.Singleton.PirateDroneSpawningEnabled; } set { ServerConfig.Singleton.PirateDroneSpawningEnabled = value; } }

        [DisplayName("Hardened Armor Max Amount")]
        [Category("AFK")]
        [Description("The maximum amount of Hardened Armor a ship can have")]
        public System.Int32 HardenedArmorMaxAmount
        { get { return ServerConfig.Singleton.HardenedArmorMaxAmount; } set { ServerConfig.Singleton.HardenedArmorMaxAmount = value; } }

        [DisplayName("Hardene Armor Decay Speed")]
        [Category("AFK")]
        [Description("The amount of Hardened Armor a ship will lose per tick once it takes damage (20 ticks/second, server only ticks if there are players in the system)")]
        public System.Int32 HardeneArmorDecaySpeed
        { get { return ServerConfig.Singleton.HardeneArmorDecaySpeed; } set { ServerConfig.Singleton.HardeneArmorDecaySpeed = value; } }

        [DisplayName("Hardened Armor Offline Time In Ticks")]
        [Category("AFK")]
        [Description("The amount of ticks Hardened Armor will remain offline once it is completely drained (20 ticks/second, server only ticks if there are players in the system)")]
        public System.Int32 HardenedArmorOfflineTimeInTicks
        { get { return ServerConfig.Singleton.HardenedArmorOfflineTimeInTicks; } set { ServerConfig.Singleton.HardenedArmorOfflineTimeInTicks = value; } }

        [DisplayName("Damage Fine Scale")]
        [Category("Fines")]
        [Description("The fines player receives when they deal damage to a protected entity is multiplied by this number")]
        public System.Single DamageFineScale
        { get { return ServerConfig.Singleton.DamageFineScale; } set { ServerConfig.Singleton.DamageFineScale = value; } }

        [DisplayName("Contraband Fine Scale")]
        [Category("Fines")]
        [Description("The fines player receives when they are caught with contraband is multiplied by this number")]
        public System.Single ContrabandFineScale
        { get { return ServerConfig.Singleton.ContrabandFineScale; } set { ServerConfig.Singleton.ContrabandFineScale = value; } }

        [DisplayName("Gas Release Fine Scale")]
        [Category("Fines")]
        [Description("The fines player receives when they release gas in a safe zone is multiplied by this number")]
        public System.Single GasReleaseFineScale
        { get { return ServerConfig.Singleton.GasReleaseFineScale; } set { ServerConfig.Singleton.GasReleaseFineScale = value; } }

        [DisplayName("Loan Fine Scale")]
        [Category("Fines")]
        [Description("The amount of u-nits the player has to pay in order to pay off a loan is multiplied by this number")]
        public System.Single LoanFineScale
        { get { return ServerConfig.Singleton.LoanFineScale; } set { ServerConfig.Singleton.LoanFineScale = value; } }

        [DisplayName("Hacking Fine Scale")]
        [Category("Fines")]
        [Description("The fines player receive when they try to hack a protected entity is multiplied by this number")]
        public System.Single HackingFineScale
        { get { return ServerConfig.Singleton.HackingFineScale; } set { ServerConfig.Singleton.HackingFineScale = value; } }

        [DisplayName("Hydrogen Consumption Scale")]
        [Category("Power")]
        [Description("The amount of hydrogen consumed by Hydrogen Generators is multiplied by this number")]
        public System.Single HydrogenConsumptionScale
        { get { return ServerConfig.Singleton.HydrogenConsumptionScale; } set { ServerConfig.Singleton.HydrogenConsumptionScale = value; } }

        [DisplayName("Solar Power Scale")]
        [Category("Power")]
        [Description("The amount of power produced by Solar Panels is multiplied by this number")]
        public System.Single SolarPowerScale
        { get { return ServerConfig.Singleton.SolarPowerScale; } set { ServerConfig.Singleton.SolarPowerScale = value; } }

        [DisplayName("Hydrogen Power Scale")]
        [Category("Power")]
        [Description("The amount of power produced by Hydrogen Generators is multiplied by this number")]
        public System.Single HydrogenPowerScale
        { get { return ServerConfig.Singleton.HydrogenPowerScale; } set { ServerConfig.Singleton.HydrogenPowerScale = value; } }

        [DisplayName("Nuclear Power Scale")]
        [Category("Power")]
        [Description("The amount of power produced by Nuclear Power Plants is multiplied by this number")]
        public System.Single NuclearPowerScale
        { get { return ServerConfig.Singleton.NuclearPowerScale; } set { ServerConfig.Singleton.NuclearPowerScale = value; } }

        [DisplayName("Extractor Mine Speed Scale")]
        [Category("Production")]
        [Description("The speed at which Extractors mine asteroids is multiplied by this number")]
        public System.Single ExtractorMineSpeedScale
        { get { return ServerConfig.Singleton.ExtractorMineSpeedScale; } set { ServerConfig.Singleton.ExtractorMineSpeedScale = value; } }

        [DisplayName("Refinery Refine Speed Scale")]
        [Category("Production")]
        [Description("The speed at which Refineries refine materials is multiplied by this number")]
        public System.Single RefineryRefineSpeedScale
        { get { return ServerConfig.Singleton.RefineryRefineSpeedScale; } set { ServerConfig.Singleton.RefineryRefineSpeedScale = value; } }

        [DisplayName("Assembler Assemble Speed Scale")]
        [Category("Production")]
        [Description("The speed at which Assemblers produce products is multiplied by this number")]
        public System.Single AssemblerAssembleSpeedScale
        { get { return ServerConfig.Singleton.AssemblerAssembleSpeedScale; } set { ServerConfig.Singleton.AssemblerAssembleSpeedScale = value; } }

        [DisplayName("Three D Printer Print Speed Scale")]
        [Category("Production")]
        [Description("The speed at which 3d Printers produce items is multiplied by this number")]
        public System.Single ThreeDPrinterPrintSpeedScale
        { get { return ServerConfig.Singleton.ThreeDPrinterPrintSpeedScale; } set { ServerConfig.Singleton.ThreeDPrinterPrintSpeedScale = value; } }

        [DisplayName("Auto Mine Cartridge Durability In Ticks")]
        [Category("Tools")]
        [Description("The time Auto Mine Cartridges can be used in ticks (20 ticks/second, server only ticks if there are players in the system), does not affect tools which already exist.")]
        public System.Int32 AutoMineCartridgeDurabilityInTicks
        { get { return ServerConfig.Singleton.AutoMineCartridgeDurabilityInTicks; } set { ServerConfig.Singleton.AutoMineCartridgeDurabilityInTicks = value; } }

        [DisplayName("Auto Production Cartridge Durability In Ticks")]
        [Category("Tools")]
        [Description("The time Auto Production Cartridges can be used in ticks (20 ticks/second, server only ticks if there are players in the system), does not affect tools which already exist.")]
        public System.Int32 AutoProductionCartridgeDurabilityInTicks
        { get { return ServerConfig.Singleton.AutoProductionCartridgeDurabilityInTicks; } set { ServerConfig.Singleton.AutoProductionCartridgeDurabilityInTicks = value; } }

        [DisplayName("Auto Printing Cartridge Durability In Ticks")]
        [Category("Tools")]
        [Description("The time Auto Printing Cartridges can be used in ticks (20 ticks/second, server only ticks if there are players in the system), does not affect tools which already exist.")]
        public System.Int32 AutoPrintingCartridgeDurabilityInTicks
        { get { return ServerConfig.Singleton.AutoPrintingCartridgeDurabilityInTicks; } set { ServerConfig.Singleton.AutoPrintingCartridgeDurabilityInTicks = value; } }

        [DisplayName("Auto Drone Control Cartridge Durability In Ticks")]
        [Category("Tools")]
        [Description("The time Auto Drone Control Cartridges can be used in ticks (20 ticks/second, server only ticks if there are players in the system), does not affect tools which already exist.")]
        public System.Int32 AutoDroneControlCartridgeDurabilityInTicks
        { get { return ServerConfig.Singleton.AutoDroneControlCartridgeDurabilityInTicks; } set { ServerConfig.Singleton.AutoDroneControlCartridgeDurabilityInTicks = value; } }

        [DisplayName("Auto Teleportation Cartridge Durability In Ticks")]
        [Category("Tools")]
        [Description("The time Auto Teleportation Cartridges can be used in ticks (20 ticks/second, server only ticks if there are players in the system), does not affect tools which already exist.")]
        public System.Int32 AutoTeleportationCartridgeDurabilityInTicks
        { get { return ServerConfig.Singleton.AutoTeleportationCartridgeDurabilityInTicks; } set { ServerConfig.Singleton.AutoTeleportationCartridgeDurabilityInTicks = value; } }

        [DisplayName("Rift Generator Cartridge Durability In Uses")]
        [Category("Tools")]
        [Description("The amount of times rift generator cartridges can be used, does not affect tools which already exist.")]
        public System.Int32 RiftGeneratorCartridgeDurabilityInUses
        { get { return ServerConfig.Singleton.RiftGeneratorCartridgeDurabilityInUses; } set { ServerConfig.Singleton.RiftGeneratorCartridgeDurabilityInUses = value; } }

        [DisplayName("Heatsink TI Durability In Ticks")]
        [Category("Tools")]
        [Description("The durability of tier 1 Heat Sinks, does not affect tools which already exist.")]
        public System.Int32 HeatsinkTIDurabilityInTicks
        { get { return ServerConfig.Singleton.HeatsinkTIDurabilityInTicks; } set { ServerConfig.Singleton.HeatsinkTIDurabilityInTicks = value; } }

        [DisplayName("Heatsink TII Durability In Ticks")]
        [Category("Tools")]
        [Description("The durability of tier 2 Heat Sinks, does not affect tools which already exist.")]
        public System.Int32 HeatsinkTIIDurabilityInTicks
        { get { return ServerConfig.Singleton.HeatsinkTIIDurabilityInTicks; } set { ServerConfig.Singleton.HeatsinkTIIDurabilityInTicks = value; } }

        [DisplayName("Heatsink TIII Durability In Ticks")]
        [Category("Tools")]
        [Description("The durability of tier 3 Heat Sinks, does not affect tools which already exist.")]
        public System.Int32 HeatsinkTIIIDurabilityInTicks
        { get { return ServerConfig.Singleton.HeatsinkTIIIDurabilityInTicks; } set { ServerConfig.Singleton.HeatsinkTIIIDurabilityInTicks = value; } }

        [DisplayName("Heatsink Advanced Durability In Ticks")]
        [Category("Tools")]
        [Description("The durability of advanced Heat Sinks, does not affect tools which already exist.")]
        public System.Int32 HeatsinkAdvancedDurabilityInTicks
        { get { return ServerConfig.Singleton.HeatsinkAdvancedDurabilityInTicks; } set { ServerConfig.Singleton.HeatsinkAdvancedDurabilityInTicks = value; } }

        [DisplayName("Heatsink TI Heat Capacity")]
        [Category("Tools")]
        [Description("The heat capacity of tier 1 Heat Sinks, does not affect tools which already exist.")]
        public System.Int32 HeatsinkTIHeatCapacity
        { get { return ServerConfig.Singleton.HeatsinkTIHeatCapacity; } set { ServerConfig.Singleton.HeatsinkTIHeatCapacity = value; } }

        [DisplayName("Heatsink TII Heat Capacity")]
        [Category("Tools")]
        [Description("The heat capacity of tier 2 Heat Sinks, does not affect tools which already exist.")]
        public System.Int32 HeatsinkTIIHeatCapacity
        { get { return ServerConfig.Singleton.HeatsinkTIIHeatCapacity; } set { ServerConfig.Singleton.HeatsinkTIIHeatCapacity = value; } }

        [DisplayName("Heatsink TIII Heat Capacity")]
        [Category("Tools")]
        [Description("The heat capacity of tier 3 Heat Sinks, does not affect tools which already exist.")]
        public System.Int32 HeatsinkTIIIHeatCapacity
        { get { return ServerConfig.Singleton.HeatsinkTIIIHeatCapacity; } set { ServerConfig.Singleton.HeatsinkTIIIHeatCapacity = value; } }

        [DisplayName("Heatsink Advanced Heat Capacity")]
        [Category("Tools")]
        [Description("The heat capacity of advanced Heat Sinks, does not affect tools which already exist.")]
        public System.Int32 HeatsinkAdvancedHeatCapacity
        { get { return ServerConfig.Singleton.HeatsinkAdvancedHeatCapacity; } set { ServerConfig.Singleton.HeatsinkAdvancedHeatCapacity = value; } }

        [DisplayName("Focussing Crystal TI Durability In Ticks")]
        [Category("Tools")]
        [Description("The time tier 1 Focusing Crystals can be used in ticks (20 ticks/second, server only ticks if there are players in the system), does not affect tools which already exist.")]
        public System.Int32 FocussingCrystalTIDurabilityInTicks
        { get { return ServerConfig.Singleton.FocussingCrystalTIDurabilityInTicks; } set { ServerConfig.Singleton.FocussingCrystalTIDurabilityInTicks = value; } }

        [DisplayName("Focussing Crystal TII Durability In Ticks")]
        [Category("Tools")]
        [Description("The time tier 2 Focusing Crystals can be used in ticks (20 ticks/second, server only ticks if there are players in the system), does not affect tools which already exist.")]
        public System.Int32 FocussingCrystalTIIDurabilityInTicks
        { get { return ServerConfig.Singleton.FocussingCrystalTIIDurabilityInTicks; } set { ServerConfig.Singleton.FocussingCrystalTIIDurabilityInTicks = value; } }

        [DisplayName("Focussing Crystal TIII Durability In Ticks")]
        [Category("Tools")]
        [Description("The time tier 3 Focusing Crystals can be used in ticks (20 ticks/second, server only ticks if there are players in the system), does not affect tools which already exist.")]
        public System.Int32 FocussingCrystalTIIIDurabilityInTicks
        { get { return ServerConfig.Singleton.FocussingCrystalTIIIDurabilityInTicks; } set { ServerConfig.Singleton.FocussingCrystalTIIIDurabilityInTicks = value; } }

        [DisplayName("Vaultron Pass T0 Slots Per Player")]
        [Category("Tools")]
        [Description("The amount of slots a tier 0 Vaultron pass will add to the vault. In a crew or fleet vault, this number will be multiplied by the number of players in the crew or fleet")]
        public System.Int32 VaultronPassT0SlotsPerPlayer
        { get { return ServerConfig.Singleton.VaultronPassT0SlotsPerPlayer; } set { ServerConfig.Singleton.VaultronPassT0SlotsPerPlayer = value; } }

        [DisplayName("Vaultron Pass TI Slots Per Player")]
        [Category("Tools")]
        [Description("The amount of slots a tier 1 Vaultron pass will add to the vault. In a crew or fleet vault, this number will be multiplied by the number of players in the crew or fleet")]
        public System.Int32 VaultronPassTISlotsPerPlayer
        { get { return ServerConfig.Singleton.VaultronPassTISlotsPerPlayer; } set { ServerConfig.Singleton.VaultronPassTISlotsPerPlayer = value; } }

        [DisplayName("Vaultron Pass TII Slots Per Player")]
        [Category("Tools")]
        [Description("The amount of slots a tier 2 Vaultron pass will add to the vault. In a crew or fleet vault, this number will be multiplied by the number of players in the crew or fleet")]
        public System.Int32 VaultronPassTIISlotsPerPlayer
        { get { return ServerConfig.Singleton.VaultronPassTIISlotsPerPlayer; } set { ServerConfig.Singleton.VaultronPassTIISlotsPerPlayer = value; } }

        [DisplayName("Vaultron Pass TIII Slots Per Player")]
        [Category("Tools")]
        [Description("The amount of slots a tier 3 Vaultron pass will add to the vault. In a crew or fleet vault, this number will be multiplied by the number of players in the crew or fleet")]
        public System.Int32 VaultronPassTIIISlotsPerPlayer
        { get { return ServerConfig.Singleton.VaultronPassTIIISlotsPerPlayer; } set { ServerConfig.Singleton.VaultronPassTIIISlotsPerPlayer = value; } }

        [DisplayName("Portable Battery T0 Max Power")]
        [Category("Tools")]
        [Description("The amount of power a T0 portable battery can hold.")]
        public System.Int32 PortableBatteryT0MaxPower
        { get { return ServerConfig.Singleton.PortableBatteryT0MaxPower; } set { ServerConfig.Singleton.PortableBatteryT0MaxPower = value; } }

        [DisplayName("Portable Battery T0 Charge Rate")]
        [Category("Tools")]
        [Description("The amount of power a T0 portable battery can add to its storage per frame.")]
        public System.Int32 PortableBatteryT0ChargeRate
        { get { return ServerConfig.Singleton.PortableBatteryT0ChargeRate; } set { ServerConfig.Singleton.PortableBatteryT0ChargeRate = value; } }

        [DisplayName("Portable Battery T0 Dis Charge Rate")]
        [Category("Tools")]
        [Description("The amount of power a T0 portable battery can discharge per frame.")]
        public System.Int32 PortableBatteryT0DisChargeRate
        { get { return ServerConfig.Singleton.PortableBatteryT0DisChargeRate; } set { ServerConfig.Singleton.PortableBatteryT0DisChargeRate = value; } }

        [DisplayName("Portable Battery T1 Max Power")]
        [Category("Tools")]
        [Description("The amount of power a T1 portable battery can hold.")]
        public System.Int32 PortableBatteryT1MaxPower
        { get { return ServerConfig.Singleton.PortableBatteryT1MaxPower; } set { ServerConfig.Singleton.PortableBatteryT1MaxPower = value; } }

        [DisplayName("Portable Battery T1 Charge Rate")]
        [Category("Tools")]
        [Description("The amount of power a T1 portable battery can add to its storage per frame.")]
        public System.Int32 PortableBatteryT1ChargeRate
        { get { return ServerConfig.Singleton.PortableBatteryT1ChargeRate; } set { ServerConfig.Singleton.PortableBatteryT1ChargeRate = value; } }

        [DisplayName("Portable Battery T1 Dis Charge Rate")]
        [Category("Tools")]
        [Description("The amount of power a T1 portable battery can discharge per frame.")]
        public System.Int32 PortableBatteryT1DisChargeRate
        { get { return ServerConfig.Singleton.PortableBatteryT1DisChargeRate; } set { ServerConfig.Singleton.PortableBatteryT1DisChargeRate = value; } }

        [DisplayName("Portable Battery T2 Max Power")]
        [Category("Tools")]
        [Description("The amount of power a T2 portable battery can hold.")]
        public System.Int32 PortableBatteryT2MaxPower
        { get { return ServerConfig.Singleton.PortableBatteryT2MaxPower; } set { ServerConfig.Singleton.PortableBatteryT2MaxPower = value; } }

        [DisplayName("Portable Battery T2 Charge Rate")]
        [Category("Tools")]
        [Description("The amount of power a T2 portable battery can add to its storage per frame.")]
        public System.Int32 PortableBatteryT2ChargeRate
        { get { return ServerConfig.Singleton.PortableBatteryT2ChargeRate; } set { ServerConfig.Singleton.PortableBatteryT2ChargeRate = value; } }

        [DisplayName("Portable Battery T2 Dis Charge Rate")]
        [Category("Tools")]
        [Description("The amount of power a T2 portable battery can discharge per frame.")]
        public System.Int32 PortableBatteryT2DisChargeRate
        { get { return ServerConfig.Singleton.PortableBatteryT2DisChargeRate; } set { ServerConfig.Singleton.PortableBatteryT2DisChargeRate = value; } }

        [DisplayName("Portable Battery T3 Max Power")]
        [Category("Tools")]
        [Description("The amount of power a T3 portable battery can hold.")]
        public System.Int32 PortableBatteryT3MaxPower
        { get { return ServerConfig.Singleton.PortableBatteryT3MaxPower; } set { ServerConfig.Singleton.PortableBatteryT3MaxPower = value; } }

        [DisplayName("Portable Battery T3 Charge Rate")]
        [Category("Tools")]
        [Description("The amount of power a T3 portable battery can add to its storage per frame.")]
        public System.Int32 PortableBatteryT3ChargeRate
        { get { return ServerConfig.Singleton.PortableBatteryT3ChargeRate; } set { ServerConfig.Singleton.PortableBatteryT3ChargeRate = value; } }

        [DisplayName("Portable Battery T3 Dis Charge Rate")]
        [Category("Tools")]
        [Description("The amount of power a T3 portable battery can discharge per frame.")]
        public System.Int32 PortableBatteryT3DisChargeRate
        { get { return ServerConfig.Singleton.PortableBatteryT3DisChargeRate; } set { ServerConfig.Singleton.PortableBatteryT3DisChargeRate = value; } }

        [DisplayName("Small Armor Generator External Repair Speed Per Tick")]
        [Category("Repair")]
        [Description("The amount of armor small armor generators will gain per tick when repaired by any type of drone. (20 ticks/second)")]
        public System.Int32 SmallArmorGeneratorExternalRepairSpeedPerTick
        { get { return ServerConfig.Singleton.SmallArmorGeneratorExternalRepairSpeedPerTick; } set { ServerConfig.Singleton.SmallArmorGeneratorExternalRepairSpeedPerTick = value; } }

        [DisplayName("Medium Armor Generator External Repair Speed Per Tick")]
        [Category("Repair")]
        [Description("The amount of armor medium armor generators will gain per tick when repaired by any type of drone. (20 ticks/second)")]
        public System.Int32 MediumArmorGeneratorExternalRepairSpeedPerTick
        { get { return ServerConfig.Singleton.MediumArmorGeneratorExternalRepairSpeedPerTick; } set { ServerConfig.Singleton.MediumArmorGeneratorExternalRepairSpeedPerTick = value; } }

        [DisplayName("Large Armor Generator External Repair Speed Per Tick")]
        [Category("Repair")]
        [Description("The amount of armor large armor generators will gain per tick when repaired by any type of drone. (20 ticks/second)")]
        public System.Int32 LargeArmorGeneratorExternalRepairSpeedPerTick
        { get { return ServerConfig.Singleton.LargeArmorGeneratorExternalRepairSpeedPerTick; } set { ServerConfig.Singleton.LargeArmorGeneratorExternalRepairSpeedPerTick = value; } }

        [DisplayName("Min Default Armor Repair External Repair Speed Per Tick")]
        [Category("Repair")]
        [Description("When repaired by any type of drone, ships will gain anywhere between [MinDefaultArmorRepairExternalRepairSpeedPerTick] and [MaxDefaultArmorRepairExternalRepairSpeedPerTick] per tick based on their size. (20 ticks/second)")]
        public System.Int32 MinDefaultArmorRepairExternalRepairSpeedPerTick
        { get { return ServerConfig.Singleton.MinDefaultArmorRepairExternalRepairSpeedPerTick; } set { ServerConfig.Singleton.MinDefaultArmorRepairExternalRepairSpeedPerTick = value; } }

        [DisplayName("Rift Cartridges Enabled")]
        [Category("Miscellaneous")]
        [Description("Indicates rift cartridges are required to open rifts to systems of tier 1 or higher")]
        public System.Boolean RiftCartridgesEnabled
        { get { return ServerConfig.Singleton.RiftCartridgesEnabled; } set { ServerConfig.Singleton.RiftCartridgesEnabled = value; } }

        [DisplayName("Ship Storage Limit")]
        [Category("Miscellaneous")]
        [Description("The maximum mass the store-o-tron will allow people to store")]
        public System.Int32 ShipStorageLimit
        { get { return ServerConfig.Singleton.ShipStorageLimit; } set { ServerConfig.Singleton.ShipStorageLimit = value; } }

        [DisplayName("Private Ship Storage Limit")]
        [Category("Miscellaneous")]
        [Description("The maximum mass the private ship storage hangar will allow people to store")]
        public System.Int32 PrivateShipStorageLimit
        { get { return ServerConfig.Singleton.PrivateShipStorageLimit; } set { ServerConfig.Singleton.PrivateShipStorageLimit = value; } }

        [DisplayName("Warp Speed Scale")]
        [Category("Miscellaneous")]
        [Description("The speed of all warp levels is multiplied by this number")]
        public System.Single WarpSpeedScale
        { get { return ServerConfig.Singleton.WarpSpeedScale; } set { ServerConfig.Singleton.WarpSpeedScale = value; } }

        [DisplayName("Crew Disband Time In Seconds")]
        [Category("Miscellaneous")]
        [Description("The amount of time (in seconds) all crew members of a crew have to be offline before the crew disbands")]
        public System.Single CrewDisbandTimeInSeconds
        { get { return ServerConfig.Singleton.CrewDisbandTimeInSeconds; } set { ServerConfig.Singleton.CrewDisbandTimeInSeconds = value; } }

        //[DisplayName("Overwrite Resource Price Stats")]
        //[Category("Miscellaneous")]
        //[Description("Sets the global trade stats of resources.")]
        //public System.Collections.Generic.Dictionary`2[Game.ClientServer.Classes.Economics.ResourceTypes,Game.ClientServer.Classes.Economics.ResourcePriceStats] OverwriteResourcePriceStats
        //{get{return ServerConfig.Singleton.OverwriteResourcePriceStats;} set {ServerConfig.Singleton.OverwriteResourcePriceStats = value;}}

        //[DisplayName("Overwrite Tool Price Stats")]
        //[Category("Miscellaneous")]
        //[Description("Sets the global trade stats of tools.")]
        //public System.Collections.Generic.Dictionary`2[Game.ClientServer.Classes.Economics.ToolTypes,Game.ClientServer.Classes.Economics.ToolPriceStats] OverwriteToolPriceStats
        //{get{return ServerConfig.Singleton.OverwriteToolPriceStats;} set {ServerConfig.Singleton.OverwriteToolPriceStats = value;}}

        [DisplayName("Upgrade Salvage Armor Perc")]
        [Category("Miscellaneous")]
        [Description("The amount of damage a ship can have in order for it to be upgraded or salvaged.")]
        public System.Single UpgradeSalvageArmorPerc
        { get { return ServerConfig.Singleton.UpgradeSalvageArmorPerc; } set { ServerConfig.Singleton.UpgradeSalvageArmorPerc = value; } }

        [DisplayName("Device Damage Resource Loss Threshold")]
        [Category("Miscellaneous")]
        [Description("The amount of damage a device needs before it will yield less resources in a salvage or upgrade operation. 0 = never lose resources, 1 = only don't lose resources if it is completely healthy")]
        public System.Single DeviceDamageResourceLossThreshold
        { get { return ServerConfig.Singleton.DeviceDamageResourceLossThreshold; } set { ServerConfig.Singleton.DeviceDamageResourceLossThreshold = value; } }

        [DisplayName("Device Damage Resource Loss Min Scale")]
        [Category("Miscellaneous")]
        [Description("The amount of resources a device will yield if it is completely broken in a salvage or upgrade operation. 0 = don't yield any resources, 1 = yield all resources regardless of damage.")]
        public System.Single DeviceDamageResourceLossMinScale
        { get { return ServerConfig.Singleton.DeviceDamageResourceLossMinScale; } set { ServerConfig.Singleton.DeviceDamageResourceLossMinScale = value; } }

        //[DisplayName("Specific System Settings")]
        //[Category("Miscellaneous")]
        //[Description("Sets custom settings for specific systems.")]
        //public System.Collections.Generic.Dictionary`2[System.String,Game.Universe.SystemSettings] SpecificSystemSettings
        //{get{return ServerConfig.Singleton.SpecificSystemSettings;} set {ServerConfig.Singleton.SpecificSystemSettings = value;}}

        [DisplayName("Max Universe Generator Generation Dist")]
        [Category("Generation")]
        [Description("Indicates how far the universe will generate from a point between the starter systems. 0 = unlimited")]
        public System.Int32 MaxUniverseGeneratorGenerationDist
        { get { return ServerConfig.Singleton.MaxUniverseGeneratorGenerationDist; } set { ServerConfig.Singleton.MaxUniverseGeneratorGenerationDist = value; } }

        [DisplayName("Start Universe Generator Seed")]
        [Category("Generation")]
        [Description("The random seed for universe generator")]
        public System.UInt32 StartUniverseGeneratorSeed
        { get { return ServerConfig.Singleton.StartUniverseGeneratorSeed; } set { ServerConfig.Singleton.StartUniverseGeneratorSeed = value; } }

        [DisplayName("Tier0 Protected System Chance")]
        [Category("Generation")]
        [Description("The chance a Tier 0 system is protected (PvE). 0 = always PvP, 100 = always PvE")]
        public System.Int32 Tier0ProtectedSystemChance
        { get { return ServerConfig.Singleton.Tier0ProtectedSystemChance; } set { ServerConfig.Singleton.Tier0ProtectedSystemChance = value; } }

        [DisplayName("Tier1 Protected System Chance")]
        [Category("Generation")]
        [Description("The chance a Tier 1 system is protected (PvE). 0 = always PvP, 100 = always PvE")]
        public System.Int32 Tier1ProtectedSystemChance
        { get { return ServerConfig.Singleton.Tier1ProtectedSystemChance; } set { ServerConfig.Singleton.Tier1ProtectedSystemChance = value; } }

        [DisplayName("Tier2 Protected System Chance")]
        [Category("Generation")]
        [Description("The chance a Tier 2 system is protected (PvE). 0 = always PvP, 100 = always PvE")]
        public System.Int32 Tier2ProtectedSystemChance
        { get { return ServerConfig.Singleton.Tier2ProtectedSystemChance; } set { ServerConfig.Singleton.Tier2ProtectedSystemChance = value; } }

        [DisplayName("Tier3 Protected System Chance")]
        [Category("Generation")]
        [Description("The chance a Tier 3 system is protected (PvE). 0 = always PvP, 100 = always PvE")]
        public System.Int32 Tier3ProtectedSystemChance
        { get { return ServerConfig.Singleton.Tier3ProtectedSystemChance; } set { ServerConfig.Singleton.Tier3ProtectedSystemChance = value; } }

        [DisplayName("Enable Server Welcome Popup")]
        [Category("Welcome message")]
        [Description("Indicates whether or not players will be greeted with a welcome popup when they connect to the server")]
        public System.Boolean EnableServerWelcomePopup
        { get { return ServerConfig.Singleton.EnableServerWelcomePopup; } set { ServerConfig.Singleton.EnableServerWelcomePopup = value; } }

        [DisplayName("Server Welcome Popup Message Title")]
        [Category("Welcome message")]
        [Description("The title of the welcome popup people will see when they connect to the server")]
        public System.String ServerWelcomePopupMessageTitle
        { get { return ServerConfig.Singleton.ServerWelcomePopupMessageTitle; } set { ServerConfig.Singleton.ServerWelcomePopupMessageTitle = value; } }

        [DisplayName("Server Welcome Popup Message Text")]
        [Category("Welcome message")]
        [Description("The body text of the welcome popup people will see when they connect to the server")]
        public System.String ServerWelcomePopupMessageText
        { get { return ServerConfig.Singleton.ServerWelcomePopupMessageText; } set { ServerConfig.Singleton.ServerWelcomePopupMessageText = value; } }

        //[DisplayName("Server Welcome Popup Message Title Color")]
        //[Category("Welcome message")]
        //[Description("The color of the title of the welcome popup people will see when they connect to the server")]
        //public System.UInt32[] ServerWelcomePopupMessageTitleColor
        //{get{return ServerConfig.Singleton.ServerWelcomePopupMessageTitleColor;} set {ServerConfig.Singleton.ServerWelcomePopupMessageTitleColor = value;}}

        [DisplayName("Detailed Desync Info")]
        [Category("Desyncs")]
        [Description("Setting used by developers to debug desyncs")]
        public System.Boolean DetailedDesyncInfo
        { get { return ServerConfig.Singleton.DetailedDesyncInfo; } set { ServerConfig.Singleton.DetailedDesyncInfo = value; } }

        [DisplayName("Track Time In Ticks")]
        [Category("Desyncs")]
        [Description("Setting used by developers to debug desyncs")]
        public System.Int32 TrackTimeInTicks
        { get { return ServerConfig.Singleton.TrackTimeInTicks; } set { ServerConfig.Singleton.TrackTimeInTicks = value; } }

        [DisplayName("Resync Tries Before Server Reload")]
        [Category("Desyncs")]
        [Description("Amount of times the server tries to resync a player before the server reloads the system the desyncing player is in.")]
        public System.Int32 ResyncTriesBeforeServerReload
        { get { return ServerConfig.Singleton.ResyncTriesBeforeServerReload; } set { ServerConfig.Singleton.ResyncTriesBeforeServerReload = value; } }

        [DisplayName("Ticks After System Load For Server Reload")]
        [Category("Desyncs")]
        [Description("The amount of ticks the server will not reload, even if players are desyncing. (20 ticks/second, server only ticks if there are players in the system)")]
        public System.Int32 TicksAfterSystemLoadForServerReload
        { get { return ServerConfig.Singleton.TicksAfterSystemLoadForServerReload; } set { ServerConfig.Singleton.TicksAfterSystemLoadForServerReload = value; } }

        [DisplayName("Reconnect Critical Desync Players")]
        [Category("Desyncs")]
        [Description("Indicates Whether or not players will automatically be reconnected when they desync")]
        public System.Boolean ReconnectCriticalDesyncPlayers
        { get { return ServerConfig.Singleton.ReconnectCriticalDesyncPlayers; } set { ServerConfig.Singleton.ReconnectCriticalDesyncPlayers = value; } }

        [DisplayName("Refresh Starter Ships On Startup")]
        [Category("Starter Ships")]
        [Description("Resets the starterships to the default values, removing all custom starter ships")]
        public System.Boolean RefreshStarterShipsOnStartup
        { get { return ServerConfig.Singleton.RefreshStarterShipsOnStartup; } set { ServerConfig.Singleton.RefreshStarterShipsOnStartup = value; } }

        //[DisplayName("Faction Starter Ships")]
        //[Category("Starter Ships")]
        //[Description("Specifics of starter ships.")]
        //public System.Collections.Generic.Dictionary`2[Game.ClientServer.Classes.FactionTypes,System.Collections.Generic.List`1[Game.ClientServer.Classes.FactionStarterShipDetails]] FactionStarterShips
        //{get{return ServerConfig.Singleton.FactionStarterShips;} set {ServerConfig.Singleton.FactionStarterShips = value;}}

        [DisplayName("Refresh Derelict Ships On Startup")]
        [Category("Derelict Ships")]
        [Description("Resets the Derelict ships to the default values, removing all custom ships")]
        public System.Boolean RefreshDerelictShipsOnStartup
        { get { return ServerConfig.Singleton.RefreshDerelictShipsOnStartup; } set { ServerConfig.Singleton.RefreshDerelictShipsOnStartup = value; } }

        //[DisplayName("Derelict Ship Stats")]
        //[Category("Derelict Ships")]
        //[Description("Specifics of starter ships.")]
        //public System.Collections.Generic.List`1[Game.Configuration.ServerConfig+DerelictShipDetails] DerelictShipStats
        //{get{return ServerConfig.Singleton.DerelictShipStats;} set {ServerConfig.Singleton.DerelictShipStats = value;}}

        [DisplayName("Rich Asteriod Min Tier")]
        [Category("Rich Asteroids")]
        [Description("The minimum tier a system needs to be for it to spawn rich asteroids")]
        public System.Int32 RichAsteriodMinTier
        { get { return ServerConfig.Singleton.RichAsteriodMinTier; } set { ServerConfig.Singleton.RichAsteriodMinTier = value; } }

        [DisplayName("Rich Asteriod Min Ticks For First Spawn")]
        [Category("Rich Asteroids")]
        [Description("The minimum amount of time it takes for the first rich asteroid to spawn in ticks (20 ticks/second, server only ticks if there are players in the system)")]
        public System.Int32 RichAsteriodMinTicksForFirstSpawn
        { get { return ServerConfig.Singleton.RichAsteriodMinTicksForFirstSpawn; } set { ServerConfig.Singleton.RichAsteriodMinTicksForFirstSpawn = value; } }

        [DisplayName("Rich Asteriod Max Ticks For First Spawn")]
        [Category("Rich Asteroids")]
        [Description("The maximum amount of time it takes for the first rich asteroid to spawn in ticks (20 ticks/second, server only ticks if there are players in the system)")]
        public System.Int32 RichAsteriodMaxTicksForFirstSpawn
        { get { return ServerConfig.Singleton.RichAsteriodMaxTicksForFirstSpawn; } set { ServerConfig.Singleton.RichAsteriodMaxTicksForFirstSpawn = value; } }

        [DisplayName("Rich Asteriod Min Ticks Between Spawns")]
        [Category("Rich Asteroids")]
        [Description("The minimum amount of time it takes for consecutive rich asteroid to spawn in ticks (20 ticks/second, server only ticks if there are players in the system)")]
        public System.Int32 RichAsteriodMinTicksBetweenSpawns
        { get { return ServerConfig.Singleton.RichAsteriodMinTicksBetweenSpawns; } set { ServerConfig.Singleton.RichAsteriodMinTicksBetweenSpawns = value; } }

        [DisplayName("Rich Asteriod Max Ticks Between Spawns")]
        [Category("Rich Asteroids")]
        [Description("The maximum amount of time it takes for consecutive rich asteroid to spawn in ticks (20 ticks/second, server only ticks if there are players in the system)")]
        public System.Int32 RichAsteriodMaxTicksBetweenSpawns
        { get { return ServerConfig.Singleton.RichAsteriodMaxTicksBetweenSpawns; } set { ServerConfig.Singleton.RichAsteriodMaxTicksBetweenSpawns = value; } }

        [DisplayName("Rich Asteriod Max Amount")]
        [Category("Rich Asteroids")]
        [Description("The maximum amount of rich asteroids that can exist in a system simultaneously.")]
        public System.Int32 RichAsteriodMaxAmount
        { get { return ServerConfig.Singleton.RichAsteriodMaxAmount; } set { ServerConfig.Singleton.RichAsteriodMaxAmount = value; } }

        //---<ENDGEN>---
    }
}