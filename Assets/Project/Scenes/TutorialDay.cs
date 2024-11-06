using UnityEngine;

public class TutorialDay : MonoBehaviour
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
        if (laneManager.priorityModifier.Equals(2))
            laneManager.priorityModifier = 0;
    }

    private void TubularBellAddPriority()
    {
        if (laneManager.priorityModifier.Equals(0))
            laneManager.priorityModifier = 2;
    }
}