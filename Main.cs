using System;
using System.Collections.Generic;
using UnityEngine;
using RoR2;

namespace RoRCheats
{
    public class Main : MonoBehaviour
    {
     
        private void OnGUI()
        {
            Menu.SetRect();
            if (Menu.isMenuOpen)
            {
                Menu.DrawMenu();
            }
        }

        public void Update()
        {
            CheckInputs();
            Menu.Update();
        }

        private void CheckInputs()
        {
            if (Input.GetKeyDown(KeyCode.Insert))
            {
                Menu.isMenuOpen = !Menu.isMenuOpen;
                Character.GetCharacter();
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                Character.GiveMoney();
            }
        }

        public void Start()
        {
            Menu.Start();
        }
    }
}