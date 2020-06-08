using ChessBuildPieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessBuildStones
{
    public class King : Piece, IPiece
    {
        public King()
        {
            Name = "King";
            AvailableSquares = new List<Square>();
            FreeToMove = true;
            MoveBack = false;
        }

        public override void CheckSquare(ref Board board)
        {
            Square currentSquare = board.AllSquares.Select(t => t).Where(t => ReferenceEquals(this, t.Piece)).FirstOrDefault();
            int coordinateX = currentSquare.Coordinate.X;
            int coordinateY = currentSquare.Coordinate.Y;
            AvailableSquares.Clear();
            AvailableSquares.Add(currentSquare);


            int one = 1;
            PickSquare(coordinateX + one, coordinateY, ref board);
            PickSquare(coordinateX, coordinateY + one, ref board);
            PickSquare(coordinateX - one, coordinateY, ref board);
            PickSquare(coordinateX, coordinateY - one, ref board);
            PickSquare(coordinateX + one, coordinateY + one, ref board);
            PickSquare(coordinateX + one, coordinateY - one, ref board);
            PickSquare(coordinateX - one, coordinateY + one, ref board);
            PickSquare(coordinateX - one, coordinateY - one, ref board);

            if (MoveCounter == 0)
            {
                if (Color == Color.black)
                {
                    Square square = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 2 && t.Coordinate.Y == 8).FirstOrDefault();
                    Square square1 = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 6 && t.Coordinate.Y == 8).FirstOrDefault();
                    AvailableSquares.Add(square);
                    AvailableSquares.Add(square1);
                }
                else
                {
                    Square square = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 2 && t.Coordinate.Y == 1).FirstOrDefault();
                    Square square1 = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 6 && t.Coordinate.Y == 1).FirstOrDefault();
                    AvailableSquares.Add(square);
                    AvailableSquares.Add(square1);
                }
            }
        }

        public override void InitialPositionSet(ref Board board)
        {
            Square square;

            square = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 4 && t.Coordinate.Y == 1).FirstOrDefault();
            square.Piece = new King() { Color = Color.white, ImageURL = Constant.whiteKingImageURL, Touchable = false };
            board.WhitePieces.Add(square.Piece);

            square = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 4 && t.Coordinate.Y == 8).FirstOrDefault();
            square.Piece = new King() { Color = Color.black, ImageURL = Constant.blackKingImageURL, Touchable = false };
            board.BlackPieces.Add(square.Piece);
        }
        public void ShortCastle(ref Board board)
        {
            Square currentSquare = board.AllSquares.Select(t => t).Where(t => ReferenceEquals(this, t.Piece)).FirstOrDefault();
            
            if (Color == Color.black)
            {
                Square blackRookSquare = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 1 && t.Coordinate.Y == 8).FirstOrDefault();
                Piece blackRook = board.BlackPieces.Select(t => t).Where(t => ReferenceEquals(t,blackRookSquare.Piece)).FirstOrDefault();
                Square targetSquareForBlack = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 2 && t.Coordinate.Y == 8).FirstOrDefault();
                Square interSquareForBlack = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 3 && t.Coordinate.Y == 8).FirstOrDefault();

                if (blackRook != null)
                {
                    if (blackRook.MoveCounter == 0 && MoveCounter == 0 && targetSquareForBlack.Piece == null && interSquareForBlack.Piece == null)
                    {
                        if (CheckCounterPiece(interSquareForBlack, ref board) && CheckCounterPiece(currentSquare, ref board) && CheckCounterPiece(targetSquareForBlack, ref board))
                        {
                            MoveTo(targetSquareForBlack, ref board);
                            blackRook.MoveTo(interSquareForBlack, ref board);
                        }

                    }
                }
            }
            else
            {
                Square whiteRookSquare = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 1 && t.Coordinate.Y == 1).FirstOrDefault();
                Piece whiteRook = board.WhitePieces.Select(t => t).Where(t => ReferenceEquals(t, whiteRookSquare.Piece)).FirstOrDefault();
                Square interSquareForWhite = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 3 && t.Coordinate.Y == 1).FirstOrDefault();
                Square targetSquareForWhite = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 2 && t.Coordinate.Y == 1).FirstOrDefault();

                if (whiteRook != null)
                {
                    if (whiteRook.MoveCounter == 0 && MoveCounter == 0 && targetSquareForWhite.Piece == null && interSquareForWhite.Piece == null)
                    {
                        if (CheckCounterPiece(currentSquare, ref board) && CheckCounterPiece(interSquareForWhite, ref board) && CheckCounterPiece(targetSquareForWhite, ref board))
                        {
                            MoveTo(targetSquareForWhite, ref board);
                            whiteRook.MoveTo(interSquareForWhite, ref board);
                        }
                    }
                }
            }
        }

        public void LongCastle(ref Board board)
        {

            Square currentSquare = board.AllSquares.Select(t => t).Where(t => ReferenceEquals(this, t.Piece)).FirstOrDefault();

            if (Color == Color.black)
            {
                Square blackRookSquare = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 1 && t.Coordinate.Y == 8).FirstOrDefault();
                Piece blackRook = board.BlackPieces.Select(t => t).Where(t => ReferenceEquals(t, blackRookSquare.Piece)).FirstOrDefault();

                Square targetSquareForBlack = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 6 && t.Coordinate.Y == 8).FirstOrDefault();
                Square intervalSquareForBlack = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 5 && t.Coordinate.Y == 8).FirstOrDefault();

                if (blackRook != null)
                {
                    if (blackRook.MoveCounter == 0 && MoveCounter == 0 && targetSquareForBlack.Piece == null && intervalSquareForBlack.Piece == null)
                    {
                        if (CheckCounterPiece(intervalSquareForBlack, ref board) && CheckCounterPiece(currentSquare, ref board) && CheckCounterPiece(targetSquareForBlack, ref board))
                        {
                            MoveTo(targetSquareForBlack, ref board);
                            blackRook.MoveTo(intervalSquareForBlack, ref board);
                        }

                    }
                }

            }
            else
            {
                Square whiteRookSquare = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 8 && t.Coordinate.Y == 1).FirstOrDefault();
                Piece whiteRook = board.WhitePieces.Select(t => t).Where(t => ReferenceEquals(t, whiteRookSquare.Piece)).FirstOrDefault();
                Square intervalSquareForWhite = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 5 && t.Coordinate.Y == 1).FirstOrDefault();
                Square targetSquareForWhite = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 6 && t.Coordinate.Y == 1).FirstOrDefault();

                if (whiteRook != null)
                {
                    if (whiteRook.MoveCounter == 0 && MoveCounter == 0 && targetSquareForWhite.Piece == null && intervalSquareForWhite.Piece == null)
                    {
                        if (CheckCounterPiece(currentSquare, ref board) && CheckCounterPiece(intervalSquareForWhite, ref board) && CheckCounterPiece(targetSquareForWhite, ref board))
                        {
                            MoveTo(targetSquareForWhite, ref board);
                            whiteRook.MoveTo(intervalSquareForWhite, ref board);
                        }
                    }
                }
            }
        }

        public bool CheckCounterPiece(Square square, ref Board board)
        {
            Square targetSquare = board.AllSquares.Select(t => t).Where(t => t.Piece == square.Piece).FirstOrDefault();

            if (Color == Color.black)
            {
                foreach (var piece in board.WhitePieces)
                {
                    
                    piece.CheckSquare(ref board);
                    if (piece.AvailableSquares.Contains(square))
                    {
                        if (square.Piece != null)
                        {
                            if (square != targetSquare)
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
                foreach (var piece in board.BlackPieces)
                {
                    piece.CheckSquare(ref board);
                    if (piece.AvailableSquares.Contains(square))
                    {
                        if (square.Piece != null)
                        {
                            if (square != targetSquare)
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
