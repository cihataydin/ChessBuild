using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessBuildPieces.Stones
{
    public class Rook : Piece, IPiece
    {
        public Rook()
        {
            Name = "Rook";
            FreeToMove = true;
            MoveBack = false;
        }
        public override void CheckSquare(Board board)
        {
            Square currentSquare = board.AllSquares.Select(t => t).Where(t => ReferenceEquals(this, t.Piece)).FirstOrDefault();

            int coordinateX = currentSquare.Coordinate.X;
            int coordinateY = currentSquare.Coordinate.Y;
            board.AvailableSquares.Clear();
            board.AvailableSquares.Add(currentSquare);

            for (int i = coordinateX + 1; i < (int)Boards.upperLimit; i++)
            {
                if (!PickSquare(i, coordinateY, board))
                    break;
            }

            for (int i = coordinateX - 1; i > (int)Boards.lowerLimit; i--)
            {
                if (!PickSquare(i, coordinateY, board))
                    break;
            }

            for (int i = coordinateY + 1; i < (int)Boards.upperLimit; i++)
            {
                if (!PickSquare(coordinateX, i, board))
                    break;
            }

            for (int i = coordinateY - 1; i > (int)Boards.lowerLimit; i--)
            {
                if (!PickSquare(coordinateX, i, board))
                    break;
            }
        }

        public override void InitialPositionSet(Board board)
        {
            List<Square> squares;

            squares = board.AllSquares.Select(t => t).Where(t => (t.Coordinate.X == 1 && t.Coordinate.Y == 1) || (t.Coordinate.X == 8 && t.Coordinate.Y == 1)).ToList();
            foreach (var square in squares)
            {
                square.Piece = new Rook() { Color = Color.white, ImageURL = Constant.whiteRookImageURL, Touchable = true };
                //board.WhitePieces.Add(square.Piece);
            }
            squares.Clear();

            squares = board.AllSquares.Select(t => t).Where(t => (t.Coordinate.X == 1 && t.Coordinate.Y == 8) || (t.Coordinate.X == 8 && t.Coordinate.Y == 8)).ToList();
            foreach (var square in squares)
            {
                square.Piece = new Rook() { Color = Color.black, ImageURL = Constant.blackRookImageURL, Touchable = true };
                //board.BlackPieces.Add(square.Piece);
            }
        }
    }
}
