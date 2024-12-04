using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform character;
    private Vector3 lastMousePosition;
    [SerializeField] float rotationSpeed = 5f;

    private void LateUpdate()

    {
        float mouseX = Input.GetAxis("Mouse X");
        float rotationAmount = mouseX* rotationSpeed;
        transform.Rotate(0f,0f,-rotationAmount);

        Vector3 updatePosition=character.position;
        updatePosition.y=transform.position.y;
        transform.position=updatePosition;

        // transform.rotation=Quaternion.Euler(90f,character.eulerAngles.y,0f); Rotar Según Personaje
    }
}
