using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    public static CursorManager instance;

    private void Awake() {
        instance = this;
    }

    public Transform crosshair;
    public Player.CameraMovement cm;

    private void Start() {
        Cursor.visible = false;
    }

    private void Update() {
        crosshair.transform.position = Input.mousePosition;
        RotateCursor(cm.target.transform.position);
    }

    public void RotateCursor(Vector3 playerPos){
        Vector3 playerOnScreen = cm.GetComponent<Camera>().WorldToScreenPoint(playerPos);
    }
}
