using UnityEditor;
using UnityEngine;

public class TutorialNight : MonoBehaviour
{
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
    }
}