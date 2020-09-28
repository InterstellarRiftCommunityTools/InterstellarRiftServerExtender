using Game.ClientServer.Packets;
using Game.Configuration;
using Game.Framework.Networking;
using Game.Server;
using System;
using System.Collections.Generic;

namespace Game.ClientServer.Packets
{
    public struct ClientConnected
    {
        private Player player;
    }

    public struct ClientDisconnected
    {
        private Player player;
    }
}

namespace IRSE.Managers.Events
{
    public class EventManager
    {
        private Dictionary<Type, RPCDelegate> cFunctions;
        private ControllerManager m_controllerManager;
        public Dictionary<Type, List<EventListener>> Events { get; } = new Dictionary<Type, List<EventListener>>();

        public static EventManager Instance = new EventManager();

        public void Intercept(ControllerManager controllerManager)
        {
            
            m_controllerManager = controllerManager;

            RPCDispatcher rpcDispatcher = controllerManager.Network.Net.RpcDispatcher;

            // initialize all Lists
            foreach (var evt in rpcDispatcher.Functions)
            {
                Events[evt.Key] = new List<EventListener>();

            }

            // initialize custom struct lists
            Events[typeof(ClientConnected)] = new List<EventListener>();
            Events[typeof(ClientDisconnected)] = new List<EventListener>();


            // make a copy of the original events
            cFunctions = new Dictionary<Type, RPCDelegate>(rpcDispatcher.Functions);


            // hook my events in and call the old ones
            rpcDispatcher.Functions[typeof(ClientLogin)] = new RPCDelegate(OnPacketLogin);
            rpcDispatcher.Functions[typeof(ClientLoginAsGhost)] = new RPCDelegate(OnPacketLoginAsGhost);
            rpcDispatcher.Functions[typeof(ClientRespawn)] = new RPCDelegate(OnPacketRespawn);
            rpcDispatcher.Functions[typeof(ClientRequestSystViewData)] = new RPCDelegate(OnPacketRequestSystemViewData);
            rpcDispatcher.Functions[typeof(ClientChunkSaveData)] = new RPCDelegate(OnPacketClientSaveData);
            rpcDispatcher.Functions[typeof(ClientEntData)] = new RPCDelegate(OnPacketEntData);
            rpcDispatcher.Functions[typeof(ClientDeviceData)] = new RPCDelegate(OnPacketClientDeviceData);
            rpcDispatcher.Functions[typeof(ClientDeviceHelperData)] = new RPCDelegate(OnPacketDeviceHelperData);
            rpcDispatcher.Functions[typeof(ClientShipUpgradeData)] = new RPCDelegate(OnPacketShipUpgradeData);
            rpcDispatcher.Functions[typeof(ClientUserGroupsData)] = new RPCDelegate(OnPacketClientUserGroupData);
            rpcDispatcher.Functions[typeof(ClientPlayersOnShipData)] = new RPCDelegate(i_onPacketClientPlayersOnShipStatsData);
            rpcDispatcher.Functions[typeof(ClientGiveInventoryItemToPlayer)] = new RPCDelegate(OnPacketClientGiveInventoryItemToPlayer);
            rpcDispatcher.Functions[typeof(ClientPlayerToolData)] = new RPCDelegate(i_onPacketClientPlayerCommandData);
            rpcDispatcher.Functions[typeof(ClientPlayerState)] = new RPCDelegate(OnPacketClientStateSync);
            rpcDispatcher.Functions[typeof(ClientChatMessage)] = new RPCDelegate(OnPacketChatMessage);
            rpcDispatcher.Functions[typeof(ClientMailMessage)] = new RPCDelegate(OnPacketMailMessage);
            rpcDispatcher.Functions[typeof(ClientRemoveMail)] = new RPCDelegate(OnPacketRemoveMail);
            rpcDispatcher.Functions[typeof(ClientMailChangeReadState)] = new RPCDelegate(OnPacketMailChangeReadState);
            rpcDispatcher.Functions[typeof(ClientAcceptFleetInvite)] = new RPCDelegate(OnPacketAcceptFleetInvite);
            rpcDispatcher.Functions[typeof(ClientCreateFleet)] = new RPCDelegate(OnPacketCreateFleet);
            rpcDispatcher.Functions[typeof(ClientRemoveFleetMember)] = new RPCDelegate(OnPacketRemoveFactionMember);
            rpcDispatcher.Functions[typeof(ClientCreateFleetRank)] = new RPCDelegate(OnPacketCreateFactionRank);
            rpcDispatcher.Functions[typeof(ClientRemoveFleetRank)] = new RPCDelegate(OnPacketRemoveFactionRank);
            rpcDispatcher.Functions[typeof(ClientEditFleetRank)] = new RPCDelegate(OnPacketEditFactionRank);
            rpcDispatcher.Functions[typeof(ClientReorderFleetRank)] = new RPCDelegate(OnPacketReorderFactionRank);
            rpcDispatcher.Functions[typeof(ClientChangeMemberRank)] = new RPCDelegate(OnPacketChangeMemberRank);
            rpcDispatcher.Functions[typeof(ClientDisbandFleet)] = new RPCDelegate(OnPacketDisbandFaction);
            rpcDispatcher.Functions[typeof(ClientLeaveFleet)] = new RPCDelegate(OnPacketLeaveFaction);
            rpcDispatcher.Functions[typeof(ClientPayFactionBounty)] = new RPCDelegate(OnPacketPayNPCFactionBounty);
            rpcDispatcher.Functions[typeof(ClientCreateCrew)] = new RPCDelegate(OnPacketCreateCrew);
            rpcDispatcher.Functions[typeof(ClientAcceptCrewInvite)] = new RPCDelegate(OnPacketAcceptCrewInvite);
            rpcDispatcher.Functions[typeof(ClientLeaveCrew)] = new RPCDelegate(OnPacketLeaveCrew);
            rpcDispatcher.Functions[typeof(ClientPayFine)] = new RPCDelegate(OnPacketPayFine);
            rpcDispatcher.Functions[typeof(ClientPayAllFines)] = new RPCDelegate(i_onPacketPayAllFines);
            rpcDispatcher.Functions[typeof(ClientKickCrewMember)] = new RPCDelegate(OnPacketKickCrewMember);
            rpcDispatcher.Functions[typeof(ClientDisbandCrew)] = new RPCDelegate(OnPacketDisbandCrew);
            rpcDispatcher.Functions[typeof(ClientAdminCommand)] = new RPCDelegate(OnPacketAdminCommand);
            rpcDispatcher.Functions[typeof(ClientSelectStartFaction)] = new RPCDelegate(OnPacketSelectStartFaction);
            rpcDispatcher.Functions[typeof(ClientAbandonMission)] = new RPCDelegate(OnPacketAbandonMission);
            rpcDispatcher.Functions[typeof(ClientProjectileHit)] = new RPCDelegate(OnPacketProjectileHit);
            rpcDispatcher.Functions[typeof(ClientAddProjectile)] = new RPCDelegate(OnPacketAddProjectile);
            rpcDispatcher.Functions[typeof(ClientRemoveProjectile)] = new RPCDelegate(OnPacketRemoveProjectile);
            rpcDispatcher.Functions[typeof(ClientProjectileSyncData)] = new RPCDelegate(OnPacketProjectileSyncData);
            rpcDispatcher.Functions[typeof(ClientProjectileSpawnData)] = new RPCDelegate(OnPacketProjectileSpawnData);
            rpcDispatcher.Functions[typeof(ClientNotifyDesync)] = new RPCDelegate(OnPacketNotifyDesync);
            rpcDispatcher.Functions[typeof(ClientNetStats)] = new RPCDelegate(OnPacketNetStats);
            rpcDispatcher.Functions[typeof(ClientNetTrack)] = new RPCDelegate(OnPacketNetTrack);
            rpcDispatcher.Functions[typeof(ClientUnequipPlayerEquipment)] = new RPCDelegate(OnPacketUnequipPlayerEquipment);
            rpcDispatcher.Functions[typeof(ClientRequestShipDelivery)] = new RPCDelegate(OnPacketRequestShipDelivery);
            rpcDispatcher.Functions[typeof(ClientRequestSystemMetadata)] = new RPCDelegate(OnPacketRequestSystemMetadata);
            rpcDispatcher.Functions[typeof(ClientChangeCaptainsTask)] = new RPCDelegate(OnPacketChangeCaptainsTask);
            rpcDispatcher.Functions[typeof(ClientCompleteMissionObjective)] = new RPCDelegate(OnPacketCompleteMissionObjective);
            rpcDispatcher.Functions[typeof(ClientVariableEdit)] = new RPCDelegate(i_onPacketVariableEdit);
            rpcDispatcher.Functions[typeof(ClientSpawnEntity)] = new RPCDelegate(i_onPacketSpawnEntity);
            rpcDispatcher.Functions[typeof(ClientClaimRewardsForProgressionItem)] = new RPCDelegate(OnPacketClaimProgressionReward);
            rpcDispatcher.Functions[typeof(GhostClientHeartbeat)] = new RPCDelegate(i_onPacketGhostClientHeartbeat);
            rpcDispatcher.Functions[typeof(GhostClientRequestChunkSaveDataAcknowledge)] = new RPCDelegate(i_onPacketGhostClientRequestChunkSaveDataAcknowledge);
            rpcDispatcher.Functions[typeof(ClientChangeActiveMission)] = new RPCDelegate(OnPacketChangeActiveMission);
            rpcDispatcher.Functions[typeof(ClientSetShipPurchasable)] = new RPCDelegate(OnPacketSetShipPurchasable);
            rpcDispatcher.Functions[typeof(ClientPurchaseShip)] = new RPCDelegate(OnPacketPurchaseShip);
            rpcDispatcher.Functions[typeof(ClientSetShipName)] = new RPCDelegate(OnPacketSetShipName);
            rpcDispatcher.Functions[typeof(ClientSplitStack)] = new RPCDelegate(OnPacketSplitStack);

            controllerManager.Players.OnAddPlayer += new Action<Player>(Players_OnAddPlayer);
            controllerManager.Players.OnRemovePlayer += new Action<Player>(Players_OnRemovePlayer);
        }

