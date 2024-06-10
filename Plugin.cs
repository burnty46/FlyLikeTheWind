using System;
using System.Runtime.InteropServices.ComTypes;
using BepInEx;
using GorillaNetworking;
using HarmonyLib;
using UnityEngine;
using Utilla;
using System.Threading.Tasks;

namespace GorillaTagModTemplateProject
{
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        bool inRoom = false;
        bool modEnabled = true;
        bool toggleButtonPressedLastFrame = false;
        
        void Start()
        {
            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        void OnEnable()
        {
            HarmonyPatches.ApplyHarmonyPatches();
        }

        void OnDisable()
        {
            HarmonyPatches.RemoveHarmonyPatches();
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
           
        }







        void Update()
        {
            if (!inRoom) return;




            bool toggleButtonPressed = ControllerInputPoller.instance.rightControllerSecondaryButton;

            if (toggleButtonPressed && !toggleButtonPressedLastFrame)
            {
                modEnabled = !modEnabled;

                if (modEnabled)
                {
                    OnEnable();
                }
                else
                {
                    OnDisable();
                }
            }

            toggleButtonPressedLastFrame = toggleButtonPressed;

            if (!modEnabled) return;

            if (ControllerInputPoller.instance.rightGrab)
            
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = GorillaLocomotion.Player.Instance.headCollider.transform.forward * Time.deltaTime * 1600f;
            
            else if(ControllerInputPoller.instance.leftGrab)
            
               GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = GorillaLocomotion.Player.Instance.headCollider.transform.forward * Time.deltaTime * -1500f;
            
            else if(ControllerInputPoller.instance.rightControllerPrimaryButton)
            
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = GorillaLocomotion.Player.Instance.bodyCollider.transform.right * Time.deltaTime * 1600f;
            
            else if(ControllerInputPoller.instance.leftControllerPrimaryButton)
            
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = GorillaLocomotion.Player.Instance.bodyCollider.transform.right * Time.deltaTime * -1600f;
            
            else if(ControllerInputPoller.instance.rightControllerIndexFloat >= 0.5f)

                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().AddForce(new Vector3(0, 2300, 0));

            else if(ControllerInputPoller.instance.leftControllerIndexFloat >= 0.5f)

                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().AddForce(new Vector3(0, -2300, 0));



        }





        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            inRoom = true;
        }

        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            inRoom = false;
        }

    }
}
