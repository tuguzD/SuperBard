using UnityEngine;

public class TutorialDay : MonoBehaviour
{
    public GameObject laneManager;
    public GameManager game;

    private LaneManager[] _lanes;

    private void Start()
    {
        _lanes = laneManager.GetComponents<LaneManager>();
        var delay = game.startDelay;

        Invoke(nameof(TubularBellRemovePriority),
            delay + 0);

        Invoke(nameof(TubularBellAddPriority),
            delay + 45);
        Invoke(nameof(TubularBellRemovePriority),
            delay + 70);

        Invoke(nameof(TubularBellAddPriority),
            delay + 145);
        Invoke(nameof(TubularBellRemovePriority),
            delay + 170);
    }

    private void TubularBellRemovePriority()
    {
        foreach (var lane in _lanes)
            if (lane.instrumentName.Equals("Tubular Bell"))
            {
                lane.priorityModifier = 0;
                Debug.LogWarning("Priority removed");
            }
    }

    private void TubularBellAddPriority()
    {
        foreach (var lane in _lanes)
            if (lane.instrumentName.Equals("Tubular Bell"))
            {
                lane.priorityModifier = 1.5f;
                Debug.LogWarning("Priority added back");
            }
    }
}