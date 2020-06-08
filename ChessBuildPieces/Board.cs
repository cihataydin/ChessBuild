using ChessBuildPieces.Stones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessBuildPieces
{

    public class Board
    {
        public Board()
        {
            AvailableSquares = new List<Square>();
            AllSquares = new List<Square>();
            OldSquares = new List<Square>();
            EmptyAvailableSquares = new List<Square>();
            KingAvailableSquares = new List<Square>();
        }
        public List<Square> AvailableSquares { get; set; }
        public List<Square> AllSquares { get; set; }
        //public List<Piece> BlackPieces { get; set; }
        //public List<Piece> WhitePieces { get; set; }
        public List<Square> OldSquares { get; set; }
        public List<Square> EmptyAvailableSquares { get; set; }
        public List<Square> KingAvailableSquares { get; set; }

        public void CreateBoard()
        {
            for (int y = 1; y < (int)Boards.upperLimit; y++)
            {
                for (int x = 1; x < (int)Boards.upperLimit; x++)
                {
                    Square square = new Square(new Coordinate { X = x, Y = y });
                    if ((x + y) % 2 == 0)
                    {
                        square.Color = Color.white;
                        AllSquares.Add(square);
                    }
                    else
                    {
                        square.Color = Color.black;
                        AllSquares.Add(square);
                    }

                }

            }
        }
        public void StateWhiteKing()
        {
            List<Square> squares = AllSquares.Select(t => t).Where(t => t.Piece != null).ToList();
            List<Piece> pieces = squares.Select(t => t.Piece).Where(t => t.Color == Color.white).ToList();
            foreach (var item in pieces)
            {
                if (item.Touchable == true)
                    item.FreeToMove = false;
                else
                    item.FreeToMove = true;
            }
        }

        public void StateBlackKing()
        {
            List<Square> squares = AllSquares.Select(t => t).Where(t => t.Piece != null).ToList();
            List<Piece> pieces = squares.Select(t => t.Piece).Where(t => t.Color == Color.black).ToList();
            foreach (var item in pieces)
            {
                if (item.Touchable == true)
                    item.FreeToMove = false;
                else
                    item.FreeToMove = true;
            }
        }

        public void StateWhitePieces(bool boolean)
        {
            List<Square> squares = AllSquares.Select(t => t).Where(t => t.Piece != null).ToList();
            List<Piece> pieces = squares.Select(t => t.Piece).Where(t => t.Color == Color.white).ToList();
            if (boolean)
            {
                foreach (var piece in pieces)
                {
                    piece.FreeToMove = true;
                }
            }
            else
            {
                foreach (var piece in pieces)
                {
                    piece.FreeToMove = false;
                }
            }
        }

        public void StateBlackPieces(bool boolean)
        {
            List<Square> squares = AllSquares.Select(t => t).Where(t => t.Piece != null).ToList();
            List<Piece> pieces = squares.Select(t => t.Piece).Where(t => t.Color == Color.black).ToList();
            if (boolean)
            {
                foreach (var piece in pieces)
                {
                    piece.FreeToMove = true;
                }
            }
            else
            {
                foreach (var piece in pieces)
                {
                    piece.FreeToMove = false;
                }
            }
        }

        public void CastAll()
        {

            List<Square> squares = AllSquares.Select(t => t).Where(t=>t.Piece!=null).ToList();
            
            foreach (var square in squares)
            {
                switch (square.Piece.Name)
                {
                    case "Bishop":
                        square.Bishop = (Bishop)square.Piece;
                        
                        break;
                    case "King":
                        square.King = (King)square.Piece;
                        
                        break;
                    case "Knight":
                        square.Knight = (Knight)square.Piece;
                        
                        break;
                    case "Pawn":
                        square.Pawn = (Pawn)square.Piece;
                        
                        break;
                    case "Queen":
                        square.Queen = (Queen)square.Piece;
                        
                        break;
                    case "Rook":
                        square.Rook = (Rook)square.Piece;
                        
                        break;
                    default:
                        break;
                }
            }
            
        
        }

        public void TakeBackCastAll()
        {
            List<Square> squares = AllSquares.Select(t => t).Where(t => t.Bishop != null || t.King!=null || t.Knight!=null || t.Pawn!=null || t.Queen!=null || t.Rook!= null).ToList();

            foreach (var square in squares)
            {
                if (square.Bishop != null)
                {
                    square.Piece = square.Bishop;
                    square.Bishop = null;
                }
                else if(square.King!= null)
                {
                    square.Piece = square.King;
                    square.King = null;
                }
                else if(square.Knight != null)
                {
                    square.Piece = square.Knight;
                    square.Knight = null;
                }
                else if (square.Pawn != null)
                {
                    square.Piece = square.Pawn;
                    square.Pawn = null;
                }
                else if(square.Queen != null)
                {
                    square.Piece = square.Queen;
                    square.Queen = null;
                }
                else if (square.Rook != null)
                {
                    square.Piece = square.Rook;
                    square.Rook = null;
                }

            }
        }

        public Piece Cast(Piece piece)
        {
            List<Square> squares = AllSquares.Select(t => t).Where(t => t.Piece != null).ToList();
            Square square = squares.Select(t => t).Where(t => t.Piece==piece).FirstOrDefault();
            switch (piece.Name)
            {
                case "Bishop":
                    square.Bishop = (Bishop)piece;
                    return square.Bishop;
                case "King":
                    square.King = (King)piece;
                    return square.King;
                case "Knight":
                    square.Knight = (Knight)piece;
                    return square.Knight;
                case "Pawn":
                    square.Pawn = (Pawn)piece;
                    return square.Pawn;
                case "Queen":
                    square.Queen = (Queen)piece;
                    return square.Queen;
                case "Rook":
                    square.Rook = (Rook)piece;
                    return square.Rook;
                default:
                    return null;
            }
        }

        public void ReverseCast(Square square)
        {
            if (square.Bishop != null)
            {
                square.Bishop = null;
            }
            else if (square.King != null)
            {
                square.King = null;
            }
            else if (square.Knight != null)
            {
                square.Knight = null;
            }
            else if (square.Pawn != null)
            {
                square.Pawn = null;
            }
            else if (square.Queen != null)
            {
                square.Queen = null;
            }
            else if (square.Rook != null)
            {

                square.Rook = null;
            }
        }

    }

}
