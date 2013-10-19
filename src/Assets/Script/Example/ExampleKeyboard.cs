using System.Text;
using UnityEngine;

using UniVRPNity;
using UniVRPNity.Device;

public class ExampleKeyboard : MonoBehaviour
{
    public string deviceName = "Keyboard0";
    public string deviceIP = "localhost";

    public ButtonRemote button;

    public Transform transformHandled;

    public Vector3 sensibilityTranslation = new Vector3(10F, 10F, 10F);
    public Vector3 sensibilityRotation = new Vector3(100F, 100F, 100F);

    public Keyboard.FrenchKeys forwardKey = Keyboard.FrenchKeys.KC_HAUT;
    public Keyboard.FrenchKeys backwardKey = Keyboard.FrenchKeys.KC_BAS;
    public Keyboard.FrenchKeys leftKey = Keyboard.FrenchKeys.KC_GAUCHE;
    public Keyboard.FrenchKeys rightKey = Keyboard.FrenchKeys.KC_DROITE;

    public bool forward = false;
    public bool backward = false;
    public bool left = false;
    public bool right = false;

    public void Start()
    {
        button = new ButtonRemote(deviceName + '@' + deviceIP, 256);
        button.ButtonChanged += new ButtonRemote.ButtonChangeEventHandler(ButtonChanged);
    }

    public void Reset()
    {
        transformHandled = transform;
    }

    public void Update()
    {
        button.Mainloop();

        if (transformHandled != null)
        {
            if (forward)
                transformHandled.Translate(0, 0, sensibilityTranslation.z * Time.deltaTime, Space.Self);
            if (backward)
                transformHandled.Translate(0, 0, -sensibilityTranslation.z * Time.deltaTime, Space.Self);
            if (left)
                transformHandled.Translate(-sensibilityTranslation.x * Time.deltaTime, 0, 0, Space.Self);
            if (right)
                transformHandled.Translate(sensibilityTranslation.x * Time.deltaTime, 0, 0, Space.Self);
        }
    }

    /* Analog event handler */
    private void ButtonChanged(ButtonEvent e)
    {
        forward = button.GetButtonState((int)(object)forwardKey);
        backward = button.GetButtonState((int)(object)backwardKey);
        left = button.GetButtonState((int)(object)leftKey);
        right = button.GetButtonState((int)(object)rightKey);
    }
}