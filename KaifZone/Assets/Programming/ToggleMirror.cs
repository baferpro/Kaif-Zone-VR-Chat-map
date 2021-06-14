
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;
using VRC.SDK3;

public class ToggleMirror : UdonSharpBehaviour
{
    [SerializeField] private Text InteractText;
    [SerializeField] private Text[] OtherText;
    private Color32 cTrue = new Color32(200,200,200, 255);
    private Color32 cFalse = new Color32(79,79,79, 255);
    [SerializeField] private GameObject MirrorHigh;
    [SerializeField] private GameObject MirrorMiddle;
    [SerializeField] private GameObject MirrorLow;

    private void Start()
    {
        MirrorHigh.SetActive(false);
        MirrorMiddle.SetActive(false);
        MirrorLow.SetActive(false);
    }

    public override void Interact()
    {
        if(InteractText.color == cTrue)
        {
            MirrorHigh.SetActive(false);
            MirrorMiddle.SetActive(false);
            MirrorLow.SetActive(false);
            InteractText.color = cFalse;
        }
        else
        {
            InteractText.color = cTrue;
            foreach (Text text in OtherText)
            {
                text.color = cFalse;
            }
            if (InteractText.text.Contains("Low"))
            {
                MirrorHigh.SetActive(false);
                MirrorMiddle.SetActive(false);
                MirrorLow.SetActive(true);
            }
            else if (InteractText.text.Contains("Middle"))
            {
                MirrorHigh.SetActive(false);
                MirrorMiddle.SetActive(true);
                MirrorLow.SetActive(false);
            }
            else if (InteractText.text.Contains("High"))
            {
                MirrorHigh.SetActive(true);
                MirrorMiddle.SetActive(false);
                MirrorLow.SetActive(false);
            }
        }
    }
}
