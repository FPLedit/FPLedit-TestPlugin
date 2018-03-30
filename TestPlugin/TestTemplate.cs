using FPLedit.Buchfahrplan;
using System.Linq;
using FPLedit.Shared;
using FPLedit.Shared.Templating;

namespace TestPlugin
{
    public class TestTemplate : ITemplateProxy
    {
        public string Name => "Einfaches Beispiel-Template";

        public string TemplateIdentifier => "ext:TestPlugin/test-template";

        public string GetTemplateCode()
        {
            return @"
<!doctype html>
<html>
<head>
<title>TestTemplate</title>
</head>
<body>
    <h1><#= tt.GetLineName(TrainDirection.ta) #></h1>
    <ul>
        <li><#= string.Join(""</li><li>"", tt.Trains.Select(t => t.TName)) #></li>
    </ul>
</body>
</html>
";
        }
    }
}
