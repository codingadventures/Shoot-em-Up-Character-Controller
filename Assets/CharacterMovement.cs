using UnityEngine;
using System.Collections;
using InControl;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private Quaternion _cameraRotation;

    private void Start()
    {
        _cameraRotation = Camera.main.transform.rotation;
    }
    private void Update()
    {
        float leftStickX = InputManager.ActiveDevice.LeftStick.X;
        float leftStickY = InputManager.ActiveDevice.LeftStick.Y;

        Vector2 movementVector = new Vector2(leftStickX, leftStickY);
         
        if (InputManager.ActiveDevice.LeftStick.State)
        {
            //if we are using Left and Right sticks means that we are walking and aiming.

            if (InputManager.ActiveDevice.RightStick.State) return;

            //We need to take into account the camera rotation and the character facing the positive z
            float angle = Mathf.PI / 2 + _cameraRotation.eulerAngles.y * Mathf.Deg2Rad - Mathf.Atan2(leftStickY, leftStickX);

            //we only want to trigger the walking animation on the positive y-axis
            float blendTreeInputY = movementVector.magnitude;
            float blendTreeInputX = 0f;

            _animator.SetFloat("Y", blendTreeInputY);
            _animator.SetFloat("X", 0f);

            if (Mathf.Approximately(movementVector.magnitude, 0f)) return;

            Quaternion rotation = Quaternion.Euler(0f, Mathf.Rad2Deg*angle, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime*10f);
        }
        else
        {
            _animator.SetFloat("Y", 0f);
            _animator.SetFloat("X", 0f);
        }
    }

}

