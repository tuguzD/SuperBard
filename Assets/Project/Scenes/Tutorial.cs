using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public LaneManager laneManager;

    private void Start()
    {
        Invoke(nameof(TubularBellRemovePriority), 0);

        Invoke(nameof(TubularBellAddPriority), 45);
        Invoke(nameof(TubularBellRemovePriority), 70);

        Invoke(nameof(TubularBellAddPriority), 145);
        Invoke(nameof(TubularBellRemovePriority), 170);
    }

    private void TubularBellRemovePriority()
    {
        if (laneManager.priorityModifier.Equals(1.5f))
            laneManager.priorityModifier = 0.0f;
    }

    private void TubularBellAddPriority()
    {
        if (laneManager.priorityModifier.Equals(0.0f))
            laneManager.priorityModifier = 1.5f;
    }
}