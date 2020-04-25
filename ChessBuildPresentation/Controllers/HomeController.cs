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
            int? SessionX = HttpContext.Session.GetInt32("Xcoord");
            int? SessionY = HttpContext.Session.GetInt32("Ycoord");
            Square square = Board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == instantaneousX && t.Coordinate.Y == instantaneousY).FirstOrDefault();
            if(square != null)
            {
                if (square.Coordinate.X == instantaneousX && square.Coordinate.Y == instantaneousY)
                {
                    if (SessionX == null && SessionY == null && square.Piece != null)
                    {
                        HttpContext.Session.SetInt32("Xcoord", instantaneousX);
                        HttpContext.Session.SetInt32("Ycoord", instantaneousY);
                        model.Squares = Board.AllSquares;
                        return View(model);
                    }
                    else
                    {
                        if (SessionX != null && SessionY != null)
                        {
                            Square item = Board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == SessionX && t.Coordinate.Y == SessionY).FirstOrDefault();

                            if (item.Piece != null)
                            {
                                item.Piece.CheckSquare();
                                if (item.Piece.Touchable == false)
                                {
                                    King king = (King)item.Piece;
                                    king.CheckCounterPiece(square);
                                }

                                if (item.Piece.MoveTo(square))
                                {
                                    if (!square.Piece.DiscoverCheckToMove())
                                    {
                                        square.Piece.MoveBack = true;
                                        square.Piece.MoveTo(item);
                                    }
                                    else
                                    {
                                        square.Piece.StateOrder();
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

        public IActionResult Index()
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
