using System;
using System.Collections.Generic;
using Behavior;
using PlasmaModding;
using Newtonsoft.Json;

namespace JSONNode
{
    public class JSONSet : CustomAgent 
    {
        [SketchNodePortOperation(1)]
        public void Set(SketchNode node)
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


            // Output
            json[GetProperty("Key").GetValueString()] = GetProperty("Value").GetValueString();
         
            WriteOutput("Result", new Data(JsonConvert.SerializeObject(json)));
        }
    }
}