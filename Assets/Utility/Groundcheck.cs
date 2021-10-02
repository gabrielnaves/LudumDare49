using UnityEngine;

public class Groundcheck : MonoBehaviour {

    [ViewOnly] public bool onGround;
    [ViewOnly] public Collider2D floor;

    public Vector3 position { get { return transform.position; } }

    void OnTriggerEnter2D(Collider2D collision) {
        onGround = true;
        floor = collision;
    }

    void OnTriggerStay2D(Collider2D collision) {
        onGround = true;
        floor = collision;
    }

    void OnTriggerExit2D(Collider2D collision) {
        onGround = false;
    }

    void LateUpdate() {
        if (floor == null)
            onGround = false;
        else if (!floor.isActiveAndEnabled)
            onGround = false;
    }
}
