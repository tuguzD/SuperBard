using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    
    private static void NextLevel()
    {
        SceneManager.LoadScene("TToaCW (Tutorial #1)");
    }

    private void Start()
    {
        GameManager.Instance.EndAction += NextLevel;
    }
}