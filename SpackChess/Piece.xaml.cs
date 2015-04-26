using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpackChess
{
    /// <summary>
    /// Interaction logic for Piece.xaml
    /// </summary>
    public abstract partial class Piece : UserControl
    {
        public Image Graphic
        {
            get;
            set;
        }

        public Alignments Alignment
        {
            get;
            set;
        }

        public Piece()
        {
            InitializeComponent();
        }

        public abstract List<Square> GetValidMoves(Square currentLocation);
    }
}
