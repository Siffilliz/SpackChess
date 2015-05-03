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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IChessboard
    {
        private List<Square> m_allSqaures = new List<Square>();

        public List<Square> AllSquares
        {
            get { return m_allSqaures; }
        }

        public MainWindow()
        {
            InitializeComponent();

            for (int x = 1; x <= 8; ++x)
            {
                for (int y = 1; y <= 8; ++y)
                {
                    var newSquare = new Square(x, y);
                    newSquare.SetValue(Grid.RowProperty, 8 - y);
                    newSquare.SetValue(Grid.ColumnProperty, x - 1);
                    this.m_allSqaures.Add(newSquare);
                    this.GrRoot.Children.Add(newSquare);
                }
            }
        }

        public Square GetSquare(int x, int y)
        {
            return this.m_allSqaures.FirstOrDefault(s => s.XCoordinate == x && s.YCoordinate == y);
        }
    }
}
