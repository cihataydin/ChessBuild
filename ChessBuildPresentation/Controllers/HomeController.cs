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
        
        public IActionResult Click(int x1, int y1, Color color)
        {
            int? Xcoord = HttpContext.Session.GetInt32("Xcoord");
            int? Ycoord = HttpContext.Session.GetInt32("Ycoord");
            foreach (var square in Board.AllSquares)
            {
                if (square.Coordinate.X == x1 && square.Coordinate.Y == y1)
                {
                    if (Xcoord == null && Ycoord == null && square.Piece!=null)
                    {
                        HttpContext.Session.SetInt32("Xcoord", x1);
                        HttpContext.Session.SetInt32("Ycoord", y1);
                        return View(Board.AllSquares);
                    }
                    else
                    {
                        if(Xcoord!=null && Ycoord != null)
                        {
                            foreach (var item in Board.AllSquares)
                            {
                                if (item.Coordinate.X == Xcoord && item.Coordinate.Y == Ycoord && item.Piece != null)
                                {
                                    item.Piece.CheckSquare();
                                    item.Piece.MoveTo(square);
                                    HttpContext.Session.Clear();
                                    return View(Board.AllSquares);
                                }

                            }
                        }
                    }
                }
            }
            return View(Board.AllSquares);
        }

        public IActionResult Index()
        {
            Board.CreateBoard();
            piecePawn = new Pawn();
            piecePawn.SetPiece();

            pieceRook = new Rook();
            pieceRook.SetPiece();

            pieceBishop = new Bishop();
            pieceBishop.SetPiece();

            pieceKnight = new Knight();
            pieceKnight.SetPiece();

            pieceQueen = new Queen();
            pieceQueen.SetPiece();

            pieceKing = new King();
            pieceKing.SetPiece();

            ViewBag.WhiteSquares = Board.WhiteSquares;
            ViewBag.BlackSquares = Board.BlackSquares;
            return View();
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
