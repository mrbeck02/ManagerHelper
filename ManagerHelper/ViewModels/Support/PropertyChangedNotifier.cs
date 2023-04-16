using System;
using System.ComponentModel;
using System.Diagnostics;

namespace ManagerHelper.ViewModels.Support
{
    public class PropertyChangedNotifier : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            VerifyPropertyName(propertyName);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Verifies the name of the property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <exception cref="System.Exception"></exception>
        [Conditional("DEBUG"), DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real,
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                throw new Exception($"Invalid property name or the property isn't public: {propertyName}");
            }
        }

        /// <summary>
        /// Used to tell viewmodels whether they should create design time data now or not.
        /// </summary>
        /// <returns></returns>
        protected bool IsInDesignMode()
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime;
        }
    }
}
