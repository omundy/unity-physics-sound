using UnityEngine;

public class PhysicsDebug : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        DrawContacts(other);
        DrawMouseContacts(other);
    }

    private void OnCollisionStay(Collision other)
    {
        DrawContacts(other);
        DrawMouseContacts(other);
    }

    void DrawContacts(Collision other)
    {
        // draw all contact points and normals
        foreach (ContactPoint contact in other.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white, 5f);
        }
    }

    void DrawMouseContacts(Collision other)
    {
        Debug.DrawLine(Camera.main.transform.position, other.transform.position, Color.red, 1f);
    }
}
