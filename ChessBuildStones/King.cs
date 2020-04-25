using ChessBuildPieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessBuildStones
{
    public class King : Piece, IPiece
    {
        public King() : base() 
        {
            
        }

        public void CheckSquare()
        {
            int coordinateX = Square.Coordinate.X;
            int coordinateY = Square.Coordinate.Y;
            AvailableSquares.Clear();
            AvailableSquares.Add(Square);

            int one = 1;
            PickSquare(coordinateX + one, coordinateY);
            PickSquare(coordinateX, coordinateY + one);
            PickSquare(coordinateX - one, coordinateY);
            PickSquare(coordinateX, coordinateY - one);
            PickSquare(coordinateX + one, coordinateY + one);
            PickSquare(coordinateX + one, coordinateY - one);
            PickSquare(coordinateX - one, coordinateY + one);
            PickSquare(coordinateX - one, coordinateY - one);
        }

        public void InitialPositionSet()
        {
            Square square;

            square = Board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 4 && t.Coordinate.Y == 1).FirstOrDefault();
            square.Piece = new King() { Color = Color.white, ImageURL = Constant.whiteKingImageURL, Square = square, Touchable = false };
            Board.WhitePieces.Add(square.Piece);
            square = Board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 4 && t.Coordinate.Y == 8).FirstOrDefault();
            square.Piece = new King() { Color = Color.black, ImageURL = Constant.blackKingImageURL, Square = square, Touchable = false };
            Board.BlackPieces.Add(square.Piece);
        }

        public void CheckCounterPiece(Square square)
        {
            if (Color == Color.black)
            {
                foreach (var piece in Board.WhitePieces)
                {
                    piece.CheckSquare();
                    if (piece.AvailableSquares.Contains(square))
                    {
                        if (square.Piece != null)
                        {
                            if (square != square.Piece.Square)
                                AvailableSquares.Remove(square);
                        }
                        else
                        {
                            AvailableSquares.Remove(square);
                        }
                    }
                    
                }
            }
            else
            {
                foreach (var piece in Board.BlackPieces)
                {
                    piece.CheckSquare();
                    if (piece.AvailableSquares.Contains(square))
                    {
                        if (square.Piece != null)
                        {
                            if (square != square.Piece.Square)
                                AvailableSquares.Remove(square);
                        }
                        else
                        {
                            AvailableSquares.Remove(square);
                        }
                       
                    }

                        
                }
            }
        }
    }
}
