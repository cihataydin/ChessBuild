using System;
using System.Collections.Generic;
using System.Text;

namespace ChessBuildPieces
{
    public enum Color
    {
        black,
        white
    }
    public enum Boards
    {
        lowerLimit=0,
        upperLimit=9
    }
    public class Constant
    {
        public static string rookImageURL = "../../img/BlackRook.png";
        public static string bishopImageURL = "../../img/BlackBishop.png";
        public static string knightImageURL = "../../img/BlackKnight.png";
    }
}
