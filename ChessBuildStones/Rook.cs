using ChessBuildPieces;
using System;
using System.Collections.Generic;

namespace ChessBuildStones
{
    public class Rook : IPiece
    {
        public Rook()
        {
            AvailableSquares = new List<Square>();
        }
        public Square Square { get; set; }
        public List<Square> AvailableSquares { get; set; }
        public Color Color { get; set; }
        public string ImageURL { get; set; }

        public void CheckSquares()
        {
            int CoordinateX = Square.Coordinate.X;
            int CoordinateY = Square.Coordinate.Y;
            AvailableSquares.Clear();
            for (int i = CoordinateX + 1; i < (int)Boards.upperLimit; i++)
            {
                if(!PickCoordinate(i, CoordinateY))
                    break;
            }

            for (int i = CoordinateX - 1; i > (int)Boards.lowerLimit; i--)
            {
                if (!PickCoordinate(i, CoordinateY))
                    break;
            }

            for (int i = CoordinateY + 1; i < (int)Boards.upperLimit; i++)
            {
                if (!PickCoordinate(CoordinateX, i))
                    break;
            }

            for (int i = CoordinateY - 1; i > (int)Boards.lowerLimit; i--)
            {
                if (!PickCoordinate(CoordinateX, i))
                    break;
            }
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

        public void SetPiece()
        {
            foreach (var square in Board.AllSquares)
            {
                if ((square.Coordinate.X == 1 && square.Coordinate.Y == 1) || (square.Coordinate.X == 8 && square.Coordinate.Y == 1))
                {
                    square.Piece = new Rook() { Color = Color.white, ImageURL = Constant.rookImageURL, Square = square };
                    Board.WhitePieces.Add(square.Piece);
                }
                if ((square.Coordinate.X == 1 && square.Coordinate.Y == 8) || (square.Coordinate.X == 8 && square.Coordinate.Y == 8))
                {
                    square.Piece = new Rook() { Color = Color.black, ImageURL = Constant.rookImageURL, Square = square };
                    Board.BlackPieces.Add(square.Piece);
                }
            }
        }

        bool PickCoordinate(int x, int y)
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
            return true;
        }
    }
}
