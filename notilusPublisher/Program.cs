using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace notilusPublisher
{
    enum Result
    {
        SUCCESS,
        FAILURE
    };

    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> pathPairs = new Dictionary<string, string>();
            pathPairs.Add(@"C:\Users\Tahirhan\source\repos\notilussteeler\NotilusSteeler", @"\\NOTILUSNAS\Server\15- Notilus Software\NotilusSteeler\Versions");
            pathPairs.Add(@"C:\Users\Tahirhan\source\repos\NotilusDrafting\NotilusDrafting", @"\\NOTILUSNAS\Server\15- Notilus Software\Notilus Drafting\Versions");
            pathPairs.Add(@"C:\Users\Tahirhan\source\repos\NotilusPiping\NotilusPiping", @"\\NOTILUSNAS\Server\15- Notilus Software\NotilusPiping\Versions");
            pathPairs.Add(@"C:\Users\Tahirhan\source\repos\NotilusTools\NotilusTools", @"\\NOTILUSNAS\Server\15- Notilus Software\NotilusTools\Versions");
            pathPairs.Add(@"C:\Users\Tahirhan\source\repos\NotilusRulemaster\NotilusRulemaster", @"\\NOTILUSNAS\Server\15- Notilus Software\NotilusRulemaster\Versions");
            pathPairs.Add(@"C:\Users\Tahirhan\source\repos\NotilusPID\NotilusPID", @"\\NOTILUSNAS\Server\15- Notilus Software\NotilusPID\Versions");
            pathPairs.Add(@"C:\Users\Tahirhan\source\repos\NotilusOnBoard\NotilusOnBoard", @"\\NOTILUSNAS\Server\15- Notilus Software\NotilusOnBoard\Versions");
            pathPairs.Add(@"C:\Users\Tahirhan\source\repos\Notilus_Clipper\Notilus_Clipper", @"\\NOTILUSNAS\Server\15- Notilus Software\Notilus_Clipper\Versions_");

            csServerPublisher serverPublisher = new csServerPublisher(pathPairs);
            serverPublisher.run();
        }
    }
}
