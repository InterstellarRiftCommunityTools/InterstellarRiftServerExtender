using Game.ClientServer.Packets;
using Game.Configuration;
using Game.Framework;
using Game.Server;
using System;
using System.Diagnostics;

namespace IRSE.Modules
{
    public class GhostClientState : IUpdatable
    {
        private float m_startCountResetTimer = (float)ServerConfig.Singleton.GhostClientStartCountResetDurationInSeconds;
        private float m_preventStartTimer = (float)ServerConfig.Singleton.GhostClientPreventStartDurationInSeconds;
        private DateTime m_lastHeartbeatReceived = DateTime.MinValue;
        private DateTime m_lastHeartbeatSent = DateTime.MinValue;
        private float m_heartbeatTimer = (float)ServerConfig.Singleton.GhostClientHeartbeatIntervalInSeconds;
        public Process Process;
        public int StartCount;
        public bool PreventStart;
        public string SystemName;

        public Game.Server.ControllerManager Controllers { get; private set; }

        public GhostClientState(Game.Server.ControllerManager controllers)
        {
            Controllers = controllers;
        }

        public override void Update(float dt)
        {
            if (this.StartCount != 0)
            {
                this.m_startCountResetTimer = this.m_startCountResetTimer - dt;
                if ((double)this.m_startCountResetTimer <= 0.0)
                {
                    this.StartCount = 0;
                    this.m_startCountResetTimer = (float)ServerConfig.Singleton.GhostClientStartCountResetDurationInSeconds;
                }
            }
            if (this.PreventStart)
            {
                this.m_preventStartTimer = this.m_preventStartTimer - dt;
                if ((double)this.m_preventStartTimer <= 0.0)
                {
                    this.PreventStart = false;
                    this.m_preventStartTimer = (float)ServerConfig.Singleton.GhostClientPreventStartDurationInSeconds;
                }
            }
            if (!ServerConfig.Singleton.GhostClientHeartbeatEnabled || this.Process == null || this.Process.HasExited)
                return;
            PlayerController.GhostPlayerStats ghostPlayerStats = this.Controllers.Players.GetGhostPlayerStats(this.SystemName);
            if (!(this.m_lastHeartbeatReceived != DateTime.MinValue))
                return;
            TimeSpan timeSpan1 = DateTime.UtcNow - this.m_lastHeartbeatSent;
            TimeSpan timeSpan2 = DateTime.UtcNow - this.m_lastHeartbeatReceived;
            if (timeSpan2 > TimeSpan.FromSeconds((double)ServerConfig.Singleton.GhostClientHeartbeatTimeoutInSeconds) && this.m_lastHeartbeatSent != DateTime.MinValue && (timeSpan1 > TimeSpan.FromSeconds(0.5) && timeSpan1 <= TimeSpan.FromSeconds((double)ServerConfig.Singleton.GhostClientHeartbeatIntervalInSeconds * 1.25)))
            {
                Console.WriteLine(string.Format("[IR]Non-responsive ghost client (System: {0}, PID: {1}), killing... (last heartbeat sent: {2} ago, last heartbeat received: {3} ago", new object[4]
                {
            (object) this.SystemName,
            (object) this.Process.Id,
            (object) timeSpan1,
            (object) timeSpan2
                }), "Saving", (Exception)null);
                this.Process.Kill();
            }
            else
            {
                this.m_heartbeatTimer = this.m_heartbeatTimer - dt;
                if ((double)this.m_heartbeatTimer > 0.0)
                    return;
                ghostPlayerStats.player.SendRPC((object)new ServerGhostClientHeartbeat());
                this.m_lastHeartbeatSent = DateTime.UtcNow;
                this.m_heartbeatTimer = (float)ServerConfig.Singleton.GhostClientHeartbeatIntervalInSeconds;
            }
        }

        public override void Render()
        {
        }

        public override int GetOrderIndex()
        {
            return -99;
        }

        public void Heartbeat()
        {
            this.m_lastHeartbeatReceived = DateTime.UtcNow;
        }

        public void ResetHeartbeat()
        {
            this.m_lastHeartbeatReceived = DateTime.MinValue;
            this.m_heartbeatTimer = (float)ServerConfig.Singleton.GhostClientHeartbeatIntervalInSeconds;
        }
    }
}