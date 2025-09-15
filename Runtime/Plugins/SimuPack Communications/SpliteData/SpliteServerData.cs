using System.Collections.Generic;
using Unity.VisualScripting;
using SimupactComs;

namespace ThreeDPactXR.SimuPact.UnityNode
{
    
    [UnitCategory("3D Pact/SimuPact/Split Data")]
    public class SpliteServerData : Unit
    {
        
        [DoNotSerialize] // No need to serialize ports.
        public ValueInput ServerData;
        
        [DoNotSerialize] // No need to serialize ports.
        public ValueOutput ServerName;
        
        [DoNotSerialize] // No need to serialize ports.
        public ValueOutput ServerIP;
        
        [DoNotSerialize] // No need to serialize ports.
        public ValueOutput ServerPort;
        
        [DoNotSerialize] // No need to serialize ports.
        public ValueOutput ServerDescription;
        
        [DoNotSerialize] // No need to serialize ports.
        public ValueOutput IsConnected;
        
        [DoNotSerialize] // No need to serialize ports.
        public ValueOutput ServerState;
        
        [DoNotSerialize] // No need to serialize ports.
        public ValueOutput SnapInformationList;
        
        [DoNotSerialize] // No need to serialize ports.
        public ValueOutput PropertyIDDictionary;
        
        protected override void Definition() //The method to set what our node will be doing.
        {
            ServerData = ValueInput<SimuPactServer>("ServerData");
            
            ServerName = ValueOutput<string>("ServerName", (flow) => flow.GetValue<SimuPactServer>(ServerData).ServerName);
            ServerIP = ValueOutput<string>("ServerIP", (flow) => flow.GetValue<SimuPactServer>(ServerData).ServerIP);
            ServerPort = ValueOutput<int>("ServerPort", (flow) => flow.GetValue<SimuPactServer>(ServerData).ServerPort);
            ServerDescription = ValueOutput<string>("ServerDescription", (flow) => flow.GetValue<SimuPactServer>(ServerData).ServerDescription);
            IsConnected = ValueOutput<bool>("IsConnected", (flow) => flow.GetValue<SimuPactServer>(ServerData).IsConnected);
            ServerState = ValueOutput<Simupactcommunication.SimupactStateEnum>("ServerState", (flow) => flow.GetValue<SimuPactServer>(ServerData).ServerState);
            SnapInformationList = ValueOutput<List<SnapData>>("SnapInformationList", (flow) => flow.GetValue<SimuPactServer>(ServerData).SnapInformationList);
            PropertyIDDictionary = ValueOutput<Dictionary<string, Dictionary<string,SimuPactVariable>>>("PropertyIDDictionary", (flow) => flow.GetValue<SimuPactServer>(ServerData).PropertyIDDictionary);
        }
    }
}