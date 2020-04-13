using System;
using System.Collections.Generic;
using System.Text;

namespace ChessBuildPieces
{
    public class Piece
    {
        public Piece()
        {
            AvailableSquares = new List<Square>();
        }
        public Square Square { get; set; }
        public List<Square> AvailableSquares { get; set; }
        public Color Color { get; set; }
        public string ImageURL { get; set; }

        public bool PickSquare(int x, int y)
        {
            foreach (var square in Board.AllSquares)
            {
                if (y == square.Coordinate.Y && x == square.Coordinate.X)
                {
                    if (square.Piece == null)
                    {
                        AvailableSquares.Add(square);
                        return true;
                    }
                    else
                    {
                        AvailableSquares.Add(square);
                        return false;
                    }
                }
            }
            return true; // return 0 for knight piece  
        }

        public bool MoveTo(Square square)
        {
            IPiece piece = null;
            if (AvailableSquares.Contains(square))
            {
                if (Square.Color == Color.black)
                {
                    foreach (var item in Board.BlackSquares)
                    {
                        if (item.Coordinate.X == Square.Coordinate.X && item.Coordinate.Y == Square.Coordinate.Y)
                        {
                            piece = item.Piece;
                            item.Piece = null;
                        }
                    }
                }
                if (Square.Color == Color.white)
                {
                    foreach (var item in Board.WhiteSquares)
                    {
                        if (item.Coordinate.X == Square.Coordinate.X && item.Coordinate.Y == Square.Coordinate.Y)
                        {
                            piece = item.Piece;
                            item.Piece = null;
                        }

                    }
                }

                if (square.Color == Color.black)
                {
                    foreach (var item in Board.BlackSquares)
                    {
                        if (item.Coordinate.X == square.Coordinate.X && item.Coordinate.Y == square.Coordinate.Y)
                        {
                            item.Piece = piece;
                        }
                    }
                }
                if (square.Color == Color.white)
                {
                    foreach (var item in Board.WhiteSquares)
                    {
                        if (item.Coordinate.X == square.Coordinate.X && item.Coordinate.Y == square.Coordinate.Y)
                        {
                            item.Piece = piece;
                        }
                    }
                }
                Square = square;
                return true;
            }
            return false;
        }
    }
}
