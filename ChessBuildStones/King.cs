using ChessBuildPieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessBuildStones
{
    public class King : Piece, IPiece
    {
        public void CheckSquare()
        {
            int coordinateX = Square.Coordinate.X;
            int coordinateY = Square.Coordinate.Y;
            AvailableSquares.Clear();

            int one = 1;
            PickSquare(coordinateX + one, coordinateY);
            PickSquare(coordinateX, coordinateY + one);
            PickSquare(coordinateX - one, coordinateY);
            PickSquare(coordinateX, coordinateY - one);
            PickSquare(coordinateX + one, coordinateY + one);
            PickSquare(coordinateX + one, coordinateY - one);
            PickSquare(coordinateX - one, coordinateY + one);
            PickSquare(coordinateX - one, coordinateY - one);
        }

        public void InitialPositionSet()
        {
            Square square;

            square = Board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 4 && t.Coordinate.Y == 1).FirstOrDefault();
            square.Piece = new King() { Color = Color.white, ImageURL = Constant.whiteKingImageURL, Square = square, Touchable = false };

            square = Board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 4 && t.Coordinate.Y == 8).FirstOrDefault();
            square.Piece = new King() { Color = Color.black, ImageURL = Constant.blackKingImageURL, Square = square, Touchable = false };

        }
    }
}
