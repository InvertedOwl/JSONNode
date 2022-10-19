using System;
using System.Collections.Generic;
using Behavior;
using PlasmaModding;
using Newtonsoft.Json;

namespace JSONNode
{
    public class JSONGet : CustomAgent
    {
        [SketchNodePortOperation(1)]
        public void Get(SketchNode node)
        {
            // Try parse
            Dictionary<string, string> json = null;
            try
            {
                json = JsonConvert.DeserializeObject<Dictionary<string, string>>(GetProperty("Json").GetValueString());
            }
            catch (Exception)
            {
                WriteOutput("Result", new Data("JSON not formatted correctly!"));
                return;
            }

            // Try get
            if (!json.ContainsKey(GetProperty("Key").GetValueString()))
            {
                WriteOutput("Result", new Data());
                return;
            }

            WriteOutput("Result", new Data(json[GetProperty("Key").GetValueString()]));
        }
    }
}