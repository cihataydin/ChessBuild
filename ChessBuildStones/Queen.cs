using ChessBuildPieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessBuildStones
{
    public class Queen : Piece, IPiece
    {
        public Queen() : base()
        {

        }

        public void CheckSquare()
        {
            int coordinateX = Square.Coordinate.X;
            int coordinateY = Square.Coordinate.Y;
            AvailableSquares.Clear();

            for (int i = coordinateX + 1; i < (int)Boards.upperLimit; i++)
            {
                if (!PickSquare(i, coordinateY))
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

            int s = 0;
            while (coordinateX + s < (int)Boards.upperLimit && coordinateY + s < (int)Boards.upperLimit)
            {
                s++;
                if (!PickSquare(coordinateX + s, coordinateY + s))
                    break;
            }

            int j = 0;
            while (coordinateX - j > (int)Boards.lowerLimit && coordinateY + j < (int)Boards.upperLimit)
            {
                j++;
                if (!PickSquare(coordinateX - j, coordinateY + j))
                    break;
            }

            int k = 0;
            while (coordinateX - k > (int)Boards.lowerLimit && coordinateY - k > (int)Boards.lowerLimit)
            {
                k++;
                if (!PickSquare(coordinateX - k, coordinateY - k))
                    break;
            }

            int m = 0;
            while (coordinateX + m < (int)Boards.upperLimit && coordinateY - m > (int)Boards.lowerLimit)
            {
                m++;
                if (!PickSquare(coordinateX + m, coordinateY - m))
                    break;
            }

        }

        public void InitialPositionSet()
        {
            Square square;

            var data = Board.Test.Select(t => t.Value).Where(t => t.Coordinate.X == 5 && t.Coordinate.Y == 1).FirstOrDefault();
            square = (Square)data;
            square.Piece = new Queen() { Color = Color.white, ImageURL = Constant.whiteQueenImageURL, Square = square };
            Board.WhitePieces.Add(square.Piece);

            var data2 = Board.Test.Select(t => t.Value).Where(t => t.Coordinate.X == 5 && t.Coordinate.Y == 8).FirstOrDefault();
            square = (Square)data2;
            square.Piece = new Queen() { Color = Color.black, ImageURL = Constant.blackQueenImageURL, Square = square };
            Board.BlackPieces.Add(square.Piece);

        }
    }
}
