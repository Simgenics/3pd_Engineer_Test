using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RotateStateReadout : MonoBehaviour
{
    private enum Units { Degrees, Percentage }

    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private RotateState state;

    [SerializeField]
    private Units units;

    private float displayValue = 0;
    private bool customDisplayValueSet = false;

    private void Awake()
    {
        state.WhileSelecting += DisplayRotation;
    }

    private void OnDestroy()
    {
        state.WhileSelecting -= DisplayRotation;
    }

    public void SetDisplayValue(float displayValue)
    {
        this.displayValue = displayValue;
        customDisplayValueSet = true;
    }

    private void DisplayRotation(float currentAngle)
    {
        if (units == Units.Degrees)
        {
            float angle = customDisplayValueSet ? displayValue : currentAngle;
            text.text = $"{angle:F0}°";
        }
        else
        {
            float percent = customDisplayValueSet ? displayValue : state.AngleAsPercent(currentAngle);
            text.text = $"{percent:F0}%";
        }
    }
}
