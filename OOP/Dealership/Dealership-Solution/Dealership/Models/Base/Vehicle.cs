using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dealership.Common;
using Dealership.Common.Enums;
using Dealership.Contracts;

namespace Dealership.Models.Base
{
    public abstract class Vehicle : IVehicle
    {
        private const string MakeValidationName = "Make";
        private const string PriceValidationName = "Price";
        private const string ModelValidationName = "Model";


        private string make;
        private string model;
        private decimal price;

        protected Vehicle(string make, string model, decimal price)
        {
            this.Model = model;
            this.Make = make;
            this.Price = price;
            this.Comments = new List<IComment>();
        }

        public IList<IComment> Comments { get; }
        public decimal Price
        {
            get { return this.price; }
            private set
            {
                Validator.ValidateDecimalRange(value, Constants.MinPrice, Constants.MaxPrice, string.Format(Constants.NumberMustBeBetweenMinAndMax, PriceValidationName, Constants.MinPrice, Constants.MaxPrice));

                this.price = value;
            }
        }
        public int Wheels { get; protected set; }
        public VehicleType Type { get; protected set; }

        public string Make
        {
            get { return this.make; }
            private set
            {
                Validator.ValidateIntRange(value.Length, Constants.MinMakeLength, Constants.MaxMakeLength, string.Format(Constants.StringMustBeBetweenMinAndMax, MakeValidationName, Constants.MinMakeLength, Constants.MaxMakeLength));

                this.make = value;
            }
        }

        public string Model
        {
            get { return this.model; }
            private set
            {
                Validator.ValidateIntRange(value.Length, Constants.MinModelLength, Constants.MaxModelLength, string.Format(Constants.StringMustBeBetweenMinAndMax, ModelValidationName, Constants.MinModelLength, Constants.MaxModelLength));

                this.model = value;
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendLine(string.Format("{0}:", this.Type));
            builder.AppendLine(string.Format("{0}Make: {1}", new string(' ', 2), this.Make));
            builder.AppendLine(string.Format("{0}Model: {1}", new string(' ', 2), this.Model));
            builder.AppendLine(string.Format("{0}Wheels: {1}", new string(' ', 2), this.Wheels));
            builder.AppendLine(string.Format("{0}Price: ${1}", new string(' ', 2), this.Price));

            return builder.ToString().TrimEnd();
        }
    }
}
