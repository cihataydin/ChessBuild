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
        public static string blackRookImageURL = "../../img/BlackRook.png";
        public static string blackBishopImageURL = "../../img/BlackBishop.png";
        public static string blackKnightImageURL = "../../img/BlackKnight.png";
        public static string blackQueenImageURL = "../../img/BlackQueen.png";
        public static string blackKingImageURL = "../../img/BlackKing.png";
        public static string blackPawnImageURL = "../../img/BlackPawn.png";
        public static string whiteRookImageURL = "../../img/WhiteRook.png";
        public static string whiteBishopImageURL = "../../img/WhiteBishop.png";
        public static string whiteKnightImageURL = "../../img/WhiteKnight.png";
        public static string whiteQueenImageURL = "../../img/WhiteQueen.png";
        public static string whiteKingImageURL = "../../img/WhiteKing.png";
        public static string whitePawnImageURL = "../../img/WhitePawn.png";
    }
}
