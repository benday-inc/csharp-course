using NUnit.Framework;
using ViewModelTestingLab.Core;

namespace ViewModelTestingLab.Tests
{
    [TestFixture]
    public class PersonViewModelTests
    {
        [Test]
        public void ChangingName_ShouldRaisePropertyChanged()
        {
            var viewModel = new PersonViewModel();
            var tester = new NotifyPropertyChangedTester(viewModel);

            viewModel.Name = "Alice";

            tester.AssertChange("Name");
        }

        [Test]
        public void ChangingAge_ShouldRaisePropertyChangedForAgeAndIsAdult()
        {
            var viewModel = new PersonViewModel();
            var tester = new NotifyPropertyChangedTester(viewModel);

            viewModel.Age = 25;

            tester.AssertChange(0, "Age");
            tester.AssertChange(1, "IsAdult");
        }
    }
}
