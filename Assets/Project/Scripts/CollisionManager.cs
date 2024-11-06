using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<CollisionManager>()) return;
        other.gameObject.GetComponent<NoteManager>()
            .isColliding = true;

        other.gameObject.transform.localScale = Vector3.Lerp(
            other.gameObject.transform.localScale, Vector3.zero, Time.deltaTime * 2);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<CollisionManager>()) return;
        other.gameObject.GetComponent<NoteManager>()
            .isColliding = false;

        Destroy(other.gameObject);
    }
}