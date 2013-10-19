using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniVRPNity.Device
{
    public partial class Keyboard
    {

        /// <summary>
        /// French keyboard key code layout.
        /// http://msdn.microsoft.com/en-us/library/ms892155
        /// </summary>
        public enum FrenchKeys
        {
            KC_OEM_7 = 0x29, // ²
            KC_1 = 0x02, // & 1
            KC_2 = 0x03, // é 2
            KC_3 = 0x04, // " 3 
            KC_4 = 0x05, // ' 4
            KC_5 = 0x06, // ( 5
            KC_6 = 0x07, // - 6 
            KC_7 = 0x08, // è 7 
            KC_8 = 0x09, // _ 8 
            KC_9 = 0x0a, // ç 9 
            KC_0 = 0x0b, // à @
            KC_OEM_4 = 0x0c, // ) ° ]
            KC_OEM_PLUS = 0x0d, // = + }
            KC_A = 0x10, // a A
            KC_Z = 0x11, // z Z
            KC_E = 0x12, // e E
            KC_R = 0x13, // r R
            KC_T = 0x14, // t T
            KC_Y = 0x15, // y Y
            KC_U = 0x16, // u U
            KC_I = 0x17, // i I
            KC_O = 0x18, // o O
            KC_P = 0x19, // p P
            KC_OEM_6 = 0x1a, // ^ ¨ 001b
            KC_OEM_1 = 0x1b, // $ £ 001d
            KC_OEM_5 = 0x2b, // * µ 001c
            KC_Q = 0x1e, // q Q
            KC_S = 0x1f, // s S
            KC_D = 0x20, // d D
            KC_F = 0x21, // f F
            KC_G = 0x22, // g G
            KC_H = 0x23, // h H
            KC_J = 0x24, // j J
            KC_K = 0x25, // k K
            KC_L = 0x26, // l L
            KC_M = 0x27, // m M
            KC_OEM_3 = 0x28, // ù %
            KC_W = 0x2c, // w W
            KC_X = 0x2d, // x X
            KC_C = 0x2e, // c C
            KC_V = 0x2f, // v V
            KC_B = 0x30, // b B
            KC_N = 0x31, // n N
            KC_OEM_COMMA = 0x32, // , ?
            KC_OEM_PERIOD = 0x33, // ; .
            KC_OEM_2 = 0x34, // : /
            KC_DECIMAL = 0x53, // . .
            KC_OEM_102 = 0x56, // < > 001c
            KC_OEM_8 = 0x5f, // ! §
            KC_00e2 = 0x0061,
            KC_00ee = 0x0065,
            KC_00f4 = 0x006f,
            KC_00fb = 0x0075,
            KC_00c2 = 0x0041,
            KC_00ca = 0x0045,
            KC_00ce = 0x0049,
            KC_00d4 = 0x004f,
            KC_00db = 0x0055,
            KC_005e = 0x0020,
            KC_00e4 = 0x0061,
            KC_00eb = 0x0065,
            KC_00ef = 0x0069,
            KC_00f6 = 0x006f,
            KC_00fc = 0x0075,
            KC_00ff = 0x0079,
            KC_00c4 = 0x0041,
            KC_00cb = 0x0045,
            KC_00cf = 0x0049,
            KC_00d6 = 0x004f,
            KC_00dc = 0x0055,
            KC_00a8 = 0x0020,
            KC_00e3 = 0x0061, // a -> ã
            KC_00c3 = 0x0041, // // A -> Ã
            KC_00f1 = 0x006e, // n -> ñ
            KC_00d1 = 0x004e, // N -> Ñ
            KC_00f5 = 0x006f, // o -> õ
            KC_00d5 = 0x004f, // O -> Õ
            KC_007e = 0x0020, // -> ~
            KC_00e0 = 0x0061, // a -> à
            KC_00e8 = 0x0065, // e -> è
            KC_00ec = 0x0069, // i -> ì
            KC_00f2 = 0x006f, // o -> ò
            KC_00f9 = 0x0075, // u -> ù
            KC_00c0 = 0x0041, // A -> À
            KC_00c8 = 0x0045, // E -> È
            KC_00cc = 0x0049, // I -> Ì
            KC_00d2 = 0x004f, // O -> Ò
            KC_00d9 = 0x0055, // U -> Ù
            KC_0060 = 0x0020, // -> `
            KC_ECHAP = 0x01,
            KC_RETARR = 0x0e,
            KC_TAB = 0x0f,
            KC_ENTREE = 0x1c,
            KC_CTRL = 0x1d,
            KC_MAJ = 0x2a,
            KC_MAJ_DROITE = 0x36,
            KC_NUM_STAR = 0x37, // *
            KC_ALT = 0x38,
            KC_ESPACE = 0x39,
            KC_VERR_MAJ = 0x3a,
            KC_F1 = 0x3b,
            KC_F2 = 0x3c,
            KC_F3 = 0x3d,
            KC_F4 = 0x3e,
            KC_F5 = 0x3f,
            KC_F6 = 0x40,
            KC_F7 = 0x41,
            KC_F8 = 0x42,
            KC_F9 = 0x43,
            KC_F10 = 0x44,
            KC_Pause = 0x45,
            KC_DEFIL = 0x46,
            KC_NUM_7 = 0x47,
            KC_NUM_8 = 0x48,
            KC_NUM_9 = 0x49,
            KC_NUM_MINUS = 0x4a, // -
            KC_NUM_4 = 0x4b,
            KC_NUM_5 = 0x4c,
            KC_NUM_6 = 0x4d,
            KC_NUM_PLUS = 0x4e, //+
            KC_NUM_1 = 0x4f,
            KC_NUM_2 = 0x50,
            KC_NUM_3 = 0x51,
            KC_NUM_0 = 0x52,
            KC_NUM_POINT = 0x53, // .
            KC_F11 = 0x57,
            KC_F12 = 0x58,
            KC_NUM_ENTREE = 0x1c,
            KC_CTRL_DROITE = 0x1d,
            KC_NUM_DIV = 0x35, // /
            KC_IMPR_ECRAN = 0x37,
            KC_ALT_DROITE = 0x38,
            KC_VER_NUM = 0x45,
            KC_ATTN = 0x46,
            KC_ORIGINE = 0x47,
            KC_HAUT = 0x48,
            KC_PG_PREC = 0x49,
            KC_GAUCHE = 0x4b,
            KC_DROITE = 0x4d,
            KC_FIN = 0x4f,
            KC_BAS = 0x50,
            KC_PG_SUIV = 0x51,
            KC_INS = 0x52,
            KC_SUPPR = 0x53,
            KC_00 = 0x54,
            KC_AIDE = 0x56,
            KC_WINDOWS_GAUCHE = 0x5b,
            KC_WINDOWS_DROITE = 0x5c,
            KC_APPLICATION = 0x5d,
            NONE
        }
    }

}
