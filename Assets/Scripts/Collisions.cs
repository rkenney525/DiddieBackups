using UnityEngine;
using System.Collections;

public class Collisions : MonoBehaviour {

    static Collisions() {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerProjectile"), LayerMask.NameToLayer("PlayerHittable"), false);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerProjectile"), LayerMask.NameToLayer("Default"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("ComputerProjectile"), LayerMask.NameToLayer("ComputerHittable"), false);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("ComputerProjectile"), LayerMask.NameToLayer("Default"), true);

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerHittable"), LayerMask.NameToLayer("ComputerHittable"), false);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerProjectile"), LayerMask.NameToLayer("ComputerProjectile"), true);
    }
}
