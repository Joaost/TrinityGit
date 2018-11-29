using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace connect
{
    class Program
    {
        static void Main(string[] args)
        {
            void connect()
            {
                var TmpAdedge = new Connect.Brands();
                int SpotCount = 0;

                //Parameters for the Adedge objects
                TmpAdedge.setArea("se");
                TmpAdedge.setPeriod("-1d");
                TmpAdedge.setTargetMnemonic("3+", false);
                TmpAdedge.setChannelsAll();

                //Channel list object
                List<string> channels = new List<string>();

                SpotCount = TmpAdedge.Run(true, false, -1, false);

                // Filter each channel so we dont add duplicates
                for (int i = 1; i < SpotCount - 1; i++)
                {
                    string tmpchannel = TmpAdedge.getAttrib(Connect.eAttribs.aChannel, i);
                    if (!channels.Contains(tmpchannel))
                    {
                        channels.Add(tmpchannel);
                    }
                }

                Console.WriteLine(channels);
                Console.WriteLine(SpotCount.ToString());
                Console.ReadLine();

            }


            connect();

        }
    }
}
