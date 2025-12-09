using System.Globalization;
using Domain;
using SL.Composite;
using SL.Domain;
using SL.Helpers;

namespace SL.Helpers
{
    /// <summary>
    /// Proporciona métodos para calcular el Dígito Verificador Horizontal (DVH)
    /// de distintas entidades del sistema SIGACF utilizando hashing SHA-256.
    /// </summary>
    public static class DVHHelper
    {
        /// <summary>
        /// Calcula el DVH correspondiente a una reserva (<see cref="Booking"/>),
        /// concatenando los valores relevantes y generando un hash SHA-256.
        /// </summary>
        /// <param name="booking">Instancia de la reserva para calcular su DVH.</param>
        /// <returns>Cadena hash SHA-256 resultante del cálculo del DVH.</returns>
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

        /// <summary>
        /// Calcula el DVH correspondiente a una cancha (<see cref="Field"/>),
        /// concatenando los valores relevantes y generando un hash SHA-256.
        /// </summary>
        /// <param name="field">Instancia de la cancha para calcular su DVH.</param>
        /// <returns>Cadena hash SHA-256 correspondiente al DVH calculado.</returns>
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

        /// <summary>
        /// Calcula el DVH correspondiente a un usuario (<see cref="User"/>),
        /// concatenando los valores relevantes y generando un hash SHA-256.
        /// </summary>
        /// <param name="user">Instancia del usuario para calcular su DVH.</param>
        /// <returns>Cadena hash SHA-256 que representa el DVH calculado.</returns>
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

        /// <summary>
        /// Calcula el DVH correspondiente a un componente de permisos
        /// (<see cref="PermissionComponent"/>), concatenando los valores relevantes
        /// y generando un hash SHA-256.
        /// </summary>
        /// <param name="component">Instancia del componente de permisos.</param>
        /// <returns>Cadena hash SHA-256 resultante del cálculo del DVH.</returns>
        public static string CalcularDVH(PermissionComponent component)
        {
            string data = $"{component.IdComponent}" +
                          $"{component.Name}" +
                          $"{component.FormName ?? ""}" +
                          $"{component.ComponentType}";

            return HashHelper.CalculateSHA256(data);
        }
    }
}
