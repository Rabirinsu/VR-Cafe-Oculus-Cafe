using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public bool isLocked = false;
    public CharacterController controller;
    public float speed = 5f;

    /*private float _gravity = -9.81f;
    private Vector3 _velocity;*/

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocked)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            var playerTransform = transform;
            Vector3 move = playerTransform.right * x + playerTransform.forward * z;

            controller.Move(move * speed * Time.deltaTime);
            var position = controller.transform.position;
            controller.transform.position = new Vector3(position.x, 1.1f, position.z);
            /*_velocity.y += _gravity * Time.deltaTime;
            controller.Move(_velocity * Time.deltaTime);*/
        }
    }
}