using ChessBuildPieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessBuildPresentation.Models
{
    public class ChessModel
    {
        public ChessModel()
        {
            Squares = new List<Square>();
        }
        public List<Square> Squares;
    }
}
