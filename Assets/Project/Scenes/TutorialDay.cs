using UnityEngine;

public class TutorialDay : MonoBehaviour
{
    public LaneManager laneManager;
    public GameManager gameManager;

    private void Start()
    {
        var delay = gameManager.startDelay;

        Invoke(nameof(TubularBellRemovePriority), delay + 0);

        Invoke(nameof(TubularBellAddPriority), delay + 45);
        Invoke(nameof(TubularBellRemovePriority), delay + 70);

        Invoke(nameof(TubularBellAddPriority), delay + 145);
        Invoke(nameof(TubularBellRemovePriority), delay + 170);
    }

    private void TubularBellRemovePriority()
    {
        if (laneManager.instrumentName.Equals("Tubular Bell"))
        {
            laneManager.priorityModifier = 0;
            Debug.LogWarning("Priority removed");
        }
    }

    private void TubularBellAddPriority()
    {
        if (laneManager.instrumentName.Equals("Tubular Bell"))
        {
            laneManager.priorityModifier = 2;
            Debug.LogWarning("Priority added back");
        }
    }
}