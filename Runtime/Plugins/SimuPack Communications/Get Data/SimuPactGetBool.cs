using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using SimupactComs;
using ControlSystemConnection.GrpcGenerated;

namespace ThreeDPactXR.SimuPact.UnityNode
{


    
    [UnitCategory("3D Pact/SimuPact/Get Data")]
    public class SimuPactGetBool : Unit
    {

        [DoNotSerialize] // No need to serialize ports.
        public ControlInput input; //Adding the ControlInput port variable

        [DoNotSerialize] // No need to serialize ports.
        public ControlOutput output;//Adding the ControlOutput port variable.
        
        [DoNotSerialize] // No need to serialize ports.
        public ValueInput ServerData;
        
        [DoNotSerialize] // No need to serialize ports.
        public ValueInput PropertyID;
        
        [DoNotSerialize] // No need to serialize ports.
        public ValueOutput PropertyValue;
        
        private bool Result=false;

        [DoNotSerialize] // No need to serialize ports.
        public ValueInput Debug;
        
        protected override void Definition() //The method to set what our node will be doing.
        {

            ServerData = ValueInput<SimuPactServer>("ServerData");
            input = ControlInput(nameof(input), (flow) =>
            {
                Server.ServerClient serverData = flow.GetValue<SimuPactServer>(ServerData).server;

                Result = Simupactcommunication.GetValueOnceBool( ref serverData ,flow.GetValue<bool>(Debug),
                    flow.GetValue<SimuPactVariable>(PropertyID).PropertyID);
                
                return output;
            });
            output = ControlOutput("output");
            
            PropertyID = ValueInput<SimuPactVariable>("PropertyID");
            PropertyValue = ValueOutput<bool>("PropertyValue" ,(flow) => { return Result; });
            Debug = ValueInput<bool>("Debug", false);
        }
    }
}