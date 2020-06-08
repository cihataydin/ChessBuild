using ChessBuildPieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessBuildStones
{
    public class Knight : Piece, IPiece
    {
        public Knight()
        {
            Name = "Knight";
            AvailableSquares = new List<Square>();
            FreeToMove = true;
            MoveBack = false;
        }
        public override void CheckSquare(ref Board board)
        {
            Square currentSquare = board.AllSquares.Select(t => t).Where(t => ReferenceEquals(this, t.Piece)).FirstOrDefault();

            int CoordinateX = currentSquare.Coordinate.X;
            int CoordinateY = currentSquare.Coordinate.Y;
            AvailableSquares.Clear();
            AvailableSquares.Add(currentSquare);

            int two = 2;
            int one = 1;
            PickSquare(CoordinateX + two, CoordinateY - one, ref board);
            PickSquare(CoordinateX + two, CoordinateY + one, ref board);
            PickSquare(CoordinateX - two, CoordinateY + one, ref board);
            PickSquare(CoordinateX - two, CoordinateY - one, ref board);
            PickSquare(CoordinateX + one, CoordinateY + two, ref board);
            PickSquare(CoordinateX - one, CoordinateY + two, ref board);
            PickSquare(CoordinateX - one, CoordinateY - two, ref board);
            PickSquare(CoordinateX + one, CoordinateY - two, ref board);
        }

        public override void InitialPositionSet(ref Board board)
        {
            List<Square> squares;

            squares = board.AllSquares.Select(t => t).Where(t => (t.Coordinate.X == 2 && t.Coordinate.Y == 1) || (t.Coordinate.X == 7 && t.Coordinate.Y == 1)).ToList();
            foreach (var square in squares)
            {
                square.Piece = new Knight() { Color = Color.white, ImageURL = Constant.whiteKnightImageURL, Touchable = true };
                board.WhitePieces.Add(square.Piece);
            }
            squares.Clear();

            squares = board.AllSquares.Select(t => t).Where(t => (t.Coordinate.X == 2 && t.Coordinate.Y == 8) || (t.Coordinate.X == 7 && t.Coordinate.Y == 8)).ToList();
            foreach (var square in squares)
            {
                square.Piece = new Knight() { Color = Color.black, ImageURL = Constant.blackKnightImageURL, Touchable = true };
                board.BlackPieces.Add(square.Piece);
            }
        }
    }
}
