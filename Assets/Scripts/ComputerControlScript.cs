using UnityEngine;
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
            float distCovered = (Time.time - startTime) * Speed;
            //float fracJourney = distCovered / journeyLength;
            //if (fracJourney >= 1.0f) {
            //    stopped = true;
            //    Switch();
            //}
            //float tx = Mathf.Lerp(from.x, to.x, fracJourney);
            float tx = transform.position.x + (direction * distCovered);
            transform.position = new Vector3(tx, transform.position.y, transform.position.z);

        }
        anim.SetBool("moving", moving);
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            direction *= 1f;
            Debug.Log("Wall!");
        }
        if (coll.gameObject.layer == LayerMask.NameToLayer("Projectile")) {
            TakeDamage(coll.gameObject.GetComponent<AxeStats>().Damage);
            if (health <= 0f) {
                Die();
            }
        }
    }

    private void TakeDamage(float damage) {
        anim.SetTrigger("hit");
        health -= damage;
        Stop();
    }

    private IEnumerator Wait(float time) {
        stopped = true;
        yield return new WaitForSeconds(time);
        stopped = false;
    }

    private void Die() {
        anim.SetBool("dead", true);
        stopped = true;
    }
}
