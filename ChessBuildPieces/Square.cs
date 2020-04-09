using System;
using System.Collections.Generic;
using System.Text;

namespace ChessBuildPieces
{

    public class Square
    {
        public Square(Coordinate coordinate)
        {
            Coordinate = new Coordinate();
            Coordinate = coordinate;

        }
        public Color Color { get; set; }
        public Coordinate Coordinate { get; set; }
        public IPiece Piece { get; set; }
    }
}
