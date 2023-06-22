using FunctionLib;
using System.Reflection;

namespace Tests
{
    public class FunctionLibTests

    {
        [Fact]
        public void CSVtoJSONParser_TestFirstLineAreHeaders()
        {
            CSVtoJSONParser parser = new CSVtoJSONParser('|');

           string body =  TestsResources.Example_165_products_1;

          var lines = body.Split(Environment.NewLine);

           var result =  parser.ParseCSV(lines,  "testobj", null);



        }
    }
}