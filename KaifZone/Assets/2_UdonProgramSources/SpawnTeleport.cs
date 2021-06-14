
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class SpawnTeleport : UdonSharpBehaviour
{
    [SerializeField] private Transform TeleportObject;
    [SerializeField] private Animator DoorAnimator;
    [SerializeField] private GameObject canvas;

    private VRCPlayerApi PlayerLocal;

    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        PlayerLocal = player;
        if (PlayerLocal.isLocal)
            SendCustomEvent("StartAnimation");
    }

    public void StartAnimation()
    {
        canvas.SetActive(true);
        DoorAnimator.enabled = true;
    }

    public void AnimationMiddle()
    {
        PlayerLocal.TeleportTo(TeleportObject.position, TeleportObject.rotation);
    }

    public void AnimationEnd()
    {
       DoorAnimator.enabled = false;
       canvas.SetActive(false);
    }
}
