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

            if (MoveCounter == 0)
            {
                if (Color == Color.black)
                {
                    Square square = Board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 2 && t.Coordinate.Y == 8).FirstOrDefault();
                    Square square1 = Board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 6 && t.Coordinate.Y == 8).FirstOrDefault();
                    AvailableSquares.Add(square);
                    AvailableSquares.Add(square1);
                }
                else
                {
                    Square square = Board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 2 && t.Coordinate.Y == 1).FirstOrDefault();
                    Square square1 = Board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 6 && t.Coordinate.Y == 1).FirstOrDefault();
                    AvailableSquares.Add(square);
                    AvailableSquares.Add(square1);
                }
            }
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
        public void ShortCastle()
        {
            if (Color == Color.black)
            {
                IPiece blackRook = Board.BlackPieces.Select(t => t).Where(t => t.Square.Coordinate.X == 1 && t.Square.Coordinate.Y == 8).FirstOrDefault();
                Square targetSquareForBlack = Board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 2 && t.Coordinate.Y == 8).FirstOrDefault();
                Square interSquareForBlack = Board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 3 && t.Coordinate.Y == 8).FirstOrDefault();

                if (blackRook != null)
                {
                    if (blackRook.MoveCounter == 0 && MoveCounter == 0 && targetSquareForBlack.Piece == null && interSquareForBlack.Piece == null)
                    {
                        if (CheckCounterPiece(interSquareForBlack) && CheckCounterPiece(Square) && CheckCounterPiece(targetSquareForBlack))
                        {
                            MoveTo(targetSquareForBlack);
                            blackRook.MoveTo(interSquareForBlack);
                        }

                    }
                }
            }
            else
            {
                IPiece whiteRook = Board.WhitePieces.Select(t => t).Where(t => t.Square.Coordinate.X == 1 && t.Square.Coordinate.Y == 1).FirstOrDefault();
                Square interSquareForWhite = Board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 3 && t.Coordinate.Y == 1).FirstOrDefault();
                Square targetSquareForWhite = Board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 2 && t.Coordinate.Y == 1).FirstOrDefault();

                if (whiteRook != null)
                {
                    if (whiteRook.MoveCounter == 0 && MoveCounter == 0 && targetSquareForWhite.Piece == null && interSquareForWhite.Piece == null)
                    {
                        if (CheckCounterPiece(Square) && CheckCounterPiece(interSquareForWhite) && CheckCounterPiece(targetSquareForWhite))
                        {
                            MoveTo(targetSquareForWhite);
                            whiteRook.MoveTo(interSquareForWhite);
                        }
                    }
                }
            }
        }

        public void LongCastle()
        {
            if (Color == Color.black)
            {
                IPiece blackRook = Board.BlackPieces.Select(t => t).Where(t => t.Square.Coordinate.X == 8 && t.Square.Coordinate.Y == 8).FirstOrDefault();
                Square targetSquareForBlack = Board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 6 && t.Coordinate.Y == 8).FirstOrDefault();
                Square intervalSquareForBlack = Board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 5 && t.Coordinate.Y == 8).FirstOrDefault();

                if (blackRook != null)
                {
                    if (blackRook.MoveCounter == 0 && MoveCounter == 0 && targetSquareForBlack.Piece == null && intervalSquareForBlack.Piece == null)
                    {
                        if (CheckCounterPiece(intervalSquareForBlack) && CheckCounterPiece(Square) && CheckCounterPiece(targetSquareForBlack))
                        {
                            MoveTo(targetSquareForBlack);
                            blackRook.MoveTo(intervalSquareForBlack);
                        }

                    }
                }

            }
            else
            {
                IPiece whiteRook = Board.WhitePieces.Select(t => t).Where(t => t.Square.Coordinate.X == 8 && t.Square.Coordinate.Y == 1).FirstOrDefault();
                Square intervalSquareForWhite = Board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 5 && t.Coordinate.Y == 1).FirstOrDefault();
                Square targetSquareForWhite = Board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 6 && t.Coordinate.Y == 1).FirstOrDefault();

                if (whiteRook != null)
                {
                    if (whiteRook.MoveCounter == 0 && MoveCounter == 0 && targetSquareForWhite.Piece == null && intervalSquareForWhite.Piece == null)
                    {
                        if (CheckCounterPiece(Square) && CheckCounterPiece(intervalSquareForWhite) && CheckCounterPiece(targetSquareForWhite))
                        {
                            MoveTo(targetSquareForWhite);
                            whiteRook.MoveTo(intervalSquareForWhite);
                        }
                    }
                }
            }
        }

        public bool CheckCounterPiece(Square square)
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
                            {
                                if (AvailableSquares.Contains(square))
                                    AvailableSquares.Remove(square);
                            }
                        }
                        else
                        {
                            if (AvailableSquares.Contains(square))
                                AvailableSquares.Remove(square);
                        }
                        return false;
                    }
                }
                return true;
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
                            {
                                if (AvailableSquares.Contains(square))
                                    AvailableSquares.Remove(square);
                            }
                        }
                        else
                        {
                            if (AvailableSquares.Contains(square))
                                AvailableSquares.Remove(square);
                        }
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
