using System;
using System.Collections.Generic;
using System.Text;

namespace ChessBuildPieces
{

    public static class Board
    {
        static Board()
        {
            AllSquares = new List<Square>();
            BlackPieces = new List<IPiece>();
            WhitePieces = new List<IPiece>();
        }
        public static List<Square> AllSquares { get; set; }
        public static List<IPiece> BlackPieces { get; set; }
        public static List<IPiece> WhitePieces { get; set; }

        public static void CreateBoard()
        {
            for (int y = 1; y < (int)Boards.upperLimit; y++)
            {
                for (int x = 1; x < (int)Boards.upperLimit; x++)
                {
                    Square square = new Square(new Coordinate { X = x, Y = y });
                    if ((x + y) % 2 == 0)
                    {
                        square.Color = Color.white;
                        AllSquares.Add(square);
                    }
                    else
                    {
                        square.Color = Color.black;
                        AllSquares.Add(square);
                    }

                }

            }
        }
        public static void StateWhiteKing()
        {
            foreach (var item in Board.WhitePieces)
            {
                if (item.Touchable == true)
                    item.FreeToMove = false;
                else
                    item.FreeToMove = true;
            }
        }

        public static void StateBlackKing()
        {
            foreach (var item in Board.BlackPieces)
            {
                if (item.Touchable == true)
                    item.FreeToMove = false;
                else
                    item.FreeToMove = true;
            }
        }

        public static void StateWhitePieces(bool boolean)
        {
            if (boolean)
            {
                foreach (var piece in Board.WhitePieces)
                {
                    piece.FreeToMove = true;
                }
            }
            else
            {
                foreach (var piece in Board.WhitePieces)
                {
                    piece.FreeToMove = false;
                }
            }
        }

        public static void StateBlackPieces(bool boolean)
        {
            if (boolean)
            {
                foreach (var piece in Board.BlackPieces)
                {
                    piece.FreeToMove = true;
                }
            }
            else
            {
                foreach (var piece in Board.BlackPieces)
                {
                    piece.FreeToMove = false;
                }
            }
        }


    }
}
