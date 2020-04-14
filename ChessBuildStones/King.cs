using ChessBuildPieces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessBuildStones
{
    public class King : Piece, IPiece
    {
        public void CheckSquare()
        {
            throw new NotImplementedException();
        }

        public void SetPiece()
        {
            foreach (var square in Board.AllSquares)
            {
                if (square.Coordinate.X == 4 && square.Coordinate.Y == 1)
                {
                    square.Piece = new King() { Color = Color.white, ImageURL = Constant.whiteKingImageURL, Square = square };
                    Board.WhitePieces.Add(square.Piece);
                }
                if (square.Coordinate.X == 4 && square.Coordinate.Y == 8)
                {
                    square.Piece = new King() { Color = Color.black, ImageURL = Constant.blackKingImageURL, Square = square };
                    Board.BlackPieces.Add(square.Piece);
                }
            }
        }
    }
}
