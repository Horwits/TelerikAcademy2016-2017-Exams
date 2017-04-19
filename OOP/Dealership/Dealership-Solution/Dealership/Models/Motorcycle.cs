using System.Text;
using Dealership.Common;
using Dealership.Common.Enums;
using Dealership.Contracts;
using Dealership.Models.Base;

namespace Dealership.Models
{
    public class Motorcycle : Vehicle, IVehicle, IMotorcycle
    {
        private const string CategoryValidationName = "Category";
        private string category;

        public Motorcycle(string make, string model, decimal price, string category)
            : base(make, model, price)
        {
            this.Category = category;
            this.Type = VehicleType.Motorcycle;
            this.Wheels = (int)this.Type;
        }

        public string Category
        {
            get { return this.category; }

            private set
            {
                Validator.ValidateIntRange(value.Length, Constants.MinCategoryLength, Constants.MaxCategoryLength,
                    string.Format(Constants.StringMustBeBetweenMinAndMax, CategoryValidationName,
                        Constants.MinCategoryLength, Constants.MaxCategoryLength));

                this.category = value;
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(base.ToString());
            builder.AppendLine(string.Format("{0}Category: {1}", new string(' ', 2), this.Category));

            return builder.ToString().TrimEnd();
        }
    }
}
