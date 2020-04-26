using ChessBuildPieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessBuildStones
{
    public class Pawn : Piece, IPiece
    {
        public Pawn():base()
        {
            OldSquares = new List<Square>();
            EmptyAvailableSquares = new List<Square>();
        }
        public List<Square> OldSquares { get; set; }
        public List<Square> EmptyAvailableSquares { get; set; }
        public void CheckSquare()
        {
            int coordinateX = Square.Coordinate.X;
            int coordinateY = Square.Coordinate.Y;
            AvailableSquares.Clear();
            AvailableSquares.Add(Square);

            int one = 1;
            int two = 2;

            if (Color == Color.white)
            {
                PickSquare(coordinateX, coordinateY + one);
                PickSquare(coordinateX + one, coordinateY + one);
                PickSquare(coordinateX - one, coordinateY + one);
                if (Square.Coordinate.Y == 2)
                    PickSquare(coordinateX, coordinateY + two);
            }
            if (Color == Color.black)
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
                square.Piece = new Pawn() { Color = Color.white, ImageURL = Constant.whitePawnImageURL, Square = square, Touchable = true };
                Board.WhitePieces.Add(square.Piece);
            }
            squares.Clear();

            squares = Board.AllSquares.Select(t => t).Where(t => t.Coordinate.Y == 7).ToList();
            foreach (var square in squares)
            {
                square.Piece = new Pawn() { Color = Color.black, ImageURL = Constant.blackPawnImageURL, Square = square, Touchable = true };
                Board.BlackPieces.Add(square.Piece);
            }
        }

        public override bool PickSquare(int x, int y)
        {
            Square square = Board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == x && t.Coordinate.Y == y).FirstOrDefault();
            if (square != null)
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
                       EmptyAvailableSquares.Add(square);
                    return false;
                }
            }
            return true;
        }
        public override bool MoveTo(Square square)
        {   
            OldSquares.Add(Square);
            IPiece piece = null;

            if (( EmptyAvailableSquares.Contains(square)|| AvailableSquares.Contains(square) || (OldSquares.Contains(square) && MoveBack== true))  && FreeToMove != false && Square!=square)
            {
                Square initialSquare = Board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == Square.Coordinate.X && t.Coordinate.Y == Square.Coordinate.Y).FirstOrDefault();


                Square targetSquare = Board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == square.Coordinate.X && t.Coordinate.Y == square.Coordinate.Y).FirstOrDefault();

                piece = initialSquare.Piece;
                initialSquare.Piece = null;

                if (MoveBack == false)
                {
                    BeforePiece = targetSquare.Piece;
                    if (targetSquare.Piece != null)
                    {
                        if (targetSquare.Piece.Color == Color.black)
                            Board.BlackPieces.Remove(targetSquare.Piece);
                        else
                            Board.WhitePieces.Remove(targetSquare.Piece);
                    }
                }
                else
                {
                    initialSquare.Piece = BeforePiece;
                    if (initialSquare.Piece != null)
                    {
                        if (initialSquare.Piece.Color == Color.black)
                            Board.BlackPieces.Add(initialSquare.Piece);
                        else
                            Board.WhitePieces.Add(initialSquare.Piece);
                    }
                    BeforePiece = null;
                    MoveBack = false;
                }

                if (targetSquare.Piece != null)
                {
                    if (targetSquare.Piece.Touchable == true)
                    {
                        targetSquare.Piece = piece;
                        Square = square;
                        return true;
                    }
                    else
                    {
                        initialSquare.Piece = piece;
                        return false;
                    }
                }
                else
                {
                    targetSquare.Piece = piece;
                    Square = square;
                    return true;
                }
            }
            return false;
        }
    }
}

