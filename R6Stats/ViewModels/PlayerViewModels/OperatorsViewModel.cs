using R6API;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace R6Stats
{
    class OperatorsViewModel : BaseViewModel, IMessage<Operators>
    {
        #region PrivateFields
        private Operators operators;
        private Operator selectedOperator;
        private bool isDialogOpen;
        #endregion

        #region Constuctor
        public OperatorsViewModel()
        {
            InitCommands();
        }
        #endregion

        #region Commands
        public ICommand ShowAtkStatsCommand { get; set; }
        public ICommand ShowDefStatsCommand { get; set; }
        public ICommand SortAtkCommand { get; set; }
        public ICommand SortDefCommand { get; set; }
        #endregion

        #region Properties
        public ICollectionView AtkView { get; set; }
        public ICollectionView DefView { get; set; }
        public Operator SelectedOperator
        {
            get => selectedOperator;
            set
            {
                selectedOperator = value;
                Notify();
            }
        }
        public bool IsDialogOpen
        {
            get => isDialogOpen;
            set
            {
                isDialogOpen = value;
                Notify();
            }
        }
        #endregion

        #region Methods
        private void InitCommands()
        {
            ShowAtkStatsCommand = new RelayCommand(o => ShowStats(operators.Attackers, o));
            ShowDefStatsCommand = new RelayCommand(o => ShowStats(operators.Defenders, o));
            SortAtkCommand = new RelayCommand(o => Sort(AtkView, o));
            SortDefCommand = new RelayCommand(o => Sort(DefView, o));
        }

        private void ShowStats(List<Operator> operators, object operName)
        {
            SelectedOperator = FindOperator(operators, operName.ToString());
            IsDialogOpen = true;
        }

        private void Sort(ICollectionView view, object propertyName)
        {
            view.SortDescriptions.Clear();

            if (propertyName.ToString() == "Name")
                view.SortDescriptions.Add(new SortDescription(propertyName.ToString(), ListSortDirection.Ascending));
            else
                view.SortDescriptions.Add(new SortDescription(propertyName.ToString(), ListSortDirection.Descending));

        }

        private Operator FindOperator(List<Operator> operators, string operName)
        {
            return operators.Where(o => o.Name == operName.ToString()).FirstOrDefault();
        }

        public void SendMessage(Operators message)
        {
            operators = message as Operators ?? new Operators();

            AtkView = CollectionViewSource.GetDefaultView(operators.Attackers);
            AtkView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));

            DefView = CollectionViewSource.GetDefaultView(operators.Defenders);
            DefView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));

        }
        #endregion

    }
}
