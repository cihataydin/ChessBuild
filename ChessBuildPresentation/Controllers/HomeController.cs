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
        private IPiece piece;
        
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Click(int x1, int y1, Color color)
        {
            if (color == Color.black)
            {
                foreach (var square in Board.BlackSquares)
                {
                    if (square.Coordinate.X == x1 && square.Coordinate.Y == y1)
                    {
                        if (square.Piece != null)
                        {
                            HttpContext.Session.SetInt32("Xcoord", x1);                      
                            HttpContext.Session.SetInt32("Ycoord", y1);
                            HttpContext.Session.SetInt32("color", (int)color);
                            return View(Board.AllSquares);
                        }
                        else
                        {
                            int? x = HttpContext.Session.GetInt32("Xcoord");
                            int? y = HttpContext.Session.GetInt32("Ycoord");
                            int? clr = HttpContext.Session.GetInt32("color");
                            if (x != null && y != null)
                            {
                                if(clr == (int)Color.black)
                                {
                                    foreach (var item in Board.BlackSquares)
                                    {
                                        if(item.Coordinate.X==x && item.Coordinate.Y == y)
                                        {
                                            item.Piece.CheckSquares();
                                            item.Piece.MoveTo(square);
                                            HttpContext.Session.Clear();
                                            return View(Board.AllSquares);
                                        }

                                    }
                                }
                                else if (clr == (int)Color.white)
                                {
                                    foreach (var item in Board.WhiteSquares)
                                    {
                                        if (item.Coordinate.X == x && item.Coordinate.Y == y)
                                        {
                                            item.Piece.CheckSquares();
                                            item.Piece.MoveTo(square);
                                            HttpContext.Session.Clear();
                                            return View(Board.AllSquares);
                                        }

                                    }
                                }

                            }
                        }
                    }
 
                }
                return View(Board.AllSquares);
            }
            else
            {
                foreach (var square in Board.WhiteSquares)
                {
                    if (square.Coordinate.X == x1 && square.Coordinate.Y == y1)
                    {
                        if (square.Piece != null)
                        {
                            HttpContext.Session.SetInt32("Xcoord", x1);
                            HttpContext.Session.SetInt32("Ycoord", y1);
                            HttpContext.Session.SetInt32("color", (int)color);
                            return View(Board.AllSquares);
                        }
                        else
                        {
                            int? x = HttpContext.Session.GetInt32("Xcoord");
                            int? y = HttpContext.Session.GetInt32("Ycoord");
                            int? clr = HttpContext.Session.GetInt32("color");
                            if (x != null && y != null)
                            {
                                if (clr == (int)Color.black)
                                {
                                    foreach (var item in Board.BlackSquares)
                                    {
                                        if (item.Coordinate.X == x && item.Coordinate.Y == y)
                                        {
                                            item.Piece.CheckSquares();
                                            item.Piece.MoveTo(square);
                                            HttpContext.Session.Clear();
                                            return View(Board.AllSquares);
                                        }

                                    }
                                }
                                else if (clr == (int)Color.white)
                                {
                                    foreach (var item in Board.WhiteSquares)
                                    {
                                        if (item.Coordinate.X == x && item.Coordinate.Y == y)
                                        {
                                            item.Piece.CheckSquares();
                                            item.Piece.MoveTo(square);
                                            HttpContext.Session.Clear();
                                            return View(Board.AllSquares);
                                        }

                                    }
                                }

                            }
                        }
                    }

                }
                return View(Board.AllSquares);
            }
        }
        
        public IActionResult Index()
        {

                Board.CreateBoard();
                piece = new Rook();
                piece.SetPiece();
                ViewBag.WhiteSquares = Board.WhiteSquares;
                ViewBag.BlackSquares = Board.BlackSquares;
                return View();

            //    int coolor = Convert.ToInt32(color);
            //    int x1 = Convert.ToInt32(x);
            //    int y1 = Convert.ToInt32(y);
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
