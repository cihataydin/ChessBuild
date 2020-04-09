using ChessBuildPieces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessBuildStones
{
    class Bishop : IPiece
    {
        public Square Square { get; set; }
        public List<Square> AvailableSquares { get; set; }
        public Color Color { get ; set ; }
        public string ImageURL { get ; set ; }

        public void CheckSquares()
        {
            int firstCoordinateX = Square.Coordinate.X;
            int firstCoordianteY = Square.Coordinate.Y;
            while (++Square.Coordinate.X < (int)Boards.upperLimit && ++Square.Coordinate.Y < (int)Boards.upperLimit)
            {
                PickSquare();
            }
            Square.Coordinate.X = firstCoordinateX;
            Square.Coordinate.Y = firstCoordianteY;

            while (--Square.Coordinate.X > (int)Boards.lowerLimit && ++Square.Coordinate.Y < (int)Boards.upperLimit)
            {
                PickSquare();
            }
            Square.Coordinate.X = firstCoordinateX;
            Square.Coordinate.Y = firstCoordianteY;

            while (--Square.Coordinate.X > (int)Boards.lowerLimit && --Square.Coordinate.Y > (int)Boards.lowerLimit)
            {
                PickSquare();
            }
            Square.Coordinate.X = firstCoordinateX;
            Square.Coordinate.Y = firstCoordianteY;

            while (++Square.Coordinate.X < (int)Boards.upperLimit && --Square.Coordinate.Y > (int)Boards.lowerLimit)
            {
                PickSquare();
            }
            Square.Coordinate.X = firstCoordinateX;
            Square.Coordinate.Y = firstCoordianteY;
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

        public void PickSquare()
        {
            if (Square.Color == Color.black)
                foreach (var item in Board.BlackSquares)
                {
                    if ((item.Coordinate.X == Square.Coordinate.X && item.Coordinate.Y == Square.Coordinate.Y) && item.Piece == null)
                        AvailableSquares.Add(item);
                    if ((item.Coordinate.X == Square.Coordinate.X && item.Coordinate.Y == Square.Coordinate.Y) && item.Piece != null)
                        break;
                }
            if (Square.Color == Color.white)
                foreach (var item in Board.WhiteSquares)
                {
                    if ((item.Coordinate.X == Square.Coordinate.X && item.Coordinate.Y == Square.Coordinate.Y) && item.Piece == null)
                        AvailableSquares.Add(item);
                    if ((item.Coordinate.X == Square.Coordinate.X && item.Coordinate.Y == Square.Coordinate.Y) && item.Piece != null)
                        break;
                }
        }

        public void SetPiece()
        {
            foreach (var square in Board.WhiteSquares)
            {
                if ((square.Coordinate.X == 3 && square.Coordinate.Y == 1) || (square.Coordinate.X == 6 && square.Coordinate.Y == 1))
                {
                    square.Piece = new Bishop() { Color = Color.white, ImageURL=Constant.bishopImageURL };
                    Board.WhitePieces.Add(square.Piece);
                }
                if ((square.Coordinate.X == 3 && square.Coordinate.Y == 8) || (square.Coordinate.X == 6 && square.Coordinate.Y == 8))
                {
                    square.Piece = new Bishop() { Color = Color.black, ImageURL=Constant.bishopImageURL };
                    Board.BlackPieces.Add(square.Piece);
                }
            }
        }
    }
}
