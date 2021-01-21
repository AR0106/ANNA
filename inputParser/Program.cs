using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace inputParser
{
    internal class Program
    {
        private static string[] numericEncoder()
        {
            string[] numberKey = new string[85];
            numberKey[0] = "A";
            numberKey[1] = "B";
            numberKey[2] = "C";
            numberKey[3] = "D";
            numberKey[4] = "E";
            numberKey[5] = "F";
            numberKey[6] = "G";
            numberKey[7] = "H";
            numberKey[8] = "I";
            numberKey[9] = "J";
            numberKey[10] = "K";
            numberKey[11] = "L";
            numberKey[12] = "M";
            numberKey[13] = "N";
            numberKey[14] = "O";
            numberKey[15] = "P";
            numberKey[16] = "Q";
            numberKey[17] = "R";
            numberKey[18] = "S";
            numberKey[19] = "T";
            numberKey[20] = "U";
            numberKey[21] = "V";
            numberKey[22] = "W";
            numberKey[23] = "X";
            numberKey[24] = "Y";
            numberKey[25] = "Z";
            numberKey[26] = '"'.ToString();
            numberKey[27] = "#";
            numberKey[28] = "$";
            numberKey[29] = "%";
            numberKey[30] = "&";
            numberKey[31] = "'";
            numberKey[32] = "(";
            numberKey[33] = ")";
            numberKey[34] = "*";
            numberKey[35] = "+";
            numberKey[36] = "`";
            numberKey[37] = "-";
            numberKey[38] = ".";
            numberKey[39] = "/";
            numberKey[40] = ":";
            numberKey[41] = ";";
            numberKey[42] = "<";
            numberKey[43] = "=";
            numberKey[44] = ">";
            numberKey[45] = "?";
            numberKey[46] = "@";
            numberKey[47] = "[";
            numberKey[48] = @"\";
            numberKey[49] = "]";
            numberKey[50] = "^";
            numberKey[51] = "_";
            numberKey[52] = "{";
            numberKey[53] = "|";
            numberKey[54] = "}";
            numberKey[55] = "~";
            numberKey[56] = "Δ";
            numberKey[57] = "€";
            numberKey[58] = "…";
            numberKey[59] = "ƒ";
            numberKey[60] = "†";
            numberKey[61] = "‡";
            numberKey[62] = "ˆ";
            numberKey[63] = "‰";
            numberKey[64] = "Š";
            numberKey[65] = "‹";
            numberKey[66] = "Œ";
            numberKey[67] = "Ž";
            numberKey[68] = "•";
            numberKey[69] = "™";
            numberKey[70] = "š";
            numberKey[71] = "›";
            numberKey[72] = "œ";
            numberKey[73] = "ž";
            numberKey[74] = "Ÿ";
            numberKey[75] = "¡";
            numberKey[76] = "¢";
            numberKey[77] = "£";
            numberKey[78] = "¤";
            numberKey[79] = "¥";
            numberKey[80] = "¦";
            numberKey[81] = "§";
            numberKey[82] = "¨";
            numberKey[83] = "©";
            numberKey[84] = "«";
            return numberKey;
        }

        private static void Main(string[] args)
        {
            string userInput = Console.ReadLine();

            StringBuilder stringBuilder = new StringBuilder(userInput);

            foreach (char character in userInput)
            {
                stringBuilder.Replace(character.ToString(), Array.IndexOf(numericEncoder(), character).ToString());
            }

            Console.WriteLine(stringBuilder);
            Console.ReadLine();
        }
    }
}