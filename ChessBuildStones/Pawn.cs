using ChessBuildPieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessBuildStones
{
    public class Pawn : Piece, IPiece
    {
        public Pawn()
        {
            Name = "Pawn";
            AvailableSquares = new List<Square>();
            FreeToMove = true;
            MoveBack = false;
            OldSquares = new List<Square>();
            EmptyAvailableSquares = new List<Square>();
        }
        public List<Square> OldSquares { get; set; }
        public List<Square> EmptyAvailableSquares { get; set; }
        public override void CheckSquare(ref Board board)
        {
            Square currentSquare = board.AllSquares.Select(t => t).Where(t => ReferenceEquals(this, t.Piece)).FirstOrDefault();

            int coordinateX = currentSquare.Coordinate.X;
            int coordinateY = currentSquare.Coordinate.Y;
            AvailableSquares.Clear();
            AvailableSquares.Add(currentSquare);

            int one = 1;
            int two = 2;

            if (Color == Color.white)
            {
                PickSquare(coordinateX, coordinateY + one, ref board);
                PickSquare(coordinateX + one, coordinateY + one, ref board);
                PickSquare(coordinateX - one, coordinateY + one, ref board);
                if (currentSquare.Coordinate.Y == 2)
                    PickSquare(coordinateX, coordinateY + two, ref board);
            }
            if (Color == Color.black)
            {
                PickSquare(coordinateX, coordinateY - one, ref board);
                PickSquare(coordinateX + one, coordinateY - one, ref board);
                PickSquare(coordinateX - one, coordinateY - one, ref board);
                if (currentSquare.Coordinate.Y == 7)
                    PickSquare(coordinateX, coordinateY - two, ref board);
            }
        }

        public override void InitialPositionSet(ref Board board)
        {
            List<Square> squares;

            squares = board.AllSquares.Select(t => t).Where(t => t.Coordinate.Y == 2).ToList();
            foreach (var square in squares)
            {
                square.Piece = new Pawn() { Color = Color.white, ImageURL = Constant.whitePawnImageURL, Touchable = true };
                board.WhitePieces.Add(square.Piece);
            }
            squares.Clear();

            squares = board.AllSquares.Select(t => t).Where(t => t.Coordinate.Y == 7).ToList();
            foreach (var square in squares)
            {
                square.Piece = new Pawn() { Color = Color.black, ImageURL = Constant.blackPawnImageURL, Touchable = true };
                board.BlackPieces.Add(square.Piece);
            }
        }

        public override bool PickSquare(int x, int y, ref Board board)
        {
            Square currentSquare = board.AllSquares.Select(t => t).Where(t => ReferenceEquals(this, t.Piece)).FirstOrDefault();
            Square square = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == x && t.Coordinate.Y == y).FirstOrDefault();
            if (square != null)
            {
                if (currentSquare.Coordinate.X != x)
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
        public override bool MoveTo(Square square, ref Board board)
        {
            Square currentSquare = board.AllSquares.Select(t => t).Where(t => ReferenceEquals(this, t.Piece)).FirstOrDefault();
            OldSquares.Add(currentSquare);
            Piece piece = null;

            if (( EmptyAvailableSquares.Contains(square)|| AvailableSquares.Contains(square) || (OldSquares.Contains(square) && MoveBack== true))  && FreeToMove != false && currentSquare != square)
            {
                Square initialSquare = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == currentSquare.Coordinate.X && t.Coordinate.Y == currentSquare.Coordinate.Y).FirstOrDefault();


                Square targetSquare = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == square.Coordinate.X && t.Coordinate.Y == square.Coordinate.Y).FirstOrDefault();

                piece = initialSquare.Piece;
                initialSquare.Piece = null;

                if (MoveBack == false)
                {
                    BeforePiece = targetSquare.Piece;
                    if (targetSquare.Piece != null)
                    {
                        if (targetSquare.Piece.Color == Color.black)
                            board.BlackPieces.Remove(targetSquare.Piece);
                        else
                            board.WhitePieces.Remove(targetSquare.Piece);
                    }
                }
                else
                {
                    initialSquare.Piece = BeforePiece;
                    if (initialSquare.Piece != null)
                    {
                        if (initialSquare.Piece.Color == Color.black)
                            board.BlackPieces.Add(initialSquare.Piece);
                        else
                            board.WhitePieces.Add(initialSquare.Piece);
                    }
                    BeforePiece = null;
                    MoveBack = false;
                }

                if (targetSquare.Piece != null)
                {
                    if (targetSquare.Piece.Touchable == true)
                    {
                        targetSquare.Piece = piece;
                        currentSquare = square;
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
                    currentSquare = square;
                    return true;
                }
            }
            return false;
        }
    }
}

