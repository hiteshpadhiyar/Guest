using Guest.Models;

namespace Guest.Interfaces
{
    public interface IGuestsService
    {
        Task<GuestsItem> GetGuestAsync(Guid id);
        Task<List<GuestsItem>> GetAllGuestsAsync();
        Task<GuestsItem> CreateGuestAsync(GuestsItem newGuest);
        Task<GuestsItem> AddGuestPhoneAsync(GuestsPhoneItem newGuestPhone);
        Task<GuestsItem> UpdateGuestAsync(GuestsItem Guest);
        Task<Guid> DeleteGuestAsync(Guid id);
        GuestsValidationResult ValidateNewGuest(GuestsItem Guest, out List<string> errors);
        GuestsValidationResult ValidateExistingGuest(GuestsItem Guest, out List<string> errors);
        GuestsValidationResult ValidateGuestById(Guid Id, out string error);
        GuestsValidationResult ValidateGuestPhone(GuestsPhoneItem Guest, out List<string> errors);
    }
}
