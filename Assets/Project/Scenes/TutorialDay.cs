using System.Linq;
using UnityEditor;
using UnityEngine;

public class TutorialDay : MonoBehaviour
{
    public GameObject laneManager;

    private LaneManager[] _lanes;
    private float _priorityModifier;

    private void Start()
    {
        _lanes = laneManager.GetComponents<LaneManager>();
        GameManager.Instance.Callback = QuitGame;

        InvokeTimedLogic();
    }

    private static void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void InvokeTimedLogic()
    {
        var delay = GameManager.Instance.startDelay;

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
        foreach (var lane in _lanes.Where(lane => lane.instrumentName.Equals("Tubular Bell")))
        {
            _priorityModifier = lane.priorityModifier;
            lane.priorityModifier = 0;

            Debug.LogWarning($"Priority removed, but stored as {_priorityModifier}");
        }
    }

    private void TubularBellAddPriority()
    {
        foreach (var lane in _lanes.Where(lane => lane.instrumentName.Equals("Tubular Bell")))
        {
            lane.priorityModifier = _priorityModifier;
            _priorityModifier = 0;

            Debug.LogWarning($"Priority added back, cleared in class to be {_priorityModifier}");
        }
    }
}