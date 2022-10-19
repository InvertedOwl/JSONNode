using HarmonyLib;
using System.Reflection;
using Behavior;
using UnityModManagerNet;
using PlasmaModding;

namespace JSONNode
{
#if DEBUG
	[EnableReloading]
#endif
    public static class Main
    {
        static Harmony harmony;

        public static bool Load(UnityModManager.ModEntry entry)
        {
            // This is very messy but I dont care enough to make it better 🤷‍
            
            AgentCategoryEnum category = CustomNodeManager.CustomCategory("HTTP");
            AgentGestalt setGestlat = CustomNodeManager.CreateGestalt(typeof(JSONSet), "JSON Set", "Set a string parsed as json value from key", category);
            CustomNodeManager.CreateCommandPort(setGestlat, "Set", "Set Json Value", 1);
            CustomNodeManager.CreatePropertyPort(setGestlat, "Json", "Json to be parsed", Data.Types.String, true, new Data(""));
            CustomNodeManager.CreatePropertyPort(setGestlat, "Key", "Key to be set", Data.Types.String, true, new Data(""));
            CustomNodeManager.CreatePropertyPort(setGestlat, "Value", "Value to be set", Data.Types.String, true, new Data(""));
            CustomNodeManager.CreateOutputPort(setGestlat, "Result", "Resulting JSON string", Data.Types.String);
            CustomNodeManager.CreateNode(setGestlat, "JSON Set");
            
            AgentGestalt getGestalt = CustomNodeManager.CreateGestalt(typeof(JSONGet), "JSON Get", "Get a string parsed as json value from key", category);
            CustomNodeManager.CreateCommandPort(getGestalt, "Get", "Get Json Value", 1);
            CustomNodeManager.CreatePropertyPort(getGestalt, "Json", "Json to be parsed", Data.Types.String, true, new Data(""));
            CustomNodeManager.CreatePropertyPort(getGestalt, "Key", "Key to get", Data.Types.String, true, new Data(""));
            CustomNodeManager.CreateOutputPort(getGestalt, "Result", "Resulting JSON string", Data.Types.String);
            CustomNodeManager.CreateNode(getGestalt, "JSON Get");

            harmony = new Harmony(entry.Info.Id);

            entry.OnToggle = OnToggle;
#if DEBUG
			entry.OnUnload = OnUnload;
#endif

            return true;
        }

        static bool OnToggle(UnityModManager.ModEntry entry, bool active)
        {
            if (active)
            {
                harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            else
            {
                harmony.UnpatchAll(entry.Info.Id);
            }

            return true;
        }

#if DEBUG
		static bool OnUnload(UnityModManager.ModEntry entry) {
			return true;
        }
#endif
        
    }
}
