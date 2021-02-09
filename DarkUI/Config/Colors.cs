using System.Drawing;

namespace DarkUI.Config
{
    public sealed class Colors
    {
        private static Color greyBackground = Color.FromArgb(60, 63, 65);
        private static Color headerBackground = Color.FromArgb(57, 60, 62);
        private static Color blueBackground = Color.FromArgb(66, 77, 95);
        private static Color darkBlueBackground = Color.FromArgb(52, 57, 66);
        private static Color darkBackground = Color.FromArgb(43, 43, 43);
        private static Color mediumBackground = Color.FromArgb(49, 51, 53);
        private static Color lightBackground = Color.FromArgb(69, 73, 74);
        private static Color lighterBackground = Color.FromArgb(95, 101, 102);
        private static Color lightestBackground = Color.FromArgb(178, 178, 178);
        private static Color lightBorder = Color.FromArgb(81, 81, 81);
        private static Color darkBorder = Color.FromArgb(51, 51, 51);
        private static Color lightText = Color.FromArgb(220, 220, 220);
        private static Color disabledText = Color.FromArgb(153, 153, 153);
        private static Color blueHighlight = Color.FromArgb(104, 151, 187);
        private static Color blueSelection = Color.FromArgb(75, 110, 175);
        private static Color greyHighlight = Color.FromArgb(122, 128, 132);
        private static Color greySelection = Color.FromArgb(92, 92, 92);
        private static Color darkGreySelection = Color.FromArgb(82, 82, 82);
        private static Color darkBlueBorder = Color.FromArgb(51, 61, 78);
        private static Color lightBlueBorder = Color.FromArgb(86, 97, 114);
        private static Color activeControl = Color.FromArgb(159, 178, 196);

        public static Color GreyBackground
        {
            get { return greyBackground; }
            set { greyBackground = value; }
        }

        public static Color HeaderBackground
        {
            get { return headerBackground; }
            set { headerBackground = value; }
        }

        public static Color BlueBackground
        {
            get { return blueBackground; }
            set { blueBackground = value; }
        }

        public static Color DarkBlueBackground
        {
            get { return darkBlueBackground; }
            set { darkBlueBackground = value; }
        }

        public static Color DarkBackground
        {
            get { return darkBackground; }
            set { darkBackground = value; }
        }

        public static Color MediumBackground
        {
            get { return mediumBackground; }
            set { mediumBackground = value; }
        }

        public static Color LightBackground
        {
            get { return lightBackground; }
            set { lightBackground = value; }
        }

        public static Color LighterBackground
        {
            get { return lighterBackground; }
            set { lighterBackground = value; }
        }

        public static Color LightestBackground
        {
            get { return lightestBackground; }
            set { lightestBackground = value; }
        }

        public static Color LightBorder
        {
            get { return lightBorder; }
            set { lightBorder = value; }
        }

        public static Color DarkBorder
        {
            get { return darkBorder; }
            set { darkBorder = value; }
        }

        public static Color LightText
        {
            get { return lightText; }
            set { lightText = value; }
        }

        public static Color DisabledText
        {
            get { return disabledText; }
            set { disabledText = value; }
        }

        public static Color BlueHighlight
        {
            get { return blueHighlight; }
            set { blueHighlight = value; }
        }

        public static Color BlueSelection
        {
            get { return blueSelection; }
            set { blueSelection = value; }
        }

        public static Color GreyHighlight
        {
            get { return greyHighlight; }
            set { greyHighlight = value; }
        }

        public static Color GreySelection
        {
            get { return greySelection; }
            set { greySelection = value; }
        }

        public static Color DarkGreySelection
        {
            get { return darkGreySelection; }
            set { darkGreySelection = value; }
        }

        public static Color DarkBlueBorder
        {
            get { return darkBlueBorder; }
            set { darkBlueBorder = value; }
        }

        public static Color LightBlueBorder
        {
            get { return lightBlueBorder; }
            set { lightBlueBorder = value; }
        }

        public static Color ActiveControl
        {
            get { return activeControl; }
            set { activeControl = value; }
        }
    }
}
