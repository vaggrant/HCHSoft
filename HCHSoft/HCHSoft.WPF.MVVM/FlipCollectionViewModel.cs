using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCHSoft.WPF.MVVM
{
    public class FlipCollectionViewModel<T> : ViewModelBase
    {
        protected List<T> _collection;
        protected int _capacity;

        private int _count;
        public int Count 
        {
            get { return _count; }
            set
            {
                _count = value;
                RaisePropertyChanged("Count");
            }
        }

        private int _pageCount;
        public int PageCount
        {
            get { return _pageCount; }
            set
            {
                _pageCount = value;
                RaisePropertyChanged("PageCount");
            }
        }

        private T _selectedItem;
        public T SelectedItem 
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }

        public ObservableCollection<T> SelectedItems { get; set; }
        public ObservableCollection<T> Items { get; set; }

        private int _pageIndex;
        public int PageIndex
        {
            get { return _pageIndex; }
            set
            {
                _pageIndex = value;
                RaisePropertyChanged("PageIndex");
            }
        }

        private int _startIndex;
        public int StartIndex
        {
            get { return _startIndex; }
            set
            {
                _startIndex = value;
                RaisePropertyChanged("StartIndex");
            }
        }

        private int _endIndex;
        public int EndIndex
        {
            get { return _endIndex; }
            set
            {
                _endIndex = value;
                RaisePropertyChanged("EndIndex");
            }
        }

        private RelayCommand _addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                {
                    _addCommand = new RelayCommand(
                        param => this.Add(),
                        param => this.CanExecute()
                        );
                }
                return _addCommand;
            }
        }

        private RelayCommand _removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                if (_removeCommand == null)
                {
                    _removeCommand = new RelayCommand(
                        param => this.Remove(),
                        param => this.CanExecute()
                        );
                }
                return _removeCommand;
            }
        }

        private RelayCommand _editCommand;
        public RelayCommand EditCommand
        {
            get
            {
                if (_editCommand == null)
                {
                    _editCommand = new RelayCommand(
                        param => this.Edit(),
                        param => this.CanExecute()
                        );
                }
                return _editCommand;
            }
        }

        private RelayCommand _previousCommand;
        public RelayCommand PreviousCommand
        {
            get
            {
                if (_previousCommand == null)
                {
                    _previousCommand = new RelayCommand(
                        param => this.Previous(),
                        param => this.CanExecute()
                        );
                }
                return _previousCommand;
            }
        }

        private RelayCommand _headerCommand;
        public RelayCommand HeaderCommand
        {
            get
            {
                if (_headerCommand == null)
                {
                    _headerCommand = new RelayCommand(
                        param => this.Header(),
                        param => this.CanExecute()
                        );
                }
                return _headerCommand;
            }
        }

        private RelayCommand _tailCommand;
        public RelayCommand TailCommand
        {
            get
            {
                if (_tailCommand == null)
                {
                    _tailCommand = new RelayCommand(
                        param => this.Tail(),
                        param => this.CanExecute()
                        );
                }
                return _tailCommand;
            }
        }

        private RelayCommand _nextCommand;
        public RelayCommand NextCommand
        {
            get
            {
                if (_nextCommand == null)
                {
                    _nextCommand = new RelayCommand(
                        param => this.Next(),
                        param => this.CanExecute()
                        );
                }
                return _nextCommand;
            }
        }

        protected virtual bool CanExecute()
        {
            return true;
        }
        protected virtual void Add()
        {
            T item = default(T);
            _collection.Add(item);
            PageIndex = PageCount;
            Refresh();
        }

        protected void Refresh()
        {
            this.UpdeteItemsCount();
            this.UpdatePage(PageIndex);
        }

        protected virtual void Remove()
        {
        }

        protected virtual void Edit()
        {

        }

        protected virtual void Previous()
        {
            if (PageIndex > 1)
            {
                this.UpdatePage(--PageIndex);
            }
        }

        protected virtual void Next()
        {
            if(PageIndex < PageCount)
            {
                this.UpdatePage(++PageIndex);
            }
        }

        protected virtual void Header()
        {
            PageIndex = PageCount > 0 ? 1 : 0;
            this.UpdatePage(PageIndex);
        }

        protected virtual void Tail()
        {
            PageIndex = PageCount;
            this.UpdatePage(PageIndex);
        }

        /// <summary>
        /// 构造函数，导入集合数据以及每页显示的数据个数
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="capacity">每页显示最大条数</param>
        public FlipCollectionViewModel(List<T> collection, int capacity)
        {
            _collection = collection ?? new List<T>();
            _capacity = capacity;
            Items = new ObservableCollection<T>();
            PageIndex = 1;
            Refresh();
        }

        protected void UpdeteItemsCount()
        {
            Count = _collection.Count;
            PageCount = (int)Math.Ceiling(Count * 1.0 / _capacity);
        }

        protected void UpdatePage(int index)
        {
            if (index < 1 || index > PageCount) return;
             Items.Clear();
            if(PageCount == 0)
            {
                PageIndex = 0;
                StartIndex = 0;
                EndIndex = 0;                
            }
            else
            {
                PageIndex = index;
                StartIndex = (PageIndex - 1) * _capacity + 1;
                if(PageIndex == PageCount)
                {
                    EndIndex = Count;
                }
                else
                {
                    EndIndex = PageIndex * _capacity;
                }
                _collection.GetRange(StartIndex - 1, EndIndex - StartIndex + 1).ForEach(i => Items.Add(i));
            }
        }
    }
}
