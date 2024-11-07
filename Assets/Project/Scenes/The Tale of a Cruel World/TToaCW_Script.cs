using UnityEngine;
using UnityEngine.SceneManagement;

// ReSharper disable once InconsistentNaming
public class TToaCW_Script : MonoBehaviour
{
    private static void NextLevel()
    {
        SceneManager.LoadScene("RtS (Tutorial #2)");
    }

    private void Start()
    {
        GameManager.Instance.Callback = NextLevel;
    }
}