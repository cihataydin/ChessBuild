using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ChessBuildPresentation.Models;
using ChessBuildPieces;
using Microsoft.AspNetCore.Http;
using ChessBuild.DAL;
using ChessBuildPieces.Stones;
using WebSocketManager;

namespace ChessBuildPresentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ChessModel model = new ChessModel();
        private Board board = new Board();
        private Piece pieceRook;
        private Piece pieceBishop;
        private Piece pieceKnight;
        private Piece pieceQueen;
        private Piece piecePawn;
        private Piece pieceKing;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Click()
        {
            board.CreateBoard();
            piecePawn = new Pawn();
            piecePawn.InitialPositionSet(board);

            pieceRook = new Rook();
            pieceRook.InitialPositionSet(board);

            pieceBishop = new Bishop();
            pieceBishop.InitialPositionSet(board);

            pieceKnight = new Knight();
            pieceKnight.InitialPositionSet(board);

            pieceQueen = new Queen();
            pieceQueen.InitialPositionSet(board);

            pieceKing = new King();
            pieceKing.InitialPositionSet(board);

            model.Squares = board.AllSquares;
            board.CastAll();

            int? counter = HttpContext.Session.GetInt32("counter1");

            if (counter == null)
            {
                HttpContext.Session.SetInt32("counter1", 1);
                HttpContext.Session.SetObject("board1", board);
                model.Counter = "board1";
            }
            else
            {
                counter++;
                int iCounter = Convert.ToInt32(counter);
                HttpContext.Session.SetInt32("counter1", iCounter);
                string sCount = Convert.ToString(iCounter);

                string count = Convert.ToString(iCounter);
                HttpContext.Session.SetObject("board" + count, board);
                model.Counter = "board" + count;
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Click(int initialX, int initialY, int instantaneousX, int instantaneousY, string count, int color, string onMessage)
        {
            if(onMessage == "Update")
            {
                board = HttpContext.Session.GetObject<Board>(count);
                model.Squares = board.AllSquares;
                model.Counter = count;
                model.Color = color;
                return View(model);
            }
            board = HttpContext.Session.GetObject<Board>(count);
            board.TakeBackCastAll();
            Square secondClickSquare = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == instantaneousX && t.Coordinate.Y == instantaneousY).FirstOrDefault();
            if (secondClickSquare != null)
            {
                if (secondClickSquare.Coordinate.X == instantaneousX && secondClickSquare.Coordinate.Y == instantaneousY)
                {
                    Square firstClickSquare = board.AllSquares.Select(t => t).Where(t => t.Coordinate.X == initialX && t.Coordinate.Y == initialY).FirstOrDefault();

                    if (firstClickSquare.Piece != null && (int)firstClickSquare.Piece.Color == color)
                    {
                        

                        if (firstClickSquare.Piece.Touchable == false)
                        {
                            King king = (King)firstClickSquare.Piece;
                            king.CheckCounterPiece(secondClickSquare, board);
                            if (firstClickSquare.Coordinate.X == 4 && (firstClickSquare.Coordinate.Y == 1 || firstClickSquare.Coordinate.Y == 8) && secondClickSquare.Coordinate.X == 2 && (secondClickSquare.Coordinate.Y == 1 || secondClickSquare.Coordinate.Y == 8))
                            {
                                if (!king.ShortCastle(board))
                                {
                                    if (onMessage == "Yes") { color = 1 - color; }
                                    board.CastAll();
                                    HttpContext.Session.SetObject(count, board);
                                    model.Squares = board.AllSquares;
                                    model.Counter = count;
                                    model.Color = color;
                                    return View(model);
                                }
                            }
                            else if (firstClickSquare.Coordinate.X == 4 && (firstClickSquare.Coordinate.Y == 1 || firstClickSquare.Coordinate.Y == 8) && secondClickSquare.Coordinate.X == 6 && (secondClickSquare.Coordinate.Y == 1 || secondClickSquare.Coordinate.Y == 8))
                            {
                                if (!king.LongCastle(board))
                                {
                                    if (onMessage == "Yes") { color = 1 - color; }
                                    board.CastAll();
                                    HttpContext.Session.SetObject(count, board);
                                    model.Squares = board.AllSquares;
                                    model.Counter = count;
                                    model.Color = color;
                                    return View(model);
                                }
                            }
                        }
                        if (firstClickSquare.Piece != null)
                        {
                           
                            if (firstClickSquare.Piece.MoveTo(secondClickSquare, board))
                            {
                                if (!secondClickSquare.Piece.DiscoverCheckToMove(board))
                                {
                                    secondClickSquare.Piece.MoveBack = true;
                                    secondClickSquare.Piece.MoveTo(firstClickSquare, board);
                                }
                                else
                                {
                                    secondClickSquare.Piece.StateOrder(board);
                                }
                            }
                        }
                        if (onMessage == "Yes") { color = 1 - color; }
                        board.CastAll();
                        HttpContext.Session.SetObject(count, board);
                        model.Squares = board.AllSquares;
                        model.Counter = count;
                        model.Color = color;
                        return View(model);
                    }
                }
            }
            if (onMessage == "Yes") { color = 1 - color; }
            board.CastAll();
            HttpContext.Session.SetObject(count, board);
            model.Squares = board.AllSquares;
            model.Counter = count;
            model.Color = color;
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
