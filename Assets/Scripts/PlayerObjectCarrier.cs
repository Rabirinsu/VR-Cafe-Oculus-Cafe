using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerObjectCarrier : MonoBehaviour
{
    public static PlayerObjectCarrier instance;
    private Collectable _collectable;
    private bool _isCarrying;
    private bool _isNewCreated;
    private GameObject carryingObject;
    private GameManager _gameManager;

    [SerializeField] private Transform objectspawnPoint;

    private float exactpoint_y;
    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
     /*   if (_isCarrying)
        {
            var canPlace = false;
            LayerMask groundMask = LayerMask.GetMask("ItemSurface");
            if (Physics.Raycast(_gameManager.
     .transform.position,
                    _gameManager.playerCamera.transform.forward, out var hit, 5f, groundMask))
            {
                CollectableType type;
                if (_isNewCreated)
                {
                    Debug.Log(1);
                    type = _collectable.type;
                }
                else
                {
                    type = carryingObject.GetComponent<DataStore>().collectableType;
                }

                if (type == CollectableType.Furniture && hit.transform.CompareTag("GroundSurface") ||
                    type == CollectableType.Item)
                {
                    if (!carryingObject.activeSelf)
                    {
                        carryingObject.SetActive(true);
                    }

                    canPlace = true;
                    MoveTo(hit.point);
                }
            }
            else
            {
                if (carryingObject.activeSelf)
                {
                    carryingObject.SetActive(false);
                }
            }

            RotateCheck();
            if (Input.GetMouseButtonDown(0) && canPlace)
            {
                Drop();
            }
        }
        else
        {
            CheckPickupHit();
        }*/
    }

    void RotateCheck()
    {
        if (!_isCarrying) return;
        if (Input.GetKey(KeyCode.Q))
        {
            carryingObject.transform.Rotate(0f, -50f * Time.deltaTime, 0f);
        }

        if (Input.GetKey(KeyCode.E))
        {
            carryingObject.transform.Rotate(0f, +50f * Time.deltaTime, 0f);
        }
    }

    void CheckPickupHit()
    {
        if (!_gameManager.IsInventoryOpen() && Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(_gameManager.playerCamera.transform.position,
                    _gameManager.playerCamera.transform.forward, out var hitInfo,
                    100.0f))
            {
                var obj = hitInfo.transform.gameObject;
                if (obj.CompareTag("Collectable"))
                {
                    _isCarrying = true;
                    carryingObject = obj;
                }


                if (obj.transform.parent != null && obj.transform.parent.CompareTag("Collectable"))
                {
                    _isCarrying = true;
                    carryingObject = obj.transform.parent.gameObject;
                }
            }
        }
    }

    public void CarryObject(Collectable collectable)
    {
        _collectable = collectable;
        _isCarrying = true;
        _isNewCreated = true;
        var clonePrefab = Resources.Load("CollectableOriginalPrefabs/" + _collectable.GetPrefabName()) as GameObject;
        
        carryingObject = Instantiate(clonePrefab, objectspawnPoint.position,Quaternion.identity, transform.parent = null);
    }

    public void Drop()
    {
     _isCarrying = false;
    _isNewCreated = false;
        //  carryingObject = null;
        //  _collectable = null;
    }

    public bool CanCarry()
    {
        return !_isCarrying;
    }

    public void MoveTo(Vector3 position)
    {
        carryingObject.transform.position = position;
    }
}