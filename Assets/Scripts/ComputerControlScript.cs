﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Responsible for moving the computer instance, storing info, and handling being hacked
/// </summary>
public class ComputerControlScript : MonoBehaviour {

    [SerializeField]
    public float Speed;
    [SerializeField]
    public Vector2 Bound1;
    [SerializeField]
    public Vector2 Bound2;

    float journeyLength;

    private Rigidbody2D rigidbody2d;

    private Animator anim;

    private float startTime;

    private float direction = 1f;

    private bool stopped = false;

    private Vector2 from;
    private Vector2 to;

    private float health = 10.0f;

    bool immune = false;

    void Start() {
        rigidbody2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        startTime = Time.time;
        journeyLength = Vector3.Distance(Bound1, Bound2);
        from = Bound1;
        to = Bound2;
    }

    private void Switch() {
        StartCoroutine(SwitchSupplier(2.0f));
    }

    private void Stop() {
        StartCoroutine(Wait(2.0f));
    }

    private IEnumerator SwitchSupplier(float time) {
        stopped = true;
        yield return new WaitForSeconds(time);
        Vector2 temp = to;
        to = from;
        from = temp;
        startTime = Time.time;
        stopped = false;
    }

    // Update is called once per frame
    void Update() {
        bool moving = false;
        if (!stopped) {
            moving = true;

            if (Mathf.Abs(transform.position.x - to.x) < 0.05f) {
                Switch();
                return;
            }

            Vector2 velocity = new Vector2(Speed, rigidbody2d.velocity.y);
            if (transform.position.x > to.x) {
                velocity.x *= -1;
            }

            rigidbody2d.velocity = velocity;

        }
        anim.SetBool("moving", moving);
    }

    public void TakeDamage(float damage) {
        if (!immune) {
            anim.SetTrigger("hit");
            health -= damage;
            if (health <= 0f) {
                Die();
            } else {
                Stop();
            }
        }
    }

    private IEnumerator Wait(float time) {
        stopped = true;
        immune = true;
        yield return new WaitForSeconds(time);
        immune = false;
        stopped = false;
    }

    public void Die() {
        anim.SetBool("dead", true);
        rigidbody2d.constraints = RigidbodyConstraints2D.FreezeAll;
        stopped = true;
    }
}
