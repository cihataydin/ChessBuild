using ChessBuildPieces.Stones;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

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
        public Piece Piece { get; set; }
        public Bishop Bishop { get; set; }
        public King King { get; set; }
        public Knight Knight { get; set; }
        public Pawn Pawn { get; set; }
        public Queen Queen { get; set; }
        public Rook Rook { get; set; }
    }
}
