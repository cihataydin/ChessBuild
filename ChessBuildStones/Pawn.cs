using ChessBuildPieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessBuildStones
{
    public class Pawn : Piece, IPiece
    {
        public void CheckSquare()
        {
            int coordinateX = Square.Coordinate.X;
            int coordinateY = Square.Coordinate.Y;
            AvailableSquares.Clear();

            int one = 1;
            int two = 2;
            
            if (Color == Color.white)
            {
                PickSquare(coordinateX, coordinateY + one);
                PickSquare(coordinateX + one, coordinateY + one);
                PickSquare(coordinateX - one, coordinateY + one);
                if(Square.Coordinate.Y==2)
                    PickSquare(coordinateX, coordinateY + two);
            }
            if(Color == Color.black)
            {
                PickSquare(coordinateX, coordinateY - one);
                PickSquare(coordinateX + one, coordinateY - one);
                PickSquare(coordinateX - one, coordinateY - one);
                if (Square.Coordinate.Y == 7)
                    PickSquare(coordinateX, coordinateY - two);
            }

        }

        public void InitialPositionSet()
        {
            List<Square> squares;

            squares = Board.AllSquares.Select(t => t).Where(t => t.Coordinate.Y == 2).ToList();
            foreach (var square in squares)
            {
                square.Piece = new Pawn() { Color = Color.white, ImageURL = Constant.whitePawnImageURL, Square = square };
                Board.WhitePieces.Add(square.Piece);
            }
            squares.Clear();

            squares = Board.AllSquares.Select(t => t).Where(t => t.Coordinate.Y == 7).ToList();
            foreach (var square in squares)
            {
                square.Piece = new Pawn() { Color = Color.black, ImageURL = Constant.blackPawnImageURL, Square = square };
                Board.BlackPieces.Add(square.Piece);
            }
        }

        public override bool PickSquare(int x, int y)
        {
            Square square = Board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == x && t.Coordinate.Y == y).FirstOrDefault();
            if(square != null)
            {
                if (Square.Coordinate.X != x)
                {
                    if (square.Piece != null && square.Piece.Color != Color)
                        AvailableSquares.Add(square);
                    return true;
                }
                else
                {
                    if (square.Piece == null)
                        AvailableSquares.Add(square);
                    return false;
                }
            }
            return true;
        }
    }
}

