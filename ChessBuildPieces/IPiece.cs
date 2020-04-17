using System;
using System.Collections.Generic;
using System.Text;

namespace ChessBuildPieces
{
    public interface IPiece
    {
        Color Color { get; set; }
        Square Square { get; set; }
        string ImageURL { get; set; }
        List<Square> AvailableSquares { get; set; }
        bool MoveTo(Square square);
        void CheckSquare();
        bool PickSquare(int x,int y);
        void InitialPositionSet();
        
    }
}
