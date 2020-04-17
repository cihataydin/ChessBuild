using ChessBuildPieces;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public void InitialPositionSet()
        {
            List<Square> squares;

            var data = Board.Test.Select(t => t.Value).Where(t => (t.Coordinate.X == 1 && t.Coordinate.Y == 1) || (t.Coordinate.X == 8 && t.Coordinate.Y == 1)).ToList();
            squares = data;
            foreach (var square in squares)
            {
                square.Piece = new Rook() { Color = Color.white, ImageURL = Constant.whiteRookImageURL, Square = square };
                Board.WhitePieces.Add(square.Piece);
            }
            squares.Clear();

            var data2 = Board.Test.Select(t => t.Value).Where(t => (t.Coordinate.X == 1 && t.Coordinate.Y == 8) || (t.Coordinate.X == 8 && t.Coordinate.Y == 8)).ToList();
            squares = data2;

            foreach (var square in squares)
            {
                square.Piece = new Rook() { Color = Color.black, ImageURL = Constant.blackRookImageURL, Square = square };
                Board.BlackPieces.Add(square.Piece);
            }
        }
    }
}
