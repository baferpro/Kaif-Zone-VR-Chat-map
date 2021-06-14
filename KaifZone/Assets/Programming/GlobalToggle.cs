
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

public class GlobalToggle : UdonSharpBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private GameObject toggleObject;
    [SerializeField] private GameObject[] target;
    [UdonSynced] bool toggleValue;
    private float _timer;
    [SerializeField] private float timer = 3;
    [SerializeField] private Color32 ColorOn = new Color(200, 200, 200, 255);
    [SerializeField] private Color32 ColorOff = new Color(79, 79, 79, 255);
    [SerializeField] private bool DefaultOn = false;

    private void Start()
    {
        toggleValue = DefaultOn;
    }

    private void Update()
    {
        if(_timer>= timer)
        {
            foreach (GameObject gameObject in target)
            {
                if(gameObject.activeSelf != toggleValue)
                    gameObject.SetActive(toggleValue);
            }
            _timer = 0;
        }
        else
        {
            _timer += Time.deltaTime;
        }
    }

    public override void Interact()
    {
        if (!Networking.IsOwner(toggleObject))
            Networking.SetOwner(Networking.LocalPlayer, toggleObject);
        if (text.color == ColorOff)
        {
            text.color = ColorOn;
            toggleValue = true;
        }
        else
        {
            text.color = ColorOff;
            toggleValue = false;
        }
    }

    public override void OnDeserialization()
    {
        if (!Networking.IsOwner(toggleObject))
        {
            text.color = (toggleValue) ? ColorOn : ColorOff;
            foreach (GameObject gameObject in target)
            {
                gameObject.SetActive(toggleValue);
            }
        }
    }
}
