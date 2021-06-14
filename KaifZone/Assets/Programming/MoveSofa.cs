
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;
using VRC.Udon.Common.Interfaces;

public class MoveSofa : UdonSharpBehaviour
{
    [SerializeField] private GameObject Sofa;
    [SerializeField] private int Min = 10;
    [SerializeField] private int Max = 100;

    [UdonSynced] Vector3 vectorGlobal;
    private Vector3 vectorLocal;

    private void Start()
    {
        vectorGlobal = vectorLocal = Sofa.transform.eulerAngles;
    }

    public void SofaMoveLeft()
    {
        if (!Networking.IsOwner(this.gameObject))
            Networking.SetOwner(Networking.LocalPlayer, this.gameObject);
        SendCustomNetworkEvent(NetworkEventTarget.All, "MoveLeft");
    }

    public void MoveLeft()
    {
        if ((int)vectorGlobal.y > Min)
        {
            vectorGlobal = new Vector3(
                vectorGlobal.x,
                vectorGlobal.y - (float)10,
                vectorGlobal.z
            );
        }
        Move();
    }
    
    public void SofaMoveRight()
    {
        if (!Networking.IsOwner(this.gameObject))
            Networking.SetOwner(Networking.LocalPlayer, this.gameObject);
        SendCustomNetworkEvent(NetworkEventTarget.All, "MoveRight");
    }

    public void MoveRight()
    {
        if ((int)vectorGlobal.y < Max)
        {
            vectorGlobal = new Vector3(
                vectorGlobal.x,
                vectorGlobal.y + (float)10,
                vectorGlobal.z
            );
        }
        Move();
    }

    public override void OnDeserialization()
    {
        Move();
    }

    /*private void Update()
    {
        if (vectorLocal != vectorGlobal) SyncData(false);

        Sofa.transform.eulerAngles = vectorLocal;
    }*/

    private void Move()
    {
        if (vectorLocal != vectorGlobal)
            vectorLocal = vectorGlobal;
        Sofa.transform.eulerAngles = vectorLocal;
    }
}