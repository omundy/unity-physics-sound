using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    private Vector3 lookDir;

    public float CamMoveSpeed = 5f;

    private void Start()
    {
        lookDir = transform.position;
    }

    private void Update()
    {
        float mouseXrotation = Input.GetAxisRaw("Mouse X");
        float mouseYrotation = Input.GetAxisRaw("Mouse Y");

        float x = transform.position.x;
        float z = transform.position.z;
        float y = transform.position.y;

        lookDir = new Vector3(x + mouseXrotation, y, z + mouseYrotation);

		transform.LookAt(lookDir);
    }
}
