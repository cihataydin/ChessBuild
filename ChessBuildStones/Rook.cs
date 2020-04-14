using ChessBuildPieces;
using System;
using System.Collections.Generic;

namespace ChessBuildStones
{
    public class Rook : Piece,IPiece
    {
        public Rook() : base()
        {

        }
        public void CheckSquare()
        {
            int coordinateX = Square.Coordinate.X;
            int coordinateY = Square.Coordinate.Y;
            AvailableSquares.Clear();

            for (int i = coordinateX + 1; i < (int)Boards.upperLimit; i++)
            {
                if(!PickSquare(i, coordinateY))
                    break;
            }

            for (int i = coordinateX - 1; i > (int)Boards.lowerLimit; i--)
            {
                if (!PickSquare(i, coordinateY))
                    break;
            }

            for (int i = coordinateY + 1; i < (int)Boards.upperLimit; i++)
            {
                if (!PickSquare(coordinateX, i))
                    break;
            }

            for (int i = coordinateY - 1; i > (int)Boards.lowerLimit; i--)
            {
                if (!PickSquare(coordinateX, i))
                    break;
            }
        }

        public void SetPiece()
        {
            foreach (var square in Board.AllSquares)
            {
                if ((square.Coordinate.X == 1 && square.Coordinate.Y == 1) || (square.Coordinate.X == 8 && square.Coordinate.Y == 1))
                {
                    square.Piece = new Rook() { Color = Color.white, ImageURL = Constant.whiteRookImageURL, Square = square };
                    Board.WhitePieces.Add(square.Piece);
                }
                if ((square.Coordinate.X == 1 && square.Coordinate.Y == 8) || (square.Coordinate.X == 8 && square.Coordinate.Y == 8))
                {
                    square.Piece = new Rook() { Color = Color.black, ImageURL = Constant.blackRookImageURL, Square = square };
                    Board.BlackPieces.Add(square.Piece);
                }
            }
        }

    }
}
