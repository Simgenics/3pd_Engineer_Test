using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace ThreeDPactXR.SimuPact.UnityNode
{
    [UnitCategory("3D Pact/SimuPact/Split Data")]
    public class SpliteConectionVariables : Unit
    {
        [DoNotSerialize] // No need to serialize ports.
        public ValueInput ConnectionVariable;

        [DoNotSerialize] // No need to serialize ports.
        public ValueOutput PropertyName;

        [DoNotSerialize] // No need to serialize ports.
        public ValueOutput PropertyID;

        [DoNotSerialize]
        public ValueOutput ConectedGameObject;
        
        [DoNotSerialize] // No need to serialize ports.
        public ValueOutput DataType;

        protected override void Definition() //The method to set what our node will be doing.
        {
            ConnectionVariable = ValueInput<SimuPactVariable>("ConnectionVariable");

            PropertyName = ValueOutput<string>("PropertyName", (flow) => flow.GetValue<SimuPactVariable>(ConnectionVariable).PropertyName);
            PropertyID = ValueOutput<string>("PropertyID", (flow) => flow.GetValue<SimuPactVariable>(ConnectionVariable).PropertyID);
            ConectedGameObject = ValueOutput<GameObject>("ConectedGameObject", (flow) => flow.GetValue<SimuPactVariable>(ConnectionVariable).PropertyGameObject);
            DataType = ValueOutput<SimuPactVariable.VarType>("DataType", (flow) => flow.GetValue<SimuPactVariable>(ConnectionVariable).DataType);
        }
        
    }
}