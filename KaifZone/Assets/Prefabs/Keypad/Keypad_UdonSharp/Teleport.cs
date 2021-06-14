
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Teleport : UdonSharpBehaviour
{
    [SerializeField] private Transform TeleportPoint;

    public void keypadGranted()
    {
        Networking.LocalPlayer.TeleportTo(TeleportPoint.position, TeleportPoint.rotation);
    }
}
