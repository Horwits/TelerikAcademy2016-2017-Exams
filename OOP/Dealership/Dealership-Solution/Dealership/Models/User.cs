using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Dealership.Common;
using Dealership.Common.Enums;
using Dealership.Contracts;

namespace Dealership.Models
{
    public class User : IUser
    {
        private const string LastNameValidationName = "Lastname";
        private const string FirstNameValidationName = "Firstname";

        private string username;
        private string firstName;
        private string lastName;
        private string password;
        private IList<IVehicle> vehicles;

        public User(string username, string firstName, string lastName, string password, string role)
        {
            this.Username = username;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Password = password;
            this.Role = (Role)Enum.Parse(typeof(Role), role);
            this.vehicles = new List<IVehicle>();
        }

        public string Username
        {
            get { return this.username; }
            private set
            {
                Validator.ValidateIntRange(value.Length, Constants.MinNameLength, Constants.MaxNameLength,
                    string.Format(Constants.StringMustBeBetweenMinAndMax, nameof(this.Username),
                        Constants.MinNameLength, Constants.MaxNameLength));

                Validator.ValidateSymbols(value, Constants.UsernamePattern, string.Format(Constants.InvalidSymbols, nameof(this.Username)));

                this.username = value;
            }
        }

        public string FirstName
        {
            get { return this.firstName; }
            private set
            {
                Validator.ValidateIntRange(value.Length, Constants.MinNameLength, Constants.MaxNameLength,
                    string.Format(Constants.StringMustBeBetweenMinAndMax, FirstNameValidationName,
                        Constants.MinNameLength, Constants.MaxNameLength));

                /*Validator.ValidateSymbols(value, Constants.UsernamePattern, string.Format(Constants.InvalidSymbols, FirstNameError));*/

                this.firstName = value;
            }
        }

        public string LastName
        {
            get { return this.lastName; }
            private set
            {
                Validator.ValidateIntRange(value.Length, Constants.MinNameLength, Constants.MaxNameLength,
                    string.Format(Constants.StringMustBeBetweenMinAndMax, LastNameValidationName,
                        Constants.MinNameLength, Constants.MaxNameLength));

                /* Validator.ValidateSymbols(value, Constants.UsernamePattern, string.Format(Constants.InvalidSymbols, LastNameError));*/

                this.lastName = value;
            }
        }
        public string Password
        {
            get { return this.password; }
            private set
            {
                Validator.ValidateIntRange(value.Length, Constants.MinPasswordLength, Constants.MaxPasswordLength,
                    string.Format(Constants.StringMustBeBetweenMinAndMax, nameof(this.Password),
                        Constants.MinPasswordLength, Constants.MaxPasswordLength));

                Validator.ValidateSymbols(value, Constants.PasswordPattern, string.Format(Constants.InvalidSymbols, nameof(this.Password)));

                this.password = value;
            }
        }
        public Role Role { get; }

        public IList<IVehicle> Vehicles
        {
            get
            {
                return new ReadOnlyCollection<IVehicle>(this.vehicles);
            }
        }

        public void AddVehicle(IVehicle vehicle)
        {
            Validator.ValidateNull(vehicle, Constants.VehicleCannotBeNull);

            if (this.Role == Role.Admin)
            {
                throw new InvalidOperationException(Constants.AdminCannotAddVehicles);
            }

            if (this.vehicles.Count >= Constants.MaxVehiclesToAdd)
            {
                if (this.Role == Role.Normal)
                {
                    throw new InvalidOperationException(
                        string.Format(Constants.NotAnVipUserVehiclesAdd, Constants.MaxVehiclesToAdd));
                }
            }

            this.vehicles.Add(vehicle);
        }

        public void RemoveVehicle(IVehicle vehicle)
        {
            Validator.ValidateNull(vehicle, Constants.VehicleCannotBeNull);

            this.vehicles.Remove(vehicle);
        }

        public void AddComment(IComment commentToAdd, IVehicle vehicleToAddComment)
        {
            Validator.ValidateNull(commentToAdd, Constants.CommentCannotBeNull);
            Validator.ValidateNull(vehicleToAddComment, Constants.VehicleCannotBeNull);

            vehicleToAddComment.Comments.Add(commentToAdd);
        }

        public void RemoveComment(IComment commentToRemove, IVehicle vehicleToRemoveComment)
        {
            Validator.ValidateNull(commentToRemove, Constants.CommentCannotBeNull);
            Validator.ValidateNull(vehicleToRemoveComment, Constants.VehicleCannotBeNull);

            if (commentToRemove.Author == this.Username)
            {
                vehicleToRemoveComment.Comments.Remove(commentToRemove);
            }
            else
            {
                throw new InvalidOperationException(Constants.YouAreNotTheAuthor);
            }
        }

        public override string ToString()
        {
            return string.Format(Constants.UserToString + ", Role: {3}", this.Username, this.FirstName,
                this.LastName, this.Role.ToString());
        }

        public string PrintVehicles()
        {
            var builder = new StringBuilder();

            builder.AppendLine(string.Format("--USER {0}--", this.Username));

            if (this.vehicles.Count == 0)
            {
                builder.AppendLine("--NO VEHICLES--");
            }
            else
            {
                var counter = 1;
                foreach (var vehicle in this.vehicles)
                {
                    builder.AppendLine(string.Format("{0}. {1}", counter, vehicle.ToString()));
                    builder.AppendLine(CommentPrinter.PrintComments(vehicle.Comments));
                    counter++;
                }
            }

            return builder.ToString().Trim();
        }
    }
}
