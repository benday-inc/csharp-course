using System.ComponentModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ViewModelTestingLab.Tests
{
    public class NotifyPropertyChangedTester
    {
        public NotifyPropertyChangedTester(INotifyPropertyChanged viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            Changes = new List<string>();
            viewModel.PropertyChanged += OnPropertyChangedEvent;
        }

        private void OnPropertyChangedEvent(object sender, PropertyChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.PropertyName))
                throw new InvalidOperationException("PropertyName was null or empty");

            Changes.Add(e.PropertyName);
        }

        public List<string> Changes { get; }

        public void AssertChange(int index, string expectedProperty)
        {
            Assert.That(Changes, Is.Not.Null);
            Assert.That(index < Changes.Count, $"Expected at least {index + 1} changes.");
            Assert.That(Changes[index], Is.EqualTo(expectedProperty));
        }

        public void AssertChange(string expectedProperty)
        {
            Assert.That(Changes, Does.Contain(expectedProperty),
                $"Expected a change notification for '{expectedProperty}'.");
        }
    }
}
