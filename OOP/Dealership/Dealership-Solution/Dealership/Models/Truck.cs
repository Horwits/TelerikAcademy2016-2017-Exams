using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dealership.Common;
using Dealership.Common.Enums;
using Dealership.Contracts;
using Dealership.Models.Base;

namespace Dealership.Models
{
    public class Truck : Vehicle, IVehicle, ITruck
    {
        private const string WeightCapacityValidationName = "Weight capacity";

        private int weightCapacity;

        public Truck(string make, string model, decimal price, int weightCapacity)
            : base(make, model, price)
        {
            this.WeightCapacity = weightCapacity;
            this.Type = VehicleType.Truck;
            this.Wheels = (int)this.Type;
        }

        public int WeightCapacity {
            get { return this.weightCapacity; }

            private set
            {
                Validator.ValidateIntRange(value, Constants.MinCapacity, Constants.MaxCapacity,
                    string.Format(Constants.NumberMustBeBetweenMinAndMax, WeightCapacityValidationName, Constants.MinCapacity,
                        Constants.MaxCapacity));

                this.weightCapacity = value;
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(base.ToString());
            builder.AppendLine(string.Format("{0}Weight Capacity: {1}t", new string(' ', 2), this.WeightCapacity));

            return builder.ToString().TrimEnd();
        }
    }
}
