using UnityEngine;
using System.Collections;

public class Respawnable : MonoBehaviour {
    public void Respawn() {
        transform.position = Vector2.zero;
        DamageTakingAbility dta = GetComponent<DamageTakingAbility>();
        dta.health = dta.currentHealth;
    }

    void Update() {
        if (transform.position.y < -10) {
            Respawn();
        }
    }
}
