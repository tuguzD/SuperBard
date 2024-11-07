using UnityEditor;
using UnityEngine;

public class TutorialNight : MonoBehaviour
{
    public GameObject primaryLaneManager;
    private LaneManager[] _primaryLanes;
    private float _primaryPriorityModifier;

    public GameObject secondaryLaneManager;
    private LaneManager[] _secondaryLanes;
    private float _secondaryPriorityModifier;

    private static void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void Start()
    {
        GameManager.Instance.Callback = QuitGame;

        _primaryLanes = primaryLaneManager.GetComponents<LaneManager>();
        _secondaryLanes = secondaryLaneManager.GetComponents<LaneManager>();

        InvokeTimedLogic();
    }

    private void InvokeTimedLogic()
    {
        var delay = GameManager.Instance.startDelay;

        Invoke(nameof(Test),
            delay + 0);
    }

    private void Test()
    {
        Debug.LogWarning("Hello there!");
    }
}