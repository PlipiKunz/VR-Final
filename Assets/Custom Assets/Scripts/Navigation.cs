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

    public static bool inMenu = true;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XROrigin>();

        menuParent.SetActive(inMenu);
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice primaryDevice = InputDevices.GetDeviceAtXRNode(PrimarySource);
        primaryDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out MovementAxis);

        InputDevice secondaryDevice = InputDevices.GetDeviceAtXRNode(SecondarySource);
        secondaryDevice.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out secondaryPrimaryButton);

    }

    private void FixedUpdate()
    {
        if (inMenu)
        {
            //menu code handeling
        }
        else
        {
            horizontalMove();
            verticalMove();
        }
        menuStateChanger();
    }

    private void menuStateChanger(){
        if(secondaryPrimaryButton && !prevSecondaryButton)
        {
            inMenu = !inMenu;
            menuParent.SetActive(inMenu);
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
        curFallingSpeed += Time.fixedDeltaTime * gravitySpeed;
        curFallingSpeed = Mathf.Clamp(curFallingSpeed, gravitySpeed, jumpSpeed);

        //vertical movement
        character.Move(Vector3.up * curFallingSpeed * Time.fixedDeltaTime);
    }
}
