using UnityEngine;

public class Exploder : MonoBehaviour
{
    private float _forceExplosion = 100f;
    private float _radiusExplosion = 100f;

    public void Explode(Rigidbody rigidbody, Transform transformObject)
    {
        rigidbody.AddExplosionForce(_forceExplosion, transformObject.position, _radiusExplosion);
    }
}
