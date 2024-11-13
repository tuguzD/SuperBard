using UnityEngine;

// ReSharper disable once InconsistentNaming
[AddComponentMenu("Scripts/Drum Set Lane (Primary for 'Return Of Slime')")]
public class RtS_Pri_DrumSet_LaneManager : LaneManager
{
    private float _storedPriority;

    private new void Start()
    {
        base.Start();
        var delay = gameManager.startDelay;

        Invoke(nameof(ReduceBasicPriority),
            delay + (52f - 1f));
        Invoke(nameof(RevertBasicPriority),
            delay + (98f - 3f));

        Invoke(nameof(ReduceBasicPriority),
            delay + (178f - 0.5f));
        Invoke(nameof(RevertBasicPriority),
            delay + (225f - 3f));
    }

    private void ReduceBasicPriority()
    {
        _storedPriority = priorityModifier;
        priorityModifier *= 0.625f;

        Debug.LogWarning(
            $"Priority of {instrument} removed, but stored as {_storedPriority}");
    }

    private void RevertBasicPriority()
    {
        priorityModifier = _storedPriority;
        _storedPriority = 0f;

        Debug.LogWarning(
            $"Priority of {instrument} added back, cleared in class to be {_storedPriority}");
    }
}