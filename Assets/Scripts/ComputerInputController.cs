using UnityEngine;
using System.Collections;

public class ComputerInputController : MonoBehaviour {

    private ComputerControlScript control;
    private bool jump = false;
    private bool jumpPrep = false;
    private Camera cam;

    // Use this for initialization
    void Start() {
        cam = FindObjectOfType<Camera>();
        control = GetComponent<ComputerControlScript>();
    }

    // Update is called once per frame
    void Update() {
        bool input = Input.GetButtonDown("JoyJump");
        if (jumpPrep && !input) {
            jump = true;
            jumpPrep = false;
        } else if (!jumpPrep && input) {
            jumpPrep = true;
        }
    }

    void FixedUpdate() {
        // Pass all parameters to the character control script.
        float horizontal = Input.GetAxis("JoyLeftHorizontal");
        control.NormalMove(horizontal, jump);
        jump = false;

        float lightningX = Input.GetAxis("JoyRightHorizontal");
        float lightningY = Input.GetAxis("JoyRightVertical");

        control.ShootLightning(new Vector2(lightningX, lightningY));
    }
}
