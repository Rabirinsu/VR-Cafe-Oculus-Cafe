using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.XR.OpenVR;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public Camera playerCamera;
    public PlayerInventory inventory;
    public GameObject spawnerParent;
    private int _gameTime = 120;
    private ObjectSpawner[] _spawnerList;
    private bool _collectingTime = true;
    private bool _isInventoryOpen = false;

    [SerializeField] private bool isPause;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _spawnerList = spawnerParent.GetComponentsInChildren<ObjectSpawner>();
        SetSpawners(true);

    }

    
    void Update()
    {

        if (isPause)
            SetSpawners(false);
        
       else if (!isPause && _collectingTime)
        {
            scoreText.text =  score.ToString();
            var timeLeft = (int) Math.Ceiling(_gameTime - Time.time);
            if (timeLeft < 0) timeLeft = 0;
            timeText.text =  timeLeft.ToString();
            if (timeLeft == 0)
            {
                timeText.text = "Time to place items";
                _collectingTime = false;
                SetSpawners(false);
                isPause = true;
            }
        }

       // HitCheck();


       

   /*     if (Input.GetKeyDown(KeyCode.Tab))
        {
            ShowInventory();
            _isInventoryOpen = true;
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            HideInventory();
            _isInventoryOpen = false;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(CamCapture());
        }*/
    }

    private void CheckInventory()
    {
        if (OVRInput.GetDown(OVRInput.Button.One) && !_isInventoryOpen) // A dü?mesine t?klama i?lemi yap?l?rsa
        {
            ShowInventory();
        }
        if (OVRInput.GetDown(OVRInput.Button.Two) && _isInventoryOpen) // A dü?mesine t?klama i?lemi yap?l?rsa
        {
            HideInventory();
        } 
    }
 
    public void SetSpawners(bool status)
    {
        if (status)
        {
            foreach (var spawner in _spawnerList)
            {
                spawner.StartSpawn();
            }
        }
        else
        {
            foreach (var spawner in _spawnerList)
            {
                spawner.StopSpawn();
            }
        }
    }


    public void HitCheck(GameObject hitobj)
    {
                //  var obj = hitInfo.transform.gameObject;
                var collectable = hitobj.GetComponent<Collectable>();
                if (collectable != null)
                {
                    inventory.Push(collectable);
                    score += collectable.scoreEffect;
                    Destroy(hitobj);
                }
       
    }
    /* public void HitCheck(GameObject hitobj)
      {
         if (Input.GetMouseButtonDown(0))
          {
              if (Physics.Raycast(playerCamera.transform.position,
                      playerCamera.transform.forward, out var hitInfo,
                      100.0f))
              {
                //  var obj = hitInfo.transform.gameObject;
                  var collectable = hitobj.GetComponent<Collectable>();
                  if (collectable != null)
                  {
                      inventory.Push(collectable);
                      score += collectable.scoreEffect;
                      Destroy(hitobj);
                  }
              }
          }
      }*/


    public void ShowInventory()
    {
   //  Cursor.lockState = CursorLockMode.Confined;
        inventory.Show();
            _isInventoryOpen = true;
    }

    public void HideInventory()
    {
   //   Cursor.lockState = CursorLockMode.Locked;
        inventory.Hide();
            _isInventoryOpen = false;
    }

    public bool IsInventoryOpen()
    {
        return _isInventoryOpen;
    }


    private IEnumerator CamCapture()
    {
        yield return new WaitForEndOfFrame();
        int width = Screen.width;
        int height = Screen.height;
        Texture2D screenshotTexture = new Texture2D(width, height, TextureFormat.ARGB32, false);
        Rect rect = new Rect(0, 0, width, height);
        screenshotTexture.ReadPixels(rect, 0, 0);
        screenshotTexture.Apply();

        byte[] data = screenshotTexture.EncodeToPNG();
        File.WriteAllBytes(
            Application.dataPath + "/Captures/" + DateTime.Now.ToString("_MMddyyyy_HHmmss") + ".png",
            data
        );
    }
}