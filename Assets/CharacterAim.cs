using UnityEngine;
using System.Collections;
using InControl;

public class CharacterAim : MonoBehaviour {
    [SerializeField]
    private Animator _animator;

    private Quaternion _cameraRotation;

    private void Start()
    {
        _cameraRotation = Camera.main.transform.rotation;
    }


    private void Update()
    {
        float rightStickX = InputManager.ActiveDevice.RightStick.X;
        float rightStickY = InputManager.ActiveDevice.RightStick.Y;
         
        if (InputManager.ActiveDevice.RightStick.State)
        {
            _animator.SetBool("IsAiming", true);
            float angle = Mathf.PI/2f + _cameraRotation.eulerAngles.y * Mathf.Deg2Rad - Mathf.Atan2(rightStickY, rightStickX);
             
            Quaternion rotation = Quaternion.Euler(0f, Mathf.Rad2Deg*angle, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime*10f);

            if (InputManager.ActiveDevice.LeftStick.State)
            {
                float leftStickX = InputManager.ActiveDevice.LeftStick.X;
                float leftStickY = InputManager.ActiveDevice.LeftStick.Y;

                Vector3 movementVector = new Vector3(leftStickX, leftStickY, 0f);

                Vector3 rotatedVector = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward) * movementVector;

                _animator.SetFloat("Y", rotatedVector.y);
                _animator.SetFloat("X", rotatedVector.x);
            }
        }
    
        else
        {
            _animator.SetBool("IsAiming", false);
            _animator.SetFloat("Y", 0f);
            _animator.SetFloat("X", 0f);
        }

    }
}