        protected void Players_OnAddPlayer(Player obj)
        {
            m_controllerManager.Chat.SendToAll(Config.Singleton.NotificationChatColor, "Player " + obj.Name + " joined the server", "All");

            Type type = typeof(ClientConnected);

            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, new RPCData() { DeserializedObject = obj }));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        protected void Players_OnRemovePlayer(Player obj)
        {
            m_controllerManager.Chat.SendToAll(Config.Singleton.NotificationChatColor, "Player " + obj.Name + " left the server", "All");

            Type type = typeof(ClientDisconnected);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, new RPCData() { DeserializedObject = obj }));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        protected void OnPacketLogin(RPCData data)
        {
            Type type = typeof(ClientLogin);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        protected void OnPacketLoginAsGhost(RPCData data)
        {
            Type type = typeof(ClientLoginAsGhost);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        protected void OnPacketRespawn(RPCData data)
        {
            Type type = typeof(ClientRespawn);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        protected void OnPacketRequestSystemViewData(RPCData data)
        {
            Type type = typeof(ClientRequestSystViewData);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        protected void OnPacketClientSaveData(RPCData data)
        {
            Type type = typeof(ClientChunkSaveData);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        protected void OnPacketEntData(RPCData data)
        {
            Type type = typeof(ClientEntData);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        protected void OnPacketClientDeviceData(RPCData data)
        {
            Type type = typeof(ClientDeviceData);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        protected void OnPacketDeviceHelperData(RPCData data)
        {
            Type type = typeof(ClientDeviceHelperData);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        protected void OnPacketShipUpgradeData(RPCData data)
        {
            Type type = typeof(ClientShipUpgradeData);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        protected void OnPacketClientUserGroupData(RPCData data)
        {
            Type type = typeof(ClientUserGroupsData);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        protected void i_onPacketClientPlayersOnShipStatsData(RPCData data)
        {
            Type type = typeof(ClientPlayersOnShipData);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        protected void OnPacketClientGiveInventoryItemToPlayer(RPCData data)
        {
            Type type = typeof(ClientGiveInventoryItemToPlayer);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        protected void i_onPacketClientPlayerCommandData(RPCData data)
        {
            Type type = typeof(ClientPlayerToolData);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        protected void OnPacketClientStateSync(RPCData data)
        {
            Type type = typeof(ClientPlayerState);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        protected void OnPacketChatMessage(RPCData data)
        {
            Type type = typeof(ClientChatMessage);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void OnPacketMailMessage(RPCData data)
        {
            Type type = typeof(ClientMailMessage);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void OnPacketRemoveMail(RPCData data)
        {
            Type type = typeof(ClientRemoveMail);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void OnPacketMailChangeReadState(RPCData data)
        {
            Type type = typeof(ClientMailChangeReadState);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void OnPacketCreateFleet(RPCData data)
        {
            Type type = typeof(ClientCreateFleet);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void OnPacketSplitStack(RPCData data)
        {
            Type type = typeof(ClientSplitStack);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void OnPacketSetShipPurchasable(RPCData data)
        {
            Type type = typeof(ClientSetShipPurchasable);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void OnPacketSetShipName(RPCData data)
        {
            Type type = typeof(ClientSetShipName);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void OnPacketPurchaseShip(RPCData data)
        {
            Type type = typeof(ClientPurchaseShip);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void OnPacketAcceptFleetInvite(RPCData data)
        {
            Type type = typeof(ClientAcceptFleetInvite);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void OnPacketRemoveFactionMember(RPCData data)
        {
            Type type = typeof(ClientRemoveFleetMember);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void OnPacketCreateFactionRank(RPCData data)
        {
            Type type = typeof(ClientCreateFleetRank);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void OnPacketRemoveFactionRank(RPCData data)
        {
            Type type = typeof(ClientRemoveFleetRank);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void OnPacketEditFactionRank(RPCData data)
        {
            Type type = typeof(ClientEditFleetRank);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void OnPacketReorderFactionRank(RPCData data)
        {
            Type type = typeof(ClientReorderFleetRank);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void OnPacketChangeMemberRank(RPCData data)
        {
            Type type = typeof(ClientChangeMemberRank);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void OnPacketDisbandFaction(RPCData data)
        {
            Type type = typeof(ClientDisbandFleet);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void OnPacketLeaveFaction(RPCData data)
        {
            Type type = typeof(ClientLeaveFleet);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void OnPacketPayNPCFactionBounty(RPCData data)
        {
            Type type = typeof(ClientPayFactionBounty);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void OnPacketCreateCrew(RPCData data)
        {
            Type type = typeof(ClientCreateCrew);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void OnPacketAcceptCrewInvite(RPCData data)
        {
            Type type = typeof(ClientAcceptCrewInvite);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void OnPacketLeaveCrew(RPCData data)
        {
            Type type = typeof(ClientLeaveCrew);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void OnPacketKickCrewMember(RPCData data)
        {
            Type type = typeof(ClientKickCrewMember);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void OnPacketDisbandCrew(RPCData data)
        {
            Type type = typeof(ClientDisbandCrew);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void OnPacketPayFine(RPCData data)
        {
            Type type = typeof(ClientPayFine);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void i_onPacketPayAllFines(RPCData data)
        {
            Type type = typeof(ClientPayAllFines);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void OnPacketNetStats(RPCData data)
        {
            Type type = typeof(ClientNetStats);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void OnPacketNetTrack(RPCData data)
        {
            Type type = typeof(ClientNetTrack);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void i_onPacketVariableEdit(RPCData data)
        {
            Type type = typeof(ClientVariableEdit);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void i_onPacketSpawnEntity(RPCData data)
        {
            Type type = typeof(ClientSpawnEntity);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void i_onPacketGhostClientHeartbeat(RPCData data)
        {
            Type type = typeof(GhostClientHeartbeat);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        protected void OnPacketAdminCommand(RPCData data)
        {
            Type type = typeof(ClientAdminCommand);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        protected void OnPacketSelectStartFaction(RPCData data)
        {
            Type type = typeof(ClientSelectStartFaction);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        protected void OnPacketAbandonMission(RPCData data)
        {
            Type type = typeof(ClientAbandonMission);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        protected void OnPacketProjectileHit(RPCData data)
        {
            Type type = typeof(ClientProjectileHit);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        protected void OnPacketAddProjectile(RPCData data)
        {
            Type type = typeof(ClientAddProjectile);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        protected void OnPacketRemoveProjectile(RPCData data)
        {
            Type type = typeof(ClientRemoveProjectile);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        protected void OnPacketProjectileSyncData(RPCData data)
        {
            Type type = typeof(ClientProjectileSyncData);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        protected void OnPacketProjectileSpawnData(RPCData data)
        {
            Type type = typeof(ClientProjectileSpawnData);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        protected void OnPacketNotifyDesync(RPCData data)
        {
            Type type = typeof(ClientNotifyDesync);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        protected void OnPacketUnequipPlayerEquipment(RPCData data)
        {
            Type type = typeof(ClientUnequipPlayerEquipment);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        protected void OnPacketRequestShipDelivery(RPCData data)
        {
            Type type = typeof(ClientRequestShipDelivery);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        protected void OnPacketRequestSystemMetadata(RPCData data)
        {
            Type type = typeof(ClientRequestSystemMetadata);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        protected void OnPacketChangeCaptainsTask(RPCData data)
        {
            Type type = typeof(ClientChangeCaptainsTask);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        protected void OnPacketChangeActiveMission(RPCData data)
        {
            Type type = typeof(ClientChangeActiveMission);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        protected void OnPacketCompleteMissionObjective(RPCData data)
        {
            Type type = typeof(ClientCompleteMissionObjective);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        protected void OnPacketClaimProgressionReward(RPCData data)
        {
            Type type = typeof(ClientClaimRewardsForProgressionItem);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void i_onPacketGhostClientRequestChunkSaveDataAcknowledge(RPCData data)
        {
            Type type = typeof(GhostClientRequestChunkSaveDataAcknowledge);
            cFunctions[type]?.Invoke(data);
            foreach (EventListener action in Events[type])
            {
                try
                {
                    action?.Execute(new GenericEvent(type, data));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}