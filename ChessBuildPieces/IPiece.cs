using System;
using System.Collections.Generic;
using System.Text;

namespace ChessBuildPieces
{
    public interface IPiece
    {
        Color Color { get; set; }
        Square Square { get; set; }
        public bool Touchable { get; set; }
        public bool? FreeToMove { get; set; }
        string ImageURL { get; set; }
        List<Square> AvailableSquares { get; set; }
        public bool MoveBack { get; set; }
        public IPiece BeforePiece { get; set; }
        public int MoveCounter { get; set; }
        bool MoveTo(Square square);
        void CheckSquare();
        bool PickSquare(int x,int y);
        void InitialPositionSet();
        bool DiscoverCheckToMove();
        void StateOrder();


    }
}
