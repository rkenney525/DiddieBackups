using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerControlAbility))]
public class PlayerInputController : MonoBehaviour {

    private PlayerControlAbility movementController;
    private bool jump = false;
    private bool jumpPrep = false;

    // Use this for initialization
    void Start() {
        this.movementController = GetComponent<PlayerControlAbility>();
    }

    // Update is called once per frame
    void Update() {
        bool input = Input.GetKeyDown("space");
        if (this.jumpPrep && !input) {
            this.jump = true;
            this.jumpPrep = false;
        } else if (!this.jumpPrep && input) {
            this.jumpPrep = true;
        }
    }

    void FixedUpdate() {

        // Pass all parameters to the character control script.
        float horizontal = Input.GetAxis("Horizontal");
        this.movementController.NormalMove(horizontal, this.jump);
        this.jump = false;
    }
}