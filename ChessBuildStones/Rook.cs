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
            int firstCoordinateX = Square.Coordinate.X;
            int firstCoordinateY = Square.Coordinate.Y;
            for (int i = ++Square.Coordinate.X; i < (int)Boards.upperLimit; i++)
            {
                Square.Coordinate.X = i;
                PickCoordinate();
            }
            Square.Coordinate.X = firstCoordinateX;

            for (int i = --Square.Coordinate.X; i > (int)Boards.lowerLimit; i--)
            {
                Square.Coordinate.X = i;
                PickCoordinate();
            }
            Square.Coordinate.X = firstCoordinateX;

            for (int i = ++Square.Coordinate.Y; i < (int)Boards.upperLimit; i++)
            {
                Square.Coordinate.Y = i;
                PickCoordinate();
            }
            Square.Coordinate.Y = firstCoordinateY;

            for (int i = --Square.Coordinate.Y; i > (int)Boards.lowerLimit; i--)
            {
                Square.Coordinate.Y = i;
                PickCoordinate();
            }
            Square.Coordinate.Y = firstCoordinateY;
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

        void PickCoordinate()
        {

            if (Square.Color==Color.black)
            {
                foreach (var square in Board.BlackSquares)
                {
                    if (Square.Coordinate.Y == square.Coordinate.Y && Square.Coordinate.X == square.Coordinate.X)
                    {
                        if (square.Piece == null)
                            AvailableSquares.Add(square);
                        else
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                foreach (var square in Board.WhiteSquares)
                {
                    if (Square.Coordinate.Y == square.Coordinate.Y && Square.Coordinate.X == square.Coordinate.X)
                    {
                        if (square.Piece == null)
                            AvailableSquares.Add(square);
                        else
                        {
                            break;
                        }
                    }
                }
            }

        }
    }
}
