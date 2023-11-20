using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementTest : MonoBehaviour
{
    public CharacterMovementController characterMovementController;
    public float rotationAngle = 10f;

    private void Start() 
    {
        characterMovementController = GetComponent<CharacterMovementController>();
    }
    void Update()
    {
        // Ejemplo de movimiento y rotación desde fuera
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");
        

        // Llamar a los métodos del controlador desde otro script
        characterMovementController.Move(movimientoHorizontal, movimientoVertical);
        if(Input.GetKey(KeyCode.Q))
        {
            characterMovementController.Rotate(-rotationAngle);
        }
        if(Input.GetKey(KeyCode.E))
        {
            characterMovementController.Rotate(rotationAngle);
        }
    }
}
