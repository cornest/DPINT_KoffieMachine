using Dpint_wk456_KoffieMachine.Factory;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using KoffieMachineDomain;
using KoffieMachineDomain.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using TeaAndChocoLibrary;

namespace Dpint_wk456_KoffieMachine.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private DrinkFactory _drinkFactory;
        private PaymentFactory _paymentFactory;
        private bool hasMilk = false;
        private bool hasSugar = false;
        private Strength _coffeeStrength;
        private Amount _sugarAmount;
        private Amount _milkAmount;
        private TeaBlendRepository _teaBlendRepository;
        public ObservableCollection<string> LogText { get; private set; }
        public ObservableCollection<string> Blends { get; private set; }

        public MainViewModel()
        {
            _teaBlendRepository = new TeaBlendRepository();
            Blends = new ObservableCollection<string>(_teaBlendRepository.BlendNames);
            _drinkFactory = new DrinkFactory();
            _paymentFactory = new PaymentFactory();
            
            _coffeeStrength = Strength.Normal;
            _sugarAmount = Amount.Normal;
            _milkAmount = Amount.Normal;

            LogText = new ObservableCollection<string>();
            LogText.Add("Starting up...");
            LogText.Add("Done, what would you like to drink?");
            PaymentCardUsernames = new ObservableCollection<string>(_paymentFactory.Pay("CARD").GetCashOnCards().Keys);
            SelectedPaymentCardUsername = PaymentCardUsernames[0];
        }

        #region Drink properties to bind to
        private IDrink _selectedDrink;
        public string SelectedDrinkName
        {
            get { return _selectedDrink?.Name; }
        }

        public string SelectedBlendName { get; set; }

        public double? SelectedDrinkPrice
        {
            get { return _selectedDrink?.GetPrice(); }
        }
        #endregion Drink properties to bind to

        #region Payment
        public ICommand PayByCardCommand => new RelayCommand(() =>
        {
            if (this._selectedDrink != null)
            {
                PayDrink("CARD");
            }
        });

        public ICommand PayByCoinCommand => new RelayCommand<double>(coinValue =>
        {
            if(this._selectedDrink != null)
            {
                PayDrink("CASH", insertedMoney: coinValue);
            }

        });

        private void PayDrink(string PayMethod, double insertedMoney = 0)
        {
            RemainingPriceToPay = _paymentFactory.Pay(PayMethod).remainingPriceToPay(this._selectedDrink, this._selectedPaymentCardUsername, this._remainingPriceToPay, LogText, insertedMoney);

            if (RemainingPriceToPay == 0)
            {
                _selectedDrink.LogDrinkMaking(LogText);
                LogText.Add("------------------");
                _selectedDrink = null;
            }

            this.RaisePropertyChanged(() => this.PaymentCardRemainingAmount);
        }


        public double PaymentCardRemainingAmount => _paymentFactory.Pay("CARD").GetCashOnCards().Single(x => x.Key == this.SelectedPaymentCardUsername).Value;

        public ObservableCollection<string> PaymentCardUsernames { get; set; }
        private string _selectedPaymentCardUsername;
        public string SelectedPaymentCardUsername
        {
            get { return _selectedPaymentCardUsername; }
            set
            {
                _selectedPaymentCardUsername = value;
                RaisePropertyChanged(() => SelectedPaymentCardUsername);
                RaisePropertyChanged(() => PaymentCardRemainingAmount);
            }
        }

        private double _remainingPriceToPay;
        public double RemainingPriceToPay
        {
            get { return _remainingPriceToPay; }
            set { _remainingPriceToPay = value; RaisePropertyChanged(() => RemainingPriceToPay); }
        }
        #endregion Payment

        #region Coffee buttons

        public Strength CoffeeStrength
        {
            get { return _coffeeStrength; }
            set { _coffeeStrength = value; RaisePropertyChanged(() => CoffeeStrength); }
        }


        public Amount SugarAmount
        {
            get { return _sugarAmount; }
            set { _sugarAmount = value; RaisePropertyChanged(() => SugarAmount); }
        }


        public Amount MilkAmount
        {
            get { return _milkAmount; }
            set { _milkAmount = value; RaisePropertyChanged(() => MilkAmount); }
        }

        public ICommand DrinkCommand => new RelayCommand<string>((drinkName) =>
        {
            _selectedDrink = null;
            this.hasSugar = false;
            this.hasMilk = false;
            this._selectedDrink = this._drinkFactory.CreateDrink(drinkName, this.hasSugar, this.hasMilk, this._sugarAmount, this._milkAmount, this._coffeeStrength, SelectedBlendName);

            if (_selectedDrink != null)
            {
                this._selectedDrink.LogSelect(this.LogText);
                this.RemainingPriceToPay = this._selectedDrink.GetPrice();
            }
        });

        public ICommand DrinkWithSugarCommand => new RelayCommand<string>((drinkName) =>
        {
            _selectedDrink = null;
            this.hasSugar = true;
            this.hasMilk = false;

            this._selectedDrink = this._drinkFactory.CreateDrink(drinkName, this.hasSugar, this.hasMilk, this._sugarAmount, this._milkAmount, this._coffeeStrength, SelectedBlendName);

            if (_selectedDrink != null)
            {
                this._selectedDrink.LogSelect(this.LogText);
                this.RemainingPriceToPay = this._selectedDrink.GetPrice();
            }
        });

        public ICommand DrinkWithMilkCommand => new RelayCommand<string>((drinkName) =>
        {
            this.hasSugar = false;
            this.hasMilk = true;

            this._selectedDrink = this._drinkFactory.CreateDrink(drinkName, this.hasSugar, this.hasMilk, this._sugarAmount, this._milkAmount, this._coffeeStrength);

            if (_selectedDrink != null)
            {
                this._selectedDrink.LogSelect(this.LogText);
                this.RemainingPriceToPay = this._selectedDrink.GetPrice();
            }
        });

        public ICommand DrinkWithSugarAndMilkCommand => new RelayCommand<string>((drinkName) =>
        {
            this.hasSugar = true;
            this.hasMilk = true;

            this._selectedDrink = this._drinkFactory.CreateDrink(drinkName, this.hasSugar, this.hasMilk, this._sugarAmount, this._milkAmount, this._coffeeStrength);

            if (_selectedDrink != null)
            {
                this._selectedDrink.LogSelect(this.LogText);
                this.RemainingPriceToPay = this._selectedDrink.GetPrice();
            }
        });

        #endregion Coffee buttons
    }
}