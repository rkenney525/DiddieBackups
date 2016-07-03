using UnityEngine;
using System.Collections;

/// <summary>
/// Responsible for TTL of the axe and detecting collisions with computers
/// </summary>
public class AxControlAbility : MonoBehaviour {
    /// <summary>
    /// The time, in milliseconds, to wait before destroying self
    /// </summary>
    private readonly float TTL = 2.5f;

    /// <summary>
    /// Kick off the waiting coroutine
    /// </summary>
    void Start() {
        StartCoroutine(Wait());
    }

    /// <summary>
    /// Wait the specified time and then destroy the axe
    /// </summary>
    /// <returns>The instruction to wait</returns>
    private IEnumerator Wait() {
        yield return new WaitForSeconds(TTL);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.layer == LayerMask.NameToLayer("PlayerHittable")) {
            coll.gameObject.GetComponent<ComputerControlScript>().TakeDamage(GetComponent<AxeStats>().Damage);
        }
    }
}
