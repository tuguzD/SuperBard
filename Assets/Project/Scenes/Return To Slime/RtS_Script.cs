using UnityEditor;
using UnityEngine;

// ReSharper disable once InconsistentNaming
public class RtS_Script : MonoBehaviour
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