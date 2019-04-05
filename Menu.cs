using System;
using System.Collections.Generic;
using UnityEngine;
using RoR2;

namespace RoRCheats
{
    public class Menu : MonoBehaviour
    {
        public static bool isMenuOpen = false;
        private static bool _ifDragged = false;

        public static GUIStyle BgStyle, OnStyle, OffStyle, LabelStyle, BtnStyle, CornerStyle;
        public static GUIStyle BtnStyle1, BtnStyle2, BtnStyle3;
        public static bool skillToggle, renderInteractables;
        public static float delay = 0, widthSize = 600;

        public static Rect rect = new Rect(10, 10, 20, 20);
        public static Texture2D Image = null, ontexture, onpresstexture, offtexture, offpresstexture, cornertexture, backtexture, btntexture, btnpresstexture;
        public static Texture2D NewTexture2D { get { return new Texture2D(1, 1); } }
        public static int itemsToRoll = 10;
        public static int allItemsQuantity = 5;
        public static ulong xpToGive = 100;
        public static uint moneyToGive = 100;
        public static uint coinsToGive = 10;
        public static int btnY, mulY;


        public static void SetRect()
        {
            rect = GUI.Window(0, rect, new GUI.WindowFunction(SetBG), "", new GUIStyle());
        }

        public static void Update()
        {
            ESPRoutine();
        }

        public static void Start()
        {
            if (BgStyle == null)
            {
                BgStyle = new GUIStyle();
                BgStyle.normal.background = BackTexture;
                BgStyle.onNormal.background = BackTexture;
                BgStyle.active.background = BackTexture;
                BgStyle.onActive.background = BackTexture;
                BgStyle.normal.textColor = Color.white;
                BgStyle.onNormal.textColor = Color.white;
                BgStyle.active.textColor = Color.white;
                BgStyle.onActive.textColor = Color.white;
                BgStyle.fontSize = 18;
                BgStyle.fontStyle = FontStyle.Normal;
                BgStyle.alignment = TextAnchor.UpperCenter;
            }

            if (CornerStyle == null)
            {
                CornerStyle = new GUIStyle();
                CornerStyle.normal.background = BtnTexture;
                CornerStyle.onNormal.background = BtnTexture;
                CornerStyle.active.background = BtnTexture;
                CornerStyle.onActive.background = BtnTexture;
                CornerStyle.normal.textColor = Color.white;
                CornerStyle.onNormal.textColor = Color.white;
                CornerStyle.active.textColor = Color.white;
                CornerStyle.onActive.textColor = Color.white;
                CornerStyle.fontSize = 18;
                CornerStyle.fontStyle = FontStyle.Normal;
                CornerStyle.alignment = TextAnchor.UpperCenter;
            }

            if (LabelStyle == null)
            {
                LabelStyle = new GUIStyle();
                LabelStyle.normal.textColor = Color.white;
                LabelStyle.onNormal.textColor = Color.white;
                LabelStyle.active.textColor = Color.white;
                LabelStyle.onActive.textColor = Color.white;
                LabelStyle.fontSize = 18;
                LabelStyle.fontStyle = FontStyle.Normal;
                LabelStyle.alignment = TextAnchor.UpperCenter;
            }

            if (OffStyle == null)
            {
                OffStyle = new GUIStyle();
                OffStyle.normal.background = OffTexture;
                OffStyle.onNormal.background = OffTexture;
                OffStyle.active.background = OffPressTexture;
                OffStyle.onActive.background = OffPressTexture;
                OffStyle.normal.textColor = Color.white;
                OffStyle.onNormal.textColor = Color.white;
                OffStyle.active.textColor = Color.white;
                OffStyle.onActive.textColor = Color.white;
                OffStyle.fontSize = 18;
                OffStyle.fontStyle = FontStyle.Normal;
                OffStyle.alignment = TextAnchor.MiddleCenter;
            }

            if (OnStyle == null)
            {
                OnStyle = new GUIStyle();
                OnStyle.normal.background = OnTexture;
                OnStyle.onNormal.background = OnTexture;
                OnStyle.active.background = OnPressTexture;
                OnStyle.onActive.background = OnPressTexture;
                OnStyle.normal.textColor = Color.white;
                OnStyle.onNormal.textColor = Color.white;
                OnStyle.active.textColor = Color.white;
                OnStyle.onActive.textColor = Color.white;
                OnStyle.fontSize = 18;
                OnStyle.fontStyle = FontStyle.Normal;
                OnStyle.alignment = TextAnchor.MiddleCenter;
            }

            if (BtnStyle == null)
            {
                BtnStyle = new GUIStyle();
                BtnStyle.normal.background = BtnTexture;
                BtnStyle.onNormal.background = BtnTexture;
                BtnStyle.active.background = BtnPressTexture;
                BtnStyle.onActive.background = BtnPressTexture;
                BtnStyle.normal.textColor = Color.white;
                BtnStyle.onNormal.textColor = Color.white;
                BtnStyle.active.textColor = Color.white;
                BtnStyle.onActive.textColor = Color.white;
                BtnStyle.fontSize = 18;
                BtnStyle.fontStyle = FontStyle.Normal;
                BtnStyle.alignment = TextAnchor.MiddleCenter;
            }
        }

