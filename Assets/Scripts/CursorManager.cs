using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    public static CursorManager instance;

    private void Awake() {
        instance = this;
    }

    public Player.PlayerController target;

    public Image crosshair;
    public Color cursorAlpha;

    private InputManager inputs;

    private float alphaTo;

    private void Start() {
        Cursor.visible = false;
        inputs = InputManager.instance;
    }

    private void Update() {
        crosshair.gameObject.SetActive(!UIManager.instance.pause);

        cursorAlpha = crosshair.color;

        cursorAlpha.a = LerpValue.SmoothRealtime(cursorAlpha.a, alphaTo, 12f);
        crosshair.color = cursorAlpha;

        if(target._run){
            alphaTo = 0f;
            return;
        } else {
            alphaTo = 1f;
        }

        crosshair.transform.position = Input.mousePosition;
        crosshair.transform.eulerAngles += Vector3.forward * 32f * Time.deltaTime;

        float inputFactor = (new Vector3(inputs.h,inputs.v,0)).magnitude / 2;
        crosshair.transform.localScale = new Vector3(1 + inputFactor,1 + inputFactor,0);
    }
}


