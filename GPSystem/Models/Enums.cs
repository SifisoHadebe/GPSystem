using System.ComponentModel;

namespace GPSys.Models
{
    public enum Gender
    {
        [Description("Male")]
        Male = 1,
        [Description("Female")]
        Female = 2
    }

    public enum Country
    {
        [Description("South Africa")] SouthAfrica = 1,
    }

    public enum Province
    {
        [Description("Eastern Cape")]
        EC = 1,
        [Description("Free State")]
        FS = 2,
        [Description("Gauteng")]
        G = 3,
        [Description("KwaZulu -Natal")]
        KZN = 4,
        [Description("Limpopo")]
        L = 5,
        [Description("Mpumalanga")]
        M = 6,
        [Description("Northern Cape")]
        NC = 7,
        [Description("North West")]
        NW = 8,
        [Description("Western Cape")]
        WC = 9
    }

    public enum Churches
    {
        [Description("DCC")]
        DCC = 1,
        [Description("ICC")]
        ICC = 2
    }

    public enum ISP
    {
        [Description("MTN")]
        MTN = 1,
        [Description("CellC")]
        CellC = 2,
        [Description("Vodacom")]
        Vodacom = 3
    }

}