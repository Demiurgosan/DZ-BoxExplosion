using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _impulseForceMin = 5f;
    [SerializeField] private float _impulseForceMax = 10f;

    public void Initiate(Cube[] newCubes)
    {
        Vector3 impulseDirection;

        foreach (Cube cube in newCubes)
        {
            if (cube.gameObject.TryGetComponent(out Rigidbody rigidbody))
            {
                impulseDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f))
               * Random.Range(_impulseForceMin, _impulseForceMax);
                rigidbody.AddForce(impulseDirection, ForceMode.Impulse);
            }
        }
    }
}
