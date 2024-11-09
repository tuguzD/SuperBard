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

        Invoke(nameof(RemoveBasicPriority),
            delay + (52 - 1f));
        Invoke(nameof(AddBackBasicPriority),
            delay + (98 - 3f));

        Invoke(nameof(RemoveBasicPriority),
            delay + (178 - 0.5f));
        Invoke(nameof(AddBackBasicPriority),
            delay + (225 - 3f));
    }

    private void RemoveBasicPriority()
    {
        _storedPriority = priorityModifier;
        priorityModifier = 0.625f;

        Debug.LogWarning(
            $"Priority of {instrumentName} removed, but stored as {_storedPriority}");
    }

    private void AddBackBasicPriority()
    {
        priorityModifier = _storedPriority;
        _storedPriority = 0f;

        Debug.LogWarning(
            $"Priority of {instrumentName} added back, cleared in class to be {_storedPriority}");
    }
}