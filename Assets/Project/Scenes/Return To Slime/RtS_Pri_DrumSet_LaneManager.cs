using UnityEngine;

// ReSharper disable once InconsistentNaming
[AddComponentMenu("Scripts/Drum Set Lane (Primary for 'Return Of Slime')")]
public class RtS_Pri_DrumSet_LaneManager : LaneManager
{
    private float _storedPriority;
    
    private new void Start()
    {
        var delay = gameManager.startDelay;
        base.Start();
    
        Invoke(nameof(RemoveBasicPriority),
            delay + 51f);
        Invoke(nameof(AddBackBasicPriority),
            delay + 97.5f);
        
        // Invoke(nameof(RemoveBasicPriority),
        //     delay + 51f);
        // Invoke(nameof(AddBackBasicPriority),
        //     delay + 97.5f);
    }
    
    private void RemoveBasicPriority()
    {
        _storedPriority = priorityModifier;
        priorityModifier = 0.75f;
    
        Debug.LogError(
            $"Priority removed, but stored as {_storedPriority}");
    }
    
    private void AddBackBasicPriority()
    {
        priorityModifier = _storedPriority;
        _storedPriority = 0;
    
        Debug.LogError(
            $"Priority added back, cleared in class to be {_storedPriority}");
    }
}