using ChessBuildPieces.Stones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace ChessBuildPieces
{
    public class Piece
    {
        public Piece()
        {

        }
        public string Name { get; set; }
        public Color Color { get; set; }
        public bool Touchable { get; set; }
        public bool? FreeToMove { get; set; }
        public string ImageURL { get; set; }
        public bool MoveBack { get; set; }
        public Piece BeforePiece { get; set; }
        public int MoveCounter { get; set; }
        public void StateOrder(Board board)
        {
            if (Color == Color.black)
            {
                board.StateBlackPieces(false);
                board.StateWhitePieces(true);
            }
            else
            {
                board.StateWhitePieces(false);
                board.StateBlackPieces(true);
            }
        }

        public virtual bool PickSquare(int x, int y, Board board)
        {
            Square square = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == x && t.Coordinate.Y == y).FirstOrDefault();
            if (square != null)
            {
                if (square.Piece == null)
                {
                    board.AvailableSquares.Add(square);
                    return true;
                }
                else
                {
                    if (square.Piece.Color != Color)
                    {
                        board.AvailableSquares.Add(square);
                    }
                    return false;
                }
            }
            return true;

        }

        public virtual bool MoveTo(Square square, Board board)
        {
            CheckSquare(board);
            Piece piece = null;
            Square currentSquare = board.AllSquares.Select(t => t).Where(t => ReferenceEquals(this, t.Piece)).FirstOrDefault();
            if (board.AvailableSquares.Contains(square) && FreeToMove != false && currentSquare != square)
            {
                MoveCounter++;
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
                    MoveCounter--;
                    initialSquare.Piece = BeforePiece;
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

        public bool DiscoverCheckToMove(Board board)
        {
            Piece piece1 = null;
            Piece piece2 = null;
            board.CastAll();
            List<Piece> pieces = board.AllSquares.Select(t => t.Piece).Where(t => t != null).ToList();
            List<Square> squares = board.AllSquares.Select(t => t).Where(t => t.Piece != null).ToList();
            Square square1 = squares.Select(t => t).Where(t => t.Piece.Touchable == false && t.Piece.Color == Color.black).FirstOrDefault();
            foreach (var piece in pieces)
            {

                Square sq = board.AllSquares.Select(t => t).Where(t => t.Piece==piece).FirstOrDefault();
                switch (piece.Name)
                {
                    case "Bishop":
                        piece1 = sq.Bishop;
                        break;                        
                    case "King":
                        piece1 = sq.King;
                        break;
                    case "Knight":
                        piece1 = sq.Knight;
                        break;
                    case "Pawn":
                        piece1 = sq.Pawn;
                        break;
                    case "Queen":
                        piece1 = sq.Queen;
                        break;
                    case "Rook":
                        piece1 = sq.Rook;
                        break;
                    default:
                        break;
                }
                if(piece1 != null)
                {
                    piece1.CheckSquare(board);
                    if (board.AvailableSquares.Contains(square1) && Color == Color.black)
                    {
                        board.TakeBackCastAll();
                        return false;
                    }
                        
                }
                
            }

            Square square2 = squares.Select(t => t).Where(t => t.Piece.Touchable == false && t.Piece.Color == Color.white).FirstOrDefault();
            foreach (var piece in pieces)
            {
                Square sq = board.AllSquares.Select(t => t).Where(t => t.Piece == piece).FirstOrDefault();
                switch (piece.Name)
                {
                    case "Bishop":
                        piece2 = sq.Bishop;
                        break;
                    case "King":
                        piece2 = sq.King;
                        break;
                    case "Knight":
                        piece2 = sq.Knight;
                        break;
                    case "Pawn":
                        piece2 = sq.Pawn;
                        break;
                    case "Queen":
                        piece2 = sq.Queen;
                        break;
                    case "Rook":
                        piece2 = sq.Rook;
                        break;
                    default:
                        break;
                }
                if (piece2 != null)
                {
                    piece2.CheckSquare(board);
                    if (board.AvailableSquares.Contains(square2) && Color == Color.white)
                    {
                        board.TakeBackCastAll();
                        return false;
                    }
                        
                }

            }
            board.TakeBackCastAll();
            return true;


        }

        public virtual void CheckSquare(Board board) { }
        public virtual void InitialPositionSet(Board board) { }
    }
}
