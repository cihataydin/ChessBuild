using ChessBuildPieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessBuildStones
{
    public class Bishop : Piece, IPiece
    {
        public Bishop() : base()
        {

        }
        public void CheckSquare()
        {
            int coordinateX = Square.Coordinate.X;
            int coordinateY = Square.Coordinate.Y;
            AvailableSquares.Clear();
            AvailableSquares.Add(Square);

            int i = 1;
            while (coordinateX + i < (int)Boards.upperLimit && coordinateY + i < (int)Boards.upperLimit)
            {
                if (!PickSquare(coordinateX + i, coordinateY + i))
                    break;
                i++;
            }

            int j = 1;
            while (coordinateX - j > (int)Boards.lowerLimit && coordinateY + j < (int)Boards.upperLimit)
            {
                if (!PickSquare(coordinateX - j, coordinateY + j))
                    break;
                j++;
            }

            int k = 1;
            while (coordinateX - k > (int)Boards.lowerLimit && coordinateY - k > (int)Boards.lowerLimit)
            {
                if (!PickSquare(coordinateX - k, coordinateY - k))
                    break;
                k++;
            }

            int m = 1;
            while (coordinateX + m < (int)Boards.upperLimit && coordinateY - m > (int)Boards.lowerLimit)
            {
                if (!PickSquare(coordinateX + m, coordinateY - m))
                    break;
                m++;
            }
        }
        public void InitialPositionSet()
        {
            List<Square> squares;

            squares = Board.AllSquares.Select(t => t).Where(t => (t.Coordinate.X == 3 && t.Coordinate.Y == 1) || (t.Coordinate.X == 6 && t.Coordinate.Y == 1)).ToList();
            foreach (var square in squares)
            {
                square.Piece = new Bishop() { Color = Color.white, ImageURL = Constant.whiteBishopImageURL, Square = square, Touchable = true };
                Board.WhitePieces.Add(square.Piece);
            }
            squares.Clear();

            squares = Board.AllSquares.Select(t => t).Where(t => (t.Coordinate.X == 3 && t.Coordinate.Y == 8) || (t.Coordinate.X == 6 && t.Coordinate.Y == 8)).ToList();
            foreach (var square in squares)
            {
                square.Piece = new Bishop() { Color = Color.black, ImageURL = Constant.blackBishopImageURL, Square = square, Touchable = true };
                Board.BlackPieces.Add(square.Piece);
            }
        }
    }
}
