
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class BottleSpin1 : UdonSharpBehaviour
{
    private GameObject Bottle = null;
    private float TimeEnabled = 3f;
    [UdonSynced] Vector3 BottleRotation;
    [UdonSynced] Vector3 BottlePosition;
    [UdonSynced] bool ForceJoin = false;

    private void Start()
    {
        if (Bottle == null)
            Bottle = this.gameObject;
    }

    public override void Interact()
    {
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "SpinBottle");
    }

    private void FixedUpdate()
    { 
        if (TimeEnabled > 0f && Bottle.GetComponent<ConstantForce>().enabled == true && !ForceJoin)
        {
            TimeEnabled -= Time.deltaTime;
        }
        else
        {
            Bottle.GetComponent<ConstantForce>().enabled = false;
            TimeEnabled = 3f;
            if(ForceJoin)
            {
                Bottle.GetComponent<Rigidbody>().velocity = Vector3.zero;
                Bottle.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                Bottle.GetComponent<Rigidbody>().isKinematic = true;
                Bottle.GetComponent<Rigidbody>().isKinematic = false;
                ForceJoin = false;
            }
            BottleRotation = Bottle.transform.eulerAngles;
            BottlePosition = Bottle.transform.position;
        }
    }

    public override void OnPlayerJoined(VRCPlayerApi player)
    {
        Bottle.transform.eulerAngles = BottleRotation;
        Bottle.transform.position = BottlePosition;
        ForceJoin = true;
        
    }

    /*private void Update()
    {
        if(TimeEnabled > 0f && Bottle.GetComponent<ConstantForce>().enabled == true)
        {
            TimeEnabled -= Time.deltaTime;
        }
        else
        {
            Bottle.GetComponent<ConstantForce>().enabled = false;
            TimeEnabled = 3f;
            BottleTransform = Bottle.transform;
        }
    }*/

    public void SpinBottle()
    {
        //Bottle.GetComponent<Rigidbody>().velocity = new Vector3(0, 10, 0);
        Bottle.GetComponent<ConstantForce>().enabled = true;
    }
}
