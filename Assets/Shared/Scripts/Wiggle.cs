using System.Collections;
using UnityEngine;

public class Wiggle : MonoBehaviour
{
    public Renderer meshRenderer;
    public Vector3 origin;
    public Vector3 targetPos;
    public float speed = 0.2f;
    public float distance = 0.5f;

    // runs every time code is compiled
    private void OnValidate()
    {
        if (meshRenderer == null)
            meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        origin = transform.localPosition;
        targetPos = new Vector3(origin.x, origin.y, origin.z);
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            // only the parent names are unique in the scene
            if (hit.transform.parent.name == transform.parent.name)
            {
                // Debug.Log($"hit.transform.name = {hit.transform.name}");
                Debug.DrawLine(
                    Camera.main.transform.position,
                    transform.position,
                    Color.white,
                    0.5f
                );
                meshRenderer.material.SetColor("_BaseColor", Color.red);
                StartCoroutine(Change());
            }
            else
            {
                meshRenderer.material.SetColor("_BaseColor", Color.gray);
                StopAllCoroutines();
                targetPos = Vector3.zero;
            }
        }
        var step = speed * Time.deltaTime;
        // adjusting a rigidbody with transform is not usually advised, but this is just a demo
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, step);
    }

    // save random positions
    public IEnumerator Change()
    {
        targetPos = RandomOffsetPosition(transform.localPosition, distance);
        yield return new WaitForSeconds(1f);
        // reset after 1 second
        targetPos = new Vector3(origin.x, origin.y, origin.z);
    }

    // example of a "pure" function
    Vector3 RandomOffsetPosition(Vector3 pos, float offset)
    {
        return new Vector3(
            Random.Range(pos.x - offset, pos.x + offset),
            Random.Range(pos.y - offset, pos.y + offset),
            Random.Range(pos.z - offset, pos.z + offset)
        );
    }
}
