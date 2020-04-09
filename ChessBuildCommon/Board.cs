using ChessBuildPieces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessBuildCommon
{

    public class Board
    {
        public Board()
        {
            BlackSquares = new List<Square>();
            WhiteSquares = new List<Square>();
            Pieces = new List<IPiece>();
        }
        public List<Square> BlackSquares { get; set; }

        public List<Square> WhiteSquares { get; set; }
        public List<IPiece> Pieces { get; set; }

        public void CreateBoard()
        {
            for (int y = 1; y < (int)Boards.upperLimit; y++)
            {
                for (int x = 1; x < (int)Boards.upperLimit; x++)
                {
                    Square square = new Square(new Coordinate { X = x, Y = y });
                    if ((x % 2 == 0 && y % 2 == 1) || (x % 2 == 1 && y % 2 == 0))
                    {
                        square.Color = Color.white;
                        WhiteSquares.Add(square);
                    }

                    else
                    {
                        square.Color = Color.black;
                        BlackSquares.Add(square);
                    }

                }

            }
        }

        public void SetPieces()
        {

        }
    }
}


