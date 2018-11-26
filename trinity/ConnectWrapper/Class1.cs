using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectWrapper
{
    public enum eAttribs
    {
        aBrandAdvertiser=6,
        aBrandAgency=25,
        aBrandBreakSeqID=24,
        aBrandCategory=27,
        aBrandDurationNom=7,
        aBrandFilmcode=8
    }
    public enum eDataType
    {
        mView = 0,
        mProg = 1,
        mSpot = 2,
        mPromo = 3,
    }
    public class Brands : Connect.BrandsClass
    {
        /*
       public double getDataRangeTo(eDataType attrib)
        {
            return getDataRangeTo(attrib); 
        }
        */
    }
    public class Programmes : Connect.ProgrammesClass
    {
    }
    public class Breaks : Connect.BreaksClass
    {
    }

}
