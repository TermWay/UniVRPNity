using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniVRPNity
{
    /// Copy from vrpn_Wiimote.h

    // The buttons are as read from the bit-fields of the primary controller (bits 0-15)
    //  and then a second set for any extended controller (nunchuck bits 16-31),
    //  (classic controller bits 32-47), (guitar hero 3 bits 48-63).
    //
    // If you enable "reorderButtons" by setting it to 1, the buttons on the Wiimote
    // itself are re-ordered to be reported as follows:
    //    button[0] = Home
    //    button[1] = "1"
    //    button[2] = "2"
    //    button[3] = "A"
    //    button[4] = "B"
    //    button[5] = "-"
    //    button[6] = "+"
    //    button[7] = direction pad: left
    //    button[8] = direction pad: right
    //    button[9] = direction pad: down
    //    button[10] = direction pad: up
    //    button[11] = WIIMOTE_BUTTON_ZACCEL_BIT4
    //    button[12] = WIIMOTE_BUTTON_ZACCEL_BIT5
    //    button[13] = WIIMOTE_BUTTON_ZACCEL_BIT6
    //    button[14] = WIIMOTE_BUTTON_ZACCEL_BIT7
    //    button[15] = WIIMOTE_BUTTON_UNKNOWN
    //
    // The Analogs are in an even more random order, both from the primary controller:
    //    channel[0] = battery level (0-1)
    //    channel[1] = gravity X vector calculation (1 = Earth gravity)
    //    channel[2] = gravity Y vector calculation (1 = Earth gravity)
    //    channel[3] = gravity Z vector calculation (1 = Earth gravity)
    //    channel[4] = X of first sensor spot (0-1023, -1 if not seen)
    //    channel[5] = Y of first sensor spot (0-767, -1 if not seen)
    //    channel[6] = size of first sensor spot (0-15, -1 if not seen)
    //    channel[7] = X of second sensor spot (0-1023, -1 if not seen)
    //    channel[9] = Y of second sensor spot (0-767, -1 if not seen)
    //    channel[9] = size of second sensor spot (0-15, -1 if not seen)
    //    channel[10] = X of third sensor spot (0-1023, -1 if not seen)
    //    channel[11] = Y of third sensor spot (0-767, -1 if not seen)
    //    channel[12] = size of third sensor spot (0-15, -1 if not seen)
    //    channel[13] = X of fourth sensor spot (0-1023, -1 if not seen)
    //    channel[14] = Y of fourth sensor spot (0-767, -1 if not seen)
    //    channel[15] = size of fourth sensor spot (0-15, -1 if not seen)
    // and on the secondary controllers (skipping values to leave room for expansion)
    //    channel[16] = nunchuck gravity X vector
    //    channel[17] = nunchuck gravity Y vector
    //    channel[18] = nunchuck gravity Z vector
    //    channel[19] = nunchuck joystick angle
    //    channel[20] = nunchuck joystick magnitude
    //
    //    channel[32] = classic L button
    //    channel[33] = classic R button
    //    channel[34] = classic L joystick angle
    //    channel[35] = classic L joystick magnitude
    //    channel[36] = classic R joystick angle
    //    channel[37] = classic R joystick magnitude
    //
    //    channel[48] = guitar hero whammy bar
    //    channel[49] = guitar hero joystick angle
    //    channel[50] = guitar hero joystick magnitude
    //
    // Balance board data: (requires WiiUse 0.13 or newer, preferably 0.14 or newer)
    //    channel[64] = Balance board: top-left sensor, kg
    //    channel[65] = Balance board: top-right sensor, kg
    //    channel[66] = Balance board: bottom-left sensor, kg
    //    channel[67] = Balance board: bottom-right sensor, kg
    //    channel[68] = Balance board: total mass, kg
    //    channel[69] = Balance board: center of gravity x, in [-1, 1]
    //    channel[70] = Balance board: center of gravity y, in [-1, 1]
    //


    /// <summary>
    /// Wiimote layout (VRPN).
    /// </summary>
    public enum WiimoteButton
    {
        HOME,
        ONE,
        TWO,
        A,
        B,
        MINUS,
        PLUS,
        LEFT,
        RIGHT,
        BOTTOM,
        TOP,
        Z = 16,
        C = 17,
        NONE
    }


     public enum WiimoteAnalog
     {
         ///<summary>[0,1]</summary>
        Battery = 0,
        ///<summary>Left [-1,1] Right.</summary>
        Roll = 1,
        ///<summary>Up [-1,1] Down.</summary>
        Pitch = 2,
        ///<summary>Don't work.</summary>
        Yaw = 3,
        ///<summary>Left [-0.3,0.3] Right.</summary>
        NunchuckRoll = 16,
        ///<summary>Up [-0.3,0.3] Down.</summary>
        NunchuckPitch = 17,
        ///<summary>Don't work.</summary>
        NunchuckYaw = 18,
        ///<summary> [0,360] Up : 0, Right : 90, Bottom : 180, Left : 270.</summary>
        JoystickAngle = 19,
        ///<summary>[0,1].</summary>
        JoystickMagnitude = 20,
    }
}
