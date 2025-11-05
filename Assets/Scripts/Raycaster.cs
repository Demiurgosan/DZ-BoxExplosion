using System;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private int _triggerKey = 0;

    public event Action<RaycastHit> CubeDetected;

    public void Update()
    {
        if (Input.GetMouseButtonDown(_triggerKey))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.TryGetComponent(out Cube cube))
            {
                CubeDetected.Invoke(hit);
            }
        }
    }
}