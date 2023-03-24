
using BepInEx;
using BepInEx.Configuration;
using UnityEngine;

namespace MC_SVFleetRoleHotkeys
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    public class Main : BaseUnityPlugin
    {
        public const string pluginGuid = "mc.starvalor.fleetrolehotkeys";
        public const string pluginName = "SV Fleet Role Hotkeys";
        public const string pluginVersion = "1.0.0";

        public static ConfigEntry<KeyCodeSubset> dps;
        public static ConfigEntry<KeyCodeSubset> healer;
        public static ConfigEntry<KeyCodeSubset> miner;

        public void Awake()
        {
            dps = Config.Bind<KeyCodeSubset>("Keybinds",
                "DPS",
                KeyCodeSubset.F2,
                "Set DPS role");
            healer = Config.Bind<KeyCodeSubset>("Keybinds",
                "Healer",
                KeyCodeSubset.F3,
                "Set healer role");
            miner = Config.Bind<KeyCodeSubset>("Keybinds",
                "Miner",
                KeyCodeSubset.F4,
                "Set miner role");
        }

        public void Update()
        {
            if (GameManager.instance != null && GameManager.instance.inGame)
            {
                int role = 99;
                if (Input.GetKeyDown((KeyCode)dps.Value))
                    role = (int)SVUtil.AIBehaviourRole.dps;
                if (Input.GetKeyDown((KeyCode)healer.Value))
                    role = (int)SVUtil.AIBehaviourRole.healer;
                if (Input.GetKeyDown((KeyCode)miner.Value))
                    role = (int)SVUtil.AIBehaviourRole.miner;

                if (role != 99)
                {
                    if (PChar.Char.mercenaries != null && PChar.Char.mercenaries.Count > 0)
                    {
                        foreach(AICharacter aiChar in PChar.Char.mercenaries)
                        {
                            if(aiChar is PlayerFleetMember)
                                aiChar.behavior.role = role;
                        }

                        if (FleetControl.instance != null)
                            FleetControl.instance.Refresh(false);
                    }
                    role = 99;
                }
            }
        }
    }
}
