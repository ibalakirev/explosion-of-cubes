using UnityEngine;

public class Exploder : MonoBehaviour
{
    private float _forceExplosionSeparation = 100f;
    private float _radiusExplosionSeparation = 10f;
    private float _forceUltimateExplosion = 200f;
    private float _radiusUltimateExplosion = 10f;
    private float _multiplier = 1.5f;
    private float _minValueForce = 0f;

    public bool IsPositiveValueForceUltimateExplosion => _forceUltimateExplosion > _minValueForce;

    public void ExplodeAllCubesInRadius(Transform transformObject)
    {
        float maxDistance = 100f;

        Collider[] overlappedColliders = Physics.OverlapSphere(transformObject.position, _radiusExplosionSeparation);

        for (int i = 0; i < overlappedColliders.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, overlappedColliders[i].transform.position);

            Rigidbody rigidbody = overlappedColliders[i].attachedRigidbody;

            if (rigidbody != null)
            {
                {
                    if (distance < maxDistance)
                    {
                        ReduceForceUltimateExplosion();

                        maxDistance = distance;
                    }

                    Explode(rigidbody, _forceUltimateExplosion, transformObject, _radiusUltimateExplosion);
                }
            }
        }
    }

    public void ExplodeCurrentObject(Rigidbody rigidbody, Transform transformObject)
    {
        Explode(rigidbody, _forceExplosionSeparation, transformObject, _radiusExplosionSeparation);
    }

    public void IncreaseForceUltimateExplosion()
    {
        _forceUltimateExplosion *= _multiplier;
    }

    public void IncreaseRadiusUltimateExplosion()
    {
        _radiusUltimateExplosion *= _multiplier;
    }

    private void ReduceForceUltimateExplosion()
    {
        float divider = 100f;
        float percent = 20f;
        float valueResutl;

        valueResutl = _forceUltimateExplosion * percent / divider;

        if (IsPositiveValueForceUltimateExplosion)
        {
            _forceUltimateExplosion -= valueResutl;
        }
    }

    private void Explode(Rigidbody rigidbody, float force, Transform transformObject, float radius)
    {
        rigidbody.AddExplosionForce(force, transformObject.position, radius);
    }
}
