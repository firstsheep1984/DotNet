using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace Day09TodoDBApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string sortOrder = "Id";

        public MainWindow()
        {
            InitializeComponent();
            try
            {
                Globals.Db = new Database();
                lvTodos.ItemsSource = Globals.Db.GetAllTodos(sortOrder);
            }
            catch (SystemException ex) //(SqlException ex)
            { // TODO: make message box nicer
                MessageBox.Show("Database error:\n" + ex.Message);
                Close(); // Fatal error - exit application
            }
        }

        private void AddUpdateTodo_ButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            bool isUpdating = (button.Name == "btUpdateTodo");

            string task = tbTask.Text;
            Status status = rbDone.IsChecked == true ? Status.Done : Status.Pending;
            DateTime dueDate = dpDueDate.SelectedDate.Value;
            // TODO: verify inputs
            if (task.Length < 1 || task.Length > 50)
            {
                MessageBox.Show("Task description must be between 1-50 characters long");
                return;
            }
            //
            try
            {
                if (isUpdating)
                {
                    Todo todo = lvTodos.SelectedItem as Todo;
                    if (todo == null) return; // should never happen - internal error
                    todo.Task = task;
                    todo.Status = status;
                    todo.DueDate = dueDate;
                    Globals.Db.UpdateTodo(todo);
                }
                else
                {

                    Todo todo = new Todo() { Task = task, Status = status, DueDate = dueDate };
                    Globals.Db.AddTodo(todo);
                }
                lblId.Content = "-";
                tbTask.Text = "";
                rbDone.IsChecked = false;
                rbPending.IsChecked = true;
                dpDueDate.SelectedDate = DateTime.Now;
                lvTodos.ItemsSource = Globals.Db.GetAllTodos(sortOrder);
            }
            catch (SystemException ex) //(SqlException ex)
            { // TODO: make message box nicer
                MessageBox.Show("Database error:\n" + ex.Message);
            }
        }

        private void LvTodos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Todo todo= lvTodos.SelectedItem as Todo;
            if (todo == null)
            {
                // disable update and delete buttons
                btUpdateTodo.IsEnabled = false;
                btDeleteTodo.IsEnabled = false;
                return;
            }
            // enable update and delete buttons, load data
            btUpdateTodo.IsEnabled = true;
            btDeleteTodo.IsEnabled = true;
            lblId.Content = todo.Id;
            tbTask.Text = todo.Task;
            rbDone.IsChecked = todo.Status == Status.Done;
            rbPending.IsChecked = todo.Status == Status.Pending;
            dpDueDate.SelectedDate = todo.DueDate;
        }

        private void BtDeleteTodo_Click(object sender, RoutedEventArgs e)
        {
            Todo todo = lvTodos.SelectedItem as Todo;
            if (todo == null) return; // should never happen
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this record?\n" + todo, Globals.AppName, MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
            if (result == MessageBoxResult.OK)
            {
                try
                {
                    Globals.Db.DeleteTodo(todo.Id);
                    lvTodos.ItemsSource = Globals.Db.GetAllTodos(sortOrder);
                }
                catch (SqlException ex)
                { // TODO: make message box nicer
                    MessageBox.Show("Database error:\n" + ex.Message);
                }
            }
        }

        private void SortById_MenuClick(object sender, RoutedEventArgs e)
        {
            sortOrder = "Id";
            rbSortById.IsChecked = true;
            lvTodos.ItemsSource = Globals.Db.GetAllTodos(sortOrder);
        }

        private void SortByTask_MenuClick(object sender, RoutedEventArgs e)
        {
            sortOrder = "Task";
            rbSortByTask.IsChecked = true;
            lvTodos.ItemsSource = Globals.Db.GetAllTodos(sortOrder);
        }

        private void SortByDueDate_MenuClick(object sender, RoutedEventArgs e)
        {
            sortOrder = "DueDate";
            rbSortByDueDate.IsChecked = true;
            lvTodos.ItemsSource = Globals.Db.GetAllTodos(sortOrder);
        }
    }
}
