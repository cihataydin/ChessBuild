using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessBuildPieces
{
    public class Piece
    {
        public Piece()
        {
            AvailableSquares = new List<Square>();
            FreeToMove = true;
            MoveBack = false;
        }
        public Square Square { get; set; }
        public List<Square> AvailableSquares { get; set; }
        public Color Color { get; set; }
        public bool Touchable { get; set; }
        public bool? FreeToMove { get; set; }
        public string ImageURL { get; set; }
        public bool MoveBack { get; set; }
        public IPiece BeforePiece { get; set; }
        public void StateOrder()
        {
            if (Color == Color.black)
            {
                Board.StateBlackPieces(false);
                Board.StateWhitePieces(true);
            }
            else
            {
                Board.StateWhitePieces(false);
                Board.StateBlackPieces(true);
            }
        }

        public virtual bool PickSquare(int x, int y)
        {
            Square square = Board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == x && t.Coordinate.Y == y).FirstOrDefault();
            if (square != null)
            {
                if (square.Piece == null)
                {
                    AvailableSquares.Add(square);
                    return true;
                }
                else
                {
                    if (square.Piece.Color != Color)
                    {
                        AvailableSquares.Add(square);
                    }
                    return false;
                }
            }
            return true;

        }

        public virtual bool MoveTo(Square square)
        {
            IPiece piece = null;
            if (AvailableSquares.Contains(square) && FreeToMove != false && Square != square)
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
        public bool DiscoverCheckToMove()
        {
            IPiece blackKing = Board.BlackPieces.Select(t => t).Where(t => t.Touchable == false).FirstOrDefault();
            foreach (var piece in Board.WhitePieces)
            {
                piece.CheckSquare();
                if (piece.AvailableSquares.Contains(blackKing.Square) && Color == Color.black)
                {
                    return false;
                }
            }
            IPiece whiteKing = Board.WhitePieces.Select(t => t).Where(t => t.Touchable == false).FirstOrDefault();
            foreach (var piece in Board.BlackPieces)
            {
                piece.CheckSquare();
                if (piece.AvailableSquares.Contains(whiteKing.Square) && Color == Color.white)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
