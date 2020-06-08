using System;
using System.Collections.Generic;
using System.Text;

namespace ChessBuildPieces
{
    public interface IPiece
    {
        Color Color { get; set; }
        public bool Touchable { get; set; }
        public bool? FreeToMove { get; set; }
        string ImageURL { get; set; }
        public bool MoveBack { get; set; }
        public Piece BeforePiece { get; set; }
        public int MoveCounter { get; set; }
        bool MoveTo(Square square, Board board);

        bool PickSquare(int x, int y, Board board);

        bool DiscoverCheckToMove(Board board);
        void StateOrder(Board board);
    }
}
