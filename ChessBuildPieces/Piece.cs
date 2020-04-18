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
        }
        public Square Square { get; set; }
        public List<Square> AvailableSquares { get; set; }
        public Color Color { get; set; }
        public bool Touchable { get; set; }
        public string ImageURL { get; set; }

        public virtual bool PickSquare(int x, int y)
        {
            Square square = Board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == x && t.Coordinate.Y == y).FirstOrDefault();
            if(square != null)
            {
                if (square.Piece == null)
                {
                    AvailableSquares.Add(square);
                    return true;
                }
                else
                {
                    if (square.Piece.Color != Color && square.Piece.Touchable!=false)
                    {
                        AvailableSquares.Add(square);
                    }
                    return false;
                }
            }
            return true;
            
        }

        public bool MoveTo(Square square)
        {
            IPiece piece = null;
            if (AvailableSquares.Contains(square))
            {
                Square initialSquare = Board.AllSquares.Select(t=>t).Where(t => t.Coordinate.X == Square.Coordinate.X && t.Coordinate.Y == Square.Coordinate.Y).FirstOrDefault();
                piece = initialSquare.Piece;
                initialSquare.Piece = null;

                Square targetSquare = Board.AllSquares.Select(t=>t).Where(t => t.Coordinate.X == square.Coordinate.X && t.Coordinate.Y == square.Coordinate.Y).FirstOrDefault();
                targetSquare.Piece = piece;
                Square = square;
                return true;
            }
            return false;   
        }
    }
}
