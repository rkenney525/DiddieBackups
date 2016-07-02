using UnityEngine;
using System.Collections;

public class GroundDetectionScript : MonoBehaviour {

    // Properties
    [SerializeField]
    private LayerMask WhatIsGround;

    // Attributes
    private bool grounded = false;
    public bool Grounded {
        get { return this.grounded; }
    }
    private Collider2D detect;
    //private Animator anim;

    // Use this for initialization
    void Start() {
        this.detect = this.GetComponent<Collider2D>();
        //this.anim = this.transform.root.GetComponent<Animator>();
    }

    private void FixedUpdate() {
        this.grounded = this.detect.IsTouchingLayers(WhatIsGround);
        this.UpdateAnimation();
    }

    private void UpdateAnimation() {
        //if (this.anim != null) {
            //this.anim.SetBool("InAir", !this.grounded);
        //}
    }
}
