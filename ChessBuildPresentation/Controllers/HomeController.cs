using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ChessBuildPresentation.Models;
using ChessBuildPieces;
using ChessBuildStones;
using Microsoft.AspNetCore.Http;

namespace ChessBuildPresentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ChessModel model = new ChessModel();
        private IPiece pieceRook;
        private IPiece pieceBishop;
        private IPiece pieceKnight;
        private IPiece pieceQueen;
        private IPiece piecePawn;
        private IPiece pieceKing;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Click(int instantaneousX, int instantaneousY, Color color)
        {
            if (Board.AllSquares.Count == 0)
            {
                Board.CreateBoard();
                piecePawn = new Pawn();
                piecePawn.InitialPositionSet();

                pieceRook = new Rook();
                pieceRook.InitialPositionSet();

                pieceBishop = new Bishop();
                pieceBishop.InitialPositionSet();

                pieceKnight = new Knight();
                pieceKnight.InitialPositionSet();

                pieceQueen = new Queen();
                pieceQueen.InitialPositionSet();

                pieceKing = new King();
                pieceKing.InitialPositionSet();

                model.Squares = Board.AllSquares;
                return View(model);
            }
            else
            {
                int? sessionX = HttpContext.Session.GetInt32("Xcoord");
                int? sessionY = HttpContext.Session.GetInt32("Ycoord");
                Square secondClickSquare = Board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == instantaneousX && t.Coordinate.Y == instantaneousY).FirstOrDefault();
                if (secondClickSquare != null)
                {
                    if (secondClickSquare.Coordinate.X == instantaneousX && secondClickSquare.Coordinate.Y == instantaneousY)
                    {
                        if (sessionX == null && sessionY == null && secondClickSquare.Piece != null)
                        {
                            HttpContext.Session.SetInt32("Xcoord", instantaneousX);
                            HttpContext.Session.SetInt32("Ycoord", instantaneousY);
                            model.Squares = Board.AllSquares;
                            return View(model);
                        }
                        else
                        {
                            if (sessionX != null && sessionY != null)
                            {
                                Square firstClickSquare = Board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == sessionX && t.Coordinate.Y == sessionY).FirstOrDefault();

                                if (firstClickSquare.Piece != null)
                                {
                                    firstClickSquare.Piece.CheckSquare();
                                    if (firstClickSquare.Piece.Touchable == false)
                                    {
                                        King king = (King)firstClickSquare.Piece;
                                        king.CheckCounterPiece(secondClickSquare);
                                        if (firstClickSquare.Coordinate.X == 4 && (firstClickSquare.Coordinate.Y == 1 || firstClickSquare.Coordinate.Y == 8) && secondClickSquare.Coordinate.X == 2 && (secondClickSquare.Coordinate.Y == 1 || secondClickSquare.Coordinate.Y == 8))
                                            king.ShortCastle();
                                        else if (firstClickSquare.Coordinate.X == 4 && (firstClickSquare.Coordinate.Y == 1 || firstClickSquare.Coordinate.Y == 8) && secondClickSquare.Coordinate.X == 6 && (secondClickSquare.Coordinate.Y == 1 || secondClickSquare.Coordinate.Y == 8))
                                            king.LongCastle();

                                    }
                                    if (firstClickSquare.Piece != null)
                                    {
                                        if (firstClickSquare.Piece.MoveTo(secondClickSquare))
                                        {
                                            if (!secondClickSquare.Piece.DiscoverCheckToMove())
                                            {
                                                secondClickSquare.Piece.MoveBack = true;
                                                secondClickSquare.Piece.MoveTo(firstClickSquare);
                                            }
                                            else
                                            {
                                                secondClickSquare.Piece.StateOrder();
                                            }

                                        }
                                    }

                                    HttpContext.Session.Clear();
                                    model.Squares = Board.AllSquares;
                                    return View(model);
                                }

                            }
                        }
                    }

                }
                model.Squares = Board.AllSquares;
                return View(model);
            }

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
