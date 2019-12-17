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
        private IEditBookWindow editBookWindow;
        private IEditClientWindow editClientWindow;
        private IAddClientWindow addClientWindow;

        public MainViewModel(IUserFcd userFcd, IEditClientWindow _editClientWindow, IEditBookWindow _editBookWindow, IAddClientWindow _addClientWindow)
        {
            _userFcd = userFcd;
            editBookWindow = _editBookWindow;
            editClientWindow = _editClientWindow;
            addClientWindow = _addClientWindow;

            editBookWindow.DataContext = this;
            editClientWindow.DataContext = this;
            addClientWindow.DataContext = this;

            clientToBeCreated = new Client { c_name = "", c_surname = "" };

            this.RefreshBooks();
            this.searchClients("");
            this.OpenAddClient = new RelayCommand(openAddClient);
            this.OpenEdit = new RelayCommand(openEdit, () => (CurrentBook != null && isLastClickedBook) || (CurrentClient != null && !isLastClickedBook));
            this.GetAccountBalance();
            this.RegisterClient = new RelayCommand(registerClient, () => clientToBeCreated.c_name.Any() && clientToBeCreated.c_surname.Any());        
            this.SellBook = new RelayCommand(sellBook, 
                () => CurrentClient != null 
                && CurrentBook != null 
                && CurrentBook.Amount > 0) ;
            this.RegisterClient = new RelayCommand(registerClient, 
                () => clientToBeCreated.c_name.Length > 0 
                && clientToBeCreated.c_surname.Length > 0);
            this.AddBook = new RelayCommand(addBook, 
                () => bookToBeCreated != null
                && bookToBeCreated.Name.Length > 0 
                && bookToBeCreated.Author.Length > 0 
                && bookToBeCreated.Amount > 0
                && bookToBeCreated.Price > 0
                && bookToBeCreated.SupplierID > 0);            
            this.EditBook = new RelayCommand(editBook, 
                () => currentBook != null
                && currentBook.Name.Length > 0 
                && currentBook.Author.Length > 0 
                && currentBook.Amount > 0
                && currentBook.Price > 0
                && currentBook.SupplierID > 0);
            this.EditClient = new RelayCommand(editClient,
                () => currentClient != null
                && currentClient.c_name.Length > 0
                && currentClient.c_surname.Length > 0);            
            this.AddClient = new RelayCommand(addClient);
            this.GetListOfEvents = new RelayCommand(getListOfEvents);
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
                Price = (float)x.price,
                SupplierID = x.supplierID.Value
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

        private IEnumerable<Event> events;
        private IEnumerable<Event> Events
        {
            get
            {
                return this.events;
            }
            set
            {
                this.events = value;
                this.OnPropertyChanged("Events");
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
                this.EditBook.RaiseCanExecuteChanged();
                this.OpenEdit.RaiseCanExecuteChanged();
                isLastClickedBook = true;
            }
        }

        private Book bookToBeCreated;
        public Book BookToBeCreated
        {
            get
            {
                return this.bookToBeCreated;
            }
            set
            {
                this.bookToBeCreated = value;
                this.OnPropertyChanged("BookToBeCreated");
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
                this.EditClient.RaiseCanExecuteChanged();
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
                this.AddClient.RaiseCanExecuteChanged();
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
                Price = (float)x.price,
                SupplierID = x.supplierID.Value
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

        public async void addBook()
        {
            await _userFcd.AddBook(new Data.Books
            {
                b_name = bookToBeCreated.Name,
                b_author = bookToBeCreated.Author,
                amount = bookToBeCreated.Amount,
                price = bookToBeCreated.Price,
                isNew = bookToBeCreated.isNew,
                supplierID = bookToBeCreated.SupplierID
            });
        }

        public RelayCommand AddBook { get; set; }

        public async void addClient()
        {
            await _userFcd.RegisterClient(new Data.Clients
            {
                c_name = clientToBeCreated.c_name,
                c_surname = clientToBeCreated.c_surname
            });
            addClientWindow.Close();
            if (SearchStringClient == null)
                searchClients("");
            else
                searchClients(SearchStringClient);
        }

        public RelayCommand AddClient { get; set; }

        public async void editBook()
        {
            await _userFcd.UpdateBook(new Data.Books
            {
                id = currentBook.Id,
                b_name = currentBook.Name,
                b_author = currentBook.Author,
                amount = currentBook.Amount,
                price = currentBook.Price,
                isNew = currentBook.isNew,
                supplierID = currentBook.SupplierID
            });
            editBookWindow.Close();
        }

        public RelayCommand EditBook { get; set; }

        public async void editClient()
        {
            await _userFcd.UpdateClient(new Data.Clients
            {
                id = currentClient.id,
                c_name = currentClient.c_name,
                c_surname = currentClient.c_surname
            });
            editClientWindow.Close();
        }

        public RelayCommand EditClient { get; set; }


        public async void getListOfEvents()
        {
            var result = await _userFcd.GetListOfEvents();
            this.Events = result.Select(x => new Event
            {
                account_balance = (float)x.account_balance,
                bookId = (int)x.book_id,
                supplierId = (int)x.supplier_id,
                clientId = (int)x.client_id,
                event_time = (DateTime)x.event_time,
                id = x.id
            });
        }

        public RelayCommand GetListOfEvents { get; set; }

        public void openEdit()
        {
            if (isLastClickedBook)
            {
                editBookWindow.Show();
            }
            else
            {
                editClientWindow.Show();
            }
        }
        public RelayCommand OpenEdit { get; set; }

        public void openAddClient()
        {
            addClientWindow.Show();
        }
        public RelayCommand OpenAddClient { get; set; }


        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
