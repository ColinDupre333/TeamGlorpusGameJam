using UnityEngine;

public class FlashLightDirectionController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform lightTransform;
    void Start()
    {
        lightTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit))
        {
            return;
        }

        var targetPosition = hit.point;


        var direction = (hit.point - lightTransform.position).normalized;
        lightTransform.rotation = Quaternion.LookRotation(direction);
    }
}
