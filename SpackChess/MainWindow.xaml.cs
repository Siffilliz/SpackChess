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
    public partial class MainWindow : Window
    {
        Chessboard m_chessboard = new Chessboard();

        public MainWindow()
        {
            InitializeComponent();
            this.GrMain.Children.Add(m_chessboard);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            m_chessboard.ActualGameNotation.GetFileNameToSave();
            m_chessboard.ActualGameNotation.WriteFile();
        }
    }
}
