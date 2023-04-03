using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public enum CollectableGroup
{
    Bag,
    Cup,
    Drink,
    Chair,
    Coffee,
    Table,
    TrashBin
}

public enum CollectableType
{
    Furniture,
    Item
}

public class Collectable : MonoBehaviour
{
    public string collectableName;
    public CollectableType type;
    public CollectableGroup group;
    public int scoreEffect;
    public Sprite icon;

    private Transform _target;
    private string _prefabName;
    private TextMeshProUGUI _nameText;

    private void Start()
    {
        _prefabName = transform.name.Replace("(Clone)", "").Trim();
        _nameText = GetComponentInChildren<TextMeshProUGUI>();
        _nameText.text = collectableName;
    }

    void FixedUpdate()
    {
        if (this !=null && Camera.main != null)
        {
            var lookPos = _nameText.transform.position - Camera.main.transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            _nameText.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1f);
        }

        if (_target != null)
        {
            var step = 3 * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _target.position, step);
            if (Vector3.Distance(transform.position, _target.position) < 0.001f)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("DestroyGate"))
        {
            Destroy(gameObject);
        }
    }
    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public string GetPrefabName()
    {
        return _prefabName;
    }
    public void DestroyCollactable()
    {
        Destroy(GetComponentInChildren<TextMeshProUGUI>());
        Destroy(this);
    }
   
}