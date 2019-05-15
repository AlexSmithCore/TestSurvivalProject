using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    void Awake() {
        instance = this;    
    }

    private float vertical;
    private float horizontal;

    public float v
    {
        get{return vertical;}
        private set{vertical = value;}
    }

    public float h
    {
        get{return horizontal;}
        private set{horizontal = value;}
    }

    void Update(){
        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");
    }
}
