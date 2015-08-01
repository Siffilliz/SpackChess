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

        private void SaveGame_Click(object sender, RoutedEventArgs e)
        {
            string fileName = m_chessboard.ActualGameNotation.GetFileName(true);
            m_chessboard.ActualGameNotation.WriteFile(fileName);
        }

        private void LoadGame_Click(object sender, RoutedEventArgs e)
        {
            string fileName = m_chessboard.ActualGameNotation.GetFileName(false);
            List<string> moves = m_chessboard.ActualGameNotation.LoadSaveGame(fileName);

            foreach (string move in moves)
            {
                //Rochade rausfiltern
                if (move == "0-0")
                {
                    //Zuordnung ob weiß oder schwarz => ist item move in moves gerade (weiß) oder ungerade (schwarz)? 
                    if ((moves.IndexOf(move) % 2) != 0)
                    {
                        //move übergeben 
                    }
                    else
                    {
                        //move übergeben
                    }
                }
                else if (move == "0-0-0")
                {
                    //Zuordnung ob weiß oder schwarz => ist item move in moves gerade (weiß) oder ungerade (schwarz)? 
                    if ((moves.IndexOf(move) % 2) != 0)
                    {
                        //move übergeben
                    }
                    else
                    {
                        //move übergeben
                    }
                }
                else if (move.Contains("e.p."))
                {

                }
                else
                {
                    string[] startSquareEndSquare;
                    if (move.Contains("-"))
                    {
                        startSquareEndSquare = move.Split('-');
                    }
                    else
                    {
                        startSquareEndSquare = move.Split('x');
                    }

                    string moveOldSquare, moveNewSquare;
                    if (startSquareEndSquare[0].Length > 2)
                    {
                        moveOldSquare = startSquareEndSquare[0].Substring(1, 2);
                    }
                    else
                    {
                        moveOldSquare = startSquareEndSquare[0];
                    }

                    if (startSquareEndSquare[1].Length > 2)
                    {
                        moveNewSquare = startSquareEndSquare[1].Substring(1, 2);
                    }
                    else
                    {
                        moveNewSquare = startSquareEndSquare[1];
                    }
                    //move übergeben
                }
            }
        }
    }
}
