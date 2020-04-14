using ChessBuildPieces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessBuildStones
{
    public class Pawn : Piece, IPiece
    {
        public void CheckSquare()
        {
            throw new NotImplementedException();
        }

        public void SetPiece()
        {
            foreach (var square in Board.AllSquares)
            {
                if (square.Coordinate.Y == 2)
                {
                    square.Piece = new Pawn() { Color = Color.white, ImageURL = Constant.whitePawnImageURL, Square = square };
                    Board.WhitePieces.Add(square.Piece);
                }
                if (square.Coordinate.Y == 7)
                {
                    square.Piece = new Pawn() { Color = Color.black, ImageURL = Constant.blackPawnImageURL, Square = square };
                    Board.BlackPieces.Add(square.Piece);
                }
            }
        }
    }
}
