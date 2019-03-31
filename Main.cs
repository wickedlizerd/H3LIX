using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RoR2;
using RoR2.UI;
using System.IO;
using System.Runtime.InteropServices;
using Rewired;

namespace RoRCheats
{
    public class Main : MonoBehaviour
    {

        private bool _isMenuOpen;
        private static RoR2.CharacterMaster LocalPlayer;
        private static RoR2.Inventory LocalPlayerInv;

        private void OnGUI()
        {
            if (_isMenuOpen)
            {
                DrawESPMenu();
            }
        }

        public void Start()
        {
            LocalPlayer = LocalUserManager.GetFirstLocalUser().cachedMasterController.master;
            LocalPlayerInv = LocalPlayer.GetComponent<Inventory>();
            //Do stuff here once for initialization
        }
        public void Update()
        {
            //Do stuff here on every tick

            //try and fix local player on menu injection
           // LocalPlayer.OnBodyStart = ResetFix(); how?
            CheckInputs();
        }
        private void CheckInputs()
        {
            if (Input.GetKeyDown(KeyCode.Insert))
            {
                _isMenuOpen = !_isMenuOpen;
                if (_isMenuOpen)
                {
                    LocalPlayer.GetComponent<Camera>().enabled = false;
                    Cursor.visible = true;
                }
                else
                {
                    Cursor.visible = false;
                    LocalPlayer.GetComponent<Camera>().enabled = true;
                }
            }
            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                GiveMoney();
            }
            if (Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                RollItem();
            }
        }

        private void DrawESPMenu()
        {
            GUI.color = Color.black;
            GUI.Box(new Rect(100f, 100f, 190f, 190f), "");

            GUI.color = Color.white;
            GUI.Label(new Rect(140f, 110f, 150f, 20f), "RoRCheats");

            // _showExtractESP = GUI.Toggle(new Rect(110f, 180f, 120f, 20f), _showExtractESP, "  Extract ESP");
            if (GUI.Button(new Rect(140f, 140f, 120f, 20f), "  Random Item"))
                RollItem();
        }

        private void RollItem()
        {
            try
            {
                int num;
                TextSerialization.TryParseInvariant("1", out num);
                if (num > 0)
                {
                    WeightedSelection<List<PickupIndex>> weightedSelection = new WeightedSelection<List<PickupIndex>>(8);
                    weightedSelection.AddChoice(Run.instance.availableTier1DropList, 80f);
                    weightedSelection.AddChoice(Run.instance.availableTier2DropList, 19f);
                    weightedSelection.AddChoice(Run.instance.availableTier3DropList, 1f);
                    for (int i = 0; i < num; i++)
                    {
                        List<PickupIndex> list = weightedSelection.Evaluate(UnityEngine.Random.value);
                        LocalPlayerInv.GiveItem(list[UnityEngine.Random.Range(0, list.Count)].itemIndex, 1);
                    }
                }
            }
            catch (ArgumentException)
            {
            }
        }

        private void ResetFix()
        {
            LocalPlayer = LocalUserManager.GetFirstLocalUser().cachedMasterController.master;
            LocalPlayerInv = LocalPlayer.GetComponent<Inventory>();
        }
     
        private void GiveMoney()
        {
            LocalPlayer.GiveMoney(100);
        }
        private void EnableCheats()
        {
            RoR2Application.cvCheats = new RoR2Application.CheatsConVar("cheats", ConVarFlags.ExecuteOnServer, "1", "haxx");
        }
    }
}