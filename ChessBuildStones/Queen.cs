using ChessBuildPieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessBuildStones
{
    public class Queen : Piece, IPiece
    {
        public Queen()
        {
            Name = "Queen";
            AvailableSquares = new List<Square>();
            FreeToMove = true;
            MoveBack = false;
        }

        public override void CheckSquare(ref Board board)
        {
            Square currentSquare = board.AllSquares.Select(t => t).Where(t => ReferenceEquals(this, t.Piece)).FirstOrDefault();

            int coordinateX = currentSquare.Coordinate.X;
            int coordinateY = currentSquare.Coordinate.Y;
            AvailableSquares.Clear();
            AvailableSquares.Add(currentSquare);

            for (int i = coordinateX + 1; i < (int)Boards.upperLimit; i++)
            {
                if (!PickSquare(i, coordinateY, ref board))
                    break;
            }

            for (int i = coordinateX - 1; i > (int)Boards.lowerLimit; i--)
            {
                if (!PickSquare(i, coordinateY, ref board))
                    break;
            }

            for (int i = coordinateY + 1; i < (int)Boards.upperLimit; i++)
            {
                if (!PickSquare(coordinateX, i, ref board))
                    break;
            }

            for (int i = coordinateY - 1; i > (int)Boards.lowerLimit; i--)
            {
                if (!PickSquare(coordinateX, i, ref board))
                    break;
            }

            int s = 1;
            while (coordinateX + s < (int)Boards.upperLimit && coordinateY + s < (int)Boards.upperLimit)
            {
                if (!PickSquare(coordinateX + s, coordinateY + s, ref board))
                    break;
                s++;
            }

            int j = 1;
            while (coordinateX - j > (int)Boards.lowerLimit && coordinateY + j < (int)Boards.upperLimit)
            {
                if (!PickSquare(coordinateX - j, coordinateY + j, ref board))
                    break;
                j++;
            }

            int k = 1;
            while (coordinateX - k > (int)Boards.lowerLimit && coordinateY - k > (int)Boards.lowerLimit)
            {
                if (!PickSquare(coordinateX - k, coordinateY - k, ref board))
                    break;
                k++;
            }

            int m = 1;
            while (coordinateX + m < (int)Boards.upperLimit && coordinateY - m > (int)Boards.lowerLimit)
            {
                if (!PickSquare(coordinateX + m, coordinateY - m, ref board))
                    break;
                m++;
            }
        }

        public override void InitialPositionSet(ref Board board)
        {
            Square square;

            square = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 5 && t.Coordinate.Y == 1).FirstOrDefault();
            square.Piece = new Queen() { Color = Color.white, ImageURL = Constant.whiteQueenImageURL, Touchable = true };
            board.WhitePieces.Add(square.Piece);

            square = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 5 && t.Coordinate.Y == 8).FirstOrDefault();
            square.Piece = new Queen() { Color = Color.black, ImageURL = Constant.blackQueenImageURL, Touchable = true };
            board.BlackPieces.Add(square.Piece);

        }
    }
}
