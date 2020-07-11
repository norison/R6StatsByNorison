using System;

namespace R6API
{
    public static class SeasonColor
    {
        public static string Color(this string nameOfSeason)
        {
            switch(nameOfSeason)
            {
                case "PHANTOM SIGHT":
                    return "#304395";
                case "BURNT HORIZON":
                    return "#D2005A";
                case "WIND BASTION":
                    return "#AA854F";
                case "GRIM SKY":
                    return "#81A0C1";
                case "PARA BELLUM":
                    return "#949F39";
                case "CHIMERA":
                    return "#FFC113";
                case "WHITE NOISE":
                    return "#006543";
                case "BLOOD ORCHID":
                    return "#CA361C";
                case "HEALTH":
                    return "#4A74A9";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
