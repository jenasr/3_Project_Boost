
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    void LateUpdate ()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPostion = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        //Lerp-(Linear interpolation) going from point A to B smoothly with a given speed
        transform.position = smoothedPostion;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
