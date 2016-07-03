using UnityEngine;
using System.Collections;

public class DamageTakingAbility : MonoBehaviour {
    private bool immune = false;
    [SerializeField]
    public float health;

    public float currentHealth;

    private Animator anim;

    void Start() {
        currentHealth = health;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float damage) {
        if (!immune && currentHealth > 0) {
            anim.SetTrigger("hit");
            currentHealth -= damage;
            if (currentHealth <= 0f) {
                Die();
            } else {
                StartCoroutine(MakeImmune(3f));
            }
        }
    }

    // TODO NOT WORKING
    private IEnumerator MakeImmune(float time) {
        immune = true;
        yield return new WaitForSeconds(time);
        immune = false;
    }

    private IEnumerator DoRespawn() {
        yield return new WaitForSeconds(2f);
        GetComponent<Respawnable>().Respawn();
    }

    public void Die() {
        anim.SetBool("dead", true);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        StartCoroutine(DoRespawn());
    }
}
