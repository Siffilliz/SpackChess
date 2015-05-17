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
    /// Interaction logic for Field.xaml
    /// </summary>
    public partial class Square : UserControl
    {
        private Piece m_occupyingPiece;
        /// <summary>
        /// Gets or sets the value of the XCoordinate.
        /// </summary>
        public int XCoordinate
        {
            private set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public int YCoordinate
        {
            private set;
            get;
        }

        public Piece OccupyingPiece
        {
            get
            {
                return (m_occupyingPiece);
            }
            set
            {
                m_occupyingPiece = value;
                this.GrSquare.Children.Clear();
                if (value != null)
                {
                    this.GrSquare.Children.Add(value);
                }
            }
        }

        public Square(int x, int y)
        {
            XCoordinate = x;
            YCoordinate = y;

            int sum = x + y;

            if (sum % 2 == 0)
            {
                this.Background = new SolidColorBrush(Colors.Brown);
            }
            else
            {
                this.Background = new SolidColorBrush(Colors.Beige);
            }

            InitializeComponent();
        }

        /// <summary>
        /// Gültige Züge sollen gehighlighted werden. Muss beim Ausführen des Zuges irgendwie rückgängig gemacht werden.
        /// </summary>
        public void Highlight(bool check = false)
        {
            if (check)
            {
                this.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                this.BorderBrush = new SolidColorBrush(Colors.Green);               
            }
            this.BorderThickness = new Thickness(5);
        }

        public void UnHighlight()
        {
            this.BorderBrush = new SolidColorBrush(Colors.Black);
            this.BorderThickness = new Thickness(1);
        }

        public override string ToString()
        {
            string xAsChar = string.Empty;

            switch (XCoordinate)
            {
                case 1:
                    xAsChar = "a";
                    break;
                case 2:
                    xAsChar = "b";
                    break;
                case 3:
                    xAsChar = "c";
                    break;
                case 4:
                    xAsChar = "d";
                    break;
                case 5:
                    xAsChar = "e";
                    break;
                case 6:
                    xAsChar = "f";
                    break;
                case 7:
                    xAsChar = "g";
                    break;
                case 8:
                    xAsChar = "h";
                    break;
            }

            return xAsChar + YCoordinate.ToString();
        }
    }
}
