using UnityEngine;
using System.Collections;

public class Respawnable : MonoBehaviour {
    public void Respawn() {
        transform.position = Vector2.zero;
        DamageTakingAbility dta = GetComponent<DamageTakingAbility>();
        dta.health = dta.currentHealth;
        Rigidbody2D lawl = GetComponent<Rigidbody2D>();
        lawl.constraints = RigidbodyConstraints2D.FreezeRotation;
        lawl.velocity = Vector2.zero;
    }

    void Update() {
        if (transform.position.y < -10) {
            Respawn();
        }
    }
}
