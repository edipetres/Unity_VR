using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZEffects;

public class GunController : MonoBehaviour {
    // Controller access
    public GameObject controllerRight;
    // Heptic feedback
    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;
    // Gives access to triggers and informs when stuff is clicked
    private SteamVR_TrackedController controller;

    public EffectTracer TracerEffect;
    public Transform muzzleTransform;

	// Use this for initialization
	void Start () {
        controller = controllerRight.GetComponent<SteamVR_TrackedController>();
        controller.TriggerClicked += TriggerPressed;
        trackedObj = controllerRight.GetComponent<SteamVR_TrackedObject>();
	}
	
    private void TriggerPressed(object sender, ClickedEventArgs e)
    {
        ShootWeapon();
    }

    public void ShootWeapon()
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = new Ray(muzzleTransform.position, muzzleTransform.forward);

        device = SteamVR_Controller.Input((int)trackedObj.index);
        device.TriggerHapticPulse(750); // Can range from 0 to 4000
        TracerEffect.ShowTracerEffect(muzzleTransform.position, muzzleTransform.forward, 250f); // The range of how far to shoot

        if(Physics.Raycast(ray, out hit, 5000f)) // Checks if we hit anything
        {
            if(hit.collider.attachedRigidbody)
            {
                Debug.Log("We have hit" + hit.collider.gameObject.name);
            }
        }
        

    }
}
