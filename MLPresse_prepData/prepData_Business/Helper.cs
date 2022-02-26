using System;
using System.Collections.Generic;
using System.Text;

namespace prepData_Business
{
    public class Helper
    {
        public const string IndividualModule = "IndividualModule";

        public const string RiModule = "RiModule";

        public const string SupportModule = "SupportModule";

        private const string IndividualNavigationPath = "IndividualList";

        private const string RiNavigationPath = "RiList";

        private const string SupportNavigationPath = "SupportList";

        private const string IndividualCaption = "Individu";

        private const string RiCaption = "Ri";

        private const string SupportCaption = "Support";

        public static string GetNavigationPath(string module)
        {
            string path = "";

            switch (module)
            {
                case IndividualModule:
                    path = IndividualNavigationPath;
                    break;
                case RiModule:
                    path = RiNavigationPath;
                    break;
                case SupportModule:
                    path = SupportNavigationPath;
                    break;
                default:
                    break;
            }
            return path;
        }

        public static string GetCaptionName(string module)
        {
            string Caption = "";

            switch (module)
            {
                case IndividualModule:
                    Caption = IndividualCaption;
                    break;
                case RiModule:
                    Caption = RiCaption;
                    break;
                case SupportModule:
                    Caption = SupportCaption;
                    break;
                default:
                    break;
            }
            return Caption;
        }
    }
}
