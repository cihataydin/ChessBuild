using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessBuildPieces.Stones
{
    public class Pawn : Piece, IPiece
    {
        public Pawn()
        {
            Name = "Pawn";
            FreeToMove = true;
            MoveBack = false;
        }
        public override void CheckSquare(Board board)
        {
            Square currentSquare = board.AllSquares.Select(t => t).Where(t => ReferenceEquals(this, t.Piece)).FirstOrDefault();

            int coordinateX = currentSquare.Coordinate.X;
            int coordinateY = currentSquare.Coordinate.Y;
            board.AvailableSquares.Clear();
            board.EmptyAvailableSquares.Clear();
            board.AvailableSquares.Add(currentSquare);

            int one = 1;
            int two = 2;

            if (Color == Color.white)
            {
                PickSquare(coordinateX, coordinateY + one, board);
                PickSquare(coordinateX + one, coordinateY + one, board);
                PickSquare(coordinateX - one, coordinateY + one, board);
                if (currentSquare.Coordinate.Y == 2)
                    PickSquare(coordinateX, coordinateY + two, board);
            }
            if (Color == Color.black)
            {
                PickSquare(coordinateX, coordinateY - one, board);
                PickSquare(coordinateX + one, coordinateY - one, board);
                PickSquare(coordinateX - one, coordinateY - one, board);
                if (currentSquare.Coordinate.Y == 7)
                    PickSquare(coordinateX, coordinateY - two, board);
            }
        }

        public override void InitialPositionSet(Board board)
        {
            List<Square> squares;

            squares = board.AllSquares.Select(t => t).Where(t => t.Coordinate.Y == 2).ToList();
            foreach (var square in squares)
            {
                square.Piece = new Pawn() { Color = Color.white, ImageURL = Constant.whitePawnImageURL, Touchable = true };             
            }
            squares.Clear();

            squares = board.AllSquares.Select(t => t).Where(t => t.Coordinate.Y == 7).ToList();
            foreach (var square in squares)
            {
                square.Piece = new Pawn() { Color = Color.black, ImageURL = Constant.blackPawnImageURL, Touchable = true };
            }
        }

        public override bool PickSquare(int x, int y, Board board)
        {
            Square currentSquare = board.AllSquares.Select(t => t).Where(t => ReferenceEquals(this, t.Piece)).FirstOrDefault();
            Square square = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == x && t.Coordinate.Y == y).FirstOrDefault();
            if (square != null)
            {
                if (currentSquare.Coordinate.X != x)
                {
                    if (square.Piece != null && square.Piece.Color != Color)
                        board.AvailableSquares.Add(square);
                    return true;
                }
                else
                {
                    if (square.Piece == null)
                        board.EmptyAvailableSquares.Add(square);
                    return false;
                }
            }
            return true;
        }
        public override bool MoveTo(Square square, Board board)
        {
            CheckSquare(board);
            Square currentSquare = board.AllSquares.Select(t => t).Where(t => ReferenceEquals(this, t.Piece)).FirstOrDefault();
            board.OldSquares.Add(currentSquare);
            Piece piece = null;

            if ((board.EmptyAvailableSquares.Contains(square) || board.AvailableSquares.Contains(square) || (board.OldSquares.Contains(square) && MoveBack == true)) && FreeToMove != false && currentSquare != square)
            {
                Square initialSquare = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == currentSquare.Coordinate.X && t.Coordinate.Y == currentSquare.Coordinate.Y).FirstOrDefault();


                Square targetSquare = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == square.Coordinate.X && t.Coordinate.Y == square.Coordinate.Y).FirstOrDefault();

                piece = initialSquare.Piece;
                initialSquare.Piece = null;

                if (MoveBack == false)
                {
                    BeforePiece = targetSquare.Piece;
                }
                else
                {
                    initialSquare.Piece = BeforePiece;
                    BeforePiece = null;
                    MoveBack = false;
                    board.OldSquares.Clear();
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
