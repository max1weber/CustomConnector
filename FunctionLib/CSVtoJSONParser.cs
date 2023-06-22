using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace FunctionLib
{
    public class CSVtoJSONParser
    {

        private char _separator = ',';

       public CSVtoJSONParser(char separator = ',') { 
           _separator = separator;
        }


        public string ParseCSV(string[]  lines,  string objectname = "jsonobject", string[]? headernames =null) 
        {

            
            int start = 0;
            if (headernames == null || headernames.Length <= 1)
            {
                start = 1;
                headernames = lines[0].Split(_separator);  // if there are no headers supplied just pick the firstline and split them into headernames
            }

            List<Dictionary<string, string>> jsonobjects = new List<Dictionary<string, string>>();
            for (int i = start; i < lines.Length; i++)
            {

                var objResult = new Dictionary<string, string>();

                string[] values = lines[i].Split(_separator);
                if (values.Length == headernames.Length)
                {
                    for (int j = 0; j < headernames.Length; j++)
                    {

                        if (!string.IsNullOrEmpty(headernames[j]))
                            objResult.Add(headernames[j], values[j]);


                    }
                }
               
               
                jsonobjects.Add(objResult);
            }

            return JsonConvert.SerializeObject(jsonobjects);

        }

    }
}
