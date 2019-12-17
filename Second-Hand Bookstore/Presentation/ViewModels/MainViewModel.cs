using Logic.Interfaces;
using Presentation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Presentation.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IUserFcd _userFcd;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel(IUserFcd userFcd)
        {
            _userFcd = userFcd;
            this.RefreshBooks();
            this.searchClients("");
            this.SellBook = new RelayCommand(sellBook, () => CurrentClient != null && CurrentBook != null && CurrentBook.Amount > 0);
            this.OpenEdit = new RelayCommand(openEdit, () => (CurrentBook != null && isLastClickedBook) || (CurrentClient != null && !isLastClickedBook));
            this.GetAccountBalance();
            this.RegisterClient = new RelayCommand(registerClient, () => clientToBeCreated.c_name.Any() && clientToBeCreated.c_surname.Any());        
        }

        private async void RefreshBooks()
        {
            var results = await _userFcd.GetAllBooks();
            this.Books = results.Select(x => new Book
            {
                Amount = x.amount.Value,
                Author = x.b_author,
                Id = x.id,
                isNew = x.isNew.Value,
                Name = x.b_name,
                Price = (float)x.price
            });
        }

        private bool isLastClickedBook = true;

        private String searchStringBook;
        public String SearchStringBook
        {
            get
            {
                return this.searchStringBook;
            }
            set
            {
                this.searchStringBook = value;
                OnPropertyChanged("SearchStringBook");
                searchBooks(value);
            }
        }

        public String searchStringClient;
        public String SearchStringClient
        {
            get
            {
                return this.searchStringClient;
            }
            set
            {
                this.searchStringClient = value;
                OnPropertyChanged("SearchStringClient");
                searchClients(value);
            }
        }

        public float accountBalance;
        public float AccountBalance
        {
            get
            {
                return this.accountBalance;
            }
            set
            {
                this.accountBalance = value;
                OnPropertyChanged("AccountBalance");
            }
        }

        private IEnumerable<Book> books;
        public IEnumerable<Book> Books
        {
            get
            {
                return this.books;
            }
            set
            {
                this.books = value;
                this.OnPropertyChanged("Books");
            }
        }

        private IEnumerable<Client> clients;
        public IEnumerable<Client> Clients
        {
            get
            {
                return this.clients;
            }
            set
            {
                this.clients = value;
                this.OnPropertyChanged("Clients");
            }
        }

        private Book currentBook;
        public Book CurrentBook
        {
            get
            {
                return this.currentBook;
            }
            set
            {
                this.currentBook = value;
                this.OnPropertyChanged("CurrentBook");
                this.SellBook.RaiseCanExecuteChanged();
                this.OpenEdit.RaiseCanExecuteChanged();
                isLastClickedBook = true;
            }
        }

        private Client currentClient;
        public Client CurrentClient
        {
            get
            {
                return this.currentClient;
            }
            set
            {
                this.currentClient = value;
                this.OnPropertyChanged("CurrentClient");
                this.SellBook.RaiseCanExecuteChanged();
                this.OpenEdit.RaiseCanExecuteChanged();
                isLastClickedBook = false;
            }
        }

        private Client clientToBeCreated;
        public Client ClientToBeCreated
        {
            get
            {
                return this.clientToBeCreated;
            }
            set
            {
                this.clientToBeCreated = value;
                this.OnPropertyChanged("ClientToBeCreated");
            }
        }

        public async void searchBooks(String searchS)
        {
            var result = await _userFcd.GetBooksByString(searchS);
            this.Books = result.Select(x => new Book {
                Amount = x.amount.Value,
                Author = x.b_author,
                Id = x.id,
                isNew = x.isNew.Value,
                Name = x.b_name,
                Price = (float)x.price
            });
        }

        public async void searchClients(String searchS)
        {
            var result = await _userFcd.GetClientsByString(searchS);
            this.Clients = result.Select(x => new Client
            {
                id = x.id,
                c_name = x.c_name,
                c_surname = x.c_surname,
                creationDate = (DateTime)x.creation_date
            });
            
        }

        public async void GetAccountBalance()
        {
            this.AccountBalance = await _userFcd.GetAccountBalance();
        }

        public async void sellBook()
        {
            int bookId = currentBook.Id;
            int clientId = currentClient.id;
            await _userFcd.SellBook(bookId, clientId);
            if (SearchStringBook != null)
                searchBooks(SearchStringBook);
            else
                RefreshBooks();
            GetAccountBalance();
            MessageBox.Show("Book sold successfully.", "Book sold", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public RelayCommand SellBook { get; set; }

        public async void registerClient()
        {
            await _userFcd.RegisterClient(new Data.Clients
            {
                c_name = clientToBeCreated.c_name,
                c_surname = clientToBeCreated.c_surname,
                creation_date = clientToBeCreated.creationDate
            });
        }

        public RelayCommand RegisterClient { get; set; }

        public void openEdit()
        {
            if (isLastClickedBook)
            {
                EditBookWindow window = new EditBookWindow();
                window.DataContext = this;
                window.Show();
            }
            else
            {
                EditClientWindow window = new EditClientWindow();
                window.DataContext = this;
                window.Show();
            }
        }
        public RelayCommand OpenEdit { get; set; }

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
