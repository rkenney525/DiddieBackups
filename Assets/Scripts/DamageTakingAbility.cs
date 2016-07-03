using UnityEngine;
using System.Collections;

public class DamageTakingAbility : MonoBehaviour {
    private bool immune = false;
    [SerializeField]
    public float health;

    private Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float damage) {
        if (!immune) {
            anim.SetTrigger("hit");
            health -= damage;
            if (health <= 0f) {
                Die();
            } else {
                MakeImmune(3f);
            }
        }
    }

    // TODO NOT WORKING
    private IEnumerator MakeImmune(float time) {
        immune = true;
        yield return new WaitForSeconds(time);
        immune = false;
    }

    public void Die() {
        anim.SetBool("dead", true);
        // TODO respawn
    }
}
