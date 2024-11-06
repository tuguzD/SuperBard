using UnityEngine;
using UnityEngine.UI;

public class IndicatorManager : MonoBehaviour
{
    public Image image;
    public LaneManager lane;

    private void Update()
    {
        if (lane.input.WasReleasedThisFrame())
            image.color = new Color(1, 1, 1, 1);
        
        if (lane.input.WasPressedThisFrame()) 
            image.color = new Color(1, 1, 1, 0.35f);
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