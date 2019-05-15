using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    public static CursorManager instance;

    private void Awake() {
        instance = this;
    }

    public Player.PlayerController target;

    public Transform crosshair;

    private InputManager inputs;

    private void Start() {
        Cursor.visible = false;
        inputs = InputManager.instance;
    }

    private void Update() {
        if(target._run){
            crosshair.gameObject.SetActive(false);
            return;
        }

        crosshair.gameObject.SetActive(true);

        crosshair.transform.position = Input.mousePosition;
        crosshair.eulerAngles += Vector3.forward * 32f * Time.deltaTime;

        float inputFactor = (new Vector3(inputs.h,inputs.v,0)).magnitude / 3;
        crosshair.localScale = new Vector3(1 + inputFactor,1 + inputFactor,0);
    }
}
