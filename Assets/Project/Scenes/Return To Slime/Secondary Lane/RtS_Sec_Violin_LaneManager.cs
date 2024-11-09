using UnityEngine;

// ReSharper disable once InconsistentNaming
[AddComponentMenu("Scripts/Violin Lane (Secondary for 'Return Of Slime')")]
public class RtS_Sec_Violin_LaneManager : LaneManager
{
    private float _storedPriority;

    private new void Start()
    {
        base.Start();
        // var delay = gameManager.startDelay;
        // Invoke(nameof(RemoveBasicPriority), 0f);
        //
        // Invoke(nameof(AddBackBasicPriority),
        //     delay + (52f - 1f));
        // Invoke(nameof(RemoveBasicPriority),
        //     delay + (98f - 1f));
        //
        // Invoke(nameof(AddBackBasicPriority),
        //     delay + (178f - 0.5f));
        // Invoke(nameof(RemoveBasicPriority),
        //     delay + (225f - 1f));
    }
}