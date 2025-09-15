using System;
using UnityEngine;
using Unity.VisualScripting;
using System.Collections.Generic;
using SimupactComs;
using UnityEngine.Scripting;
using Server = ControlSystemConnection.GrpcGenerated.Server;

namespace ThreeDPactXR.SimuPact.UnityNode
{
    [Preserve]
    [IncludeInSettings(true)]
    [Inspectable]
    public struct SimuPactServer
    {
        [Serialize] [Inspectable] public string ServerName;

        [Serialize] [Inspectable] public string ServerIP;

        [Serialize] [Inspectable] public int ServerPort;

        [Serialize] [Inspectable] public string ServerDescription;

        [Serialize] [Inspectable] public bool IsConnected;

        [Serialize] [Inspectable] public Simupactcommunication.SimupactStateEnum ServerState;

        public Server.ServerClient server;
        [Serialize] [Inspectable] public List<SnapData> SnapInformationList;

        [Serialize] [Inspectable] public Dictionary<string, Dictionary<string,SimuPactVariable>> PropertyIDDictionary;
    }
}