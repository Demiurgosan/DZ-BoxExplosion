using UnityEngine;
using Color = UnityEngine.Color;

public class CubeController : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefub;
    [SerializeField] private RaycastTrigger _raycastTrigger;

    private int _scaleReductionPerGeneration = 2;
    private int _splitChanceReductoinPerGeneration = 2;
    private int _dividePartsMin = 2;
    private int _dividePartsMax = 6;
    private int _rollChanceMin = 0;
    private int _rollChanceMax = 100;
    private float _impulseForceMin = 5f;
    private float _impulseForceMax = 10f;

    private void OnEnable()
    {
        _raycastTrigger.CubeDetected += PopCube;
    }

    private void OnDisable()
    {
        _raycastTrigger.CubeDetected -= PopCube;
    }

    private void Start()
    {
        GameObject startCube = Instantiate(_cubePrefub);
        startCube.transform.position = this.transform.position;
    }

    private void PopCube(RaycastHit perentCube)
    {
        Vector3 perentPosition = perentCube.transform.position;
        int perentGeneration = perentCube.collider.gameObject.GetComponent<ExplosiveCube>().Generation;
        Destroy(perentCube.collider.gameObject);

        int spawnCount = Random.Range(_dividePartsMin, _dividePartsMax);
        GameObject[] newCubes = new GameObject[spawnCount];

        if (TryCreateCubes(perentGeneration, ref newCubes))
        {
            InitializeCubes(perentGeneration, perentPosition, ref newCubes);
            KickCubes(ref newCubes);
        }
    }

    private bool TryCreateCubes(int perentGeneration, ref GameObject[] newCubes)
    {
        int currentExplosionChance = (int)((float)_rollChanceMax *
            ((float)_splitChanceReductoinPerGeneration / (float)(perentGeneration * _splitChanceReductoinPerGeneration)));
        int chanceRoll = Random.Range(_rollChanceMin, _rollChanceMax);

        if (chanceRoll <= currentExplosionChance)
        {
            for (int i = 0; i < newCubes.Length; i++)
            {
                newCubes[i] = Instantiate(_cubePrefub);
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    private void InitializeCubes(int perentGeneration, Vector3 perentPosition, ref GameObject[] newCubes)
    {
        int childrenGeneration = perentGeneration + 1;
        Vector3 childrenScale = newCubes[1].transform.localScale *
            ((float)_scaleReductionPerGeneration / (float)(childrenGeneration * _scaleReductionPerGeneration));

        foreach (var cube in newCubes)
        {
            cube.GetComponent<ExplosiveCube>().Initialize(childrenGeneration, perentPosition, childrenScale);
            cube.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
        }
    }

    private void KickCubes(ref GameObject[] newCubes)
    {
        foreach (var cube in newCubes)
        {
            Vector3 impulse = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1))
                * Random.Range(_impulseForceMin, _impulseForceMax);
            cube.GetComponent<Rigidbody>().AddForce(impulse, ForceMode.Impulse);
        }
    }
}