using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D other)
    {
        other.gameObject.transform.localScale = Vector3.Lerp(
            other.gameObject.transform.localScale, Vector3.zero, Time.deltaTime * 2);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<CollisionManager>()) return;

        Destroy(other.gameObject);
    }
}