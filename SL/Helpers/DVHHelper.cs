using System.Globalization;
using Domain;
using SL.Composite;
using SL.Domain;
using SL.Helpers;

public static class DVHHelper
{
    public static string CalcularDVH(Booking booking)
    {
        string data = $"{booking.IdBooking}" +
                      $"{booking.IdCustomer}" +
                      $"{booking.NroDocument}" +
                      $"{booking.RegistrationDate.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture)}" +
                      $"{booking.RegistrationBooking.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture)}" +
                      $"{booking.StartTime.ToString(@"hh\:mm\:ss", CultureInfo.InvariantCulture)}" +
                      $"{booking.EndTime.ToString(@"hh\:mm\:ss", CultureInfo.InvariantCulture)}" +
                      $"{booking.Field}" +
                      $"{booking.Promotion}" +
                      $"{booking.State}" +
                      $"{booking.ImporteBooking.ToString("0.00", CultureInfo.InvariantCulture)}";

        return HashHelper.CalculateSHA256(data);
    }

    public static string CalcularDVH(Field field)
    {
        string data = $"{field.IdField}" +
                      $"{field.Name}" +
                      $"{field.Capacity}" +
                      $"{field.FieldType}" +
                      $"{field.HourlyCost.ToString("0.00", CultureInfo.InvariantCulture)}" +
                      $"{field.IdFieldState}";

        return HashHelper.CalculateSHA256(data);
    }

    public static string CalcularDVH(User user)
    {
        string data = $"{user.UserId}" +
                      $"{user.LoginName}" +
                      $"{user.Password}" +
                      $"{user.NroDocument}" +
                      $"{user.FirstName}" +
                      $"{user.LastName}" +
                      $"{user.Position}" +
                      $"{user.Mail}" +
                      $"{user.Address}" +
                      $"{user.Telephone}" +
                      $"{user.State}";

        return HashHelper.CalculateSHA256(data);
    }

    public static string CalcularDVH(PermissionComponent component)
    {
        string data = $"{component.IdComponent}" +
                      $"{component.Name}" +
                      $"{component.FormName ?? ""}" +
                      $"{component.ComponentType}";

        return HashHelper.CalculateSHA256(data);
    }
}
