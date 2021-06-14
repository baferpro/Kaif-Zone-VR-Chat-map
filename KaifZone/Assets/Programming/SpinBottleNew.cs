
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

public class SpinBottleNew : UdonSharpBehaviour
{
    [SerializeField] private float min = 1;
    [SerializeField] private float max = 3;
    [SerializeField] private GameObject Bottle = null;
    [SerializeField] private bool DebugOn = true;

    private float randomTime;

    [UdonSynced] private float timerGlobal = 0f;
    private float timerLocal = 0f;

    [UdonSynced] private bool enabledGlobal;
    private bool enabledLocal;

    public override void Interact()
    {
        if (!Networking.IsOwner(Bottle))
            Networking.SetOwner(Networking.LocalPlayer, Bottle);
        if (enabledLocal == false)
        {
            /*randomTime = Random.Range(min, max);
            timerLocal = 0f;
            enabledLocal = true;

            if (DebugOn)
            {
                string text = string.Format("[Bottle] Interact = {0}", Networking.LocalPlayer);
                Debug.Log(text);
            }*/

        }
    }

    public void Update()
    {
        if (enabledLocal != false)
        {
            //if ((timerLocal != timerGlobal) || ) SyncData(false);

            timerLocal += Time.deltaTime;

            if (timerLocal >= randomTime) enabledLocal = false;

            if (enabledLocal == true)
                Bottle.GetComponent<ConstantForce>().enabled = true;
            else
            {
                if (Bottle.GetComponent<ConstantForce>().enabled == true)
                {
                    Bottle.GetComponent<ConstantForce>().enabled = false;
                }
            }
        }
    }

    public void SyncData(bool Value)
    {
        if (Value == false)
        {
            timerLocal = timerGlobal;
            enabledLocal = enabledGlobal;
        }
        else
        {
            timerGlobal = timerLocal;
            enabledGlobal = enabledLocal;
        }

        if (DebugOn)
        {
            string text = string.Format("[SyncData] Value = {0}", Value);
            Debug.Log(text);
        }
    }
}
