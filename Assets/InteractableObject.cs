
using Oculus.Interaction;
using Unity.Mathematics;
using UnityEngine;


public class InteractableObject : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    public void onselectenter()
    {
        GameManager.instance.HitCheck(transform.gameObject);
    }

    public void Drop()
    {
        PlayerObjectCarrier.instance.Drop();
        _rigidbody.isKinematic = false;
        transform.rotation = quaternion.Euler(0, transform.rotation.y, 0);
    }
    public void Grab()
    {
        _rigidbody.isKinematic = true;

    }
}
