using UnityEngine;

// ReSharper disable once InconsistentNaming
[AddComponentMenu("Scripts/Distortion Guitar Lane (Primary for 'Return Of Slime')")]
public class RtS_Pri_DistGuitar_LaneManager : LaneManager
{
    private float _storedPriority;

    private new void Start()
    {
        base.Start();
        var delay = gameManager.startDelay;
        // Invoke(nameof(RemoveBasicPriority), 0f);

        // Invoke(nameof(AddBackBasicPriority),
        //     delay + (???));
        // Invoke(nameof(RemoveBasicPriority),
        //     delay + (98f - 1f));
        //
        // Invoke(nameof(AddBackBasicPriority),
        //     delay + (???));
        // Invoke(nameof(RemoveBasicPriority),
        //     delay + (225f - 1f));
    }
}