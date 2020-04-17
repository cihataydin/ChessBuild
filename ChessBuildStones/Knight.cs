﻿using ChessBuildPieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessBuildStones
{
    public class Knight : Piece, IPiece
    {
        public void CheckSquare()
        {
            int CoordinateX = Square.Coordinate.X;
            int CoordinateY = Square.Coordinate.Y;
            AvailableSquares.Clear();

            int two = 2;
            int one = 1;
            PickSquare(CoordinateX + two, CoordinateY - one);
            PickSquare(CoordinateX + two, CoordinateY + one);
            PickSquare(CoordinateX + one, CoordinateY + two);
            PickSquare(CoordinateX - one, CoordinateY + two);
            PickSquare(CoordinateX - two, CoordinateY + one);
            PickSquare(CoordinateX - two, CoordinateY - one);
            PickSquare(CoordinateX - one, CoordinateY - two);
            PickSquare(CoordinateX + one, CoordinateY - two);

        }

        public void InitialPositionSet()
        {
            List<Square> squares;

            var data = Board.Test.Select(t => t.Value).Where(t => (t.Coordinate.X == 2 && t.Coordinate.Y == 1) || (t.Coordinate.X == 7 && t.Coordinate.Y == 1)).ToList();
            squares = data;
            foreach (var square in squares)
            {
                square.Piece = new Knight() { Color = Color.white, ImageURL = Constant.whiteKnightImageURL, Square = square };
                Board.WhitePieces.Add(square.Piece);
            }
            squares.Clear();

            var data2 = Board.Test.Select(t => t.Value).Where(t => (t.Coordinate.X == 2 && t.Coordinate.Y == 8) || (t.Coordinate.X == 7 && t.Coordinate.Y == 8)).ToList();
            squares = data2;

            foreach (var square in squares)
            {
                square.Piece = new Knight() { Color = Color.black, ImageURL = Constant.blackKnightImageURL, Square = square };
                Board.BlackPieces.Add(square.Piece);
            }
        }
    }
}
