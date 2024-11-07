using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class IndicatorManager : MonoBehaviour
{
    public Image image;

    [Tooltip("Input action to register by indicator")]
    public InputAction input;

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void Update()
    {
        image.color = new Color(
            1, 1, 1, !input.inProgress ? 1.0f : 0.35f);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<IndicatorManager>()) return;
        other.gameObject.GetComponent<NoteManager>()
            .isColliding = true;

        other.gameObject.transform.localScale = Vector3.Lerp(
            other.gameObject.transform.localScale, Vector3.zero, Time.deltaTime * 2);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<IndicatorManager>()) return;
        other.gameObject.GetComponent<NoteManager>()
            .isColliding = false;

        Destroy(other.gameObject);
    }
}