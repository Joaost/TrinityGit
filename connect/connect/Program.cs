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

                TmpAdedge.setArea("SE");

                //TmpAdedge.setBrandFilmCode("NO", "NYIANI3001N8");
                TmpAdedge.setPeriod("-1d");
                TmpAdedge.setTargetMnemonic("3+", false);
                TmpAdedge.setChannelsAll();

                List<string> channels = new List<string>();

                SpotCount = TmpAdedge.Run(false, false, -1,false);

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
