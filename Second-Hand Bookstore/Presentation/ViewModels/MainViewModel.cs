using Logic.Interfaces;
using Presentation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private String searchString;
        public String SearchString
        {
            get
            {
                return this.searchString;
            }
            set
            {
                this.searchString = value;
                OnPropertyChanged("SearchString");
                searchBooks(value);
            }
        }

        public float accountBalance;
        public float AccountBalance
        {
            get
            {
                return this.AccountBalance;
            }
            set
            {
                this.accountBalance = value;
                OnPropertyChanged("AccountBalance");
                GetAccountBalance();
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

        public async void GetAccountBalance()
        {
            this.AccountBalance = await _userFcd.GetAccountBalance();
        }


        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
