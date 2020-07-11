using System;
using static R6API.Enums;

namespace R6API
{
    public static class EnumsExtensions
    {
        public static string ToStringValue(this Platform platform)
        {
            switch (platform)
            {
                case Platform.UPLAY:
                    return "uplay";
                case Platform.XBOX:
                    return "xbl";
                case Platform.PLAYSTATION:
                    return "psn";
                default:
                    throw new ArgumentOutOfRangeException("Platform");
            }
        }

        public static string ToStringUrlValue(this Platform platform)
        {
            switch (platform)
            {
                case Platform.UPLAY:
                    return "OSBOR_PC_LNCH_A";
                case Platform.XBOX:
                    return "OSBOR_XBOXONE_LNCH_A";
                case Platform.PLAYSTATION:
                    return "OSBOR_PS4_LNCH_A";
                default:
                    throw new ArgumentOutOfRangeException("Platform");
            }
        }

        public static string ToStringSpacedId(this Platform platform)
        {
            switch (platform)
            {
                case Platform.UPLAY:
                    return "5172a557-50b5-4665-b7db-e3f2e8c5041d";
                case Platform.XBOX:
                    return "98a601e5-ca91-4440-b1c5-753f601a2c90";
                case Platform.PLAYSTATION:
                    return "05bfb3f7-6c21-4c42-be1f-97a33fb5cf66";
                default:
                    throw new ArgumentOutOfRangeException("Platform");
            }
        }

        public static string ToStringValue(this RankedRegion region)
        {
            switch (region)
            {
                case RankedRegion.EU:
                    return "emea";
                case RankedRegion.NA:
                    return "ncsa";
                case RankedRegion.ASIA:
                    return "apac";
                default:
                    throw new ArgumentOutOfRangeException("Region");
            }
        }

        public static string ToStringValue(this GamemodeNames gamemode)
        {
            switch (gamemode)
            {
                case GamemodeNames.securearea:
                    return "Secure Area";
                case GamemodeNames.rescuehostage:
                    return "Hostage Rescue";
                case GamemodeNames.plantbomb:
                    return "Bomb";
                default:
                    throw new ArgumentOutOfRangeException("Platform");
            }
        }
    }
}
