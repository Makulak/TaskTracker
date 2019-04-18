using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml;
using TaskTracker.Models;
using TaskTracker.ViewModels.VM.Base;

namespace TaskTracker.ViewModels.VM
{
    internal class BoardVM : BaseVM
    {
        public Board Base {
            get => _base;
            private set {
                _base = value;

                if (_base.Columns != null)
                    ColumnsCollection = new ObservableCollection<ColumnVM>(Base.Columns.ConvertAll<ColumnVM>(x => x));

                OnPropertyChanged(nameof(Base));
            }
        }
        private Board _base;

        #region ModelProperties

        public int Id => Base.Id;

        public string Name {
            get => Base.Name;
            set {
                Base.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public List<Column> Columns {
            get => Base.Columns;
            set {
                Base.Columns = value;
                if (_base.Columns != null)
                    ColumnsCollection = new ObservableCollection<ColumnVM>(Base.Columns.ConvertAll<ColumnVM>(x => x));

                OnPropertyChanged(nameof(Columns));
            }
        }

        public int[] AssignedUserIds {
            get => Base.AssignedUsersIds;
            set {
                Base.AssignedUsersIds = value;
                OnPropertyChanged(nameof(AssignedUserIds));
            }
        }

        #endregion

        #region ViewModelProperties

        public ObservableCollection<UserVM> AssignedUsers {
            get => _assignedUsers;
            set {
                _assignedUsers = value;
                OnPropertyChanged(nameof(AssignedUsers));
            }
        }
        private ObservableCollection<UserVM> _assignedUsers;

        public ObservableCollection<ColumnVM> ColumnsCollection {
            get => _columnsCollection;
            set {
                _columnsCollection = value;
                OnPropertyChanged(nameof(ColumnsCollection));
            }
        }
        private ObservableCollection<ColumnVM> _columnsCollection;

        #endregion

        public static implicit operator BoardVM(Board board)
        {
            return new BoardVM
            {
                Base = board
            };
        }

        public BoardVM()
        {
            ColumnsCollection = new ObservableCollection<ColumnVM>();
        }
    }
}
