using System;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private UserInput _userInput;

    public event Action<Cube> CubeDetected;

    private void OnEnable()
    {
        _userInput.KeyTriggered += TryDetectCube;
    }

    private void OnDisable()
    {
        _userInput.KeyTriggered -= TryDetectCube;
    }

    private void TryDetectCube()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.TryGetComponent<Cube>(out Cube hittedCube))
        {
            CubeDetected.Invoke(hittedCube);
        }
    } 
}