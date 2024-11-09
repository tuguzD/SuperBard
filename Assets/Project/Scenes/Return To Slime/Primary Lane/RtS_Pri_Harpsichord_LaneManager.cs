using UnityEngine;

// ReSharper disable once InconsistentNaming
[AddComponentMenu("Scripts/Harpsichord Lane (Primary for 'Return Of Slime')")]
public class RtS_Pri_Harpsichord_LaneManager : LaneManager
{
    private float _storedPriority;

    private new void Start()
    {
        base.Start();
        var delay = gameManager.startDelay;
        Invoke(nameof(RemoveBasicPriority), 0f);

        Invoke(nameof(AddBackBasicPriority),
            delay + (52f - 1f));
        Invoke(nameof(RemoveBasicPriority),
            delay + (98f - 1f));

        Invoke(nameof(AddBackBasicPriority),
            delay + (178f - 0.5f));
        Invoke(nameof(RemoveBasicPriority),
            delay + (225f - 1f));
    }

    private void RemoveBasicPriority()
    {
        _storedPriority = priorityModifier;
        priorityModifier = 0f;

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