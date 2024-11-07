using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialDay : MonoBehaviour
{
    private static void NextLevel()
    {
        SceneManager.LoadScene("Tutorial Night");
    }

    private void Start()
    {
        GameManager.Instance.Callback = NextLevel;
    }
}