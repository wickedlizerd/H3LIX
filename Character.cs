using System;
using System.Collections.Generic;
using UnityEngine;
using RoR2;

namespace RoRCheats
{
    class Character
    {
        public static RoR2.CharacterMaster LocalPlayer;
        public static RoR2.Inventory LocalPlayerInv;
        public static RoR2.HealthComponent LocalHealth;
        public static RoR2.SkillLocator LocalSkills;
        public static RoR2.NetworkUser LocalNetworkUser;
        public static bool isCharacterSelected = false;

        public void Update()
        {
            CharacterRoutine();
            StatsRoutine();
        }

        public void CharacterRoutine()
        {
            if (!isCharacterSelected)
            {
                GetCharacter();
            }
        }

        public void StatsRoutine()
        {
            if (isCharacterSelected)
            {
                if (Menu.skillToggle)
                {
                    LocalSkills.ApplyAmmoPack();
                }
            }
        }

        // random items
        public static void RollItems(string ammount)
        {
            try
            {
                int num;
                TextSerialization.TryParseInvariant(ammount, out num);
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

        // try and setup our character, if we hit an error we set it to false
        //TODO: Find a way to stop it from checking whilst in main menu/lobby menu
        public static void GetCharacter()
        {
            try
            {
                LocalNetworkUser = null;
                foreach (NetworkUser readOnlyInstance in NetworkUser.readOnlyInstancesList)
                {
                    //localplayer == you!
                    if (readOnlyInstance.isLocalPlayer)
                    {
                        LocalNetworkUser = readOnlyInstance;
                        LocalPlayer = LocalNetworkUser.master;
                        LocalPlayerInv = LocalPlayer.GetComponent<Inventory>();
                        LocalHealth = LocalPlayer.GetBody().GetComponent<HealthComponent>();
                        LocalSkills = LocalPlayer.GetBody().GetComponent<SkillLocator>();
                        if (LocalPlayer.alive) isCharacterSelected = true;
                        else isCharacterSelected = false;
                    }
                }
            }
            catch (Exception e)
            {
                isCharacterSelected = false;
            }
        }
        //clears inventory, duh.
        public static void ClearInventory()
        {
            if (LocalPlayerInv)
            {
                for (ItemIndex itemIndex = ItemIndex.Syringe; itemIndex < (ItemIndex)78; itemIndex++)
                {

                    LocalPlayerInv.ResetItem(itemIndex);

                }
                LocalPlayerInv.SetEquipmentIndex(EquipmentIndex.None);
            }
        }
        //TODO:
        //Seems like after giving all items and removing all items, something breaks.
        //throws ArgumentException: Destination array was not long enough. Look into later.
        public static void GiveAllItems()
        {
            if (LocalPlayerInv)
            {
                for (ItemIndex itemIndex = ItemIndex.Syringe; itemIndex < (ItemIndex)78; itemIndex++)
                {
                    //plantonhit kills you when you pick it up
                    if (itemIndex == ItemIndex.PlantOnHit)
                        continue;
                    //ResetItem sets quantity to 1, RemoveItem removes the last one.
                    LocalPlayerInv.GiveItem(itemIndex, Menu.allItemsQuantity);
                }
            }
        }

        public static void StackInventory()
        {
            //Does the same thing as the shrine of order. Orders all your items into stacks of several random items.
            LocalPlayerInv.ShrineRestackInventory(Run.instance.runRNG);
        }
        // self explanatory
        public static void GiveXP()
        {
            LocalPlayer.GiveExperience(Menu.xpToGive);
        }
        public static void GiveMoney()
        {
            LocalPlayer.GiveMoney(Menu.moneyToGive);
        }
        //uh, duh.
        public static void GiveLunarCoins()
        {
            LocalNetworkUser.AwardLunarCoins(Menu.coinsToGive);
        }
    }
}