        private static void ESPRoutine()
        {
            if (renderInteractables)
            {
                RenderInteractables();
            }
        }

        public static void SetBG(int windowID)
        {
            GUI.Box(new Rect(0f, 0f, widthSize + 10, 50f + 45 * mulY), "", CornerStyle);
            if (Event.current.type == EventType.MouseDrag)
            {
                delay += Time.deltaTime;
                if (delay > 0.3f)
                {
                    _ifDragged = true;
                }
            }
            else if (Event.current.type == EventType.MouseUp)
            {
                delay = 0;
                if (!_ifDragged)
                    isMenuOpen = !isMenuOpen;
                _ifDragged = false;
            }
            GUI.DragWindow();
        }

        public static void DrawMenu()
        {

            GUI.Box(new Rect(rect.x + 0f, rect.y + 0f, widthSize + 10, 50f + 45 * mulY), "", BgStyle);
            GUI.Label(new Rect(rect.x + 0f, rect.y + 5f, widthSize + 10, 95f), "Spektre menu\nv 0.03", LabelStyle);


            if (Character.isCharacterSelected)
            {

                // we dont have a god toggle bool, because we can just ref localhealth
                if (Character.LocalHealth.godMode)
                {
                    if (GUI.Button(BtnRect(1, false), "God mode: ON", OnStyle))
                    {
                        Character.LocalHealth.godMode = false;
                    }
                }
                else if (GUI.Button(BtnRect(1, false), "God mode: OFF", OffStyle))
                {
                    Character.LocalHealth.godMode = true;
                }

                if (skillToggle)
                {
                    if (GUI.Button(BtnRect(2, false), "Infinite Skills: ON", OnStyle))
                    {
                        skillToggle = false;
                    }
                }
                else if (GUI.Button(BtnRect(2, false), "Infinite Skills: OFF", OffStyle))
                {
                    skillToggle = true;
                }

                if (renderInteractables)
                {
                    if (GUI.Button(BtnRect(3, false), "Interactables ESP: ON", OnStyle))
                    {
                        renderInteractables = false;
                    }
                }
                else if (GUI.Button(BtnRect(3, false), "Interactables ESP: OFF", OffStyle))
                {
                    renderInteractables = true;
                }

                if (GUI.Button(BtnRect(4, true), "Give Money: " + moneyToGive.ToString(), BtnStyle))
                {
                    Character.GiveMoney();
                }
                if (GUI.Button(new Rect(rect.x + widthSize - 80, rect.y + btnY, 40, 40), "-", OffStyle))
                {
                    if (moneyToGive > 100)
                        moneyToGive -= 100;
                }
                if (GUI.Button(new Rect(rect.x + widthSize - 35, rect.y + btnY, 40, 40), "+", OffStyle))
                {
                    if (moneyToGive >= 100)
                        moneyToGive += 100;
                }
                if (GUI.Button(BtnRect(5, true), "Give Lunar Coins: " + coinsToGive.ToString(), BtnStyle))
                {
                    Character.GiveLunarCoins();
                }
                if (GUI.Button(new Rect(rect.x + widthSize - 80, rect.y + btnY, 40, 40), "-", OffStyle))
                {
                    if (coinsToGive > 10)
                        coinsToGive -= 10;
                }
                if (GUI.Button(new Rect(rect.x + widthSize - 35, rect.y + btnY, 40, 40), "+", OffStyle))
                {
                    if (coinsToGive >= 10)
                        coinsToGive += 10;
                }
                if (GUI.Button(BtnRect(6, true), "Give Experience: " + xpToGive.ToString(), BtnStyle))
                {
                    Character.GiveXP();
                }
                if (GUI.Button(new Rect(rect.x + widthSize - 80, rect.y + btnY, 40, 40), "-", OffStyle))
                {
                    if (xpToGive > 100)
                        xpToGive -= 100;
                }
                if (GUI.Button(new Rect(rect.x + widthSize - 35, rect.y + btnY, 40, 40), "+", OffStyle))
                {
                    if (xpToGive >= 100)
                        xpToGive += 100;
                }
                if (GUI.Button(BtnRect(7, true), "Roll Items: " + itemsToRoll.ToString(), BtnStyle))
                {
                    Character.RollItems(itemsToRoll.ToString());
                }
                if (GUI.Button(new Rect(rect.x + widthSize - 80, rect.y + btnY, 40, 40), "-", OffStyle))
                {
                    if (itemsToRoll > 5)
                        itemsToRoll -= 5;
                }
                if (GUI.Button(new Rect(rect.x + widthSize - 35, rect.y + btnY, 40, 40), "+", OffStyle))
                {
                    if (itemsToRoll >= 5)
                        itemsToRoll += 5;
                }
                if (GUI.Button(BtnRect(8, true), "Give All Items: " + allItemsQuantity.ToString(), BtnStyle))
                {
                    Character.GiveAllItems();
                }
                if (GUI.Button(new Rect(rect.x + widthSize - 80, rect.y + btnY, 40, 40), "-", OffStyle))
                {
                    if (allItemsQuantity > 1)
                        allItemsQuantity -= 1;
                }
                if (GUI.Button(new Rect(rect.x + widthSize - 35, rect.y + btnY, 40, 40), "+", OffStyle))
                {
                    if (allItemsQuantity >= 1)
                        allItemsQuantity += 1;
                }
                if (GUI.Button(BtnRect(9, false), "Stack Inventory", BtnStyle))
                {
                    Character.StackInventory();

                }
                if (GUI.Button(BtnRect(10, false), "Clear Inventory", BtnStyle))
                {

                    Character.ClearInventory();
                }
            }
            else
            {
                GUI.Box(new Rect(rect.x, rect.y + 95f * mulY, widthSize + 10, 50f), "", BgStyle);
                GUI.Label(new Rect(rect.x, rect.y + 95f * mulY, widthSize + 10, 50f), "<color=yellow>Note: Buttons Will Appear in Match Only</color>", LabelStyle);
            }
        }

