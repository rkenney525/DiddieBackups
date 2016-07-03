using UnityEngine;
using System.Collections;

public class Collisions : MonoBehaviour {

    static Collisions() {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerProjectile"), LayerMask.NameToLayer("PlayerHittable"), false);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerProjectile"), LayerMask.NameToLayer("Default"), true);
    }
}
