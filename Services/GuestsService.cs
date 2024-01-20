using AutoMapper;
using Guest.Data;
using Guest.Entities;
using Guest.Interfaces;
using Guest.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Guest.Services
{
    public class GuestsService : IGuestsService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GuestsService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<GuestsItem> AddGuestPhoneAsync(GuestsPhoneItem newGuestPhone)
        {
            return await Task.Run(
                async () =>
                {
                    var _Guests = _context.Guests.First(f => f.Id == newGuestPhone.Id);

                    var Existing_PhoneNumbers = _Guests.PhoneNumbers.Split(',').ToList();
                    Existing_PhoneNumbers.AddRange(newGuestPhone.Phone_Numbers);
                    _Guests.PhoneNumbers = string.Join(",", Existing_PhoneNumbers);

                    await _context.SaveChangesAsync();

                    return _mapper.Map<GuestsItem>(_Guests);
                });
        }
        public async Task<GuestsItem> CreateGuestAsync(GuestsItem newGuest)
        {
            return await Task.Run(
                async () =>
                {
                    var _Guests = new Guests
                    {
                        Id = Guid.NewGuid(),
                        Title = newGuest.Title,
                        FirstName = newGuest.FirstName,
                        LastName = newGuest.LastName,
                        BirthDate = newGuest.BirthDate,
                        Email = newGuest.Email,
                        PhoneNumbers = string.Join(",", newGuest.Phone_Numbers),
                    };

                    _context.Guests.Add(_Guests);

                    await _context.SaveChangesAsync();

                    return _mapper.Map<GuestsItem>(_Guests);
                });
        }
        public async Task<GuestsItem> UpdateGuestAsync(GuestsItem Guest)
        {
            return await Task.Run(
                async () =>
                {
                    var _Guests = _context.Guests.First(f => f.Id == Guest.Id);
                    _Guests.FirstName = Guest.FirstName;
                    _Guests.LastName = Guest.LastName;
                    _Guests.Email = Guest.Email;
                    _Guests.BirthDate = Guest.BirthDate;
                    _Guests.PhoneNumbers = string.Join(",", Guest.Phone_Numbers);

                    await _context.SaveChangesAsync();

                    return _mapper.Map<GuestsItem>(_Guests);
                });
        }
        public async Task<Guid> DeleteGuestAsync(Guid id)
        {
            return await Task.Run(
                async () =>
                {
                    var _Guests = _context.Guests.FirstOrDefault(f => f.Id == id);

                    _context.Remove(_Guests);
                    await _context.SaveChangesAsync();
                    return id;
                });
        }
        public async Task<GuestsItem> GetGuestAsync(Guid id)
        {
            return await Task.Run(
                async () =>
                {
                    var result = await _context.Guests.FindAsync(id);
                    return _mapper.Map<GuestsItem>(result);
                });
        }
        public async Task<List<GuestsItem>> GetAllGuestsAsync()
        {
            return await Task.Run(
                async () =>
                {
                    var result = await _context.Guests.ToListAsync();
                    return _mapper.Map<List<GuestsItem>>(result);
                });
        }
        public GuestsValidationResult ValidateNewGuest(GuestsItem Guest, out List<string> errors)
        {
            errors = new List<string>();

            var result = GuestsValidationResult.Default;

            if (Guest.Phone_Numbers == null || Guest.Phone_Numbers.Count() == 0)
            {
                result |= GuestsValidationResult.GuestExists;
                errors.Add($"Atleast 1 PhoneNumber is required.");
                return result;
            }

            if (Guest.Phone_Numbers.Count() > 0)
            {
                foreach (var item in Guest.Phone_Numbers)
                {
                    Regex pattern = new Regex(@"(?<!\d)\d{10}(?!\d)");

                    if (!pattern.IsMatch(item))
                    {
                        result |= GuestsValidationResult.NotValid;
                        errors.Add($"PhoneNumber must be 10 digit only.");
                    }
                }

                if (errors.Count() > 0)
                    return result;
            }

            if (_context.Guests.Any(f => f.Email == Guest.Email))
            {
                result |= GuestsValidationResult.GuestExists;
                errors.Add($"Guest :{Guest.Email}, already exists.");
                return result;
            }

            return GuestsValidationResult.Ok;
        }
        public GuestsValidationResult ValidateExistingGuest(GuestsItem Guest, out List<string> errors)
        {
            errors = new List<string>();

            var result = GuestsValidationResult.Default;

            if (Guest.Phone_Numbers == null || Guest.Phone_Numbers.Count() == 0)
            {
                result |= GuestsValidationResult.GuestExists;
                errors.Add($"Atleast 1 PhoneNumber is required.");
                return result;
            }

            if (Guest.Phone_Numbers.Count() > 0)
            {
                foreach (var item in Guest.Phone_Numbers)
                {
                    Regex pattern = new Regex(@"(?<!\d)\d{10}(?!\d)");

                    if (!pattern.IsMatch(item))
                    {
                        result |= GuestsValidationResult.NotValid;
                        errors.Add($"PhoneNumber must be 10 digit only.");
                    }
                }

                if (errors.Count() > 0)
                    return result;
            }

            if (!_context.Guests.Any(f => f.Id == Guest.Id))
            {
                result |= GuestsValidationResult.GuestNotExists;
                errors.Add($"Guest : {Guest.Id}, does not exists.");
                return result;
            }

            if (_context.Guests.Any(f => f.Email == Guest.Email && f.Id != Guest.Id))
            {
                result |= GuestsValidationResult.GuestExists;
                errors.Add($"Guest :{Guest.Email}, already exists.");
                return result;
            }

            return GuestsValidationResult.Ok;
        }
        public GuestsValidationResult ValidateGuestById(Guid Id, out string error)
        {
            error = string.Empty;
            if (!_context.Guests.Any(f => f.Id == Id))
            {
                error = $"Guest : {Id}, does not exists.";
                return GuestsValidationResult.GuestNotExists;
            }

            return GuestsValidationResult.Ok;
        }
        public GuestsValidationResult ValidateGuestPhone(GuestsPhoneItem Guest, out List<string> errors)
        {
            errors = new List<string>();

            var result = GuestsValidationResult.Default;

            if (Guest.Phone_Numbers.Count() > 0)
            {
                foreach (var item in Guest.Phone_Numbers)
                {
                    Regex pattern = new Regex(@"(?<!\d)\d{10}(?!\d)");

                    if (!pattern.IsMatch(item))
                    {
                        result |= GuestsValidationResult.NotValid;
                        errors.Add($"PhoneNumber must be 10 digit only.");
                    }
                }

                if (errors.Count() > 0)
                    return result;
            }

            var _Guests = _context.Guests.FirstOrDefault(f => f.Id == Guest.Id);
            if (_Guests != null)
            {
                var Existing_PhoneNumbers = _Guests.PhoneNumbers.Split(',').ToList();

                bool hasMatch = Existing_PhoneNumbers.Any(x => Guest.Phone_Numbers.Any(y => y == x));

                if (hasMatch)
                {
                    result |= GuestsValidationResult.GuestExists;
                    errors.Add($"Guest Phone Numbers already exists.");
                    return result;
                }
            }
            else
            {
                result |= GuestsValidationResult.GuestNotExists;
                errors.Add($"Guest : {Guest.Id}, does not exists.");
                return result;
            }

            return GuestsValidationResult.Ok;
        }
    }
}
