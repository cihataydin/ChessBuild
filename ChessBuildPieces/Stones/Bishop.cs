using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessBuildPieces.Stones
{
    public class Bishop : Piece, IPiece
    {
        public Bishop()
        {
            Name = "Bishop";
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

            int i = 1;
            while (coordinateX + i < (int)Boards.upperLimit && coordinateY + i < (int)Boards.upperLimit)
            {
                if (!PickSquare(coordinateX + i, coordinateY + i, board))
                    break;
                i++;
            }

            int j = 1;
            while (coordinateX - j > (int)Boards.lowerLimit && coordinateY + j < (int)Boards.upperLimit)
            {
                if (!PickSquare(coordinateX - j, coordinateY + j, board))
                    break;
                j++;
            }

            int k = 1;
            while (coordinateX - k > (int)Boards.lowerLimit && coordinateY - k > (int)Boards.lowerLimit)
            {
                if (!PickSquare(coordinateX - k, coordinateY - k, board))
                    break;
                k++;
            }

            int m = 1;
            while (coordinateX + m < (int)Boards.upperLimit && coordinateY - m > (int)Boards.lowerLimit)
            {
                if (!PickSquare(coordinateX + m, coordinateY - m, board))
                    break;
                m++;
            }
        }
        public override void InitialPositionSet(Board board)
        {
            List<Square> squares;

            squares = board.AllSquares.Select(t => t).Where(t => (t.Coordinate.X == 3 && t.Coordinate.Y == 1) || (t.Coordinate.X == 6 && t.Coordinate.Y == 1)).ToList();
            foreach (var square in squares)
            {
                square.Piece = new Bishop() { Color = Color.white, ImageURL = Constant.whiteBishopImageURL, Touchable = true };
                //board.WhitePieces.Add(square.Piece);
            }
            squares.Clear();

            squares = board.AllSquares.Select(t => t).Where(t => (t.Coordinate.X == 3 && t.Coordinate.Y == 8) || (t.Coordinate.X == 6 && t.Coordinate.Y == 8)).ToList();
            foreach (var square in squares)
            {
                square.Piece = new Bishop() { Color = Color.black, ImageURL = Constant.blackBishopImageURL, Touchable = true };
                //board.BlackPieces.Add(square.Piece);
            }
        }
    }
}
