using ChessBuildPieces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessBuildStones
{
    public class Knight : Piece, IPiece
    {
        public void CheckSquare()
        {
            int CoordinateX = Square.Coordinate.X;
            int CoordinateY = Square.Coordinate.Y;
            AvailableSquares.Clear();

            int two = 2;
            int one = 1;
            PickSquare(CoordinateX + two, CoordinateY - one);
            PickSquare(CoordinateX + two, CoordinateY + one);
            PickSquare(CoordinateX + one, CoordinateY + two);
            PickSquare(CoordinateX - one, CoordinateY + two);
            PickSquare(CoordinateX - two, CoordinateY + one);
            PickSquare(CoordinateX - two, CoordinateY - one);
            PickSquare(CoordinateX - one, CoordinateY - two);
            PickSquare(CoordinateX + one, CoordinateY - two);

        }

        public void SetPiece()
        {
            foreach (var square in Board.AllSquares)
            {
                if ((square.Coordinate.X == 2 && square.Coordinate.Y == 1) || (square.Coordinate.X == 7 && square.Coordinate.Y == 1))
                {
                    square.Piece = new Knight() { Color = Color.white, ImageURL = Constant.whiteKnightImageURL, Square = square };
                    Board.WhitePieces.Add(square.Piece);
                }
                if ((square.Coordinate.X == 2 && square.Coordinate.Y == 8) || (square.Coordinate.X == 7 && square.Coordinate.Y == 8))
                {
                    square.Piece = new Knight() { Color = Color.black, ImageURL = Constant.blackKnightImageURL, Square = square };
                    Board.BlackPieces.Add(square.Piece);
                }
            }
        }
    }
}
