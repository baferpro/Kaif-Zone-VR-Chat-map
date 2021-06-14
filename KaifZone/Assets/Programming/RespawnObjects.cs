using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

public class RespawnObjects : UdonSharpBehaviour
{
    [SerializeField] private GameObject[] objects;
    [SerializeField] private Transform[] spawnPoints;

    public override void Interact()
    {
        SendCustomNetworkEvent(NetworkEventTarget.All, "Spawn");
    }

    public void Spawn()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            Networking.SetOwner(Networking.LocalPlayer, objects[i]);
            objects[i].transform.position = spawnPoints[i].position;
            objects[i].transform.rotation = spawnPoints[i].rotation;
            objects[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
