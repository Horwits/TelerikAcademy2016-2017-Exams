using System.Collections.Generic;
using System.Text;
using Dealership.Common;
using Dealership.Common.Enums;
using Dealership.Contracts;
using Dealership.Models.Base;

namespace Dealership.Models
{
    public class Car : Vehicle, ICar, IVehicle
    {
        private const string SeatsValidationName = "Seats";
        private int seats;

        public Car(string make, string model, decimal price, int seats)
            : base(make, model, price)
        {
            this.Seats = seats;
            this.Type = VehicleType.Car;
            this.Wheels = (int)this.Type;
        }

        public int Seats
        {
            get { return this.seats; }

            private set
            {
                Validator.ValidateIntRange(value, Constants.MinSeats, Constants.MaxSeats,
                    string.Format(Constants.NumberMustBeBetweenMinAndMax, SeatsValidationName, Constants.MinSeats,
                        Constants.MaxSeats));
                this.seats = value;
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(base.ToString());
            builder.AppendLine(string.Format("{0}Seats: {1}", new string(' ', 2), this.Seats));

            return builder.ToString().TrimEnd();
        }
    }
}