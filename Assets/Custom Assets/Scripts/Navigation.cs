using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.XR;
using Unity.XR.CoreUtils;

public class Navigation : MonoBehaviour
{
    public XRNode PrimarySource;
    private Vector2 MovementAxis;
    private bool primaryTrigger;

    public XRNode SecondarySource;
    private bool secondaryPrimaryButton;

    private CharacterController character;
    private XROrigin rig;

    public float movementSpeed = 5;

    public float gravitySpeed = -9.81f;
    private float curFallingSpeed;
    public float jumpSpeed = 5;

    private bool prevSecondaryButton = false;
    public GameObject menuParent;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XROrigin>();
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice primaryDevice = InputDevices.GetDeviceAtXRNode(PrimarySource);
        primaryDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out MovementAxis);
        primaryDevice.TryGetFeatureValue(CommonUsages.triggerButton, out primaryTrigger);

        InputDevice secondaryDevice = InputDevices.GetDeviceAtXRNode(SecondarySource);
        secondaryDevice.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out secondaryPrimaryButton);

    }

    private void FixedUpdate()
    {
        horizontalMove();
        verticalMove();
        menuUpdate();
    }

    private void menuUpdate(){
        if(secondaryPrimaryButton && !prevSecondaryButton){
            menuParent.SetActive(!menuParent.activeSelf);
        }

        prevSecondaryButton = secondaryPrimaryButton;
    }

    private void horizontalMove()
    {
        Quaternion headYaw = Quaternion.Euler(0, rig.Camera.transform.eulerAngles.y, 0);
        Vector3 direction = headYaw * new Vector3(MovementAxis.x, 0, MovementAxis.y);
        character.Move(direction * Time.fixedDeltaTime * movementSpeed);
    }

    private void verticalMove()
    {
        //if the jump button has been pressed and cur falling speed equals gravity speed
        // if (secondaryPrimaryButton && curFallingSpeed == gravitySpeed)
        // {
        //     //jump
        //     curFallingSpeed = jumpSpeed;
        // }
        //fall
        curFallingSpeed += Time.fixedDeltaTime * gravitySpeed;
        curFallingSpeed = Mathf.Clamp(curFallingSpeed, gravitySpeed, jumpSpeed);

        //vertical movement
        character.Move(Vector3.up * curFallingSpeed * Time.fixedDeltaTime);
    }
}
