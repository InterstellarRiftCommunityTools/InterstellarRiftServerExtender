using Game.ClientServer.Packets;
using Game.Framework.Networking;
using Game.Server;
using System;
using System.Collections.Generic;

namespace IRSE.Managers.Events
{
    public class EventManager
    {
        private Dictionary<Type, RPCDelegate> cFunctions;

        public Dictionary<Type, List<RPCDelegate>> Events { get; } = new Dictionary<Type, List<RPCDelegate>>();

        public static EventManager Instance { get; set; }

        public void Intercept(RPCDispatcher rpcDispatcher)
        {
            Instance = new EventManager();

            // make a copy of the original events
            cFunctions = new Dictionary<Type, RPCDelegate>(rpcDispatcher.Functions);

            // hook my events in and call the old ones 
            rpcDispatcher.Functions[typeof(ClientLogin)] = new RPCDelegate(this.OnPacketLogin);
            rpcDispatcher.Functions[typeof(ClientLoginAsGhost)] = new RPCDelegate(this.OnPacketLoginAsGhost);
            rpcDispatcher.Functions[typeof(ClientRespawn)] = new RPCDelegate(this.OnPacketRespawn);
            rpcDispatcher.Functions[typeof(ClientRequestSystViewData)] = new RPCDelegate(this.OnPacketRequestSystemViewData);
            rpcDispatcher.Functions[typeof(ClientChunkSaveData)] = new RPCDelegate(this.OnPacketClientSaveData);
            rpcDispatcher.Functions[typeof(ClientEntData)] = new RPCDelegate(this.OnPacketEntData);
            rpcDispatcher.Functions[typeof(ClientDeviceData)] = new RPCDelegate(this.OnPacketClientDeviceData);
            rpcDispatcher.Functions[typeof(ClientDeviceHelperData)] = new RPCDelegate(this.OnPacketDeviceHelperData);
            rpcDispatcher.Functions[typeof(ClientShipUpgradeData)] = new RPCDelegate(this.OnPacketShipUpgradeData);
            rpcDispatcher.Functions[typeof(ClientUserGroupsData)] = new RPCDelegate(this.OnPacketClientUserGroupData);
            rpcDispatcher.Functions[typeof(ClientPlayersOnShipData)] = new RPCDelegate(this.i_onPacketClientPlayersOnShipStatsData);
            rpcDispatcher.Functions[typeof(ClientGiveInventoryItemToPlayer)] = new RPCDelegate(this.OnPacketClientGiveInventoryItemToPlayer);
            rpcDispatcher.Functions[typeof(ClientPlayerToolData)] = new RPCDelegate(this.i_onPacketClientPlayerCommandData);
            rpcDispatcher.Functions[typeof(ClientPlayerState)] = new RPCDelegate(this.OnPacketClientStateSync);
            rpcDispatcher.Functions[typeof(ClientChatMessage)] = new RPCDelegate(this.OnPacketChatMessage);
            rpcDispatcher.Functions[typeof(ClientMailMessage)] = new RPCDelegate(this.OnPacketMailMessage);
            rpcDispatcher.Functions[typeof(ClientRemoveMail)] = new RPCDelegate(this.OnPacketRemoveMail);
            rpcDispatcher.Functions[typeof(ClientMailChangeReadState)] = new RPCDelegate(this.OnPacketMailChangeReadState);
            rpcDispatcher.Functions[typeof(ClientAcceptFleetInvite)] = new RPCDelegate(this.OnPacketAcceptFleetInvite);
            rpcDispatcher.Functions[typeof(ClientCreateFleet)] = new RPCDelegate(this.OnPacketCreateFleet);
            rpcDispatcher.Functions[typeof(ClientRemoveFleetMember)] = new RPCDelegate(this.OnPacketRemoveFactionMember);
            rpcDispatcher.Functions[typeof(ClientCreateFleetRank)] = new RPCDelegate(this.OnPacketCreateFactionRank);
            rpcDispatcher.Functions[typeof(ClientRemoveFleetRank)] = new RPCDelegate(this.OnPacketRemoveFactionRank);
            rpcDispatcher.Functions[typeof(ClientEditFleetRank)] = new RPCDelegate(this.OnPacketEditFactionRank);
            rpcDispatcher.Functions[typeof(ClientReorderFleetRank)] = new RPCDelegate(this.OnPacketReorderFactionRank);
            rpcDispatcher.Functions[typeof(ClientChangeMemberRank)] = new RPCDelegate(this.OnPacketChangeMemberRank);
            rpcDispatcher.Functions[typeof(ClientDisbandFleet)] = new RPCDelegate(this.OnPacketDisbandFaction);
            rpcDispatcher.Functions[typeof(ClientLeaveFleet)] = new RPCDelegate(this.OnPacketLeaveFaction);
            rpcDispatcher.Functions[typeof(ClientPayFactionBounty)] = new RPCDelegate(this.OnPacketPayNPCFactionBounty);
            rpcDispatcher.Functions[typeof(ClientCreateCrew)] = new RPCDelegate(this.OnPacketCreateCrew);
            rpcDispatcher.Functions[typeof(ClientAcceptCrewInvite)] = new RPCDelegate(this.OnPacketAcceptCrewInvite);
            rpcDispatcher.Functions[typeof(ClientLeaveCrew)] = new RPCDelegate(this.OnPacketLeaveCrew);
            rpcDispatcher.Functions[typeof(ClientPayFine)] = new RPCDelegate(this.OnPacketPayFine);
            rpcDispatcher.Functions[typeof(ClientPayAllFines)] = new RPCDelegate(this.i_onPacketPayAllFines);
            rpcDispatcher.Functions[typeof(ClientKickCrewMember)] = new RPCDelegate(this.OnPacketKickCrewMember);
            rpcDispatcher.Functions[typeof(ClientDisbandCrew)] = new RPCDelegate(this.OnPacketDisbandCrew);
            rpcDispatcher.Functions[typeof(ClientAdminCommand)] = new RPCDelegate(this.OnPacketAdminCommand);
            rpcDispatcher.Functions[typeof(ClientSelectStartFaction)] = new RPCDelegate(this.OnPacketSelectStartFaction);
            rpcDispatcher.Functions[typeof(ClientAbandonMission)] = new RPCDelegate(this.OnPacketAbandonMission);
            rpcDispatcher.Functions[typeof(ClientProjectileHit)] = new RPCDelegate(this.OnPacketProjectileHit);
            rpcDispatcher.Functions[typeof(ClientAddProjectile)] = new RPCDelegate(this.OnPacketAddProjectile);
            rpcDispatcher.Functions[typeof(ClientRemoveProjectile)] = new RPCDelegate(this.OnPacketRemoveProjectile);
            rpcDispatcher.Functions[typeof(ClientProjectileSyncData)] = new RPCDelegate(this.OnPacketProjectileSyncData);
            rpcDispatcher.Functions[typeof(ClientProjectileSpawnData)] = new RPCDelegate(this.OnPacketProjectileSpawnData);
            rpcDispatcher.Functions[typeof(ClientNotifyDesync)] = new RPCDelegate(this.OnPacketNotifyDesync);
            rpcDispatcher.Functions[typeof(ClientNetStats)] = new RPCDelegate(this.OnPacketNetStats);
            rpcDispatcher.Functions[typeof(ClientNetTrack)] = new RPCDelegate(this.OnPacketNetTrack);
            rpcDispatcher.Functions[typeof(ClientUnequipPlayerEquipment)] = new RPCDelegate(this.OnPacketUnequipPlayerEquipment);
            rpcDispatcher.Functions[typeof(ClientRequestShipDelivery)] = new RPCDelegate(this.OnPacketRequestShipDelivery);
            rpcDispatcher.Functions[typeof(ClientRequestSystemMetadata)] = new RPCDelegate(this.OnPacketRequestSystemMetadata);
            rpcDispatcher.Functions[typeof(ClientChangeCaptainsTask)] = new RPCDelegate(this.OnPacketChangeCaptainsTask);
            rpcDispatcher.Functions[typeof(ClientCompleteMissionObjective)] = new RPCDelegate(this.OnPacketCompleteMissionObjective);
            rpcDispatcher.Functions[typeof(ClientVariableEdit)] = new RPCDelegate(this.i_onPacketVariableEdit);
            rpcDispatcher.Functions[typeof(ClientSpawnEntity)] = new RPCDelegate(this.i_onPacketSpawnEntity);
            rpcDispatcher.Functions[typeof(ClientClaimRewardsForProgressionItem)] = new RPCDelegate(this.OnPacketClaimProgressionReward);
            rpcDispatcher.Functions[typeof(GhostClientHeartbeat)] = new RPCDelegate(this.i_onPacketGhostClientHeartbeat);
            rpcDispatcher.Functions[typeof(GhostClientRequestChunkSaveDataAcknowledge)] = new RPCDelegate(this.i_onPacketGhostClientRequestChunkSaveDataAcknowledge);
            rpcDispatcher.Functions[typeof(ClientChangeActiveMission)] = new RPCDelegate(this.OnPacketChangeActiveMission);
            rpcDispatcher.Functions[typeof(ClientSetShipPurchasable)] = new RPCDelegate(this.OnPacketSetShipPurchasable);
            rpcDispatcher.Functions[typeof(ClientPurchaseShip)] = new RPCDelegate(this.OnPacketPurchaseShip);
            rpcDispatcher.Functions[typeof(ClientSetShipName)] = new RPCDelegate(this.OnPacketSetShipName);
            rpcDispatcher.Functions[typeof(ClientSplitStack)] = new RPCDelegate(this.OnPacketSplitStack);
        }



        protected void OnPacketLogin(RPCData data)
        {
            Type type = typeof(ClientLogin);
            cFunctions[type]?.Invoke(data);
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
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
            foreach (RPCDelegate action in Events[type])
            {
                try
                {
                    action?.Invoke(data);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}