using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerControlAbility))]
public class PlayerInputController : MonoBehaviour {

    private PlayerControlAbility playerControl;
    private bool jump = false;
    private bool jumpPrep = false;
    private Camera cam;

    // Use this for initialization
    void Start() {
        cam = FindObjectOfType<Camera>();
        playerControl = GetComponent<PlayerControlAbility>();
    }

    // Update is called once per frame
    void Update() {
        bool input = Input.GetKeyDown("space");
        if (jumpPrep && !input) {
            jump = true;
            jumpPrep = false;
        } else if (!jumpPrep && input) {
            jumpPrep = true;
        }
    }

    void FixedUpdate() {
        // Pass all parameters to the character control script.
        float horizontal = Input.GetAxis("Horizontal");
        playerControl.NormalMove(horizontal, jump);
        jump = false;

        if (Input.GetMouseButtonDown(0)) {
            playerControl.ThrowAxe(cam.WorldToViewportPoint(transform.position),
                cam.ScreenToViewportPoint(Input.mousePosition));
        }
    }
}