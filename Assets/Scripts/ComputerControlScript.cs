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

    private float startTime;

    private bool stopped = false;

    private Vector2 from;
    private Vector2 to;

    private float health = 10.0f;

    void Start() {
        rigidbody2d = GetComponent<Rigidbody2D>();
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
        if (!stopped) {
            float distCovered = (Time.time - startTime) * Speed;
            float fracJourney = distCovered / journeyLength;
            if (fracJourney >= 1.0f) {
                stopped = true;
                Switch();
            }
            float tx = Mathf.Lerp(from.x, to.x, fracJourney);
            transform.position = new Vector3(tx, transform.position.y, transform.position.z);

        }
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Projectile")) {
            TakeDamage(coll.gameObject.GetComponent<AxeStats>().Damage);
            if (health <= 0f) {
                Die();
            }
        }
    }

    private void TakeDamage(float damage) {
        health -= damage;
        Stop();
    }

    private IEnumerator Wait(float time) {
        stopped = true;
        yield return new WaitForSeconds(time);
        stopped = false;
    }

    private void Die() {
        stopped = true;
    }
}
