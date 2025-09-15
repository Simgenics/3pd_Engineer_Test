using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

public class PullState : InteractableState
{
    [SerializeField]
    private enum Axis { X, Y, Z };

    [SerializeField]
    [Tooltip("The min limit is the current transform position on the defined axis. " +
        "The max limit is the minimum limit + this value.")]
    private float unitsUntilLimit = 1;

    private Vector3 minLimit, maxLimit;

    // TO DO: Add in world-based options
    private bool useLocal = true;

    [SerializeField]
    private Axis axis = Axis.Z;

    [SerializeField]
    private Transform attachTransform;

    private PullGrabTransformer pullGrabTransformer;
    public Vector3 MinLimit() => minLimit;
    public Vector3 MaxLimit() => maxLimit;

    public float UnitsUntilLimit() => unitsUntilLimit;

    private bool positiveDirection;

    private Vector3 lastPosition;
    protected override void Initialize()
    {
        Interactable.selectExited.AddListener(CheckPosition);
        Interactable.hoverEntered.AddListener(OnPositionChanged);

        positiveDirection = unitsUntilLimit > 0;
        // gameObject.AddComponent<PC_PullState>();
    }
    private void OnDestroy()
    {
        Interactable?.selectExited.RemoveListener(CheckPosition);
        Interactable.hoverEntered.RemoveListener(OnPositionChanged);
    }

    protected override void AddComponents()
    {
        base.AddComponents();

        // Rigidbody
        Rigidbody rb = GetComponent<Rigidbody>() ? GetComponent<Rigidbody>() : gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;

        // Interactable
        XRGrabInteractable interactable = GetComponent<XRGrabInteractable>()
            ? GetComponent<XRGrabInteractable>()
            : gameObject.AddComponent<XRGrabInteractable>();        

        SetInteractable(interactable);

        interactable.trackRotation = false;

        if (!attachTransform)
        {
            interactable.useDynamicAttach = true;
        }
        attachTransform = attachTransform ? attachTransform : transform;
        interactable.attachTransform = attachTransform;
        interactable.secondaryAttachTransform = attachTransform;

        // Transformer
        pullGrabTransformer = GetComponent<PullGrabTransformer>()
            ? GetComponent<PullGrabTransformer>()
            : gameObject.AddComponent<PullGrabTransformer>();

        pullGrabTransformer.constrainedAxisDisplacementMode = useLocal 
            ? PullGrabTransformer.ConstrainedAxisDisplacementMode.ObjectRelative
            : PullGrabTransformer.ConstrainedAxisDisplacementMode.WorldAxisRelative;
        
        pullGrabTransformer.permittedDisplacementAxes = axis switch
        {
            Axis.X => PullGrabTransformer.ManipulationAxes.X,
            Axis.Y => PullGrabTransformer.ManipulationAxes.Y,
            _ => PullGrabTransformer.ManipulationAxes.Z,
        };
    }

    private void OnPositionChanged(HoverEnterEventArgs args)
    {
        if (lastPosition != transform.position)
        {
            CalculateLimits();
        }
    }

    private void CalculateLimits()
    {
        float startAxisPos = PositionOnWorldAxis(transform.position);
        float endAxisPos = startAxisPos + unitsUntilLimit;

        // If it's already at its max limit, it should move backwards from here
        if (ProgressTracker.StateEquals(Progress.End))
        {
            endAxisPos = startAxisPos - unitsUntilLimit;
        }

        Vector3 minAxisLimit = LocalAxis() * startAxisPos;
        Vector3 maxAxisLimit = LocalAxis() * endAxisPos;
        
        Vector3 relativeMin = minAxisLimit + (transform.position - minAxisLimit);
        Vector3 relativeMax = maxAxisLimit + (transform.position - minAxisLimit);

        minLimit = relativeMin;
        maxLimit = relativeMax;        
    }
    public override void Set()
    {
        base.Set();

        if (ProgressTracker.StateEquals(Progress.None))
        {
            CalculateLimits();
        }

        if (!positiveDirection && ProgressTracker.StateEquals(Progress.End) && unitsUntilLimit < 0
            || !positiveDirection && ProgressTracker.StateEquals(Progress.Start) && unitsUntilLimit > 0)
        {
            unitsUntilLimit *= -1;
            CalculateLimits();
        }

        lastPosition = transform.position;
    }

    public Vector3 ClampPoint(Vector3 point)
    {
        return ClampProjection(ProjectPoint(point));
    }

    public Vector3 ProjectPoint(Vector3 point)
    {
        return minLimit + Vector3.Project(point - minLimit, maxLimit - minLimit);
    }

    private Vector3 ClampProjection(Vector3 point)
    {
        var toStart = (point - minLimit).sqrMagnitude;
        var toEnd = (point - maxLimit).sqrMagnitude;
        var segment = (minLimit - maxLimit).sqrMagnitude;
        if (toStart > segment || toEnd > segment) return toStart > toEnd ? maxLimit : minLimit;
        return point;
    }

    private float PositionOnWorldAxis(Vector3 position)
    {
        return axis switch
        {
            Axis.X => position.x,
            Axis.Y => position.y,
            _ => position.z,
        };
    }

    private float PositionOnLocalAxis(Vector3 position)
    {
        return Vector3.Dot(position, LocalAxis());
    }

    private Vector3 LocalAxis()
    {
        return axis switch
        {
            Axis.X => transform.right,
            Axis.Y => transform.up,
            _ => transform.forward,
        };
    }

    private void CheckPosition(SelectExitEventArgs args)
    {
        float current = PositionOnLocalAxis(transform.position);
        float min = PositionOnLocalAxis(minLimit);
        float max = PositionOnLocalAxis(maxLimit);

        if (min < max)
        {
            if (Mathf.Approximately(current, min))
            {
                ProgressTracker.SetState(Progress.Start);
            }
            else if (Mathf.Approximately(current, max))
            {
                ProgressTracker.SetState(Progress.End);
            }
            else
            {
                ProgressTracker.SetState(Progress.Middle);
            }
        }

        if (min > max)
        {
            if (Mathf.Approximately(current, min))
            {
                ProgressTracker.SetState(Progress.End);
            }
            else if (Mathf.Approximately(current, max))
            {
                ProgressTracker.SetState(Progress.Start);
            }
            else
            {
                ProgressTracker.SetState(Progress.Middle);
            }
        }

        lastPosition = transform.position;
    }
}
