using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float rotationSpeed = 180f;

    /// <summary>
    /// Used to move the character with an specified distance
    /// </summary>
    /// <param name="x">horizontal movement</param>
    /// <param name="z">vertical movement(not height)</param>
    public void Move(float x, float z)
    {
        Vector3 movement = new Vector3(x,0f,z) * movementSpeed * Time.deltaTime;        
        transform.Translate(movement);
    }
    public void Rotate(float rotation)
    {
        transform.Rotate(Vector3.up * rotation * rotationSpeed* Time.deltaTime);
    }
}


