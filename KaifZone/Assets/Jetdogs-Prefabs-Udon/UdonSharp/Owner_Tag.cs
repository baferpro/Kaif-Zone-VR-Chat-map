using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Owner_Tag : UdonSharpBehaviour
{
    [SerializeField] private GameObject[] CreatorMarker = null;
    [SerializeField] private float offset = 1;
    [SerializeField] private bool follow = false;
    private VRCPlayerApi owner = null;

    private void Start()
    {
        for(int i = 0; i<CreatorMarker.Length; i++)
        {
            CreatorMarker[i].GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public override void OnPlayerJoined(VRCPlayerApi player)
    {
        if (player.displayName.Equals("Faustenok"))
        {
            owner = player;
            for (int i = 0; i < CreatorMarker.Length; i++)
            {
                if(CreatorMarker[i].GetComponent<MeshRenderer>().enabled == false)
                    CreatorMarker[i].GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }

    public override void OnPlayerLeft(VRCPlayerApi player)
    {
        if (player.displayName.Equals("Faustenok"))
        {
            for (int i = 0; i < CreatorMarker.Length; i++)
            {
                if (CreatorMarker[i].GetComponent<MeshRenderer>().enabled == true)
                    CreatorMarker[i].GetComponent<MeshRenderer>().enabled = false;
            }
            owner = null;
        }
    }

    private void FixedUpdate()
    {
        if (follow && Utilities.IsValid(owner))
        {
            if(!owner.IsOwner(CreatorMarker[0]))
            {
                Networking.SetOwner(owner, CreatorMarker[0]);
            }
            CreatorMarker[0].transform.SetPositionAndRotation(owner.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).position + new Vector3(0, offset, 0), owner.GetRotation());
        }
    }
}
