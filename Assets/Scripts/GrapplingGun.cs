using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.XR;


public class GrapplingGun : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform gunTip, player;
    private float maxDistance = 180f;
    private SpringJoint joint;
    private bool _canShoot = false;
    bool triggerValue;

    private float chlen;

    private XRIDefaultInputActions inputActions;

    private Vector2 trackpadPrimaryAxis;

    private void Update()
    {
        //TrackpadClick();
        //Debug.Log(trackpadPrimaryAxis.y);
    }

    private void Start()
    {
        trackpadPrimaryAxis = inputActions.XRIRightHandInteraction.ExtraButton.ReadValue<Vector2>();
        Physics.IgnoreLayerCollision(7, 8);
    }
    void Awake()
    {
        inputActions = new XRIDefaultInputActions();
        inputActions.XRIRightHandInteraction.Activate.performed += context => Shoot();
        inputActions.XRIRightHandInteraction.ExtraButton.performed += ContextMenu => TrackpadClick();
        lr = GetComponent<LineRenderer>();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void Shoot()
    {
        Debug.Log("SHOOT");
        if (_canShoot == false)
        {
            StartGrapple();
            _canShoot = true;
        }
        else
        {
            StopGrapple();
            _canShoot = false;
        }   
    }
    void LateUpdate()
    {
        DrawRope();
    }
    void TrackpadClick()
    {
        Debug.Log(trackpadPrimaryAxis.y);

        //if (trackpadPrimaryAxis.y > 0.5f)
        //{
        //    Debug.Log("Atttract");
        //    joint.maxDistance += Time.deltaTime * climbSpeed;

        //} 
        //else if (trackpadPrimaryAxis.y < 0.5f)
        //{
        //    Debug.Log("Atttract");
        //    joint.maxDistance -= Time.deltaTime * climbSpeed;
        //}
    }

    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(gunTip.position, gunTip.forward, out hit, maxDistance, whatIsGrappleable))
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            chlen = distanceFromPoint * 0.8f;

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            chlen = joint.maxDistance;

            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;
        }
    }
    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }

    private Vector3 currentGrapplePosition;

    void DrawRope()
    {
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, currentGrapplePosition);
    }

    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;

    }
}