        // Rect for buttons
        // It automatically auto position buttons. There is no need to change it
        public static Rect BtnRect(int y, bool multiplyBtn)
        {
            mulY = y;
            if (multiplyBtn)
            {
                btnY = 5 + 45 * y;
                return new Rect(rect.x + 5, rect.y + 5 + 45 * y, widthSize - 90, 40);
            }
            return new Rect(rect.x + 5, rect.y + 5 + 45 * y, widthSize, 40);
        }

        // Textures
        public static Texture2D BtnTexture
        {
            get
            {
                if (btntexture == null)
                {
                    btntexture = NewTexture2D;
                    btntexture.SetPixel(0, 0, new Color32(3, 155, 229, 255));
                    btntexture.Apply();
                }
                return btntexture;
            }
        }

        public static Texture2D BtnPressTexture
        {
            get
            {
                if (btnpresstexture == null)
                {
                    btnpresstexture = NewTexture2D;
                    btnpresstexture.SetPixel(0, 0, new Color32(2, 119, 189, 255));
                    btnpresstexture.Apply();
                }
                return btnpresstexture;
            }
        }

        public static Texture2D OnPressTexture
        {
            get
            {
                if (onpresstexture == null)
                {
                    onpresstexture = NewTexture2D;
                    onpresstexture.SetPixel(0, 0, new Color32(62, 119, 64, 255));
                    onpresstexture.Apply();
                }
                return onpresstexture;
            }
        }

        public static Texture2D OnTexture
        {
            get
            {
                if (ontexture == null)
                {
                    ontexture = NewTexture2D;
                    ontexture.SetPixel(0, 0, new Color32(79, 153, 82, 255));
                    ontexture.Apply();
                }
                return ontexture;
            }
        }

        public static Texture2D OffPressTexture
        {
            get
            {
                if (offpresstexture == null)
                {
                    offpresstexture = NewTexture2D;
                    offpresstexture.SetPixel(0, 0, new Color32(79, 79, 79, 255));
                    offpresstexture.Apply();
                }
                return offpresstexture;
            }
        }

        public static Texture2D OffTexture
        {
            get
            {
                if (offtexture == null)
                {
                    offtexture = NewTexture2D;
                    offtexture.SetPixel(0, 0, new Color32(99, 99, 99, 255));
                    offtexture.Apply();
                }
                return offtexture;
            }
        }

        public static Texture2D BackTexture
        {
            get
            {
                if (backtexture == null)
                {
                    backtexture = NewTexture2D;
                    //ToHtmlStringRGBA  new Color(33, 150, 243, 1)
                    backtexture.SetPixel(0, 0, new Color32(42, 42, 42, 200));
                    backtexture.Apply();
                }
                return backtexture;
            }
        }

        public static Texture2D CornerTexture
        {
            get
            {
                if (cornertexture == null)
                {
                    cornertexture = NewTexture2D;
                    //ToHtmlStringRGBA  new Color(33, 150, 243, 1)
                    cornertexture.SetPixel(0, 0, new Color32(42, 42, 42, 0));
                    cornertexture.Apply();
                }
                return cornertexture;
            }
        }

        private static void RenderInteractables()
        {
            foreach (PurchaseInteraction purchaseInteraction in PurchaseInteraction.readOnlyInstancesList)
            {
                if (purchaseInteraction.available)
                {
                    float distanceToObject = Vector3.Distance(Camera.main.transform.position, purchaseInteraction.transform.position);
                    Vector3 Position = Camera.main.WorldToScreenPoint(purchaseInteraction.transform.position);
                    var BoundingVector = new Vector3(Position.x, Position.y, Position.z);
                    if (BoundingVector.z > 0.01)
                    {
                        GUI.color = Color.green;
                        int distance = (int)distanceToObject;
                        String friendlyName = purchaseInteraction.GetDisplayName();
                        int cost = purchaseInteraction.cost;
                        string boxText = $"{friendlyName}\n${cost}\n{distance}m";
                        GUI.Label(new Rect(BoundingVector.x - 50f, (float)Screen.height - BoundingVector.y, 100f, 50f), boxText);
                    }
                }
            }
        }
    }
}