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
            //BlackPieces = new List<IPiece>();
            //WhitePieces = new List<IPiece>();
        }
        public static List<Square> AllSquares { get; set; }
        //public static List<IPiece> BlackPieces { get; set; }
        //public static List<IPiece> WhitePieces { get; set; }

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


    }
}
