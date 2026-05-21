using System;
using System.Collections.Generic;
using System.Text;

namespace StoreZoneV2API.Domain.Entities.TenantDB
{
    public class Address
    {
        public string FullName { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;

        public string Country { get; private set; } = string.Empty;
        public string City { get; private set; }    = string.Empty;
        public string District { get; private set; } = string.Empty;
        public string Street { get; private set; } = string.Empty;

        public string? BuildingNumber { get; private set; }
        public string? PostalCode { get; private set; }
        public string? AdditionalNumber { get; private set; }

        public string? Notes { get; private set; }




        private Address() { }  // For EF Core

        public Address(string fullName, string phoneNumber, string country, 
                       string city, string district, string street, string? buildingNumber = null,
                       string? postalCode = null, string? additionalNumber = null, string? notes = null)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Full name is required.", nameof(fullName));
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("Phone number is required.", nameof(phoneNumber));
            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentException("Country is required.", nameof(country));
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("City is required.", nameof(city));
            if (string.IsNullOrWhiteSpace(district))
                throw new ArgumentException("District is required.", nameof(district));
            if (string.IsNullOrWhiteSpace(street))
                throw new ArgumentException("Street is required.", nameof(street));
            FullName = fullName;
            PhoneNumber = phoneNumber;
            Country = country;
            City = city;
            District = district;
            Street = street;
            BuildingNumber = buildingNumber;
            PostalCode = postalCode;
            AdditionalNumber = additionalNumber;
            Notes = notes;
        }
    }
}
