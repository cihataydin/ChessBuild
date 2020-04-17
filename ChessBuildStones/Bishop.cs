using ChessBuildPieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessBuildStones
{
    public class Bishop : Piece,IPiece
    {
        public Bishop() :base()
        {

        }
        public void CheckSquare()
        {
            int coordinateX = Square.Coordinate.X;
            int coordinateY = Square.Coordinate.Y;
            AvailableSquares.Clear();

            int i = 0;
            while (coordinateX + i < (int)Boards.upperLimit && coordinateY + i < (int)Boards.upperLimit)
            {
                i++;
                if (!PickSquare(coordinateX + i, coordinateY + i))
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
            List<Square> squares;

            var data = Board.Test.Select(t => t.Value).Where(t => (t.Coordinate.X == 3 && t.Coordinate.Y == 1) || (t.Coordinate.X == 6 && t.Coordinate.Y == 1)).ToList();
            squares = data;
            foreach (var square in squares)
            {
                square.Piece = new Bishop() { Color = Color.white, ImageURL = Constant.whiteBishopImageURL, Square = square };
                Board.WhitePieces.Add(square.Piece);
            }
            squares.Clear();

            var data2 = Board.Test.Select(t => t.Value).Where(t => (t.Coordinate.X == 3 && t.Coordinate.Y == 8) || (t.Coordinate.X == 6 && t.Coordinate.Y == 8)).ToList();
            squares = data2;

            foreach (var square in squares)
            {
                square.Piece = new Bishop() { Color = Color.black, ImageURL = Constant.blackBishopImageURL, Square = square };
                Board.BlackPieces.Add(square.Piece);
            }
        }
    }
}
