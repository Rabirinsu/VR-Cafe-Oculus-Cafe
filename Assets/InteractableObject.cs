
using Oculus.Interaction;
using UnityEngine;


public class InteractableObject : MonoBehaviour
{

    private void Start()
    {
        var rayinteractable = GetComponent<RayInteractable>();
    }
    public void onselectenter()
    {
        GameManager.instance.HitCheck(transform.gameObject);
    }

    public void Drop()
    {
        PlayerObjectCarrier.instance.Drop();
    }
}
