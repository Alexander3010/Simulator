using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public FixedJoystick Joystick;
    public float SpeedMove = 5f;
    private CharacterController Controller;
    void Start()
    {
        Controller= GetComponent<CharacterController>();
    }

    
    void Update()
    {
        Vector3 Move = transform.right * Joystick.Horizontal + transform.forward * Joystick.Vertical;
        Controller.Move(Move * SpeedMove * Time.deltaTime);
    }
}
