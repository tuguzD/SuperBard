using UnityEngine;

// ReSharper disable once InconsistentNaming
[AddComponentMenu("Scripts/Tubular Bell Lane (Secondary for 'The Tale of a Cruel World')")]
public class TToaCW_Sec_TubBell_LaneManager : LaneManager
{
    public GameManager gameManager;
    
    private float _storedPriority;

    protected new void Start()
    {
        var delay = gameManager.startDelay;

        Invoke(nameof(RemovePriority),
            delay + 0);

        Invoke(nameof(AddBackPriority),
            delay + 45);
        Invoke(nameof(RemovePriority),
            delay + 70);

        Invoke(nameof(AddBackPriority),
            delay + 145);
        Invoke(nameof(RemovePriority),
            delay + 170);

        base.Start();
    }

    private void RemovePriority()
    {
        _storedPriority = priorityModifier;
        priorityModifier = 0;

        Debug.LogWarning(
            $"Priority removed, but stored as {_storedPriority}");
    }

    private void AddBackPriority()
    {
        priorityModifier = _storedPriority;
        _storedPriority = 0;

        Debug.LogWarning(
            $"Priority added back, cleared in class to be {_storedPriority}");
    }
}