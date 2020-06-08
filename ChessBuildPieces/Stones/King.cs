using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessBuildPieces.Stones
{
    public class King : Piece, IPiece
    {
        public King()
        {
            Name = "King";
            FreeToMove = true;
            MoveBack = false;
        }

        public override void CheckSquare(Board board)
        {
            Square currentSquare = board.AllSquares.Select(t => t).Where(t => ReferenceEquals(this, t.Piece)).FirstOrDefault();
            int coordinateX = currentSquare.Coordinate.X;
            int coordinateY = currentSquare.Coordinate.Y;
            board.KingAvailableSquares.Clear();
            board.KingAvailableSquares.Add(currentSquare);


            int one = 1;
            PickSquare(coordinateX + one, coordinateY, board);
            PickSquare(coordinateX, coordinateY + one, board);
            PickSquare(coordinateX - one, coordinateY, board);
            PickSquare(coordinateX, coordinateY - one, board);
            PickSquare(coordinateX + one, coordinateY + one, board);
            PickSquare(coordinateX + one, coordinateY - one, board);
            PickSquare(coordinateX - one, coordinateY + one, board);
            PickSquare(coordinateX - one, coordinateY - one, board);

            if (MoveCounter == 0)
            {
                if (Color == Color.black)
                {
                    Square square = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 2 && t.Coordinate.Y == 8).FirstOrDefault();
                    Square square1 = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 6 && t.Coordinate.Y == 8).FirstOrDefault();
                    board.KingAvailableSquares.Add(square);
                    board.KingAvailableSquares.Add(square1);
                }
                else
                {
                    Square square = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 2 && t.Coordinate.Y == 1).FirstOrDefault();
                    Square square1 = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 6 && t.Coordinate.Y == 1).FirstOrDefault();
                    board.KingAvailableSquares.Add(square);
                    board.KingAvailableSquares.Add(square1);
                }
            }
        }

        public override bool PickSquare(int x, int y, Board board)
        {
            Square square = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == x && t.Coordinate.Y == y).FirstOrDefault();
            if (square != null)
            {
                if (square.Piece == null)
                {
                    board.KingAvailableSquares.Add(square);
                    return true;
                }
                else
                {
                    if (square.Piece.Color != Color)
                    {
                        board.KingAvailableSquares.Add(square);
                    }
                    return false;
                }
            }
            return true;
        }

        public override bool MoveTo(Square square, Board board)
        {
            CheckSquare(board);
            Piece piece = null;
            Square currentSquare = board.AllSquares.Select(t => t).Where(t => ReferenceEquals(this, t.Piece)).FirstOrDefault();
            if (board.KingAvailableSquares.Contains(square) && FreeToMove != false && currentSquare != square)
            {
                MoveCounter++;
                Square initialSquare = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == currentSquare.Coordinate.X && t.Coordinate.Y == currentSquare.Coordinate.Y).FirstOrDefault();


                Square targetSquare = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == square.Coordinate.X && t.Coordinate.Y == square.Coordinate.Y).FirstOrDefault();

                piece = initialSquare.Piece;
                initialSquare.Piece = null;

                if (MoveBack == false)
                {
                    BeforePiece = targetSquare.Piece;
                }
                else
                {
                    MoveCounter--;
                    initialSquare.Piece = BeforePiece;
                    BeforePiece = null;
                    MoveBack = false;
                }

                if (targetSquare.Piece != null)
                {
                    if (targetSquare.Piece.Touchable == true)
                    {
                        targetSquare.Piece = piece;
                        currentSquare = square;
                        return true;
                    }
                    else
                    {
                        initialSquare.Piece = piece;
                        return false;
                    }
                }
                else
                {
                    targetSquare.Piece = piece;
                    currentSquare = square;
                    return true;
                }
            }
            return false;
        }

        public override void InitialPositionSet(Board board)
        {
            Square square;

            square = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 4 && t.Coordinate.Y == 1).FirstOrDefault();
            square.Piece = new King() { Color = Color.white, ImageURL = Constant.whiteKingImageURL, Touchable = false };     

            square = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 4 && t.Coordinate.Y == 8).FirstOrDefault();
            square.Piece = new King() { Color = Color.black, ImageURL = Constant.blackKingImageURL, Touchable = false };
        }
        public bool ShortCastle(Board board)
        {
            Square currentSquare = board.AllSquares.Select(t => t).Where(t => ReferenceEquals(this, t.Piece)).FirstOrDefault();

            List<Square> squares = board.AllSquares.Select(t => t).Where(t => t.Piece != null).ToList();
            List<Piece> whitePieces = squares.Select(t => t.Piece).Where(t => t.Color == Color.white).ToList();
            List<Piece> blackPieces = squares.Select(t => t.Piece).Where(t => t.Color == Color.black).ToList();

            if (Color == Color.black)
            {
                Square blackRookSquare = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 1 && t.Coordinate.Y == 8).FirstOrDefault();
                Piece blackRook = blackPieces.Select(t => t).Where(t => ReferenceEquals(t, blackRookSquare.Piece)).FirstOrDefault();
                Square targetSquareForBlack = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 2 && t.Coordinate.Y == 8).FirstOrDefault();
                Square interSquareForBlack = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 3 && t.Coordinate.Y == 8).FirstOrDefault();

                if (blackRook != null)
                {
                    if (blackRook.MoveCounter == 0 && MoveCounter == 0 && targetSquareForBlack.Piece == null && interSquareForBlack.Piece == null)
                    {
                        if (CheckCounterPiece(interSquareForBlack, board) && CheckCounterPiece(currentSquare, board) && CheckCounterPiece(targetSquareForBlack, board))
                        {
                            if (blackRook.MoveTo(interSquareForBlack, board) && MoveTo(targetSquareForBlack, board))
                            {
                                StateOrder(board);
                                return true;
                            }

                        }

                    }
                }
                return false;
            }
            else
            {
                Square whiteRookSquare = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 1 && t.Coordinate.Y == 1).FirstOrDefault();
                Piece whiteRook = whitePieces.Select(t => t).Where(t => ReferenceEquals(t, whiteRookSquare.Piece)).FirstOrDefault();
                Square interSquareForWhite = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 3 && t.Coordinate.Y == 1).FirstOrDefault();
                Square targetSquareForWhite = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 2 && t.Coordinate.Y == 1).FirstOrDefault();

                if (whiteRook != null)
                {
                    if (whiteRook.MoveCounter == 0 && MoveCounter == 0 && targetSquareForWhite.Piece == null && interSquareForWhite.Piece == null)
                    {
                        if (CheckCounterPiece(currentSquare, board) && CheckCounterPiece(interSquareForWhite, board) && CheckCounterPiece(targetSquareForWhite, board))
                        {
                            if(whiteRook.MoveTo(interSquareForWhite, board) && MoveTo(targetSquareForWhite, board))
                            {
                                StateOrder(board);
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
        }

        public bool LongCastle(Board board)
        {

            Square currentSquare = board.AllSquares.Select(t => t).Where(t => ReferenceEquals(this, t.Piece)).FirstOrDefault();

            List<Square> squares = board.AllSquares.Select(t => t).Where(t => t.Piece != null).ToList();
            List<Piece> whitePieces = squares.Select(t => t.Piece).Where(t => t.Color == Color.white).ToList();
            List<Piece> blackPieces = squares.Select(t => t.Piece).Where(t => t.Color == Color.black).ToList();

            if (Color == Color.black)
            {
                Square blackRookSquare = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 8 && t.Coordinate.Y == 8).FirstOrDefault();
                Piece blackRook = blackPieces.Select(t => t).Where(t => ReferenceEquals(t, blackRookSquare.Piece)).FirstOrDefault();

                Square targetSquareForBlack = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 6 && t.Coordinate.Y == 8).FirstOrDefault();
                Square intervalSquareForBlack = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 5 && t.Coordinate.Y == 8).FirstOrDefault();

                if (blackRook != null)
                {
                    if (blackRook.MoveCounter == 0 && MoveCounter == 0 && targetSquareForBlack.Piece == null && intervalSquareForBlack.Piece == null)
                    {
                        if (CheckCounterPiece(intervalSquareForBlack, board) && CheckCounterPiece(currentSquare, board) && CheckCounterPiece(targetSquareForBlack, board))
                        {
                            if (blackRook.MoveTo(intervalSquareForBlack, board) && MoveTo(targetSquareForBlack, board))
                            {
                                StateOrder(board);
                                return true;
                            }
                            
                        }

                    }
                    
                }
                return false;

            }
            else
            {
                Square whiteRookSquare = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 8 && t.Coordinate.Y == 1).FirstOrDefault();
                Piece whiteRook = whitePieces.Select(t => t).Where(t => ReferenceEquals(t, whiteRookSquare.Piece)).FirstOrDefault();
                Square intervalSquareForWhite = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 5 && t.Coordinate.Y == 1).FirstOrDefault();
                Square targetSquareForWhite = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == 6 && t.Coordinate.Y == 1).FirstOrDefault();

                if (whiteRook != null)
                {
                    if (whiteRook.MoveCounter == 0 && MoveCounter == 0 && targetSquareForWhite.Piece == null && intervalSquareForWhite.Piece == null)
                    {
                        if (CheckCounterPiece(currentSquare, board) && CheckCounterPiece(intervalSquareForWhite, board) && CheckCounterPiece(targetSquareForWhite, board))
                        {
                            if (whiteRook.MoveTo(intervalSquareForWhite, board) && MoveTo(targetSquareForWhite, board)) 
                            {
                                StateOrder(board);
                                return true;
                            }
                            
                        }
                    }
                }
                return false;
            }
        }

        public bool CheckCounterPiece(Square square, Board board)
        {
            Square targetSquare = board.AllSquares.Select(t => t).Where(t => t.Piece == square.Piece).FirstOrDefault();

            List<Square> squares = board.AllSquares.Select(t => t).Where(t => t.Piece != null).ToList();
            List<Piece> whitePieces = squares.Select(t => t.Piece).Where(t => t.Color == Color.white).ToList();
            List<Piece> blackPieces = squares.Select(t => t.Piece).Where(t => t.Color == Color.black).ToList();
            CheckSquare(board);
            

            if (Color == Color.black)
            {
                
                foreach (var piece in whitePieces)
                {

                    piece.CheckSquare(board);
                    if (board.AvailableSquares.Contains(square))
                    {
                        if (square.Piece != null)
                        {
                            if (square != targetSquare)
                            {
                                if (board.KingAvailableSquares.Contains(square))
                                    board.KingAvailableSquares.Remove(square);
                            }
                        }
                        else
                        {
                            if (board.KingAvailableSquares.Contains(square))
                                board.KingAvailableSquares.Remove(square);
                        }
                        return false;
                    }
                }
                return true;
            }
            else
            {
               
                foreach (var piece in blackPieces)
                {
                    piece.CheckSquare(board);
                    if (board.AvailableSquares.Contains(square))
                    {
                        if (square.Piece != null)
                        {
                            if (square != targetSquare)
                            {
                                if (board.KingAvailableSquares.Contains(square))
                                    board.KingAvailableSquares.Remove(square);
                            }
                        }
                        else
                        {
                            if (board.KingAvailableSquares.Contains(square))
                                board.KingAvailableSquares.Remove(square);
                        }
                        return false;
                    }
                }
                return true;
            }
        }

    }
}
