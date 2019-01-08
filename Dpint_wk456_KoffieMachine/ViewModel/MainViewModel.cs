using Dpint_wk456_KoffieMachine.Factory;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using KoffieMachineDomain;
using KoffieMachineDomain.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;

namespace Dpint_wk456_KoffieMachine.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        private DrinkFactory _drinkFactory;
        private bool hasMilk = false;
        private bool hasSugar = false;

        private Dictionary<string, double> _cashOnCards;
        public ObservableCollection<string> LogText { get; private set; }

        public MainViewModel()
        {
            _drinkFactory = new DrinkFactory();

            _coffeeStrength = Strength.Normal;
            _sugarAmount = Amount.Normal;
            _milkAmount = Amount.Normal;

            LogText = new ObservableCollection<string>();
            LogText.Add("Starting up...");
            LogText.Add("Done, what would you like to drink?");

            _cashOnCards = new Dictionary<string, double>();
            _cashOnCards["Arjen"] = 5.0;
            _cashOnCards["Bert"] = 3.5;
            _cashOnCards["Chris"] = 7.0;
            _cashOnCards["Daan"] = 6.0;
            PaymentCardUsernames = new ObservableCollection<string>(_cashOnCards.Keys);
            SelectedPaymentCardUsername = PaymentCardUsernames[0];

        }

        #region Drink properties to bind to
        private IDrink _selectedDrink;
        public string SelectedDrinkName
        {
            get { return _selectedDrink?.Name; }
        }

        public double? SelectedDrinkPrice
        {
            get { return _selectedDrink?.GetPrice(); }
        }
        #endregion Drink properties to bind to

        #region Payment
        public RelayCommand PayByCardCommand => new RelayCommand(() =>
        {
            PayDrink(payWithCard: true);
        });

        public ICommand PayByCoinCommand => new RelayCommand<double>(coinValue =>
        {
            PayDrink(payWithCard: false, insertedMoney: coinValue);
        });

        private void PayDrink(bool payWithCard, double insertedMoney = 0)
        {
            if (_selectedDrink != null && payWithCard)
            {
                insertedMoney = _cashOnCards[SelectedPaymentCardUsername];
                if (RemainingPriceToPay <= insertedMoney)
                {
                    _cashOnCards[SelectedPaymentCardUsername] = insertedMoney - RemainingPriceToPay;
                    RemainingPriceToPay = 0;
                }
                else // Pay what you can, fill up with coins later.
                {
                    _cashOnCards[SelectedPaymentCardUsername] = 0;
                    
                    RemainingPriceToPay -= insertedMoney;
                }
                LogText.Add($"Inserted {insertedMoney.ToString("C", CultureInfo.CurrentCulture)}, Remaining: {RemainingPriceToPay.ToString("C", CultureInfo.CurrentCulture)}.");
                RaisePropertyChanged(() => PaymentCardRemainingAmount);
            }
            else if (_selectedDrink != null && !payWithCard)
            {
                RemainingPriceToPay = Math.Max(Math.Round(RemainingPriceToPay - insertedMoney, 2), 0);
                LogText.Add($"Inserted {insertedMoney.ToString("C", CultureInfo.CurrentCulture)}, Remaining: {RemainingPriceToPay.ToString("C", CultureInfo.CurrentCulture)}.");
            }

            if (_selectedDrink != null && RemainingPriceToPay == 0)
            {
                _selectedDrink.LogDrinkMaking(LogText);
                LogText.Add("------------------");
                _selectedDrink = null;
            }
        }
        

        public double PaymentCardRemainingAmount => _cashOnCards.ContainsKey(SelectedPaymentCardUsername ?? "") ? _cashOnCards[SelectedPaymentCardUsername] : 0;

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
        private Strength _coffeeStrength;
        public Strength CoffeeStrength
        {
            get { return _coffeeStrength; }
            set { _coffeeStrength = value; RaisePropertyChanged(() => CoffeeStrength); }
        }

        private Amount _sugarAmount;
        public Amount SugarAmount
        {
            get { return _sugarAmount; }
            set { _sugarAmount = value; RaisePropertyChanged(() => SugarAmount); }
        }

        private Amount _milkAmount;
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
            this._selectedDrink = this._drinkFactory.CreateDrink(drinkName, this.hasSugar, this.hasMilk, this._sugarAmount, this._milkAmount, this._coffeeStrength);

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

            this._selectedDrink = this._drinkFactory.CreateDrink(drinkName, this.hasSugar, this.hasMilk, this._sugarAmount, this._milkAmount, this._coffeeStrength);

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