using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace func_parsesvc
{
    public  static  class Constants
    {
       public  const string FUNCTIONDESCRIPTION = @"This function will parse the Request Body for the CSV content. Based  on Input Variable (UseHeaders (bool)) will the function use the Header info from the first row. Otherwise is will use the headers supplied from the QueryString param (headers)";
        public const string FUNCTIONSUMMARY = @"Get the JSON Array from the CSV Content";


        public const string OBJECTNAMEPARAMSUMMARY = @"The name of the JSON object returned";
        public const string OBJECTNAMEPARAMDESCRIPTION = @"JSON Object name: what does one line represent e.g. { objectname : { 'key' :'value' } } ";

        public const string HEADERPARAMSUMMARY = @"Comma Seperated array of HeaderNames";
        public const string HEADERPARAMDESCRIPTION = @"Headernames of the CSV content if missing in the first line. If present those headernames will be used in the Result Objects as JSON property names ";


        public const string SEPARATORPARAMSUMMARY = @"The field separator";
        public const string SEPARATORPARAMDESCRIPTION = @"Give the separator is not comma for eacht field. eg: '|' or ':'  or ';' ";






    }
}
